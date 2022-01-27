using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;
using XFree.Simple.Domain.Shared.SystemManagement.Domain;

namespace XFree.Simple.Application.Contracts.SystemManagement.Permission
{
    /// <summary>
    ///  权限资源定义
    /// </summary>
    public class SystemManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void Define(IPermissionDefinitionContext context)
        {
            DefinePlatform(context);
            DefineSystemManagement(context);
        }

        /// <summary>
        ///  定义平台接口权限
        /// </summary>
        /// <param name="context"></param>
        private static void DefinePlatform(IPermissionDefinitionContext context)
        {
            // 平台
            var platformGroup = context.AddGroup(PlatformPermissions.GroupName, L("Permission:Platform"), MultiTenancySides.Host);
            // 基础权限
            var platformBases = platformGroup.AddPermission(PlatformPermissions.Base.Default, L("Permission:Base"), MultiTenancySides.Host);
            platformBases.AddChild(PlatformPermissions.Base.WithToken, L("Permission:WithToken"), MultiTenancySides.Host);
            // 租户
            var tenants = platformGroup.AddPermission(PlatformPermissions.Tenants.Default, L("Permission:Tenant"), MultiTenancySides.Host);
            tenants.AddChild(PlatformPermissions.Tenants.Create, L("Permission:Create"), MultiTenancySides.Host);
            tenants.AddChild(PlatformPermissions.Tenants.Update, L("Permission:Update"), MultiTenancySides.Host);
            tenants.AddChild(PlatformPermissions.Tenants.Delete, L("Permission:Delete"), MultiTenancySides.Host);
            // 数据库连接信息
            var databaseConnections = platformGroup.AddPermission(PlatformPermissions.DatabaseConnections.Default, L("Permission:DatabaseConnection"), MultiTenancySides.Host);
            databaseConnections.AddChild(PlatformPermissions.DatabaseConnections.Create, L("Permission:Create"), MultiTenancySides.Host);
            databaseConnections.AddChild(PlatformPermissions.DatabaseConnections.Update, L("Permission:Update"), MultiTenancySides.Host);
            databaseConnections.AddChild(PlatformPermissions.DatabaseConnections.Delete, L("Permission:Delete"), MultiTenancySides.Host);
        }

        /// <summary>
        /// 定义系统管理模块接口权限
        /// </summary>
        /// <param name="context"></param>
        private static void DefineSystemManagement(IPermissionDefinitionContext context)
        {
            // 系统管理
            var systemManagementGroup = context.AddGroup(SystemManagementPermissions.GroupName, L("Permission:SystemManagement"));
            // 基础权限
            var bases = systemManagementGroup.AddPermission(SystemManagementPermissions.Base.Default, L("Permission:Base"));
            bases.AddChild(SystemManagementPermissions.Base.WithToken, L("Permission:WithToken"));
            bases.AddChild(SystemManagementPermissions.Base.ChangePassword, L("Permission:ChangePassword"));

            // 用户
            var users = systemManagementGroup.AddPermission(SystemManagementPermissions.Users.Default, L("Permission:Users"));
            users.AddChild(SystemManagementPermissions.Users.Create, L("Permission:Create"));
            users.AddChild(SystemManagementPermissions.Users.Update, L("Permission:Update"));
            users.AddChild(SystemManagementPermissions.Users.Delete, L("Permission:Delete"));
            users.AddChild(SystemManagementPermissions.Users.Export, L("Permission:Export"));
            users.AddChild(SystemManagementPermissions.Users.Import, L("Permission:Import"));
            users.AddChild(SystemManagementPermissions.Users.ResetPassword, L("Permission:ResetPassword"));
            // 角色
            var roles = systemManagementGroup.AddPermission(SystemManagementPermissions.Roles.Default, L("Permission:Roles"));
            roles.AddChild(SystemManagementPermissions.Roles.Create, L("Permission:Create"));
            roles.AddChild(SystemManagementPermissions.Roles.Update, L("Permission:Update"));
            roles.AddChild(SystemManagementPermissions.Roles.Delete, L("Permission:Delete"));
            // 部门
            var departs = systemManagementGroup.AddPermission(SystemManagementPermissions.Departs.Default, L("Permission:Departs"));
            departs.AddChild(SystemManagementPermissions.Departs.Create, L("Permission:Create"));
            departs.AddChild(SystemManagementPermissions.Departs.Update, L("Permission:Update"));
            departs.AddChild(SystemManagementPermissions.Departs.Delete, L("Permission:Delete"));
            departs.AddChild(SystemManagementPermissions.Departs.QueryUser, L("Permission:QueryUser"));
            // 职务
            var posts = systemManagementGroup.AddPermission(SystemManagementPermissions.Posts.Default, L("Permission:Posts"));
            posts.AddChild(SystemManagementPermissions.Posts.Create, L("Permission:Create"));
            posts.AddChild(SystemManagementPermissions.Posts.Update, L("Permission:Update"));
            posts.AddChild(SystemManagementPermissions.Posts.Delete, L("Permission:Delete"));
            // 菜单/权限
            var uiPermissions = systemManagementGroup.AddPermission(SystemManagementPermissions.UiPermissions.Default, L("Permission:UiPermissions"));
            uiPermissions.AddChild(SystemManagementPermissions.UiPermissions.Create, L("Permission:Create"));
            uiPermissions.AddChild(SystemManagementPermissions.UiPermissions.Update, L("Permission:Update"));
            uiPermissions.AddChild(SystemManagementPermissions.UiPermissions.Delete, L("Permission:Delete"));
            uiPermissions.AddChild(SystemManagementPermissions.UiPermissions.RefreshBackgroundApi, L("Permission:RefreshBackgroundApi"));
            // 字典
            var dicts = systemManagementGroup.AddPermission(SystemManagementPermissions.Dicts.Default, L("Permission:Dicts"));
            dicts.AddChild(SystemManagementPermissions.Dicts.Create, L("Permission:Create"));
            dicts.AddChild(SystemManagementPermissions.Dicts.Update, L("Permission:Update"));
            dicts.AddChild(SystemManagementPermissions.Dicts.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<SystemManagementResource>(name);
        }

    }
}
