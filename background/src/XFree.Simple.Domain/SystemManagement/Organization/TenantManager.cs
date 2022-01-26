using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using XFree.Simple.Domain.Shared.SystemManagement;
using XFree.Simple.Domain.Shared.SystemManagement.Domain;
using XFree.Simple.Domain.Shared.SystemManagement.Enum;
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.Domain.SystemManagement.Organization
{
    /// <summary>
    /// 
    /// </summary>
    public class TenantManager : DomainService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IRepository<Tenant, string> _tenantRepository;

        /// <summary>
        /// 
        /// </summary>
        private readonly ErrorMessageService _errorMessageService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenantRepository"></param>
        /// <param name="errorMessageService"></param>
        public TenantManager(IRepository<Tenant, string> tenantRepository,
            ErrorMessageService errorMessageService)
        {
            _tenantRepository = tenantRepository;
            _errorMessageService = errorMessageService;
        }

        /// <summary>
        /// 创建租户
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        public async Task<Tenant> CreateAsync(Tenant tenant)
        {
            var code = tenant.Code;
            var name = tenant.Name;
            var existingTenant = await _tenantRepository.FirstOrDefaultAsync(p => p.Code == code || p.Name == name);
            if (existingTenant != null)
            {
                if (existingTenant.Code == code)
                    _errorMessageService.ThrowMessageParam(SystemFriendlyExceptionCode.DuplicateMerchantCode, code);
                if (existingTenant.Name == name)
                    _errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.DuplicateMerchantName);
            }
            tenant.SetInitialDataStatus();
            return await _tenantRepository.InsertAsync(tenant);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenant"></param>
        /// <param name="isStandaloneDatabase"></param>
        /// <param name="defaultConnectionStringName"></param>
        /// <param name="standaloneDatabaseConnectionString"></param>
        public void CheckTenantUpdate(Tenant tenant, bool isStandaloneDatabase, string defaultConnectionStringName, string standaloneDatabaseConnectionString)
        {
            if (tenant.InitialDataStatus > ProcessingStatus.Ready)
            {
                if (isStandaloneDatabase != tenant.IsStandaloneDatabase
                    || defaultConnectionStringName != tenant.DefaultConnectionStringName
                    || standaloneDatabaseConnectionString != tenant.StandaloneDatabaseConnectionString)
                {
                    _errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.MerchantError005);
                }
            }
        }

        /// <summary>
        /// 更新租户信息
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        public async Task<Tenant> UpdateAsync(Tenant tenant)
        {
            var code = tenant.Code;
            var name = tenant.Name;
            var existingTenant = await _tenantRepository.FirstOrDefaultAsync(p => (p.Code == code || p.Name == name) && p.Id != tenant.Id);
            if (existingTenant != null)
            {
                if (existingTenant.Code == code)
                    _errorMessageService.ThrowMessageParam(SystemFriendlyExceptionCode.DuplicateMerchantCode, code);
                if (existingTenant.Name == name)
                    _errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.DuplicateMerchantName);
            }
            tenant.SetInitialDataStatus();
            return await _tenantRepository.UpdateAsync(tenant);
        }

    }
}
