using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using XFree.Simple.Application.Contracts.SystemManagement;
using XFree.Simple.Application.Contracts.SystemManagement.AppService;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Common;
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.HttpApi.SystemManage.Controllers
{
    /// <summary>
    /// 字典项接口
    /// </summary>
    [RemoteService]
    [Area("systemManagement")]
    [Route("api/systemManagement/dictItem")]
    public class DictItemControllerController : AbpController, IDictItemAppService
    {
        private readonly IDictItemAppService _dictItemAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictItemAppService"></param>
        public DictItemControllerController(IDictItemAppService dictItemAppService)
        {
            _dictItemAppService = dictItemAppService;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<WebApiResult<PagedEResultDto<DictItemDto>>> GetListPagedAsync(DictItemPagedAndSortedResultRequestDto input)
        {
            return await _dictItemAppService.GetListPagedAsync(input);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<WebApiResult<ListResultDto<DictItemDto>>> GetListAsync()
        {
            return await _dictItemAppService.GetListAsync();
        }

        /// <summary>
        /// 通过字典编码查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get")]
        public async Task<WebApiResult<ListResultDto<DictItemDto>>> GetListAsync(string dictCode)
        {
            return await _dictItemAppService.GetListAsync(dictCode);
        }

        /// <summary>
        /// 主键获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<WebApiResult<DictItemDto>> GetAsync(string id)
        {
            return await _dictItemAppService.GetAsync(id);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiResult<DictItemDto>> CreateAsync(DictItemDto input)
        {
            return await _dictItemAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<WebApiResult<DictItemDto>> UpdateAsync(string id, DictItemDto input)
        {
            return await _dictItemAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDictItemStatusDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/status")]
        public async Task<WebApiResult<DictItemDto>> UpdateStatusAsync(string id, UpdateDictItemStatusDto updateDictItemStatusDto)
        {
            return await _dictItemAppService.UpdateStatusAsync(id, updateDictItemStatusDto);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<WebApiResult> DeleteAsync(string id)
        {
            return await _dictItemAppService.DeleteAsync(id);
        }
    }
}
