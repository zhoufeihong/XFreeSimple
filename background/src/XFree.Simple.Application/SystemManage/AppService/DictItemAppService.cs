using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using XFree.Simple.Application.Common;
using XFree.Simple.Application.Contracts.SystemManagement;
using XFree.Simple.Application.Contracts.SystemManagement.AppService;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Common;
using XFree.Simple.Application.Contracts.SystemManagement.Permission;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.SystemManagement.Common;

namespace XFree.Simple.Application.SystemManage.AppService
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(SystemManagementPermissions.Base.WithToken)]
    public class DictItemAppService : ApplicationService, IDictItemAppService
    {
        private readonly IRepository<Dict, string> _dictRepository;
        private readonly IRepository<DictItem, string> _dictItemRepository;
        private readonly DictManager _dictManager;

        public DictItemAppService(IRepository<Dict, string> dictRepository,
            IRepository<DictItem, string> dictItemRepository,
            DictManager dictManager)
        {
            _dictRepository = dictRepository;
            _dictItemRepository = dictItemRepository;
            _dictManager = dictManager;
        }

        [Authorize(SystemManagementPermissions.Dicts.Update)]
        public async Task<WebApiResult<DictItemDto>> CreateAsync(DictItemDto input)
        {
            var dictItem = await _dictManager.AddDictItem(new DictItem(GuidGenerator.Create().ToString())
            {
                DictId = input.DictId,
                ItemValue = input.ItemValue,
                ItemText = input.ItemText,
                ItemEnText = input.ItemEnText,
                Description = input.Description,
                SortOrder = input.SortOrder
            });
            return WebApiResult<DictItemDto>.SuccessResult(ObjectMapper.Map<DictItem, DictItemDto>(dictItem));
        }

        [Authorize(SystemManagementPermissions.Dicts.Update)]
        public async Task<WebApiResult> DeleteAsync(string id)
        {
            await _dictItemRepository.DeleteAsync(id);
            return WebApiResult.SuccessResult();
        }

        public async Task<WebApiResult<DictItemDto>> GetAsync(string id)
        {
            return WebApiResult<DictItemDto>.SuccessResult(ObjectMapper.Map<DictItem, DictItemDto>(await _dictItemRepository.GetAsync(id
                )));
        }

        public async Task<WebApiResult<ListResultDto<DictItemDto>>> GetListAsync(string dictCode)
        {
            var tempQuery = _dictItemRepository.Join(_dictRepository.Where(w => w.DictCode == dictCode),
                di => di.DictId, d => d.Id, (di, d) => di);
            var dtos = ObjectMapper.Map<List<DictItem>, List<DictItemDto>>(await tempQuery.ToListAsync());
            return WebApiResult<ListResultDto<DictItemDto>>.SuccessResult(new ListResultDto<DictItemDto>(dtos));
        }

        public async Task<WebApiResult<ListResultDto<DictItemDto>>> GetListAsync()
        {
            var dtos = ObjectMapper.Map<List<DictItem>, List<DictItemDto>>(await _dictItemRepository.GetListAsync());
            return WebApiResult<ListResultDto<DictItemDto>>.SuccessResult(new ListResultDto<DictItemDto>(dtos));
        }

        public async Task<WebApiResult<PagedEResultDto<DictItemDto>>> GetListPagedAsync(DictItemPagedAndSortedResultRequestDto input)
        {
            var dictItemQuery = _dictItemRepository
                 .WhereIf(!string.IsNullOrEmpty(input.DictId), w => w.DictId == input.DictId)
                 .WhereIf(!string.IsNullOrEmpty(input.ItemValue), w => w.ItemValue == w.ItemValue);

            var tempQuery = dictItemQuery.Join(_dictRepository.WhereIf(!string.IsNullOrEmpty(input.DictId), w => w.Id == input.DictId),
                di => di.DictId, d => d.Id, (di, d) => di)
                 .OrderBy(input.Sorting ?? "ItemValue");

            return await tempQuery.GetListPaged<DictItem, DictItemDto>(ObjectMapper, input);

        }

        [Authorize(SystemManagementPermissions.Dicts.Update)]
        public async Task<WebApiResult<DictItemDto>> UpdateAsync(string id, DictItemDto input)
        {
            var dictItem = await _dictManager.UpdateDictItem(id, input.ItemValue, input.ItemText, input.Description, input.SortOrder, input.ItemEnText);
            return WebApiResult<DictItemDto>.SuccessResult(ObjectMapper.Map<DictItem, DictItemDto>(dictItem));
        }

        [Authorize(SystemManagementPermissions.Dicts.Update)]
        public async Task<WebApiResult<DictItemDto>> UpdateStatusAsync(string id, UpdateDictItemStatusDto updateDictItemStatusDto)
        {
            var dictItem = await _dictItemRepository.GetAsync(id);
            dictItem.Enabled = updateDictItemStatusDto.Enabled;
            return WebApiResult<DictItemDto>.SuccessResult(ObjectMapper.Map<DictItem, DictItemDto>(dictItem));
        }

    }
}
