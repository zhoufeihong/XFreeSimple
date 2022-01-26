using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus;
using Volo.Abp.Guids;
using XFree.Simple.Domain.SystemManagement.Organization.Events;

namespace XFree.Simple.Domain.SystemManagement.Organization.EventHandlers
{
    /// <summary>
    /// 用户操作事件处理器
    /// </summary>
    public class OperationEventHandler : ILocalEventHandler<OperationEvent>,
          ITransientDependency
    {

        private readonly IRepository<OperationInfo, string> _operationRepository;

        private readonly IGuidGenerator _guidGenerator;

        private readonly IAuditingManager _auditingManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationRepository"></param>
        /// <param name="guidGenerator"></param>
        /// <param name="auditingManager"></param>
        public OperationEventHandler(IRepository<OperationInfo, string> operationRepository,
            IGuidGenerator guidGenerator,
            IAuditingManager auditingManager)
        {
            _operationRepository = operationRepository;
            _guidGenerator = guidGenerator;
            _auditingManager = auditingManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public async Task HandleEventAsync(OperationEvent eventData)
        {
            var auditLog = _auditingManager.Current.Log;
            await _operationRepository.InsertAsync(new OperationInfo(_guidGenerator.Create().ToString())
            {
                Title = eventData.Title,
                Content = eventData.Content,
                Ip = auditLog.ClientIpAddress,
                UserId = eventData.UserId ?? auditLog.UserId?.ToString(),
                CreationTime = DateTime.Now,
                Status = eventData.Status
            });
        }

    }
}
