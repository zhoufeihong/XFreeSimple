using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Common;
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.Application.Contracts.SystemManagement.AppService
{
    /// <summary>
    /// 字典接口
    /// </summary>
    public interface IDictAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<PagedEResultDto<DictDto>>> GetListPagedAsync(DictPagedAndSortedRequestDto input);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        Task<WebApiResult<ListResultDto<DictDto>>> GetListAsync();

        /// <summary>
        /// 主键获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<WebApiResult<DictDto>> GetAsync(string id);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<DictDto>> CreateAsync(DictDto input);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<DictDto>> UpdateAsync(string id, DictDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<WebApiResult> DeleteAsync(string id);
    }
}
