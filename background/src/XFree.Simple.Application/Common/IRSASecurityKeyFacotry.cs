using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace XFree.Simple.Application.Common
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRSASecurityKeyFacotry : ISingletonDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rsaPrivateKey"></param>
        /// <returns></returns>
        RsaSecurityKey Get(string rsaPrivateKey);
    }
}
