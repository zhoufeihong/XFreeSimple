using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace XFree.SimpleService.Host.Permission
{
    /// <summary>
    /// 
    /// </summary>
    internal static class AuthenticationExtesion
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationBuilder"></param>
        /// <param name="rsaPublicKey"></param>
        /// <param name="bearerAuthenticationScheme"></param>
        /// <param name="subsystemAuthenticationScheme"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddJwtBearerR(this AuthenticationBuilder authenticationBuilder, string rsaPublicKey, string bearerAuthenticationScheme, string subsystemAuthenticationScheme)
        {
            authenticationBuilder.AddJwtBearer(bearerAuthenticationScheme, options =>
            {
                RsaSecurityKey key = GetRsaSecurityKey(rsaPublicKey);

                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        LifetimeValidator = (before, expires, token, parameters) => expires > DateTime.UtcNow,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateActor = false,
                        ValidateLifetime = true,
                        IssuerSigningKey = key,
                        RequireExpirationTime = true,
                        ValidateIssuerSigningKey = true
                    };

                options.ForwardDefaultSelector = context =>
                {
                    var bearerAuth = context.Request.Headers["Authorization"].FirstOrDefault()?.StartsWith("Bearer ") ?? false;
                    if (bearerAuth)
                        return bearerAuthenticationScheme;
                    else
                        return subsystemAuthenticationScheme;
                };
            });
            return authenticationBuilder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationBuilder"></param>
        /// <param name="rsaPublicKey"></param>
        /// <param name="subsystemAuthenticationScheme"></param>
        /// <param name="subSystemAuthenticationHeader"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddJwtSystemR(this AuthenticationBuilder authenticationBuilder, string rsaPublicKey, string subsystemAuthenticationScheme, string subSystemAuthenticationHeader)
        {
            authenticationBuilder.AddJwtBearer(subsystemAuthenticationScheme, options =>
            {

                RsaSecurityKey key = GetRsaSecurityKey(rsaPublicKey);

                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        LifetimeValidator = (before, expires, token, parameters) => expires > DateTime.UtcNow,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateActor = false,
                        ValidateLifetime = true,
                        IssuerSigningKey = key,
                        RequireExpirationTime = true,
                        ValidateIssuerSigningKey = true
                    };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (string.IsNullOrEmpty(context.Token))
                        {
                            var accessToken = context.Request.Headers[subSystemAuthenticationHeader].ToString();
                            if (!string.IsNullOrEmpty(accessToken))
                            {
                                context.Token = accessToken["Bearer ".Length..].Trim();
                            }
                        }
                        return Task.CompletedTask;
                    }
                };

            });
            return authenticationBuilder;
        }

        /// <summary>
        ///  创建Rsa公钥
        /// </summary>
        /// <param name="publicKeyStr"></param>
        /// <returns></returns>
        private static RsaSecurityKey GetRsaSecurityKey(string publicKeyStr)
        {
            var rsa = RSA.Create();
            byte[] publicKey = Convert.FromBase64String(publicKeyStr);
            RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(publicKey);
            var pub = new RSAParameters
            {
                Modulus = publicKeyParam.Modulus.ToByteArrayUnsigned(),
                Exponent = publicKeyParam.Exponent.ToByteArrayUnsigned()
            };
            rsa.ImportParameters(pub);
            var key = new RsaSecurityKey(rsa);
            return key;
        }

    }
}
