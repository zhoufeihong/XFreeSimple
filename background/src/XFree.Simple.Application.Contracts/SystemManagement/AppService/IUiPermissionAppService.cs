using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Permission;
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.Application.Contracts.SystemManagement.AppService
{
    /// <summary>
    /// Ui菜单权限接口
    /// </summary>
    public interface IUiPermissionAppService
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<UiPermissionDto>> CreateAsync(CreateUiPermissionDto input);
        /// <summary>
        /// 通过父节点查询Ui菜单权限
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        Task<WebApiResult<ListResultDto<UiPermissionDto>>> QueryAsync(string parentId);
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
        Task<WebApiResult<UiPermissionDto>> GetAsync(string id);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<UiPermissionDto>> UpdateAsync(string id, UpdateUiPermissionDto input);
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        Task<WebApiResult<ListResultDto<UiPermissionDto>>> GetListAsync();
        /// <summary>
        /// 分配接口资源
        /// </summary>
        /// <param name="id"></param>
        /// <param name="permissionCodes"></param>
        /// <returns></returns>
        Task<WebApiResult> BindBackgroundApi(string id, string[] permissionCodes);
        /// <summary>
        /// 查询接口资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<WebApiResult<List<string>>> QueryPermissionCodes(string id);
        /// <summary>
        /// 获取全部接口资源
        /// </summary>
        /// <returns></returns>
        Task<WebApiResult<List<BackgroundApiDto>>> GetAllBackgroundApiAsync();
        /// <summary>
        /// 刷新接口资源
        /// </summary>
        /// <returns></returns>
        Task<WebApiResult> RefreshBackgroundApiAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        WebApiResult RemoveAllMemoryCache();
    }
}
