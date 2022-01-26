using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.Shared.SystemManagement.Enum;

namespace XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization
{
    /// <summary>
    /// 机构
    /// </summary>
    public class DepartDto
    {
        /// <summary>
        /// 机构Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 父机构ID
        /// </summary>
        public virtual string ParentId { get; set; }

        /// <summary>
        /// 机构/部门名称
        /// </summary>
        [Required]
        [MaxLength(256)]
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
        [MaxLength(256)]
        public virtual string OrgCode { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        [MaxLength(256)]
        public virtual string Contact { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
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

        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }

    /// <summary>
    /// 创建机构
    /// </summary>
    public class CreateDepartDto
    {
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

    /// <summary>
    /// 更新机构
    /// </summary>
    public class UpdateDepartDto
    {

        /// <summary>
        /// 机构/部门名称
        /// </summary>
        [Required]
        [MaxLength(256)]
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

    /// <summary>
    /// 分页查询
    /// </summary>
    public class DepartPagedAndSortedRequestDto : PagedAndSortedRequestDto
    {
        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgName { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; set; }
    }

}
