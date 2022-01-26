using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using XFree.Simple.Domain;
using XFree.Simple.Domain.SystemManagement;

namespace XFree.Simple.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(
       typeof(DomainModule),
       typeof(AbpEntityFrameworkCoreModule)
   )]
    public class ApplicationEntityFrameworkCoreModule : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<ApplicationDbContext>(options =>
            {
                options.AddDefaultRepositories(true);
            });
        }
    }
}
