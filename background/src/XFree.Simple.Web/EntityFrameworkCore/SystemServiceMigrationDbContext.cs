using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using XFree.Simple.EntityFrameworkCore.SystemManagement.EntityFrameworkCore;

namespace XFree.SimpleService.Host.EntityFrameworkCore
{
    /// <summary>
    /// 数据库迁移更新DcContext定义
    /// </summary>
    public class SystemServiceMigrationDbContext : AbpDbContext<SystemServiceMigrationDbContext>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public SystemServiceMigrationDbContext(
            DbContextOptions<SystemServiceMigrationDbContext> options
            ) : base(options)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureSystemManagementBase();
            modelBuilder.ConfigureSystemManagementOrganization();
            modelBuilder.ConfigureSystemManagementPermission();
        }
    }
}
