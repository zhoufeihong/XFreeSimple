using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XFree.Simple.Domain.Shared.SystemManagement.Enum
{
    /// <summary>
    ///  角色可访问类型
    /// </summary>
    public enum RoleAccessType
    {
        /// <summary>
        ///  公开访问
        /// </summary>
        [Description("公开的")]
        Public = 1,
        /// <summary>
        /// 对管理员公开
        /// </summary>
        [Description("管理员用户可访问")]
        Admin = 2,
        /// <summary>
        /// 按授权码分配
        /// 1、授权编码可以配置动态增加
        /// 2、授权编码可以做为权限资源分配给用户
        /// 3、用户访问时通过权限资源获取角色信息
        /// </summary>
        [Description("按授权编码分配")]
        WithAccessCode = 3
    }
}
