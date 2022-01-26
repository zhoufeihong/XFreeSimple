using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using XFree.Simple.Domain.Shared.SystemManagement.Enum;

namespace XFree.Simple.Domain.SystemManagement.Organization
{
    /// <summary>
    ///  租户信息
    /// </summary>
    public class Tenant : FullAuditedAggregateRoot<string>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public Tenant(string id)
        {
            this.Id = id;
        }

        /// <summary>
        ///  租户编码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///  备注
        /// </summary>
        public virtual string Memo { get; set; }

        /// <summary>
        ///  数据库连接配置名称
        /// </summary>
        public virtual string DefaultConnectionStringName { get; set; }

        /// <summary>
        ///  采用独立的数据库
        /// </summary>
        public virtual bool IsStandaloneDatabase { get; set; }

        /// <summary>
        ///  采用独立的数据库连接字符串
        /// </summary>
        public virtual string StandaloneDatabaseConnectionString { get; set; }

        /// <summary>
        /// 手机号/电话
        /// </summary>
        public virtual string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public virtual string Language { get; set; }

        /// <summary>
        /// 商户状态
        /// </summary>
        public virtual NormalLockedStatus Status { get; set; }

        /// <summary>
        ///  数据初始化状态
        ///  TODO: Job重试
        /// </summary>
        public virtual ProcessingStatus InitialDataStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void SetInitialDataStatus()
        {
            if (InitialDataStatus != ProcessingStatus.Initial)
            {
                return;
            }
            if (IsStandaloneDatabase && !string.IsNullOrEmpty(StandaloneDatabaseConnectionString))
            {
                InitialDataStatus = ProcessingStatus.Ready;
            }
            if (!IsStandaloneDatabase && !string.IsNullOrEmpty(DefaultConnectionStringName))
            {
                InitialDataStatus = ProcessingStatus.Ready;
            }
        }

    }
}
