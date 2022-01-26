using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using XFree.Simple.Application.Contracts.SystemManage;
using XFree.Simple.Application.Contracts.SystemManagement;
using XFree.Simple.Application.Contracts.SystemManagement.AppService;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization;
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.HttpApi.SystemManage.Controllers
{
    /// <summary>
    /// 部门接口
    /// </summary>
    [RemoteService]
    [Area("systemManagement")]
    [Route("api/systemManagement/depart")]
    public class DepartController : AbpController, IDepartAppService
    {
        private readonly IDepartAppService _departAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departAppService"></param>
        public DepartController(IDepartAppService departAppService)
        {
            _departAppService = departAppService;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<WebApiResult<PagedEResultDto<DepartDto>>> GetListPagedAsync(DepartPagedAndSortedRequestDto input)
        {
            return await _departAppService.GetListPagedAsync(input);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<WebApiResult<ListResultDto<DepartDto>>> GetListAsync()
        {
            return await _departAppService.GetListAsync();
        }

        /// <summary>
        /// 主键获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<WebApiResult<DepartDto>> GetAsync(string id)
        {
            return await _departAppService.GetAsync(id);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiResult<DepartDto>> CreateAsync(CreateDepartDto input)
        {
            return await _departAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<WebApiResult<DepartDto>> UpdateAsync(string id, UpdateDepartDto input)
        {
            return await _departAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<WebApiResult> DeleteAsync(string id)
        {
            return await _departAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 分页获取部门下的用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/getUserListPaged")]
        public async Task<WebApiResult<PagedEResultDto<UserDto>>> GetUserListPagedAsync(string id, PagedAndSortedRequestDto input)
        {
            return await _departAppService.GetUserListPagedAsync(id, input);
        }

    }
}
