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
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.HttpApi.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RemoteService]
    [Area("systemManagement")]
    [Route("api/systemManagement/post")]
    public class PostController : AbpController, IPostAppService
    {
        private readonly IPostAppService _postAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postAppService"></param>
        public PostController(IPostAppService postAppService)
        {
            _postAppService = postAppService;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<WebApiResult<PagedEResultDto<PostDto>>> GetListPagedAsync(PostPagedAndSortedRequestDto input)
        {
            return await _postAppService.GetListPagedAsync(input);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<WebApiResult<ListResultDto<PostDto>>> GetListAsync()
        {
            return await _postAppService.GetListAsync();
        }

        /// <summary>
        /// 主键获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<WebApiResult<PostDto>> GetAsync(string id)
        {
            return await _postAppService.GetAsync(id);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiResult<PostDto>> CreateAsync(CreatePostDto input)
        {
            return await _postAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<WebApiResult<PostDto>> UpdateAsync(string id, UpdatePostDto input)
        {
            return await _postAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/status")]
        public async Task<WebApiResult<PostDto>> UpdateStatusAsync(string id, UpdatePostStatusDto input)
        {
            return await _postAppService.UpdateStatusAsync(id, input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<WebApiResult> DeleteAsync(string id)
        {
            return await _postAppService.DeleteAsync(id);
        }

    }
}
