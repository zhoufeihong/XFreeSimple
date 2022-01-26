using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.Shared.Common.Enum;
using XFree.Simple.Domain.Shared.SystemManagement.Enum;

namespace XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Common
{
    /// <summary>
    /// 数据库连接信息
    /// </summary>
    public class DatabaseConnectionDto
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 0: NotSpecified
        /// 1: EntityFrameworkCore
        /// 2: MongoDb
        /// </summary>
        public DatabaseProviderType DatabaseProviderType { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
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
        ///  限定商户范围,默认不限定
        /// </summary>
        public Guid[] RangeTenantIds { get; set; }

        /// <summary>
        ///  数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

    }

    /// <summary>
    /// 创建租户
    /// </summary>
    public class CreateDatabaseConnectionDto
    {

        /// <summary>
        /// 0: NotSpecified
        /// 1: EntityFrameworkCore
        /// 2: MongoDb
        /// </summary>
        public DatabaseProviderType DatabaseProviderType { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(256)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        ///  限定商户范围,默认不限定
        /// </summary>
        public Guid[] RangeTenantIds { get; set; }

        /// <summary>
        ///  数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class UpdateDatabaseConnectionDto
    {
        /// <summary>
        /// 0: NotSpecified
        /// 1: EntityFrameworkCore
        /// 2: MongoDb
        /// </summary>
        public DatabaseProviderType DatabaseProviderType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        ///  限定商户范围,默认不限定
        /// </summary>
        public Guid[] RangeTenantIds { get; set; }

        /// <summary>
        ///  数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
    }

    /// <summary>
    /// 更新状态
    /// </summary>
    public class UpdateDatabaseConnectionStatusDto
    {
        /// <summary>
        /// 
        /// </summary>
        public NormalLockedStatus Status { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DatabaseConnectionPagedAndSortedRequestDto : PagedAndSortedRequestDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }


}
