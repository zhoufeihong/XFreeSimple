using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization;
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.Application.Contracts.SystemManagement.AppService
{
    /// <summary>
    /// 租户接口
    /// </summary>
    public interface ITenantAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<PagedEResultDto<TenantDto>>> GetListPagedAsync(TenantPagedAndSortedRequestDto input);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        Task<WebApiResult<ListResultDto<TenantDto>>> GetListAsync();

        /// <summary>
        /// 主键获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<WebApiResult<TenantDto>> GetAsync(string id);
        /// <summary>
        /// 名称获取
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<WebApiResult<TenantDto>> GetByNameAsync(string name);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<TenantDto>> CreateAsync(CreateTenantDto input);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<TenantDto>> UpdateAsync(string id, UpdateTenantDto input);

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<TenantDto>> UpdateStatusAsync(string id, UpdateTenantStatusDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<WebApiResult> DeleteAsync(string id);
    }
}
