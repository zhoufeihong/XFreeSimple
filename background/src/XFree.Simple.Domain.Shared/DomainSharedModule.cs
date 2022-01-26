using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using XFree.Simple.Domain.Shared.Common.Localization;
using XFree.Simple.Domain.Shared.SystemManagement;
using XFree.Simple.Domain.Shared.SystemManagement.Domain;

namespace XFree.Simple.Domain.Shared.Common
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(
        typeof(CommonDomainSharedModule))]
    public class DomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DomainSharedModule>("XFree.Simple.Domain.Shared");
            });
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Add<SystemManagementResource>(Const.LanguageCultureName.ZH_CN)
                .AddVirtualJson("/SystemManagement/Localization");
            });
        }
    }
}
