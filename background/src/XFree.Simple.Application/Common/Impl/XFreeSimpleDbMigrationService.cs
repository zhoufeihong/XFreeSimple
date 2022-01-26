using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;
using XFree.Simple.Domain.SystemManagement.Organization;

namespace XFree.Simple.Application.Common.Impl
{
    public class XFreeSimpleDbMigrationService : ITransientDependency
    {
        public ILogger<XFreeSimpleDbMigrationService> Logger { get; set; }

        private readonly IDataSeeder _dataSeeder;
        private readonly ICurrentTenant _currentTenant;

        public XFreeSimpleDbMigrationService(
            IDataSeeder dataSeeder,
            ICurrentTenant currentTenant)
        {
            _dataSeeder = dataSeeder;
            _currentTenant = currentTenant;

            Logger = NullLogger<XFreeSimpleDbMigrationService>.Instance;
        }

        public async Task MigrateAsync(Tenant tenant)
        {
            Logger.LogInformation($"Started migrations tenant {tenant.Code}...");

            using (_currentTenant.Change(Guid.Parse(tenant.Id)))
            {
                await _dataSeeder.SeedAsync(new DataSeedContext(Guid.Parse(tenant.Id)));
            }

            Logger.LogInformation($"Successfully completed migrations {tenant.Code}.");
        }

    }
}
