using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.Shared.SystemManagement.Enum;

namespace XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization
{
    /// <summary>
    /// 租户
    /// </summary>
    public class TenantDto
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///  备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public NormalLockedStatus Status { get; set; }

        /// <summary>
        ///  数据库连接配置名称
        /// </summary>
        public string DefaultConnectionStringName { get; set; }

        /// <summary>
        ///  采用独立的数据库
        /// </summary>
        public bool IsStandaloneDatabase { get; set; }

        /// <summary>
        ///  采用独立的数据库
        /// </summary>
        public string StandaloneDatabaseConnectionString { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public virtual string Language { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

    }

    /// <summary>
    /// 创建租户
    /// </summary>
    public class CreateTenantDto
    {

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///  备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public NormalLockedStatus Status { get; set; }

        /// <summary>
        ///  数据库连接配置名称
        /// </summary>
        public string DefaultConnectionStringName { get; set; }

        /// <summary>
        ///  采用独立的数据库
        /// </summary>
        public bool IsStandaloneDatabase { get; set; }

        /// <summary>
        ///  采用独立的数据库
        /// </summary>
        public string StandaloneDatabaseConnectionString { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public virtual string Language { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class UpdateTenantDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///  备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public NormalLockedStatus Status { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public virtual string Language { get; set; }

        /// <summary>
        ///  数据库连接配置名称
        /// </summary>
        public string DefaultConnectionStringName { get; set; }

        /// <summary>
        ///  采用独立的数据库
        /// </summary>
        public bool IsStandaloneDatabase { get; set; }

        /// <summary>
        ///  采用独立的数据库
        /// </summary>
        public string StandaloneDatabaseConnectionString { get; set; }
    }

    /// <summary>
    /// 更新状态
    /// </summary>
    public class UpdateTenantStatusDto
    {
        /// <summary>
        /// 
        /// </summary>
        public NormalLockedStatus Status { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TenantPagedAndSortedRequestDto : PagedAndSortedRequestDto
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
