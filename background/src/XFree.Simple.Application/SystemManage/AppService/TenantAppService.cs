using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using XFree.Simple.Application.Common;
using XFree.Simple.Application.Contracts.Common;
using XFree.Simple.Application.Contracts.SystemManagement;
using XFree.Simple.Application.Contracts.SystemManagement.AppService;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization;
using XFree.Simple.Application.Contracts.SystemManagement.Permission;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.SystemManagement;
using XFree.Simple.Domain.SystemManagement.Organization;

namespace XFree.Simple.Application.SystemManage.AppService
{
    [Authorize(PlatformPermissions.Tenants.Default)]
    public class TenantAppService : ApplicationService, ITenantAppService
    {
        private readonly TenantManager _tenantManager;
        private readonly IRepository<Tenant, string> _tenantRepository;
        private readonly ICodeGenerator _codeGenerator;
        private readonly string CodePrefix = "T";

        public TenantAppService(TenantManager tenantManager,
            IRepository<Tenant, string> tenantRepository,
            ICodeGenerator codeGenerator)
        {
            _tenantManager = tenantManager;
            _tenantRepository = tenantRepository;
            _codeGenerator = codeGenerator;
        }

        public async Task<WebApiResult<PagedEResultDto<TenantDto>>> GetListPagedAsync(TenantPagedAndSortedRequestDto input)
        {
            var resultQuery = _tenantRepository
                .WhereIf(!string.IsNullOrEmpty(input.Name), w => w.Name == input.Name)
                .WhereIf(!string.IsNullOrEmpty(input.Code), w => w.Code == input.Code)
                .OrderBy(input.Sorting ?? "Name");

            return await resultQuery.GetListPaged<Tenant, TenantDto>(ObjectMapper, input);
        }

        public async Task<WebApiResult<ListResultDto<TenantDto>>> GetListAsync()
        {
            var tenants = await _tenantRepository.GetListAsync();

            var tenantList = ObjectMapper.Map<List<Tenant>, List<TenantDto>>(tenants);

            return WebApiResult<ListResultDto<TenantDto>>.SuccessResult(new ListResultDto<TenantDto>(tenantList));
        }

        public async Task<WebApiResult<TenantDto>> GetAsync(string id)
        {
            var tenant = await _tenantRepository.GetAsync(id);

            return WebApiResult<TenantDto>.SuccessResult(ObjectMapper.Map<Tenant, TenantDto>(tenant));
        }

        [Authorize(PlatformPermissions.Tenants.Create)]
        public async Task<WebApiResult<TenantDto>> CreateAsync(CreateTenantDto input)
        {
            if (string.IsNullOrEmpty(input.Code))
            {
                input.Code = await _codeGenerator.Create(CodePrefix);
            }
            var newEntity = new Tenant(GuidGenerator.Create().ToString());
            ObjectMapper.Map(input, newEntity);
            var tenant = await _tenantManager.CreateAsync(newEntity);
            return WebApiResult<TenantDto>.SuccessResult(ObjectMapper.Map<Tenant, TenantDto>(tenant));
        }

        [Authorize(PlatformPermissions.Tenants.Update)]
        public async Task<WebApiResult<TenantDto>> UpdateAsync(string id, UpdateTenantDto input)
        {
            var updateEntity = await _tenantRepository.GetAsync(id);
            _tenantManager.CheckTenantUpdate(updateEntity, input.IsStandaloneDatabase, input.DefaultConnectionStringName, input.StandaloneDatabaseConnectionString);
            ObjectMapper.Map(input, updateEntity);
            await _tenantManager.UpdateAsync(updateEntity);
            return WebApiResult<TenantDto>.SuccessResult(ObjectMapper.Map<Tenant, TenantDto>(updateEntity));
        }

        [Authorize(PlatformPermissions.Tenants.Update)]
        public async Task<WebApiResult<TenantDto>> UpdateStatusAsync(string id, UpdateTenantStatusDto input)
        {
            var entity = await _tenantRepository.GetAsync(id);
            entity.Status = input.Status;
            return WebApiResult<TenantDto>.SuccessResult(ObjectMapper.Map<Tenant, TenantDto>(entity));
        }

        [Authorize(PlatformPermissions.Tenants.Delete)]
        public async Task<WebApiResult> DeleteAsync(string id)
        {
            await _tenantRepository.DeleteAsync(id);
            return WebApiResult.SuccessResult();
        }

        public async Task<WebApiResult<TenantDto>> GetByNameAsync(string name)
        {
            var tenant = await _tenantRepository.FirstOrDefaultAsync(f => f.Name == name);

            return WebApiResult<TenantDto>.SuccessResult(ObjectMapper.Map<Tenant, TenantDto>(tenant));
        }
    }
}
