using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.Shared.SystemManagement.Enum;
using XFree.Simple.Domain.SystemManagement;
using XFree.Simple.Domain.SystemManagement.Common;
using XFree.Simple.Domain.SystemManagement.Organization;
using XFree.Simple.Domain.SystemManagement.Permission;
using Microsoft.Extensions.DependencyInjection;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization;
using XFree.Simple.Application.Contracts.Common;
using Volo.Abp.Users;
using Volo.Abp.EntityFrameworkCore;
using XFree.Simple.EntityFrameworkCore;
using System.IO;
using Volo.Abp.Localization;
using System.Globalization;
using Microsoft.Extensions.Localization;
using XFree.Simple.Domain.Shared.SystemManagement.Domain;

namespace XFree.SimpleService.Host.DataSeeders
{
    /// <summary>
    ///  初始化商户信息
    /// </summary>
    public class TenantServiceDataSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Tenant, string> _tenantRepository;
        private readonly IRepository<UiPermission, string> _uiPermissionRepository;
        private readonly IRepository<BackgroundApi, string> _backgroundApiRepository;
        private readonly IRepository<UiWithApi, string> _uiWithApiRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ICodeGenerator _codeGenerator;
        private readonly IStringLocalizerFactory _stringLocalizerFactory;

        public TenantServiceDataSeeder(IRepository<Tenant, string> tenantRepository,
            IRepository<UiPermission, string> uiPermissionRepository,
            IRepository<BackgroundApi, string> backgroundApiRepository,
            IRepository<UiWithApi, string> uiWithApiRepository,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            IUnitOfWorkManager unitOfWorkManager,
            ICodeGenerator codeGenerator,
            IStringLocalizerFactory stringLocalizerFactory)
        {
            _tenantRepository = tenantRepository;
            _uiPermissionRepository = uiPermissionRepository;
            _backgroundApiRepository = backgroundApiRepository;
            _uiWithApiRepository = uiWithApiRepository;
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            _unitOfWorkManager = unitOfWorkManager;
            _codeGenerator = codeGenerator;
            _stringLocalizerFactory = stringLocalizerFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task SeedAsync(DataSeedContext context)
        {
            if (context.TenantId == null)
            {
                return;
            }
            await DbSchemaMigrator();
            await AddTenantsAsync(context);
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
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task AddTenantsAsync(DataSeedContext context)
        {
            var tenant = await _tenantRepository.GetAsync(context.TenantId.Value.ToString());
            using (CultureHelper.Use(CultureInfo.GetCultureInfo(tenant.Language ?? Const.LanguageCultureName.ZH_CN)))
            {
                List<UiPermission> uiPermissions;
                List<UiWithApi> uiWithApis;
                List<BackgroundApi> backgroundApis;
                // 切换到Host
                using (_currentTenant.Change(null))
                {
                    uiPermissions = await _uiPermissionRepository.AsNoTracking().ToListAsync();
                    uiWithApis = await _uiWithApiRepository.AsNoTracking().ToListAsync();
                    backgroundApis = await _backgroundApiRepository.AsNoTracking().ToListAsync();
                }
                using (_currentTenant.Change(context.TenantId))
                {
                    using var unitOfWork = _unitOfWorkManager.Begin(true);
                    var currentTenant = unitOfWork.ServiceProvider.GetService<ICurrentTenant>();
                    await AddUser(tenant, unitOfWork);
                    await AddDict(unitOfWork);
                    await AddPostDepart(unitOfWork);
                    await AddUiPermission(unitOfWork, uiPermissions, backgroundApis, uiWithApis);
                    await unitOfWork.CompleteAsync();
                }
                tenant.InitialDataStatus = ProcessingStatus.Successful;
                await _tenantRepository.UpdateAsync(tenant);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenant"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        private async Task AddUser(Tenant tenant, IUnitOfWork unitOfWork)
        {
            var userRepository = unitOfWork.ServiceProvider.GetService<IRepository<User, string>>();
            if (await userRepository.GetCountAsync() > 0)
            {
                return;
            }
            var input = new CreateUserDto
            {
                EmployeeIDNumber = tenant.Code,
                LoginName = tenant.Code,
                Nickname = tenant.Code,
                Memo = L("TenantServiceDataSeeder:User:Memo").Localize(_stringLocalizerFactory),
                Password = "123456",
                Sex = SexType.Unknown,
                UserType = UserType.Tenant,
                SupperUser = true
            };
            var newUser = new User(_guidGenerator.Create().ToString())
            {
                LoginName = input.LoginName,
                Birthday = input.Birthday,
                DepartId = input.DepartId,
                Email = input.Email,
                EmployeeIDNumber = input.EmployeeIDNumber,
                Memo = input.Memo,
                Nickname = input.Nickname,
                Phone = input.Phone,
                PostId = input.PostId,
                Sex = input.Sex,
                UserType = input.UserType,
                SupperUser = true
            };
            newUser.SetPassword(input.Password);
            await userRepository.InsertAsync(newUser);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        private async Task AddDict(IUnitOfWork unitOfWork)
        {
            var dictRepository = unitOfWork.ServiceProvider.GetService<IRepository<Dict, string>>();
            var dictManager = unitOfWork.ServiceProvider.GetService<DictManager>();
            if (await dictRepository.GetCountAsync() > 0)
            {
                return;
            }

            var dict = await dictRepository.InsertAsync(
                          new Dict(_guidGenerator.Create().ToString())
                          {
                              // 内部_系统管理模块_部门类型
                              DictCode = "Inside_SystemMangement_OrgType",
                              DictName = "部门类型",
                              DictEnName = "Org type",
                              Description = "部门类型"
                          }
                       );
            var dictItem1 = new DictItem(_guidGenerator.Create().ToString())
            {
                DictId = dict.Id,
                Enabled = true,
                SortOrder = 1,
                ItemValue = "Company",
                ItemText = "公司",
                ItemEnText = "Company"
            };
            var dictItem2 = new DictItem(_guidGenerator.Create().ToString())
            {
                DictId = dict.Id,
                Enabled = true,
                SortOrder = 2,
                ItemValue = "Structure",
                ItemText = "部门",
                ItemEnText = "Department"
            };
            await dictManager.AddDictItem(dictItem1);
            await dictManager.AddDictItem(dictItem2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        private async Task AddPostDepart(IUnitOfWork unitOfWork)
        {
            var postRepository = unitOfWork.ServiceProvider.GetService<IRepository<Post, string>>();
            var departRepository = unitOfWork.ServiceProvider.GetService<IRepository<Depart, string>>();
            var post = new Post(_guidGenerator.Create().ToString())
            {
                Code = await _codeGenerator.Create("P"),
                Name = L("TenantServiceDataSeeder:Post:Name").Localize(_stringLocalizerFactory),
                Memo = L("TenantServiceDataSeeder:Post:Memo").Localize(_stringLocalizerFactory),
                SortOrder = 1
            };
            await postRepository.InsertAsync(post);
            var parentDepart = new Depart(_guidGenerator.Create().ToString())
            {
                OrgCode = await _codeGenerator.Create("D"),
                OrgName = L("TenantServiceDataSeeder:Depart:OrgName1").Localize(_stringLocalizerFactory),
                OrgLevelType = OrgLevelType.PrimaryDepartment,
                OrgCategory = OrgCategory.Structure,
                OrgType = "Company",
                SortOrder = 1
            };
            var depart = new Depart(_guidGenerator.Create().ToString())
            {
                ParentId = parentDepart.Id,
                OrgCode = await _codeGenerator.Create("D"),
                OrgName = L("TenantServiceDataSeeder:Depart:OrgName2").Localize(_stringLocalizerFactory),
                OrgLevelType = OrgLevelType.SubsidiaryDepartment,
                OrgCategory = OrgCategory.Structure,
                OrgType = "Structure",
                SortOrder = 1
            };
            await departRepository.InsertManyAsync(new List<Depart> { parentDepart, depart });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="uiPermissions"></param>
        /// <param name="backgroundApis"></param>
        /// <param name="uiWithApis"></param>
        /// <returns></returns>
        private async Task AddUiPermission(IUnitOfWork unitOfWork, List<UiPermission> uiPermissions, List<BackgroundApi> backgroundApis, List<UiWithApi> uiWithApis)
        {
            var uiPermissionRepository = unitOfWork.ServiceProvider.GetService<IRepository<UiPermission, string>>();
            var backgroundApiRepository = unitOfWork.ServiceProvider.GetService<IRepository<BackgroundApi, string>>();
            var uiWithApiRepository = unitOfWork.ServiceProvider.GetService<IRepository<UiWithApi, string>>();
            Dictionary<string, string> idMapper = new();
            var newUiPermissions = uiPermissions.Where(w => MultiTenancySides.Tenant == (MultiTenancySides.Tenant & w.MultiTenancySides)).Select(s =>
            {
                var newEntity = new UiPermission(_guidGenerator.Create().ToString())
                {
                    Component = s.Component,
                    ComponentName = s.ComponentName,
                    Description = s.Description,
                    EnName = s.EnName,
                    Enabled = s.Enabled,
                    Hidden = s.Hidden,
                    Icon = s.Icon,
                    IsRoute = s.IsRoute,
                    IsLeaf = s.IsLeaf,
                    KeepAlive = s.KeepAlive,
                    Name = s.Name,
                    OpenMode = s.OpenMode,
                    ParentId = s.ParentId,
                    Perms = s.Perms,
                    PermsType = s.PermsType,
                    Redirect = s.Redirect,
                    SortOrder = s.SortOrder,
                    UiMenuType = s.UiMenuType,
                    Url = s.Url
                };
                idMapper.Add(s.Id, newEntity.Id);
                return newEntity;
            }).ToList();
            newUiPermissions.ForEach(newEntity =>
            {
                if (!string.IsNullOrEmpty(newEntity.ParentId) && idMapper.ContainsKey(newEntity.ParentId))
                {
                    newEntity.ParentId = idMapper[newEntity.ParentId];
                }
            });
            var newBackgroundApis = backgroundApis.Where(w => MultiTenancySides.Tenant == (MultiTenancySides.Tenant & w.MultiTenancySides)).Select(s =>
            {
                var newEntity = new BackgroundApi(_guidGenerator.Create().ToString())
                {
                    EnName = s.EnName,
                    Method = s.Method,
                    Module = s.Module,
                    Name = s.Name,
                    ParentPermissionCode = s.ParentPermissionCode,
                    Path = s.Path,
                    PermissionCode = s.PermissionCode,
                    PrimaryNode = s.PrimaryNode,
                    SortOrder = s.SortOrder
                };
                return newEntity;
            });
            var newUiWithApis = uiWithApis.Select(s =>
            {
                var newEntity = new UiWithApi(_guidGenerator.Create().ToString())
                {
                    PermissionCode = s.PermissionCode,
                    // 避免脏数据报错
                    UiPermissionId = idMapper.ContainsKey(s.UiPermissionId) ? idMapper[s.UiPermissionId] : s.UiPermissionId
                };
                return newEntity;
            });
            await uiPermissionRepository.InsertManyAsync(newUiPermissions);
            await backgroundApiRepository.InsertManyAsync(newBackgroundApis);
            await uiWithApiRepository.InsertManyAsync(newUiWithApis);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<SystemManagementResource>(name);
        }
    }
}
