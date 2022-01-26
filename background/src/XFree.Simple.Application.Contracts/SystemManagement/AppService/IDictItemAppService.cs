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
    /// 字典项接口
    /// </summary>
    public interface IDictItemAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<PagedEResultDto<DictItemDto>>> GetListPagedAsync(DictItemPagedAndSortedResultRequestDto input);

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns></returns>
        Task<WebApiResult<ListResultDto<DictItemDto>>> GetListAsync();

        /// <summary>
        /// 通过字典编码获取
        /// </summary>
        /// <param name="dictCode">字典编码</param>
        /// <returns></returns>
        Task<WebApiResult<ListResultDto<DictItemDto>>> GetListAsync(string dictCode);

        /// <summary>
        /// 主键获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<WebApiResult<DictItemDto>> GetAsync(string id);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<DictItemDto>> CreateAsync(DictItemDto input);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<DictItemDto>> UpdateAsync(string id, DictItemDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<WebApiResult> DeleteAsync(string id);

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDictItemStatusDto"></param>
        /// <returns></returns>
        Task<WebApiResult<DictItemDto>> UpdateStatusAsync(string id, UpdateDictItemStatusDto updateDictItemStatusDto);
    }
}
