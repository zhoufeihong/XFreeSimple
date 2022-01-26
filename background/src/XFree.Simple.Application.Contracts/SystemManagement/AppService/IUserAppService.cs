using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Authentication.Dto;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Permission;
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.Application.Contracts.SystemManage.AppService
{
    /// <summary>
    /// 用户接口
    /// </summary>
    public interface IUserAppService
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<UserDto>> CreateAsync(CreateUserDto input);
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
        Task<WebApiResult<UserDto>> GetAsync(string id);
        /// <summary>
        /// 通过Token获取用户信息
        /// </summary>
        /// <returns></returns>
        Task<WebApiResult<UserDto>> GetByTokenAsync();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<PagedEResultDto<UserDto>>> GetListPagedAsync(UserPagedAndSortedRequestDto input);
        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        Task<WebApiResult<ImportResult>> ImportAsync(List<UserDto> inputs);
        /// <summary>
        /// 导出数据查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<PagedEResultDto<UserDto>>> GetExportListPagedAsync(UserPagedAndSortedRequestDto input);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<UserDto>> UpdateAsync(string id, UpdateUserDto input);
        /// <summary>
        ///  重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<WebApiResult> ResetPassword(string id);
        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUserStatusDto"></param>
        /// <returns></returns>
        Task<WebApiResult<UserDto>> UpdateStatusAsync(string id, UpdateUserStatusDto updateUserStatusDto);
        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUserPasswordDto"></param>
        /// <returns></returns>
        Task<WebApiResult<UserDto>> UpdatePasswordAsync(string id, UpdateUserPasswordDto updateUserPasswordDto);
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        Task<WebApiResult<UserPermisionInfo>> GetUserPermisionInfo();
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <returns></returns>
        Task<WebApiResult<List<UiPermissionDto>>> GetUiPermission();
        /// <summary>
        /// 分页查询用户操作记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<PagedEResultDto<OperationInfoDto>>> GetOperationInfoListPagedAsync(PagedAndSortedRequestDto input);
    }
}