using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.Shared.Common.Enum;
using XFree.Simple.Domain.Shared.SystemManagement.Enum;

namespace XFree.Simple.Domain.SystemManagement.Common
{
    /// <summary>
    ///  数据库连接信息
    /// </summary>
    public class DatabaseConnection : FullAuditedAggregateRoot<string>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public DatabaseConnection(string id)
        {
            Id = id;
            DatabaseProviderType = DatabaseProviderType.EntityFrameworkCore;
            Status = NormalLockedStatus.Normal;
        }

        /// <summary>
        /// 0: NotSpecified
        /// 1: EntityFrameworkCore
        /// 2: MongoDb
        /// </summary>
        public virtual DatabaseProviderType DatabaseProviderType { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Memo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public NormalLockedStatus Status { get; set; }

        /// <summary>
        ///  限定商户范围,默认不限定
        /// </summary>
        public virtual Guid[] RangeTenantIds { get; set; }

        /// <summary>
        ///  数据库连接字符串
        /// </summary>
        public virtual string ConnectionString { get; set; }
    }
}
