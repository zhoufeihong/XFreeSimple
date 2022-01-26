using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using XFree.Simple.Domain.Shared.SystemManagement;
using XFree.Simple.Domain.Shared.SystemManagement.Domain;
using XFree.Simple.Domain.Shared.SystemManagement.Enum;
using XFree.Simple.Domain.SystemManagement.Permission;

namespace XFree.Simple.Domain.SystemManagement.Organization
{
    /// <summary>
    ///  组织机构管理者
    /// </summary>
    public class DepartManager : DomainService
    {

        private readonly IRepository<Depart, string> _departRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departRepository"></param>
        public DepartManager(IRepository<Depart, string> departRepository)
        {
            _departRepository = departRepository;
        }

        /// <summary>
        /// 获取组织机构及其下级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Depart>> GetDeepDeparts(string id)
        {
            var departs = await _departRepository.ToListAsync();
            if (string.IsNullOrEmpty(id))
            {
                return departs;
            }
            var currentDepart = departs.FirstOrDefault(f => f.Id == id);
            if (currentDepart == null)
            {
                return new List<Depart>();
            }
            var tempResult = new List<Depart>();
            Queue<Depart> tempQueue = new();
            tempQueue.Enqueue(currentDepart);
            while (tempQueue.Count > 0)
            {
                var tempDepart = tempQueue.Dequeue();
                tempResult.Add(tempDepart);
                foreach (var waitEnqueue in departs.Where(w => w.ParentId == tempDepart.Id))
                {
                    tempQueue.Enqueue(waitEnqueue);
                }
            }
            return tempResult;
        }

    }
}
