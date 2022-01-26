using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using XFree.Simple.Application.Contracts;
using XFree.Simple.Application.SystemManage.MapProfiles;
using XFree.Simple.Domain;
using XFree.Simple.Domain.SystemManagement;

namespace XFree.Simple.Application.SystemManage
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(
     typeof(DomainModule),
     typeof(ApplicationContractsModule),
     typeof(AbpAutoMapperModule)
     )]
    public class SystemManagementApplicationModule : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                // 添加AutoMapper实体映射
                options.AddProfile<SystemManagementApplicationAutoMapperProfile>(validate: true);
            });
        }
    }
}
