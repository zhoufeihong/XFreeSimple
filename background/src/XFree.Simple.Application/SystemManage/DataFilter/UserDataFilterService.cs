using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using XFree.Simple.Domain.SystemManagement.Organization;

namespace XFree.Simple.Application.SystemManage.DataFilter
{
    /// <summary>
    /// 
    /// </summary>
    public class UserDataFilterService : IScopedDependency
    {
        private readonly DepartManager _departManager;
        private readonly IRepository<User, string> _userReponsitory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departManager"></param>
        /// <param name="userReponsitory"></param>
        public UserDataFilterService(DepartManager departManager, IRepository<User, string> userReponsitory)
        {
            _departManager = departManager;
            _userReponsitory = userReponsitory;
        }

        /// <summary>
        ///  查询用户可访问部门列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<(bool, List<string>)> GetDepartIds(string userId)
        {
            var user = await _userReponsitory.GetAsync(userId);
            var supperUser = user.SupperUser;
            if (supperUser)
            {
                return (supperUser, (await _departManager.GetDeepDeparts(string.Empty)).Select(s => s.Id).ToList());
            }
            if (!supperUser && string.IsNullOrEmpty(user.DepartId))
            {
                return (supperUser, new List<string>());
            }
            return (supperUser, (await _departManager.GetDeepDeparts(user.DepartId)).Select(s => s.Id).ToList());
        }

    }
}
