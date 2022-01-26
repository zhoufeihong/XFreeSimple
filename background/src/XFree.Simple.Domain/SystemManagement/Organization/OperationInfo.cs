using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;
using XFree.Simple.Domain.Shared.SystemManagement.Enum;

namespace XFree.Simple.Domain.SystemManagement.Organization
{
    /// <summary>
    /// 操作信息
    /// </summary>
    public class OperationInfo : AggregateRoot<string>, IMultiTenant
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public OperationInfo(string id)
        {
            Id = id;
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public virtual string UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        ///  内容
        /// </summary>
        public virtual string Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Ip { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }

        /// <summary>
        /// 状态(1-成功 2-失败)
        /// </summary>
        public virtual SuccessFailStatus Status { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? TenantId { get; set; }
    }
}
