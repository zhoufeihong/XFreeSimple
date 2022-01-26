using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.Shared.SystemManagement.Enum;

namespace XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization
{

    /// <summary>
    /// 职务
    /// </summary>
    public class PostDto
    {
        /// <summary>
        /// 职务Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public NormalLockedStatus Status { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int? SortOrder { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }

    /// <summary>
    /// 创建职务
    /// </summary>
    public class CreatePostDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int? SortOrder { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }

    /// <summary>
    /// 更新职务
    /// </summary>
    public class UpdatePostDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int? SortOrder { get; set; }
    }

    /// <summary>
    /// 更新状态
    /// </summary>
    public class UpdatePostStatusDto
    {
        /// <summary>
        /// 
        /// </summary>
        public NormalLockedStatus Status { get; set; }
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    public class PostPagedAndSortedRequestDto : PagedAndSortedRequestDto
    {
        /// <summary>
        /// 职务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 职务编码
        /// </summary>
        public string Code { get; set; }
    }

}
