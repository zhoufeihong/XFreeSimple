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
    /// 组织机构/岗位
    /// </summary>
    public class Depart : FullAuditedAggregateRoot<string>, IMultiTenant
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public Depart(string id)
        {
            Id = id;
            Status = NormalLockedStatus.Normal;
        }

        /// <summary>
        /// 租户Id
        /// </summary>
        public virtual Guid? TenantId { get; set; }

        /// <summary>
        /// 父机构ID
        /// </summary>
        public virtual string ParentId { get; set; }

        /// <summary>
        /// 机构/部门名称
        /// </summary>
        public virtual string OrgName { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public virtual string OrgNameEn { get; set; }

        /// <summary>
        /// 机构类别 1组织机构，2岗位
        /// </summary>
        public virtual OrgCategory OrgCategory { get; set; }

        /// <summary>
        /// 机构类型 1一级部门 2子部门
        /// </summary>
        public virtual OrgLevelType OrgLevelType { get; set; }

        /// <summary>
        ///  类型
        /// </summary>
        public virtual string OrgType { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        public virtual string OrgCode { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public virtual string Contact { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Memo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual NormalLockedStatus Status { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int? SortOrder { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }

    }
}
