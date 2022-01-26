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
using XFree.Simple.Domain.Shared.SystemManagement;
using XFree.Simple.Domain.SystemManagement.Common;

namespace XFree.Simple.Application.SystemManage.AppService
{
    /// <summary>
    /// 
    /// </summary>
    public class DictAppService : ApplicationService, IDictAppService
    {
        private readonly IRepository<Dict, string> _dictRepository;

        private readonly ErrorMessageService _errorMessageService;

        public DictAppService(IRepository<Dict, string> dictRepository,
            ErrorMessageService errorMessageService)
        {
            _dictRepository = dictRepository;
            _errorMessageService = errorMessageService;
        }

        [Authorize(SystemManagementPermissions.Dicts.Create)]
        public async Task<WebApiResult<DictDto>> CreateAsync(DictDto input)
        {
            var existingDict = await _dictRepository.FirstOrDefaultAsync(p => p.DictCode == input.DictCode);
            if (existingDict != null)
            {
                if (existingDict.DictCode == input.DictCode)
                    _errorMessageService.ThrowMessageParam(SystemFriendlyExceptionCode.DuplicateMerchantCode, input.DictCode);
            }
            var dict = await _dictRepository.InsertAsync(
               new Dict(GuidGenerator.Create().ToString())
               {
                   DictCode = input.DictCode,
                   DictName = input.DictName,
                   DictEnName = input.DictEnName,
                   Description = input.Description
               }
            );
            return WebApiResult<DictDto>.SuccessResult(ObjectMapper.Map<Dict, DictDto>(dict));
        }

        [Authorize(SystemManagementPermissions.Dicts.Delete)]
        public async Task<WebApiResult> DeleteAsync(string id)
        {
            await _dictRepository.DeleteAsync(id);
            return WebApiResult.SuccessResult();
        }

        [Authorize(SystemManagementPermissions.Dicts.Default)]
        public async Task<WebApiResult<DictDto>> GetAsync(string id)
        {
            var dict = await _dictRepository.GetAsync(id);
            return WebApiResult<DictDto>.SuccessResult(ObjectMapper.Map<Dict, DictDto>(dict));
        }

        [Authorize(SystemManagementPermissions.Dicts.Default)]
        public async Task<WebApiResult<ListResultDto<DictDto>>> GetListAsync()
        {
            var dicts = await _dictRepository.GetListAsync();

            var dictList = ObjectMapper.Map<List<Dict>, List<DictDto>>(dicts);

            return WebApiResult<ListResultDto<DictDto>>.SuccessResult(new ListResultDto<DictDto>(dictList));
        }

        [Authorize(SystemManagementPermissions.Dicts.Default)]
        public async Task<WebApiResult<PagedEResultDto<DictDto>>> GetListPagedAsync(DictPagedAndSortedRequestDto input)
        {
            var resultQuery = _dictRepository
             .WhereIf(!string.IsNullOrEmpty(input.DictName), w => w.DictName.Contains(input.DictName))
             .WhereIf(!string.IsNullOrEmpty(input.DictCode), w => w.DictCode.Contains(input.DictCode))
             .OrderBy(input.Sorting ?? "DictName");

            return await resultQuery.GetListPaged<Dict, DictDto>(ObjectMapper, input);
        }

        [Authorize(SystemManagementPermissions.Dicts.Update)]
        public async Task<WebApiResult<DictDto>> UpdateAsync(string id, DictDto input)
        {
            var dict = await _dictRepository.GetAsync(id);
            if (dict == null)
            {
                throw new ArgumentException(null, nameof(id));
            }
            if (await _dictRepository.AnyAsync(w => w.DictCode == input.DictCode && w.Id != id))
            {
               _errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.DuplicateDictCode);
            }
            dict.DictCode = input.DictCode;
            dict.DictName = input.DictName;
            dict.DictEnName = input.DictEnName;
            dict.Description = input.Description;
            return WebApiResult<DictDto>.SuccessResult(ObjectMapper.Map<Dict, DictDto>(dict));
        }

    }
}
