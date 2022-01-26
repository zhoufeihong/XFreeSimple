using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace XFree.Simple.Domain.SystemManagement.Permission
{
    /// <summary>
    /// Ui菜单权限
    /// </summary>
    public class UiPermission : FullAuditedAggregateRoot<string>, IMultiTenant
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public UiPermission(string id)
        {
            Id = id;
            IsLeaf = true;
            MultiTenancySides = MultiTenancySides.Both;
        }

        /// <summary>
        /// 租户Id
        /// </summary>
        public virtual Guid? TenantId { get; set; }

        /// <summary>
        /// 父id
        /// </summary>
        public virtual string ParentId { get; set; }

        /// <summary>
        /// 菜单标题
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 菜单英文标题
        /// </summary>
        public virtual string EnName { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public virtual string Url { get; set; }

        /// <summary>
        /// 组件名称
        /// </summary>
        public virtual string ComponentName { get; set; }

        /// <summary>
        /// 组件
        /// </summary>
        public virtual string Component { get; set; }

        /// <summary>
        /// 一级菜单跳转地址
        /// </summary>
        public virtual string Redirect { get; set; }

        /// <summary>
        /// 菜单类型(0:一级菜单; 1:子菜单:2:按钮权限)
        /// </summary>
        public virtual int UiMenuType { get; set; }

        /// <summary>
        /// 菜单权限编码
        /// </summary>
        public virtual string Perms { get; set; }

        /// <summary>
        /// 权限策略1显示2禁用
        /// </summary>
        public virtual string PermsType { get; set; }

        /// <summary>
        /// 菜单排序
        /// </summary>
        public virtual int SortOrder { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public virtual string Icon { get; set; }

        /// <summary>
        /// 是否路由菜单: 0:不是  1:是（默认值1）
        /// </summary>
        public virtual bool IsRoute { get; set; }

        /// <summary>
        /// 是否叶子节点:    1:是   0:不是
        /// </summary>
        public virtual bool IsLeaf { get; set; }

        /// <summary>
        /// 是否缓存该页面:    1:是   0:不是
        /// </summary>
        public virtual bool KeepAlive { get; set; }

        /// <summary>
        /// 是否隐藏路由: 0否,1是
        /// </summary>
        public virtual bool Hidden { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 按钮权限状态(0无效1有效)
        /// </summary>
        public virtual bool Enabled { get; set; }

        /// <summary>
        /// 菜单打开方式 0/内部打开 1/外部打开
        /// </summary>
        public virtual bool OpenMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual MultiTenancySides MultiTenancySides { get; set; }
    }
}
