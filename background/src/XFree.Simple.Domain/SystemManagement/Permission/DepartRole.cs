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
    ///  部门角色表
    /// </summary>
    public class DepartRole : Entity<string>, IMultiTenant
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        public virtual Guid? TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string DepartId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string RoleId { get; set; }
    }
}
