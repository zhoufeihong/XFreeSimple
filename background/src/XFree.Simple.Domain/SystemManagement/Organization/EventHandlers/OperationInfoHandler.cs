using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace XFree.Simple.Domain.SystemManagement.Organization.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class OperationInfoHandler
        : ILocalEventHandler<EntityCreatedEventData<OperationInfo>>,
          ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public async Task HandleEventAsync(
            EntityCreatedEventData<OperationInfo> eventData)
        {
            await ValueTask.CompletedTask;
        }
    }
}
