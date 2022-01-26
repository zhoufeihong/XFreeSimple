using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Reflection;

namespace XFree.Simple.Application.Contracts.SystemManagement.Permission
{
    /// <summary>
    ///  权限资源编码定义
    /// </summary>
    public class SystemManagementPermissions
    {
        /// <summary>
        /// 系统管理
        /// </summary>
        public const string GroupName = "System";

        /// <summary>
        /// 基础权限
        /// </summary>
        public static class Base 
        {
            /// <summary>
            /// 默认
            /// </summary>
            public const string Default = GroupName + ".Base";

            /// <summary>
            /// 要求带上有效Token
            /// </summary>
            public const string WithToken = GroupName + ".WithToken";

            /// <summary>
            /// 修改密码
            /// </summary>
            public const string ChangePassword = GroupName + ".ChangePassword";
        }

        /// <summary>
        /// 租户
        /// </summary>
        public static class Tenants
        {
            /// <summary>
            /// 默认
            /// </summary>
            public const string Default = GroupName + ".Tenant";
            /// <summary>
            /// 删除
            /// </summary>
            public const string Delete = Default + ".Delete";
            /// <summary>
            /// 更新
            /// </summary>
            public const string Update = Default + ".Update";
            /// <summary>
            /// 创建
            /// </summary>
            public const string Create = Default + ".Create";
        }

        /// <summary>
        /// 用户
        /// </summary>
        public static class Users
        {
            /// <summary>
            /// 默认
            /// </summary>
            public const string Default = GroupName + ".User";
            /// <summary>
            /// 删除
            /// </summary>
            public const string Delete = Default + ".Delete";
            /// <summary>
            /// 更新
            /// </summary>
            public const string Update = Default + ".Update";
            /// <summary>
            /// 创建
            /// </summary>
            public const string Create = Default + ".Create";
            /// <summary>
            /// 导出
            /// </summary>
            public const string Export = Default + ".Export";
            /// <summary>
            /// 导入
            /// </summary>
            public const string Import = Default + ".Import";

            /// <summary>
            /// 重置密码
            /// </summary>
            public const string ResetPassword = Default + ".ResetPassword";
        }

        /// <summary>
        /// 部门
        /// </summary>
        public static class Departs
        {
            /// <summary>
            /// 默认
            /// </summary>
            public const string Default = GroupName + ".Depart";
            /// <summary>
            /// 删除
            /// </summary>
            public const string Delete = Default + ".Delete";
            /// <summary>
            /// 更新
            /// </summary>
            public const string Update = Default + ".Update";
            /// <summary>
            /// 创建
            /// </summary>
            public const string Create = Default + ".Create";
            /// <summary>
            /// 查询部门用户
            /// </summary>
            public const string QueryUser = Default + ".QueryUser";
        }

        /// <summary>
        /// 字典
        /// </summary>
        public static class Dicts
        {
            /// <summary>
            /// 默认
            /// </summary>
            public const string Default = GroupName + ".Dict";
            /// <summary>
            /// 删除
            /// </summary>
            public const string Delete = Default + ".Delete";
            /// <summary>
            /// 更新
            /// </summary>
            public const string Update = Default + ".Update";
            /// <summary>
            /// 创建
            /// </summary>
            public const string Create = Default + ".Create";
        }

        /// <summary>
        /// 职务
        /// </summary>
        public static class Posts
        {
            /// <summary>
            /// 默认
            /// </summary>
            public const string Default = GroupName + ".Post";
            /// <summary>
            /// 删除
            /// </summary>
            public const string Delete = Default + ".Delete";
            /// <summary>
            /// 更新
            /// </summary>
            public const string Update = Default + ".Update";
            /// <summary>
            /// 创建
            /// </summary>
            public const string Create = Default + ".Create";
        }

        /// <summary>
        /// 角色
        /// </summary>
        public static class Roles
        {
            /// <summary>
            /// 默认
            /// </summary>
            public const string Default = GroupName + ".Role";
            /// <summary>
            /// 删除
            /// </summary>
            public const string Delete = Default + ".Delete";
            /// <summary>
            /// 更新
            /// </summary>
            public const string Update = Default + ".Update";
            /// <summary>
            /// 创建
            /// </summary>
            public const string Create = Default + ".Create";
        }

        /// <summary>
        /// Ui菜单权限
        /// </summary>
        public static class UiPermissions
        {
            /// <summary>
            /// 默认
            /// </summary>
            public const string Default = GroupName + ".UiPermission";
            /// <summary>
            /// 删除
            /// </summary>
            public const string Delete = Default + ".Delete";
            /// <summary>
            /// 更新
            /// </summary>
            public const string Update = Default + ".Update";
            /// <summary>
            /// 创建
            /// </summary>
            public const string Create = Default + ".Create";
            /// <summary>
            /// 刷新接口资源
            /// </summary>
            public const string RefreshBackgroundApi = Default + ".RefreshBackgroundApi";
        }

        /// <summary>
        /// 获取所有权限编码
        /// </summary>
        /// <returns></returns>
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(SystemManagementPermissions));
        }

    }
}
