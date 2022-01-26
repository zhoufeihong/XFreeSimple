using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Encryption;
using Volo.Abp.Uow;
using XFree.Simple.Application.Contracts.Options;
using XFree.Simple.Application.Contracts.SystemManagement.AppService;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.SystemManagement.Common;
using XFree.Simple.Domain.SystemManagement.Organization;

namespace XFree.Simple.Application.Common.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class TenantStore : ITenantStore, ITransientDependency
    {
        private readonly IMemoryCache _memoryCache;

        private readonly DatabaseConnectionStringEncryption _databaseConnectionStringEncryption;

        private readonly ICurrentTenant _currentTenant;

        private readonly IServiceProvider _serviceProvider;

        private const string CachePrefix = Const.CacheKeyPrefix.TENANT;

        public TenantStore(IMemoryCache memoryCache,
            ICurrentTenant currentTenant,
            IServiceProvider serviceProvider,
            DatabaseConnectionStringEncryption databaseConnectionStringEncryption)
        {
            _memoryCache = memoryCache;
            _currentTenant = currentTenant;
            _serviceProvider = serviceProvider;
            _databaseConnectionStringEncryption = databaseConnectionStringEncryption;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<TenantConfiguration> FindAsync(string name)
        {
            return await _memoryCache.GetOrCreateAsync($"{CachePrefix}:@name={name}:TenantConfiguration", async (item) =>
            {
                // 设置2分钟缓存绝对过期时间
                item.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(120);
                using (_currentTenant.Change(null))
                {
                    using var scope = _serviceProvider.CreateScope();
                    var tenantRepository = scope.ServiceProvider.GetRequiredService<IRepository<Tenant, string>>();
                    var databaseConnectionRepository = scope.ServiceProvider.GetRequiredService<IRepository<DatabaseConnection, string>>();
                    var tenant = await tenantRepository.FirstOrDefaultAsync(f => f.Name == name);
                    if (tenant != null)
                    {
                        return await GetTenantConfiguration(tenant, databaseConnectionRepository);
                    }
                    return null;
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TenantConfiguration> FindAsync(Guid id)
        {
            return await _memoryCache.GetOrCreateAsync($"{CachePrefix}:@id={id}:TenantConfiguration", async (item) =>
            {
                // 设置2分钟缓存绝对过期时间
                item.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(120);
                using (_currentTenant.Change(null))
                {
                    using var scope = _serviceProvider.CreateScope();
                    var uowManager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();
                    using var uow = uowManager.Begin(new AbpUnitOfWorkOptions { IsTransactional = false }, true);
                    var tenantRepository = scope.ServiceProvider.GetRequiredService<IRepository<Tenant, string>>();
                    var databaseConnectionRepository = scope.ServiceProvider.GetRequiredService<IRepository<DatabaseConnection, string>>();
                    var tenant = await tenantRepository.FirstOrDefaultAsync(f => f.Id == id.ToString());
                    if (tenant != null)
                    {
                        return await GetTenantConfiguration(tenant, databaseConnectionRepository);
                    }
                    return null;
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TenantConfiguration Find(string name)
        {
            return FindAsync(name).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TenantConfiguration Find(Guid id)
        {
            return FindAsync(id).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        #region Private

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenant"></param>
        /// <param name="databaseConnectionRepository"></param>
        /// <returns></returns>
        private async Task<TenantConfiguration> GetTenantConfiguration(Tenant tenant, IRepository<DatabaseConnection, string> databaseConnectionRepository)
        {
            var tenantConfiguration = new TenantConfiguration
            {
                Id = Guid.Parse(tenant.Id),
                Name = tenant.Name,
                IsActive = tenant.Status == Domain.Shared.SystemManagement.Enum.NormalLockedStatus.Normal
            };
            var connectionStrings = new ConnectionStrings();
            // 独立数据库
            if (tenant.IsStandaloneDatabase)
            {
                connectionStrings.Add(ConnectionStrings.DefaultConnectionStringName, Decrypt(tenant.StandaloneDatabaseConnectionString));
                tenantConfiguration.ConnectionStrings = connectionStrings;
                return tenantConfiguration;
            }
            // 采用数据库集群
            var databaseConnections = await databaseConnectionRepository.AsNoTracking().FirstOrDefaultAsync(f => f.Name == tenant.DefaultConnectionStringName);
            connectionStrings.Add(ConnectionStrings.DefaultConnectionStringName, Decrypt(databaseConnections?.ConnectionString));
            tenantConfiguration.ConnectionStrings = connectionStrings;
            return tenantConfiguration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        private string Decrypt(string cipherText)
        {
            return _databaseConnectionStringEncryption.Decrypt(cipherText);
        }

        #endregion

    }
}
