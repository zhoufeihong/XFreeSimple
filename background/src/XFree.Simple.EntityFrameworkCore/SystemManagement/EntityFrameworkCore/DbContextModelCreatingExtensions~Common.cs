using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using XFree.Simple.Domain.SystemManagement;
using XFree.Simple.Domain.SystemManagement.Common;
using XFree.Simple.Domain.SystemManagement.Organization;

namespace XFree.Simple.EntityFrameworkCore.SystemManagement.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class DbContextModelCreatingExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public const string TablePrefix = "sys_";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public static void ConfigureSystemManagementBase(
           this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Dict>(b =>
            {

                b.ToTable(TablePrefix + "dict");

                //b.HasMany(x => x.DictItems).WithOne(w => w.Dict).HasForeignKey(h => h.DictId);

                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();
                b.ConfigureFullAuditedAggregateRoot();

                b.SetSnakeCaseNaming();

                b.Property(x => x.Id).HasMaxLength(64);
                b.Property(x => x.DictCode).IsRequired().HasMaxLength(100);
                b.Property(x => x.DictName).IsRequired().HasMaxLength(100);

                b.HasIndex(q => q.DictCode);
            });

            builder.Entity<DictItem>(b =>
            {

                b.ToTable(TablePrefix + "dict_item");

                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();
                b.ConfigureFullAuditedAggregateRoot();

                b.SetSnakeCaseNaming();

                b.Property(x => x.Id).HasMaxLength(64);
                b.Property(x => x.DictId).IsRequired().HasMaxLength(64);
                b.Property(x => x.ItemText).IsRequired().HasMaxLength(100);
                b.Property(x => x.ItemValue).IsRequired();

                b.HasIndex(q => q.DictId);
                b.HasIndex(q => q.ItemText);
            });


            builder.Entity<OperationInfo>(b =>
            {

                b.ToTable(TablePrefix + "operation_info");

                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();

                b.SetSnakeCaseNaming();

                b.Property(x => x.Id).HasMaxLength(64);
                b.Property(x => x.Title).HasMaxLength(100);
                b.Property(x => x.Content).HasMaxLength(500);

                b.HasIndex(q => q.UserId);
            });

            builder.Entity<DatabaseConnection>(b =>
            {

                b.ToTable(TablePrefix + "database_connection");

                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();
                b.ConfigureFullAuditedAggregateRoot();
                b.TryConfigureJsonValueProperties(x => x.RangeTenantIds, 4000);

                b.SetSnakeCaseNaming();

                b.Property(x => x.Id).HasMaxLength(64);

                b.HasIndex(q => q.Name);
            });
        }

    }
}
