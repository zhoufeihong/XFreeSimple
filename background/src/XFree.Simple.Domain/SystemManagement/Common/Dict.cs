using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using XFree.Simple.Domain.Shared.SystemManagement;

namespace XFree.Simple.Domain.SystemManagement.Common
{
    /// <summary>
    /// 字典信息
    /// </summary>
    public class Dict : FullAuditedAggregateRoot<string>, IMultiTenant
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public Dict(string id)
        {
            this.Id = id;
        }

        /// <summary>
        /// 租户Id
        /// </summary>
        public virtual Guid? TenantId { get; set; }

        /// <summary>
        /// 字典编码
        /// </summary>
        public virtual string DictCode { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        public virtual string DictEnName { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        public virtual string DictName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

    }
}
