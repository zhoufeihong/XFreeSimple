using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Common
{
    /// <summary>
    /// 字典
    /// </summary>
    public class DictDto
    {
        /// <summary>
        /// 字典Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 字典编码
        /// </summary>
        [Required]
        [MaxLength(256)]
        public virtual string DictCode { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        [Required]
        [MaxLength(256)]
        public virtual string DictName { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        public virtual string DictEnName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

    }

    /// <summary>
    /// 分页查询请求
    /// </summary>
    public class DictPagedAndSortedRequestDto : PagedAndSortedRequestDto
    {

        /// <summary>
        /// 字典编码
        /// </summary>
        public string DictCode { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        public string DictName { get; set; }
    }

    /// <summary>
    /// 字典项分页查询请求
    /// </summary>
    public class DictItemPagedAndSortedResultRequestDto : PagedAndSortedRequestDto
    {
        /// <summary>
        /// 字典Id
        /// </summary>
        public string DictId { get; set; }

        /// <summary>
        /// 字典项值
        /// </summary>
        public string ItemValue { get; set; }
    }

    /// <summary>
    /// 字典项
    /// </summary>
    public class DictItemDto
    {

        /// <summary>
        /// 字典项Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 字典id
        /// </summary>
        public string DictId { get; set; }

        /// <summary>
        /// 字典项文本
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string ItemText { get; set; }

        /// <summary>
        /// 字典项英文文本
        /// </summary>
        public virtual string ItemEnText { get; set; }

        /// <summary>
        /// 字典项值
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string ItemValue { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? SortOrder { get; set; }

        /// <summary>
        /// 状态（1启用 0不启用）
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

    }

    /// <summary>
    /// 更新状态
    /// </summary>
    public class UpdateDictItemStatusDto
    {
        /// <summary>
        /// 状态（1启用 0不启用）
        /// </summary>
        public bool Enabled { get; set; }
    }

}
