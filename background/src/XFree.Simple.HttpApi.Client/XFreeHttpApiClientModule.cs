using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using XFree.Simple.Application.Contracts;
using XFree.Simple.Application.Contracts.SystemManagement;

namespace XFree.Simple.HttpApi.Client
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(
       typeof(ApplicationContractsModule),
       typeof(AbpHttpClientModule))]
    public class XFreeSimpleHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "XFree.Simple";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(ApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
