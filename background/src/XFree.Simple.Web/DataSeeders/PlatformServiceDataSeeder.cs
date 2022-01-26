using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Uow;
using XFree.Simple.Application.Contracts.SystemManage;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization;
using XFree.Simple.Domain.SystemManagement.Organization;
using XFree.Simple.Domain.SystemManagement.Permission;
using XFree.Simple.EntityFrameworkCore;

namespace XFree.SimpleService.Host.DataSeeders
{
    /// <summary>
    /// 
    /// </summary>
    public class PlatformServiceDataSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<User, string> _userRepository;

        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public PlatformServiceDataSeeder(
            IRepository<User, string> userRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _userRepository = userRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task SeedAsync(DataSeedContext context)
        {
            // 商户初始化直接返回
            if (context.TenantId != null)
            {
                return;
            }
            await DbSchemaMigrator();
            await AddPlatformAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task DbSchemaMigrator()
        {
            using var unitOfWork = _unitOfWorkManager.Begin(true);
            var dbContextProvider = unitOfWork.ServiceProvider.GetRequiredService<IDbContextProvider<ApplicationDbContext>>();
            using var dbConnection = (await dbContextProvider.GetDbContextAsync()).Database.GetDbConnection();
            dbConnection.Open();
            var sqlCommand = dbConnection.CreateCommand();
            // 创建表
            sqlCommand.CommandText = $"SELECT count(*) from information_schema.TABLES WHERE lower(TABLE_SCHEMA)=lower('{dbConnection.Database}')";
            var tableCount = sqlCommand.ExecuteScalar();
            // 没有表信息则进行初始化
            if (int.Parse(tableCount.ToString()) > 0)
            {
                // TODO读取迁移历史
                // 写入迁移记录
                return;
            }
            var ddlFiles = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataSeeders", "DdlScript"), "*.SqlScript");
            foreach (var file in ddlFiles)
            {
                sqlCommand.CommandText = File.ReadAllText(file);
                await sqlCommand.ExecuteNonQueryAsync();
                // TODO写入迁移记录
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task AddPlatformAsync()
        {
            if (await _userRepository.GetCountAsync() > 0)
            {
                return;
            }
            using var unitOfWork = _unitOfWorkManager.Begin(true);
            var dbContextProvider = unitOfWork.ServiceProvider.GetRequiredService<IDbContextProvider<ApplicationDbContext>>();
            using var dbConnection = (await dbContextProvider.GetDbContextAsync()).Database.GetDbConnection();
            dbConnection.Open();
            var sqlCommand = dbConnection.CreateCommand();
            var dmlFiles = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataSeeders", "DmlScript"), "*.SqlScript");
            using var transaction = await dbConnection.BeginTransactionAsync();
            {
                sqlCommand.Transaction = transaction;
                foreach (var file in dmlFiles)
                {
                    sqlCommand.CommandText = File.ReadAllText(file);
                    await sqlCommand.ExecuteNonQueryAsync();
                }
                await transaction.CommitAsync();
            }
        }

    }
}
