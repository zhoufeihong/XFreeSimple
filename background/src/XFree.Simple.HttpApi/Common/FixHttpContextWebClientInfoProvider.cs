using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.WebClientInfo;
using Volo.Abp.DependencyInjection;

namespace XFree.Simple.HttpApi.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class FixHttpContextWebClientInfoProvider : IWebClientInfoProvider, ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        protected ILogger<HttpContextWebClientInfoProvider> Logger { get; }
        /// <summary>
        /// 
        /// </summary>
        protected IHttpContextAccessor HttpContextAccessor { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="httpContextAccessor"></param>
        public FixHttpContextWebClientInfoProvider(
            ILogger<HttpContextWebClientInfoProvider> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            Logger = logger;
            HttpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 
        /// </summary>
        public string BrowserInfo => GetBrowserInfo();

        /// <summary>
        /// 
        /// </summary>
        public string ClientIpAddress => GetClientIpAddress();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual string GetBrowserInfo()
        {
            return HttpContextAccessor.HttpContext?.Request?.Headers?["User-Agent"];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual string GetClientIpAddress()
        {
            try
            {
                var httpContext = HttpContextAccessor.HttpContext;
                var headers = httpContext?.Request?.Headers;
                if (headers != null && headers.ContainsKey("X-Forwarded-For"))
                {
                    httpContext.Connection.RemoteIpAddress = IPAddress.Parse(headers["X-Forwarded-For"].FirstOrDefault().ToString());
                }
                return httpContext?.Connection?.RemoteIpAddress?.ToString();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogLevel.Warning);
                return null;
            }
        }

    }
}
