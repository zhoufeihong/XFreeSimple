using System;
using System.Collections.Generic;
using System.Text;

namespace XFree.Simple.Application.Contracts.SystemManagement.AppService.Authentication.Dto
{
    /// <summary>
    ///  用户权限信息
    /// </summary>
    public class UserPermisionInfo
    {

        /// <summary>
        /// 用户Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public string DepartId { get; set; }

        /// <summary>
        /// 是否为管理员
        /// </summary>
        public bool SupperUser { get; set; }

        /// <summary>
        /// 角色信息
        /// </summary>
        public string[] Roles { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        ///  商户语言
        /// </summary>
        public string TenantLanguage { get; set; }
    }
}
