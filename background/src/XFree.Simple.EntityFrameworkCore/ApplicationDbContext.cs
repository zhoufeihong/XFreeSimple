using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;
using XFree.Simple.Domain.SystemManagement;
using XFree.Simple.Domain.SystemManagement.Common;
using XFree.Simple.Domain.SystemManagement.Organization;
using XFree.Simple.Domain.SystemManagement.Permission;
using XFree.Simple.EntityFrameworkCore.SystemManagement.EntityFrameworkCore;

namespace XFree.Simple.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    [ConnectionStringName("ApplicationConnection")]
    public class ApplicationDbContext : AbpDbContext<ApplicationDbContext>
    {

        #region system

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Dict> Dicts { get; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<DictItem> DictItems { get; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Tenant> Tenants { get; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<DatabaseConnection> DatabaseConnections { get; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Depart> Departs { get; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<User> Users { get; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Post> Posts { get; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<OperationInfo> OperationInfos { get; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<BackgroundApi> BackgroundApis { get; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Role> Roles { get; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<UiPermission> UiPermissions { get; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<RoleUiPermission> RoleUiPermissions { get; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<UserRole> UserRoles { get; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<UiWithApi> UiWithApis { get; }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureSystemManagementBase();

            builder.ConfigureSystemManagementOrganization();

            builder.ConfigureSystemManagementPermission();
        }

        protected override void ApplyAbpConceptsForAddedEntity(EntityEntry entry, EntityChangeReport changeReport)
        {
            base.ApplyAbpConceptsForAddedEntity(entry, changeReport);
            TryToSetTenantId(entry);
        }

        protected virtual void TryToSetTenantId(EntityEntry entry)
        {
            if (entry.Entity is IMultiTenant && HasTenantIdProperty(entry.Entity))
            {
                var tenantId = CurrentTenant.Id;

                if (!tenantId.HasValue)
                {
                    return;
                }

                var propertyInfo = entry.Entity.GetType().GetProperty(nameof(IMultiTenant.TenantId));

                if (propertyInfo == null || propertyInfo.GetSetMethod(true) == null)
                {
                    return;
                }

                propertyInfo.SetValue(entry.Entity, tenantId);
            }
        }

        protected virtual bool HasTenantIdProperty<TEntity>(TEntity entity)
        {
            return entity.GetType().GetProperty(nameof(IMultiTenant.TenantId)) != null;
        }

    }
}
