using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.Shared.SystemManagement.Domain;
using XFree.Simple.Domain.SystemManagement;

namespace XFree.Simple.Domain
{
    /// <summary>
    /// 这个模块设计成直接依赖EF Core，因为只有这样才能使用EF Core的表达式方法。
    /// </summary>
    [DependsOn(
        typeof(DomainSharedModule),
        typeof(AbpEntityFrameworkCoreModule)
        )]
    public class DomainModule : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DomainModule>("XFree.Simple.Domain");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Get<SystemManagementResource>().AddVirtualJson("/SystemManagement/Localization/Domain");
            });
        }
    }
}
