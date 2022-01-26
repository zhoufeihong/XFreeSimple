using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Permission
{

    /// <summary>
    /// Ui菜单权限
    /// </summary>
    public class UiPermissionDto
    {

        /// <summary>
        /// Ui菜单权限Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 父id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 菜单标题
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// 菜单英文标题
        /// </summary>
        public string EnName { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        [MaxLength(256)]
        public string Url { get; set; }

        /// <summary>
        /// 组件名称
        /// </summary>
        [MaxLength(256)]
        public string ComponentName { get; set; }

        /// <summary>
        /// 组件
        /// </summary>
        [MaxLength(256)]
        public string Component { get; set; }

        /// <summary>
        /// 一级菜单跳转地址
        /// </summary>
        [MaxLength(256)]
        public string Redirect { get; set; }

        /// <summary>
        /// 菜单类型(0:一级菜单; 1:子菜单:2:按钮权限)
        /// </summary>
        public int UiMenuType { get; set; }

        /// <summary>
        /// 菜单权限编码
        /// </summary>
        public string Perms { get; set; }

        /// <summary>
        /// 权限策略1显示2禁用
        /// </summary>
        public string PermsType { get; set; }

        /// <summary>
        /// 菜单排序
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否路由菜单: 0:不是  1:是（默认值1）
        /// </summary>
        public bool IsRoute { get; set; }

        /// <summary>
        /// 是否叶子节点:    1:是   0:不是
        /// </summary>
        public bool IsLeaf { get; set; }

        /// <summary>
        /// 是否缓存该页面:    1:是   0:不是
        /// </summary>
        public bool KeepAlive { get; set; }

        /// <summary>
        /// 是否隐藏路由: 0否,1是
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 按钮权限状态(0无效1有效)
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 菜单打开方式 0/内部打开 1/外部打开
        /// </summary>
        public bool OpenMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

    }

    /// <summary>
    /// 创建
    /// </summary>
    public class CreateUiPermissionDto
    {

        /// <summary>
        /// 父id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 菜单标题
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单英文标题
        /// </summary>
        public string EnName { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 组件名称
        /// </summary>
        public string ComponentName { get; set; }

        /// <summary>
        /// 组件
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 一级菜单跳转地址
        /// </summary>
        public string Redirect { get; set; }

        /// <summary>
        /// 菜单类型(0:一级菜单; 1:子菜单:2:按钮权限)
        /// </summary>
        public int UiMenuType { get; set; }

        /// <summary>
        /// 菜单权限编码
        /// </summary>
        public string Perms { get; set; }

        /// <summary>
        /// 权限策略1显示2禁用
        /// </summary>
        public string PermsType { get; set; }

        /// <summary>
        /// 菜单排序
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否路由菜单: 0:不是  1:是（默认值1）
        /// </summary>
        public bool IsRoute { get; set; }

        /// <summary>
        /// 是否缓存该页面:    1:是   0:不是
        /// </summary>
        public bool KeepAlive { get; set; }

        /// <summary>
        /// 是否隐藏路由: 0否,1是
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 按钮权限状态(0无效1有效)
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 菜单打开方式 0/内部打开 1/外部打开
        /// </summary>
        public bool OpenMode { get; set; }
    }

    /// <summary>
    /// 更新
    /// </summary>
    public class UpdateUiPermissionDto
    {
        /// <summary>
        /// 父id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 菜单标题
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单英文标题
        /// </summary>
        public string EnName { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 组件名称
        /// </summary>
        public string ComponentName { get; set; }

        /// <summary>
        /// 组件
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 一级菜单跳转地址
        /// </summary>
        public string Redirect { get; set; }

        /// <summary>
        /// 菜单类型(0:一级菜单; 1:子菜单:2:按钮权限)
        /// </summary>
        public int UiMenuType { get; set; }

        /// <summary>
        /// 菜单权限编码
        /// </summary>
        public string Perms { get; set; }

        /// <summary>
        /// 权限策略1显示2禁用
        /// </summary>
        public string PermsType { get; set; }

        /// <summary>
        /// 菜单排序
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否路由菜单: 0:不是  1:是（默认值1）
        /// </summary>
        public bool IsRoute { get; set; }

        /// <summary>
        /// 是否缓存该页面:    1:是   0:不是
        /// </summary>
        public bool KeepAlive { get; set; }

        /// <summary>
        /// 是否隐藏路由: 0否,1是
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 按钮权限状态(0无效1有效)
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 菜单打开方式 0/内部打开 1/外部打开
        /// </summary>
        public bool OpenMode { get; set; }
    }

}
