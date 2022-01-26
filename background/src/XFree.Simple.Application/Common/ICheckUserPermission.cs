using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace XFree.Simple.Application.Common
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICheckUserPermission : IScopedDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissionCode"></param>
        /// <returns></returns>
        Task<bool> Check(string userId,string permissionCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="perms"></param>
        /// <returns></returns>
        Task<bool> CheckUiPermission(string userId, string perms);
    }
}
