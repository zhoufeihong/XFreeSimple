using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus;
using XFree.Simple.Application.Common.Impl;
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.Domain.SystemManagement.Organization.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class TenantHandler
        : ILocalEventHandler<EntityCreatedEventData<Tenant>>,
          ILocalEventHandler<EntityUpdatedEventData<Tenant>>,
          ITransientDependency
    {

        /// <summary>
        /// 
        /// </summary>
        private readonly XFreeSimpleDbMigrationService _xfreeSimpleDbMigrationService;

        private readonly static SemaphoreSlim SemaphoreSlim = new(1, 1);

        private readonly IRepository<Tenant, string> _tenantRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xfreeSimpleDbMigrationService"></param>
        public TenantHandler(XFreeSimpleDbMigrationService xfreeSimpleDbMigrationService,
            IRepository<Tenant, string> tenantRepository)
        {
            _xfreeSimpleDbMigrationService = xfreeSimpleDbMigrationService;
            _tenantRepository = tenantRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public async Task HandleEventAsync(EntityCreatedEventData<Tenant> eventData)
        {
            await TryHandle(eventData.Entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public async Task HandleEventAsync(EntityUpdatedEventData<Tenant> eventData)
        {
            await TryHandle(eventData.Entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        public async Task TryHandle(Tenant tenant)
        {
            if (tenant.InitialDataStatus == Shared.SystemManagement.Enum.ProcessingStatus.Ready)
            {
                // TODO 分布式锁
                using var lockResult = await SemaphoreSlim.LockResultAsync(30000);
                if (!lockResult.Result)
                {
                    return;
                }
                tenant.InitialDataStatus = Shared.SystemManagement.Enum.ProcessingStatus.Processing;
                await _tenantRepository.UpdateAsync(tenant, true);
                await _xfreeSimpleDbMigrationService.MigrateAsync(tenant);
            }
        }
    }
}
