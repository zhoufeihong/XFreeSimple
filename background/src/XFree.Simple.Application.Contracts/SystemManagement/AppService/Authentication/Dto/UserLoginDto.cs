using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace XFree.Simple.Application.Contracts.SystemManagement.AppService.Authentication.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// 登录用户
        /// </summary>
        [Required]
        public string LoginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        /// <summary>
        /// 租户编码
        /// </summary>
        public string TenantCode { get; set; }

        /// <summary>
        /// 是否返回UserToken
        /// </summary>
        public bool ReturnUserToken { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserLoginVo
    {
        /// <summary>
        ///  用户Token
        /// </summary>
        public string UserToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TenantLanguage { get; set; }
    }
}
