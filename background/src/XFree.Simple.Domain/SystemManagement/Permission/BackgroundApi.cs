using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace XFree.Simple.Domain.SystemManagement.Permission
{
    /// <summary>
    /// 接口资源
    /// </summary>
    public class BackgroundApi : Entity<string>, IMultiTenant
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public BackgroundApi(string id)
        {
            Id = id;
        }

        /// <summary>
        /// 租户Id
        /// </summary>
        public virtual Guid? TenantId { get; set; }

        /// <summary>
        ///  一级节点
        /// </summary>
        public virtual bool PrimaryNode { get; set; }

        /// <summary>
        ///  模块
        /// </summary>
        public virtual string Module { get; set; }

        /// <summary>
        /// 父节点权限编码
        /// </summary>
        public virtual string ParentPermissionCode { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        public virtual string PermissionCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public virtual string EnName { get; set; }

        /// <summary>
        /// 接口路径
        /// </summary>
        public virtual string Path { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public virtual string Method { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int SortOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual MultiTenancySides MultiTenancySides { get; set; }

    }
}
