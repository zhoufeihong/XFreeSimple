using System;
using System.Collections.Generic;
using System.Text;

namespace XFree.Simple.Application.Contracts.Options
{
    /// <summary>
    ///  权限认证配置项
    /// </summary>
    public class AuthenticationJwtOptions
    {

        /// <summary>
        ///  bearer认证方案
        /// </summary>
        public string BearerAuthenticationScheme { get; set; }

        /// <summary>
        /// 子系统认证方案
        /// </summary>
        public string SubSystemAuthenticationScheme { get; set; }

        /// <summary>
        ///  子系统请求头
        /// </summary>
        public string SubSystemAuthenticationHeader { get; set; }

        /// <summary>
        /// bearer认证方案的公钥
        /// </summary>
        public string BearerPublicKey { get; set; }

        /// <summary>
        /// bearer认证方案的私钥
        /// </summary>
        public string BearerPrivateKey { get; set; }

        /// <summary>
        /// 子系统认证方案的公钥
        /// </summary>
        public string SubSystemPublicKey { get; set; }

        /// <summary>
        /// 子系统认证方案的私钥
        /// </summary>
        public string SubSystemPrivateKey { get; set; }

    }
}
