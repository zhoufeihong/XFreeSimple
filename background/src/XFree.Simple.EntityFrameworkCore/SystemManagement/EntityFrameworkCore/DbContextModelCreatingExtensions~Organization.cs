using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using XFree.Simple.Domain.SystemManagement.Organization;
using XFree.Simple.Domain.SystemManagement.Permission;

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
        /// <param name="builder"></param>
        public static void ConfigureSystemManagementOrganization(
           this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Tenant>(b =>
            {

                b.ToTable(TablePrefix + "tenant");

                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();
                b.ConfigureFullAuditedAggregateRoot();

                b.SetSnakeCaseNaming();

                b.Property(x => x.Id).HasMaxLength(64);
                b.Property(x => x.Code).IsRequired().HasMaxLength(50);
                b.Property(x => x.Name).IsRequired().HasMaxLength(255);
                b.Property(x => x.StandaloneDatabaseConnectionString).IsRequired().HasMaxLength(512);
                b.Property(x => x.Email).HasMaxLength(100);
                b.Property(x => x.Phone).HasMaxLength(100);

                b.HasIndex(q => q.Code);
                b.HasIndex(q => q.Name);
            });

            builder.Entity<Depart>(b =>
            {

                b.ToTable(TablePrefix + "depart");

                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();
                b.ConfigureFullAuditedAggregateRoot();

                b.SetSnakeCaseNaming();

                b.Property(x => x.Id).HasMaxLength(64);
                b.Property(x => x.OrgCode).IsRequired().HasMaxLength(50);
                b.Property(x => x.OrgName).IsRequired().HasMaxLength(255);
                b.Property(x => x.OrgLevelType);

                b.HasIndex(q => q.OrgCode);
                b.HasIndex(q => q.OrgName);
            });

            builder.Entity<User>(b =>
            {

                b.ToTable(TablePrefix + "user");

                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();
                b.ConfigureFullAuditedAggregateRoot();

                b.SetSnakeCaseNaming();

                b.Property(x => x.Id).HasMaxLength(64);
                b.Property(x => x.LoginName).IsRequired().HasMaxLength(50);
                b.Property(x => x.Nickname).IsRequired().HasMaxLength(50);
                b.Property(x => x.EmployeeIDNumber).IsRequired().HasMaxLength(50);
                b.Property(x => x.Email).HasMaxLength(100);
                b.Property(x => x.Phone).HasMaxLength(50);

                b.HasIndex(q => q.LoginName);
                b.HasIndex(q => q.EmployeeIDNumber);
                b.HasIndex(q => q.Phone);
            });

            builder.Entity<Post>(b =>
            {

                b.ToTable(TablePrefix + "post");

                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();
                b.ConfigureFullAuditedAggregateRoot();

                b.SetSnakeCaseNaming();

                b.Property(x => x.Id).HasMaxLength(64);
                b.Property(x => x.Code).IsRequired().HasMaxLength(50);
                b.Property(x => x.Name).IsRequired().HasMaxLength(50);
                b.Property(x => x.Memo).HasMaxLength(500);
                b.HasIndex(q => q.Code);

            });

        }

    }
}
