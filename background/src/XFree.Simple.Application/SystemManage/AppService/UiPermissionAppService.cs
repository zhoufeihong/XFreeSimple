using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;
using XFree.Simple.Application.Contracts.SystemManagement;
using XFree.Simple.Application.Contracts.SystemManagement.AppService;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Permission;
using XFree.Simple.Application.Contracts.SystemManagement.Permission;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.SystemManagement.Permission;

namespace XFree.Simple.Application.SystemManage.AppService
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(SystemManagementPermissions.Base.WithToken)]
    public class UiPermissionAppService : ApplicationService, IUiPermissionAppService
    {

        private readonly IRepository<UiPermission, string> _uiPermissionRepository;
        private readonly IRepository<BackgroundApi, string> _backgroundApiRepository;
        private readonly IRepository<UiWithApi, string> _uiWithApiRepository;
        private readonly IPermissionDefinitionManager _permissionDefinitionManager;
        private readonly IMemoryCache _memoryCache;

        private readonly UiPermissionManager _uiPermissionManager;

        public UiPermissionAppService(IRepository<UiPermission, string> uiPermissionRepository,
            UiPermissionManager uiPermissionManager,
            IRepository<BackgroundApi, string> backgroundApiRepository,
            IPermissionDefinitionManager permissionDefinitionManager,
            IRepository<UiWithApi, string> uiWithApiRepository,
            IMemoryCache memoryCache)
        {
            _uiPermissionRepository = uiPermissionRepository;
            _uiPermissionManager = uiPermissionManager;
            _backgroundApiRepository = backgroundApiRepository;
            _permissionDefinitionManager = permissionDefinitionManager;
            _uiWithApiRepository = uiWithApiRepository;
            _memoryCache = memoryCache;
        }

        [Authorize(SystemManagementPermissions.UiPermissions.Create)]
        public async Task<WebApiResult<UiPermissionDto>> CreateAsync(CreateUiPermissionDto input)
        {
            if (string.IsNullOrEmpty(input.ParentId))
            {
                input.UiMenuType = 0;
            }
            if (input.UiMenuType == 0)
            {
                input.ParentId = null;
            }
            var newEntity = new UiPermission(GuidGenerator.Create().ToString());
            ObjectMapper.Map(input, newEntity);
            newEntity = await _uiPermissionRepository.InsertAsync(newEntity);
            if (newEntity.UiMenuType != 0)
            {
                await _uiPermissionManager.SetParentIsLeaf(newEntity.ParentId);
            }
            return WebApiResult<UiPermissionDto>.SuccessResult(ObjectMapper.Map<UiPermission, UiPermissionDto>(newEntity));
        }

        [Authorize(SystemManagementPermissions.UiPermissions.Delete)]
        public async Task<WebApiResult> DeleteAsync(string id)
        {
            var entity = await _uiPermissionRepository.GetAsync(id);
            await _uiPermissionRepository.DeleteAsync(entity);
            await _uiPermissionManager.SetParentIsLeaf(id, entity.ParentId);
            return WebApiResult.SuccessResult();
        }

        public async Task<WebApiResult<UiPermissionDto>> GetAsync(string id)
        {
            return WebApiResult<UiPermissionDto>.SuccessResult(ObjectMapper.Map<UiPermission, UiPermissionDto>(await _uiPermissionRepository.GetAsync(id
                )));
        }

        public async Task<WebApiResult<ListResultDto<UiPermissionDto>>> QueryAsync(string parentId)
        {
            var tempQuery = _uiPermissionRepository
                .WhereIf(string.IsNullOrEmpty(parentId), w => w.UiMenuType == 0)
                .WhereIf(!string.IsNullOrEmpty(parentId), w => w.ParentId == parentId)
                .OrderBy(o => o.SortOrder);
            var dtos = ObjectMapper.Map<List<UiPermission>, List<UiPermissionDto>>(await tempQuery.ToListAsync());
            return WebApiResult<ListResultDto<UiPermissionDto>>.SuccessResult(new ListResultDto<UiPermissionDto>(dtos));
        }

        public async Task<WebApiResult<ListResultDto<UiPermissionDto>>> GetListAsync()
        {
            var dtos = ObjectMapper.Map<List<UiPermission>, List<UiPermissionDto>>(await _uiPermissionRepository.GetListAsync());
            return WebApiResult<ListResultDto<UiPermissionDto>>.SuccessResult(new ListResultDto<UiPermissionDto>(dtos));
        }

        [Authorize(SystemManagementPermissions.UiPermissions.Update)]
        public async Task<WebApiResult<UiPermissionDto>> UpdateAsync(string id, UpdateUiPermissionDto input)
        {
            if (string.IsNullOrEmpty(input.ParentId))
            {
                input.UiMenuType = 0;
            }
            if (input.UiMenuType == 0)
            {
                input.ParentId = string.Empty;
            }
            var updateEntity = await _uiPermissionRepository.GetAsync(id);
            var oldParentId = updateEntity.ParentId;
            ObjectMapper.Map(input, updateEntity);
            await _uiPermissionRepository.UpdateAsync(updateEntity);
            if (input.ParentId != oldParentId)
            {
                await _uiPermissionManager.SetParentIsLeaf(id, oldParentId);
                await _uiPermissionManager.SetParentIsLeaf(input.ParentId);
            }
            return WebApiResult<UiPermissionDto>.SuccessResult(ObjectMapper.Map<UiPermission, UiPermissionDto>(updateEntity));
        }

        [Authorize(SystemManagementPermissions.UiPermissions.RefreshBackgroundApi)]
        public async Task<WebApiResult> RefreshBackgroundApiAsync()
        {
            var codes = new HashSet<string>();
            var multiTenancySide = CurrentTenant.GetMultiTenancySide();
            var groupPermissions = _permissionDefinitionManager.GetGroups().Where(w => (multiTenancySide & w.MultiTenancySide) == multiTenancySide).ToList();
            var backgroundApis = new List<BackgroundApi>();
            void addPermission(string parentPermissionCode, int sortOrder, PermissionDefinition permissionDefinition)
            {
                if (!codes.Contains(permissionDefinition.Name))
                {
                    codes.Add(permissionDefinition.Name);
                    var (name, enName) = GetNameAndEnName(permissionDefinition.DisplayName, StringLocalizerFactory);
                    backgroundApis.Add(new BackgroundApi(GuidGenerator.Create().ToString())
                    {
                        ParentPermissionCode = parentPermissionCode,
                        PermissionCode = permissionDefinition.Name,
                        Name = name,
                        EnName = enName,
                        PrimaryNode = false,
                        SortOrder = sortOrder,
                        MultiTenancySides = permissionDefinition.MultiTenancySide
                    });
                    if (permissionDefinition.Children != null && permissionDefinition.Children.Count > 0)
                    {
                        var index = 1;
                        foreach (var children in permissionDefinition.Children)
                        {
                            addPermission(permissionDefinition.Name, index, children);
                            index++;
                        }
                    }
                }
            }
            if (groupPermissions.Count > 0)
            {
                var groupPermissionIndex = 1;
                foreach (var groupPermission in groupPermissions)
                {
                    var (name, enName) = GetNameAndEnName(groupPermission.DisplayName, StringLocalizerFactory);
                    backgroundApis.Add(new BackgroundApi(GuidGenerator.Create().ToString())
                    {
                        ParentPermissionCode = string.Empty,
                        PermissionCode = groupPermission.Name,
                        Name = name,
                        EnName = enName,
                        PrimaryNode = true,
                        SortOrder = groupPermissionIndex,
                        MultiTenancySides = groupPermission.MultiTenancySide
                    });
                    groupPermissionIndex++;
                    if (groupPermission.Permissions != null && groupPermission.Permissions.Count > 0)
                    {
                        var permissionIndex = 1;
                        foreach (var children in groupPermission.Permissions)
                        {
                            addPermission(groupPermission.Name, permissionIndex, children);
                            permissionIndex++;
                        }
                    }
                }
            }
            await _backgroundApiRepository.DeleteAsync(d => d.SortOrder >= -1, autoSave: true);
            await _backgroundApiRepository.InsertManyAsync(backgroundApis);
            return WebApiResult.SuccessResult();
        }

        private static (string, string) GetNameAndEnName(ILocalizableString localizableString, IStringLocalizerFactory stringLocalizerFactory)
        {
            var name = string.Empty;
            var enName = string.Empty;
            using (CultureHelper.Use(CultureInfo.GetCultureInfo(Const.LanguageCultureName.ZH_CN)))
            {
                name = localizableString.Localize(stringLocalizerFactory);
            }
            using (CultureHelper.Use(CultureInfo.GetCultureInfo(Const.LanguageCultureName.EN)))
            {
                enName = localizableString.Localize(stringLocalizerFactory);
            }
            return (name, enName);
        }

        public async Task<WebApiResult<List<BackgroundApiDto>>> GetAllBackgroundApiAsync()
        {
            var dtos = ObjectMapper.Map<List<BackgroundApi>, List<BackgroundApiDto>>(await _backgroundApiRepository.GetListAsync());
            return WebApiResult<List<BackgroundApiDto>>.SuccessResult(dtos);
        }

        [Authorize(SystemManagementPermissions.UiPermissions.Update)]
        public async Task<WebApiResult> BindBackgroundApi(string id, string[] permissionCodes)
        {
            await _uiWithApiRepository.DeleteAsync(d => d.UiPermissionId == id);
            if (permissionCodes != null)
            {
                var permissions = permissionCodes.Distinct().Select(s => new UiWithApi(GuidGenerator.Create().ToString())
                {
                    PermissionCode = s,
                    UiPermissionId = id
                });
                await _uiWithApiRepository.InsertManyAsync(permissions);
            }
            return WebApiResult.SuccessResult();
        }

        public async Task<WebApiResult<List<string>>> QueryPermissionCodes(string id)
        {
            var permissionCodes = await _uiWithApiRepository.Where(w => w.UiPermissionId == id).Select(s => s.PermissionCode).ToListAsync();
            return WebApiResult<List<string>>.SuccessResult(permissionCodes);
        }

        [Authorize(SystemManagementPermissions.UiPermissions.RefreshBackgroundApi)]
        public WebApiResult RemoveAllMemoryCache()
        {
            (_memoryCache as MemoryCache)?.Compact(1);
            return WebApiResult.SuccessResult();
        }
    }
}
