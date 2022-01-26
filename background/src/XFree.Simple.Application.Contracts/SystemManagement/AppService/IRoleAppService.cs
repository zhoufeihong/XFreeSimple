using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Permission;
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.Application.Contracts.SystemManagement.AppService
{
    /// <summary>
    /// 角色接口
    /// </summary>
    public interface IRoleAppService
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<RoleDto>> CreateAsync(CreateRoleDto input);
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        Task<WebApiResult<ListResultDto<RoleDto>>> GetListAsync();
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<WebApiResult> DeleteAsync(string id);
        /// <summary>
        /// 主键获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<WebApiResult<RoleDto>> GetAsync(string id);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<PagedEResultDto<RoleDto>>> GetListPagedAsync(RolePagedAndSortedRequestDto input);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<RoleDto>> UpdateAsync(string id, UpdateRoleDto input);
        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUserStatusDto"></param>
        /// <returns></returns>
        Task<WebApiResult<RoleDto>> UpdateStatusAsync(string id, UpdateRoleStatusDto updateUserStatusDto);
        /// <summary>
        /// 给角色授权
        /// </summary>
        /// <param name="id"></param>
        /// <param name="permissionIds"></param>
        /// <returns></returns>
        Task<WebApiResult> GrantUiPermission(string id, string[] permissionIds);
        /// <summary>
        /// 查询角色Ui菜单权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<WebApiResult<List<string>>> QueryUiPermissionIds(string id);
        /// <summary>
        /// 查询角色下的用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<WebApiResult<List<UserDto>>> QueryUsers(string roleId);
    }
}
