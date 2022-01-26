using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XFree.Simple.Application.Common.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultRSASecurityKeyFacotry : IRSASecurityKeyFacotry
    {

        private readonly ConcurrentDictionary<string, RsaSecurityKey> RsaSecurityKeys = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rsaPrivateKey"></param>
        /// <returns></returns>
        public RsaSecurityKey Get(string rsaPrivateKey)
        {
            return RsaSecurityKeys.GetOrAdd(rsaPrivateKey, InitRSASecurityKey(rsaPrivateKey));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rsaPrivateKey"></param>
        /// <returns></returns>
        private static RsaSecurityKey InitRSASecurityKey(string rsaPrivateKey)
        {
            var rsa = RSA.Create();
            byte[] privateKey = Convert.FromBase64String(rsaPrivateKey);
            rsa.ImportPkcs8PrivateKey(privateKey, out _);
            return new RsaSecurityKey(rsa);
        }

    }
}
