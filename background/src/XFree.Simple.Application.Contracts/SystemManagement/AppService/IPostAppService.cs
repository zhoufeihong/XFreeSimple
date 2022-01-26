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
    /// 职务接口
    /// </summary>
    public interface IPostAppService
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<PostDto>> CreateAsync(CreatePostDto input);
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        Task<WebApiResult<ListResultDto<PostDto>>> GetListAsync();
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
        Task<WebApiResult<PostDto>> GetAsync(string id);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<PagedEResultDto<PostDto>>> GetListPagedAsync(PostPagedAndSortedRequestDto input);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WebApiResult<PostDto>> UpdateAsync(string id, UpdatePostDto input);
        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatePostStatusDto"></param>
        /// <returns></returns>
        Task<WebApiResult<PostDto>> UpdateStatusAsync(string id, UpdatePostStatusDto updatePostStatusDto);
    }
}
