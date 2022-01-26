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
    /// 字典接口
    /// </summary>
    [RemoteService]
    [Area("systemManagement")]
    [Route("api/systemManagement/dict")]
    public class DictControllerController : AbpController, IDictAppService
    {
        private readonly IDictAppService _dictAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictAppService"></param>
        public DictControllerController(IDictAppService dictAppService)
        {
            _dictAppService = dictAppService;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<WebApiResult<PagedEResultDto<DictDto>>> GetListPagedAsync(DictPagedAndSortedRequestDto input)
        {
            return await _dictAppService.GetListPagedAsync(input);
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<WebApiResult<ListResultDto<DictDto>>> GetListAsync()
        {
            return await _dictAppService.GetListAsync();
        }

        /// <summary>
        /// 主键获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<WebApiResult<DictDto>> GetAsync(string id)
        {
            return await _dictAppService.GetAsync(id);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiResult<DictDto>> CreateAsync(DictDto input)
        {
            return await _dictAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<WebApiResult<DictDto>> UpdateAsync(string id, DictDto input)
        {
            return await _dictAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除字典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<WebApiResult> DeleteAsync(string id)
        {
            return await _dictAppService.DeleteAsync(id);
        }
    }
}
