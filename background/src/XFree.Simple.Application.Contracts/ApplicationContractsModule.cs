using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.Shared.SystemManagement;
using XFree.Simple.Domain.Shared.SystemManagement.Domain;

namespace XFree.Simple.Application.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(
        typeof(DomainSharedModule),
        typeof(AbpDddApplicationModule)
        )]
    public class ApplicationContractsModule : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<ApplicationContractsModule>("XFree.Simple.Application.Contracts");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<SystemManagementResource>()
                    .AddVirtualJson("/SystemManagement/Localization/ApplicationContracts")
                    .AddVirtualJson("/SystemManagement/Localization/ApplicationContracts/Excel");
            });
        }
    }
}
