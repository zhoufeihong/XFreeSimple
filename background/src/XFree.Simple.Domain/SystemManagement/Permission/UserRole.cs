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
    ///  用户角色
    /// </summary>
    public class UserRole : Entity<string>, IMultiTenant
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public UserRole(string id)
        {
            Id = id;
        }

        /// <summary>
        /// 租户Id
        /// </summary>
        public virtual Guid? TenantId { get; set; }

        /// <summary>
        /// 员工Id
        /// </summary>
        public virtual string UserId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public virtual string RoleId { get; set; }
    }
}
