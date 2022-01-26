using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.Extensions.Configuration;
using Namotion.Reflection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace XFree.SimpleService.Host.EntityFrameworkCore
{
    /// <summary>
    /// 数据库迁移DcContext创建工厂
    /// </summary>
    public class SystemServiceMigrationDbContextFactory : IDesignTimeDbContextFactory<SystemServiceMigrationDbContext>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public SystemServiceMigrationDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var connectStr = configuration.GetConnectionString("ApplicationConnection");

            var builder = new DbContextOptionsBuilder<SystemServiceMigrationDbContext>()
                .UseMySql(connectStr, ServerVersion.AutoDetect(connectStr));

            builder.ReplaceService<IProviderConventionSetBuilder, CustomConventionSetBuilder>();

            return new SystemServiceMigrationDbContext(builder.Options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }

    /// <summary>
    ///  自定义默认数据类型定义
    /// </summary>
    public class CustomConventionSetBuilder : MySqlConventionSetBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="relationalDependencies"></param>
        public CustomConventionSetBuilder([NotNull] ProviderConventionSetBuilderDependencies dependencies, [NotNull] RelationalConventionSetBuilderDependencies relationalDependencies) : base(dependencies, relationalDependencies)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override ConventionSet CreateConventionSet()
        {
            var conventionSet = base.CreateConventionSet();
            var stringDefaultLengthConvention = new StringDefaultLengthConvention();
            conventionSet.PropertyAddedConventions.Add(stringDefaultLengthConvention);
            conventionSet.PropertyFieldChangedConventions.Add(stringDefaultLengthConvention);
            var addCommontConvention = new AddCommontConvention();
            conventionSet.PropertyAddedConventions.Add(addCommontConvention);
            conventionSet.PropertyFieldChangedConventions.Add(addCommontConvention);
            return conventionSet;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class StringDefaultLengthConvention : IPropertyAddedConvention, IPropertyFieldChangedConvention
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyBuilder"></param>
        /// <param name="context"></param>
        public void ProcessPropertyAdded(IConventionPropertyBuilder propertyBuilder, IConventionContext<IConventionPropertyBuilder> context)
        {
            if (propertyBuilder.Metadata.ClrType == typeof(string))
                propertyBuilder.HasMaxLength(256);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyBuilder"></param>
        /// <param name="newFieldInfo"></param>
        /// <param name="oldFieldInfo"></param>
        /// <param name="context"></param>
        public void ProcessPropertyFieldChanged(IConventionPropertyBuilder propertyBuilder, FieldInfo newFieldInfo, FieldInfo oldFieldInfo, IConventionContext<FieldInfo> context)
        {
            if (newFieldInfo.FieldType == typeof(string))
                propertyBuilder.HasMaxLength(256);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class AddCommontConvention : IPropertyAddedConvention, IPropertyFieldChangedConvention
    {

        private static readonly Dictionary<string, string> ReplaceComments = new()
        {
            { "Unique identifier for this entity.", "数据唯一标识" },
            { "Creation time.", "创建时间" },
            { "Id of the creator.", "创建人Id" },
            { "The last modified time for this entity.", "最后修改时间" },
            { "Last modifier user for this entity.", "最后人Id" }
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyBuilder"></param>
        /// <param name="context"></param>
        public void ProcessPropertyAdded(IConventionPropertyBuilder propertyBuilder, IConventionContext<IConventionPropertyBuilder> context)
        {
            GetAndSetSummary(propertyBuilder);
        }

        private static void GetAndSetSummary(IConventionPropertyBuilder propertyBuilder)
        {
            string summary = propertyBuilder.Metadata.PropertyInfo.GetXmlDocsSummary()?.Replace("\n", "");
            if (ReplaceComments.ContainsKey(summary))
            {
                propertyBuilder.HasComment(ReplaceComments[summary]);
                return;
            }
            propertyBuilder.HasComment(summary?.Trim());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyBuilder"></param>
        /// <param name="newFieldInfo"></param>
        /// <param name="oldFieldInfo"></param>
        /// <param name="context"></param>
        public void ProcessPropertyFieldChanged(IConventionPropertyBuilder propertyBuilder, FieldInfo newFieldInfo, FieldInfo oldFieldInfo, IConventionContext<FieldInfo> context)
        {
            GetAndSetSummary(propertyBuilder);
        }

    }

}
