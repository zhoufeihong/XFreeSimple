using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace XFree.Simple.Domain.SystemManagement.Common
{
    /// <summary>
    /// 字典项
    /// </summary>
    public class DictItem : FullAuditedAggregateRoot<string>, IMultiTenant
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public DictItem(string id)
        {
            Id = id;
            Enabled = true;
        }

        /// <summary>
        /// 租户Id
        /// </summary>
        public virtual Guid? TenantId { get; set; }

        /// <summary>
        /// 字典id
        /// </summary>
        public virtual string DictId { get; set; }

        /// <summary>
        /// 字典项文本
        /// </summary>
        public virtual string ItemText { get; set; }

        /// <summary>
        /// 字典项英文文本
        /// </summary>
        public virtual string ItemEnText { get; set; }

        /// <summary>
        /// 字典项值
        /// </summary>
        public virtual string ItemValue { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int? SortOrder { get; set; }

        /// <summary>
        /// 状态（1启用 0不启用）
        /// </summary>
        public virtual bool Enabled { get; set; }

    }
}
