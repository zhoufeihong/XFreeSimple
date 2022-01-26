using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using XFree.Simple.Domain.SystemManagement.Permission;

namespace XFree.Simple.EntityFrameworkCore.SystemManagement.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class DbContextModelCreatingExtensions
    {

        public static void ConfigureSystemManagementPermission(
           this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Role>(b =>
            {

                b.ToTable(TablePrefix + "role");

                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();
                b.ConfigureFullAuditedAggregateRoot();

                b.SetSnakeCaseNaming();

                b.Property(x => x.Id).HasMaxLength(64);
                b.Property(x => x.Code).IsRequired().HasMaxLength(50);
                b.Property(x => x.Name).IsRequired().HasMaxLength(255);
                b.Property(x => x.Status).IsRequired();

                b.HasIndex(q => q.Code);
                b.HasIndex(q => q.Name);
            });

            builder.Entity<UiPermission>(b =>
            {

                b.ToTable(TablePrefix + "ui_permission");

                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();
                b.ConfigureFullAuditedAggregateRoot();

                b.SetSnakeCaseNaming();

                b.Property(x => x.Id).HasMaxLength(64);
                b.Property(x => x.Name).IsRequired().HasMaxLength(255);

                b.HasIndex(q => q.ParentId);
                b.HasIndex(q => q.Name);
            });

            builder.Entity<BackgroundApi>(b =>
            {

                b.ToTable(TablePrefix + "background_api");

                b.SetSnakeCaseNaming();

                b.Property(x => x.Id).HasMaxLength(64);
                b.Property(x => x.Module).HasMaxLength(100);
                b.Property(x => x.Method).HasMaxLength(20);
                b.Property(x => x.Path);

                b.HasIndex(q => q.ParentPermissionCode);
                b.HasIndex(q => q.PermissionCode);
            });

            builder.Entity<UiWithApi>(b =>
            {

                b.ToTable(TablePrefix + "ui_with_api");

                b.SetSnakeCaseNaming();

                b.Property(x => x.Id).HasMaxLength(64);
                b.Property(x => x.PermissionCode).IsRequired().HasMaxLength(64);
                b.Property(x => x.UiPermissionId).IsRequired().HasMaxLength(64);

                b.HasIndex(q => q.PermissionCode);
                b.HasIndex(q => q.UiPermissionId);
            });

            builder.Entity<DepartRole>(b =>
            {

                b.ToTable(TablePrefix + "depart_role");

                b.SetSnakeCaseNaming();

                b.Property(x => x.Id).HasMaxLength(64);
                b.Property(x => x.DepartId).IsRequired().HasMaxLength(64);
                b.Property(x => x.RoleId).IsRequired().HasMaxLength(64);

                b.HasIndex(q => q.DepartId);
                b.HasIndex(q => q.RoleId);
            });

            builder.Entity<UserRole>(b =>
            {

                b.ToTable(TablePrefix + "user_role");

                b.SetSnakeCaseNaming();

                b.Property(x => x.Id).HasMaxLength(64);
                b.Property(x => x.RoleId).IsRequired().HasMaxLength(64);
                b.Property(x => x.UserId).IsRequired().HasMaxLength(64);

                b.HasIndex(q => q.RoleId);
                b.HasIndex(q => q.UserId);
            });

            builder.Entity<RoleUiPermission>(b =>
            {

                b.ToTable(TablePrefix + "role_ui_permission");
                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();

                b.SetSnakeCaseNaming();

                b.Property(x => x.Id).HasMaxLength(64);
                b.Property(x => x.RoleId).IsRequired().HasMaxLength(64);
                b.Property(x => x.UiPermissionId).IsRequired().HasMaxLength(64);

                b.HasIndex(q => q.RoleId);
                b.HasIndex(q => q.UiPermissionId);
            });

        }

    }
}

