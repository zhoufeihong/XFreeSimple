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
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization;
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.HttpApi.SystemManage.Controllers
{
    /// <summary>
    /// 租户接口
    /// </summary>
    [RemoteService]
    [Area("systemManagement")]
    [Route("api/systemManagement/tenant")]
    public class TenantController : AbpController, ITenantAppService
    {
        private readonly ITenantAppService _tenantAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenantAppService"></param>
        public TenantController(ITenantAppService tenantAppService)
        {
            _tenantAppService = tenantAppService;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<WebApiResult<PagedEResultDto<TenantDto>>> GetListPagedAsync(TenantPagedAndSortedRequestDto input)
        {
            return await _tenantAppService.GetListPagedAsync(input);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<WebApiResult<ListResultDto<TenantDto>>> GetListAsync()
        {
            return await _tenantAppService.GetListAsync();
        }

        /// <summary>
        /// 主键获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<WebApiResult<TenantDto>> GetAsync(string id)
        {
            return await _tenantAppService.GetAsync(id);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiResult<TenantDto>> CreateAsync(CreateTenantDto input)
        {
            return await _tenantAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<WebApiResult<TenantDto>> UpdateAsync(string id, UpdateTenantDto input)
        {
            return await _tenantAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/status")]
        public async Task<WebApiResult<TenantDto>> UpdateStatusAsync(string id, UpdateTenantStatusDto input)
        {
            return await _tenantAppService.UpdateStatusAsync(id, input);
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
            return await _tenantAppService.DeleteAsync(id);
        }

        /// <summary>
        ///  通过名称获取
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get")]
        public async Task<WebApiResult<TenantDto>> GetByNameAsync([FromQuery]string name)
        {
            return await _tenantAppService.GetByNameAsync(name);
        }
    }
}
