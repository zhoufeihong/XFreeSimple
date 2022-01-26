using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using XFree.Simple.Domain.Shared.SystemManagement.Enum;

namespace XFree.Simple.Domain.SystemManagement.Permission
{
    /// <summary>
    /// 角色信息
    /// </summary>
    public class Role : FullAuditedAggregateRoot<string>, IMultiTenant
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public Role(string id)
        {
            Id = id;
            Status = NormalLockedStatus.Normal;
        }

        /// <summary>
        /// 租户Id
        /// </summary>
        public virtual Guid? TenantId { get; set; }

        /// <summary>
        /// 角色编码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Memo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual NormalLockedStatus Status { get; set; }

        /// <summary>
        /// 可访问权限类型
        /// </summary>
        public virtual RoleAccessType RoleAccessType { get; set; }

        /// <summary>
        ///  访问权限编码
        /// </summary>
        public virtual string AccessValue { get; set; }

    }
}
