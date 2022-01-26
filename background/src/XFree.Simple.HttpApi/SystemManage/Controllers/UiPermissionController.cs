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
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Permission;
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.HttpApi.SystemManage.Controllers
{
    /// <summary>
    /// Ui菜单权限接口
    /// </summary>
    [RemoteService]
    [Area("systemManagement")]
    [Route("api/systemManagement/uiPermission")]
    public class UiPermissionController : AbpController
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IUiPermissionAppService _uiPermissionAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiPermissionAppService"></param>
        public UiPermissionController(IUiPermissionAppService uiPermissionAppService)
        {
            _uiPermissionAppService = uiPermissionAppService;
        }

        /// <summary>
        /// 主键获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<WebApiResult<UiPermissionDto>> GetAsync(string id)
        {
            return await _uiPermissionAppService.GetAsync(id);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiResult<UiPermissionDto>> CreateAsync(CreateUiPermissionDto input)
        {
            return await _uiPermissionAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<WebApiResult<UiPermissionDto>> UpdateAsync(string id, UpdateUiPermissionDto input)
        {
            return await _uiPermissionAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<WebApiResult> DeleteAsync(string id)
        {
            return await _uiPermissionAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 通过父节点Id查询
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet("query")]
        public async Task<WebApiResult<ListResultDto<UiPermissionDto>>> QueryAsync(string parentId)
        {
            return await _uiPermissionAppService.QueryAsync(parentId);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll")]
        public async Task<WebApiResult<ListResultDto<UiPermissionDto>>> GetListAsync()
        {
            return await _uiPermissionAppService.GetListAsync();
        }

        /// <summary>
        /// 获取全部接口资源
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAllBackgroundApi")]
        public async Task<WebApiResult<List<BackgroundApiDto>>> GetAllBackgroundApiAsync()
        {
            return await _uiPermissionAppService.GetAllBackgroundApiAsync();
        }

        /// <summary>
        /// 刷新接口资源
        /// </summary>
        /// <returns></returns>
        [HttpPut("refreshBackgroundApi")]
        public async Task<WebApiResult> RefreshBackgroundApiAsync()
        {
            return await _uiPermissionAppService.RefreshBackgroundApiAsync();
        }

        /// <summary>
        /// 分配接口资源
        /// </summary>
        /// <param name="id"></param>
        /// <param name="permissionCodes"></param>
        /// <returns></returns>
        [HttpPut("{id}/permissionCodes")]
        public async Task<WebApiResult> BindBackgroundApi(string id, [FromBody] string[] permissionCodes)
        {
            return await _uiPermissionAppService.BindBackgroundApi(id, permissionCodes);
        }

        /// <summary>
        /// 查询接口资源
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/permissionCodes")]
        public async Task<WebApiResult<List<string>>> QueryPermissionCodes(string id)
        {
            return await _uiPermissionAppService.QueryPermissionCodes(id);
        }

        /// <summary>
        /// 清除本地缓存
        /// </summary>
        /// <returns></returns>
        [HttpPut("removeAllMemoryCache")]
        public WebApiResult RemoveAllMemoryCache()
        {
            return _uiPermissionAppService.RemoveAllMemoryCache();
        }

    }
}
