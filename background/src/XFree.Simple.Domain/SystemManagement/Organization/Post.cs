using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using XFree.Simple.Domain.Shared.SystemManagement.Enum;

namespace XFree.Simple.Domain.SystemManagement.Organization
{

    /// <summary>
    /// 职务
    /// </summary>
    public class Post : FullAuditedAggregateRoot<string>, IMultiTenant
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public Post(string id)
        {
            Id = id;
            Status = NormalLockedStatus.Normal;
        }

        /// <summary>
        /// 租户Id
        /// </summary>
        public virtual Guid? TenantId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual NormalLockedStatus Status { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int? SortOrder { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Memo { get; set; }
    }

}
