using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace XFree.Simple.Domain.SystemManagement.Permission
{
    /// <summary>
    ///  角色权限关联表
    /// </summary>
    public class RoleUiPermission : AggregateRoot<string>, IMultiTenant
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public RoleUiPermission(string id)
        {
            this.Id = id;
        }

        /// <summary>
        /// 租户Id
        /// </summary>
        public virtual Guid? TenantId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public virtual string RoleId { get; set; }

        /// <summary>
        /// 权限Id
        /// </summary>
        public virtual string UiPermissionId { get; set; }

    }
}
