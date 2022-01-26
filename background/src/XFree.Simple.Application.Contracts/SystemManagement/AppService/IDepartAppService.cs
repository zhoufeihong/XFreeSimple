using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization;
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.Application.Contracts.SystemManagement.AppService
{
    /// <summary>
    /// 部门接口
    /// </summary>
    public interface IDepartAppService
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<DepartDto>> CreateAsync(CreateDepartDto input);
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        Task<WebApiResult<ListResultDto<DepartDto>>> GetListAsync();
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
        Task<WebApiResult<DepartDto>> GetAsync(string id);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<PagedEResultDto<DepartDto>>> GetListPagedAsync(DepartPagedAndSortedRequestDto input);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<DepartDto>> UpdateAsync(string id, UpdateDepartDto input);
        /// <summary>
        /// 分页查询部门下的用户
        /// </summary>
        /// <param name="departId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<PagedEResultDto<UserDto>>> GetUserListPagedAsync(string departId, PagedAndSortedRequestDto input);
    }
}
