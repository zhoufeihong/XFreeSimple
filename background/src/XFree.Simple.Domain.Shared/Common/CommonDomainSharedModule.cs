using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using XFree.Simple.Domain.Shared.Common.Localization;

namespace XFree.Simple.Domain.Shared.Common
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(
        typeof(AbpVirtualFileSystemModule),
        typeof(AbpLocalizationModule))]
    public class CommonDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<CommonDomainSharedModule>("Common.Domain.Shared");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<DomainSharedCommonResource>(Const.LanguageCultureName.ZH_CN)
                    .AddVirtualJson("/Common/Localization");
            });
        }
    }
}
