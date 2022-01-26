using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;
using XFree.Simple.Application.Contracts;
using XFree.Simple.Application.Contracts.SystemManagement;

namespace XFree.Simple.HttpApi.SystemManage
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(
      typeof(ApplicationContractsModule),
      typeof(AbpAspNetCoreMvcModule))]
    public class SystemManagementHttpApiModule : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(SystemManagementHttpApiModule).Assembly);
            });
        }
    }
}
