using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Reflection;

namespace XFree.Simple.Application.Contracts.SystemManagement.Permission
{
    /// <summary>
    /// 
    /// </summary>
    public class PlatformPermissions
    {
        /// <summary>
        /// 平台管理
        /// </summary>
        public const string GroupName = "Platform";

        /// <summary>
        /// 平台基础权限
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
        }

        /// <summary>
        /// 数据库连接信息
        /// </summary>
        public static class DatabaseConnections
        {
            /// <summary>
            /// 默认
            /// </summary>
            public const string Default = GroupName + ".DatabaseConnection";
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
        /// 获取所有权限编码
        /// </summary>
        /// <returns></returns>
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(SystemManagementPermissions));
        }
    }
}
