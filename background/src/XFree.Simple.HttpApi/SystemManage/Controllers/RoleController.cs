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
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Permission;
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.HttpApi.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RemoteService]
    [Area("systemManagement")]
    [Route("api/systemManagement/role")]
    public class RoleController : AbpController, IRoleAppService
    {
        private readonly IRoleAppService _roleAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleAppService"></param>
        public RoleController(IRoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<WebApiResult<PagedEResultDto<RoleDto>>> GetListPagedAsync(RolePagedAndSortedRequestDto input)
        {
            return await _roleAppService.GetListPagedAsync(input);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<WebApiResult<ListResultDto<RoleDto>>> GetListAsync()
        {
            return await _roleAppService.GetListAsync();
        }

        /// <summary>
        /// 主键获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<WebApiResult<RoleDto>> GetAsync(string id)
        {
            return await _roleAppService.GetAsync(id);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiResult<RoleDto>> CreateAsync(CreateRoleDto input)
        {
            return await _roleAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<WebApiResult<RoleDto>> UpdateAsync(string id, UpdateRoleDto input)
        {
            return await _roleAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/status")]
        public async Task<WebApiResult<RoleDto>> UpdateStatusAsync(string id, UpdateRoleStatusDto input)
        {
            return await _roleAppService.UpdateStatusAsync(id, input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<WebApiResult> DeleteAsync(string id)
        {
            return await _roleAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="id"></param>
        /// <param name="permissionIds"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/grant")]
        public Task<WebApiResult> GrantUiPermission(string id, [FromBody]string[] permissionIds)
        {
            return _roleAppService.GrantUiPermission(id, permissionIds);
        }

        /// <summary>
        /// 查询角色Ui菜单权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/uiPermissionIds")]
        public async Task<WebApiResult<List<string>>> QueryUiPermissionIds(string id)
        {
            return await _roleAppService.QueryUiPermissionIds(id);
        }

        /// <summary>
        /// 查询角色下用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/users")]
        public async Task<WebApiResult<List<UserDto>>> QueryUsers(string id)
        {
            return await _roleAppService.QueryUsers(id);
        }
    }
}
