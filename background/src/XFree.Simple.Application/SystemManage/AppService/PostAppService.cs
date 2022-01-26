using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using XFree.Simple.Application.Common;
using XFree.Simple.Application.Contracts.Common;
using XFree.Simple.Application.Contracts.SystemManagement;
using XFree.Simple.Application.Contracts.SystemManagement.AppService;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization;
using XFree.Simple.Application.Contracts.SystemManagement.Permission;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.Shared.SystemManagement;
using XFree.Simple.Domain.SystemManagement.Organization;

namespace XFree.Simple.Application.SystemManage.AppService
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(SystemManagementPermissions.Base.WithToken)]
    public class PostAppService : ApplicationService, IPostAppService
    {

        private readonly IRepository<Post, string> _postRepository;

        private readonly ICodeGenerator _codeGenerator;

        private readonly string PostCodePrefix = "P";

        private readonly ErrorMessageService _errorMessageService;

        public PostAppService(IRepository<Post, string> postRepository,
            ICodeGenerator codeGenerator, 
            ErrorMessageService errorMessageService)
        {
            _postRepository = postRepository;
            _codeGenerator = codeGenerator;
            _errorMessageService = errorMessageService;
        }

        [Authorize(SystemManagementPermissions.Posts.Create)]
        public async Task<WebApiResult<PostDto>> CreateAsync(CreatePostDto input)
        {
            var newPost = new Post(GuidGenerator.Create().ToString())
            {
                Code = input.Code
            };
            if (string.IsNullOrEmpty(newPost.Code))
            {
                newPost.Code = await _codeGenerator.Create(PostCodePrefix);
            }
            newPost.Memo = input.Memo;
            newPost.Name = input.Name;
            newPost.SortOrder = input.SortOrder;
            var existingPost = await _postRepository.FirstOrDefaultAsync(p => p.Code == input.Code);
            if (existingPost != null)
            {
              _errorMessageService.ThrowMessageParam(SystemFriendlyExceptionCode.DuplicatePostCode501, existingPost.Code);
            }
            await _postRepository.InsertAsync(newPost);
            return WebApiResult<PostDto>.SuccessResult(ObjectMapper.Map<Post, PostDto>(newPost));
        }

        public async Task<WebApiResult<ListResultDto<PostDto>>> GetListAsync()
        {
            var items = await _postRepository.GetListAsync();

            var itemDtos = ObjectMapper.Map<List<Post>, List<PostDto>>(items);

            return WebApiResult<ListResultDto<PostDto>>.SuccessResult(new ListResultDto<PostDto>(itemDtos));
        }

        [Authorize(SystemManagementPermissions.Posts.Delete)]
        public async Task<WebApiResult> DeleteAsync(string id)
        {
            await _postRepository.DeleteAsync(id);
            return WebApiResult.SuccessResult();
        }

        public async Task<WebApiResult<PostDto>> GetAsync(string id)
        {
            var post = await _postRepository.GetAsync(id);

            return WebApiResult<PostDto>.SuccessResult(ObjectMapper.Map<Post, PostDto>(post));
        }

        public async Task<WebApiResult<PagedEResultDto<PostDto>>> GetListPagedAsync(PostPagedAndSortedRequestDto input)
        {
            var resultQuery = _postRepository
                              .WhereIf(!string.IsNullOrEmpty(input.Code), w => w.Code == input.Code)
                              .WhereIf(!string.IsNullOrEmpty(input.Name), w => w.Name == input.Name)
                              .OrderBy(input.Sorting ?? "CreationTime");

            return await resultQuery.GetListPaged<Post, PostDto>(ObjectMapper, input);
        }

        [Authorize(SystemManagementPermissions.Posts.Update)]
        public async Task<WebApiResult<PostDto>> UpdateAsync(string id, UpdatePostDto input)
        {
            var post = await _postRepository.GetAsync(id);
            post.Name = input.Name;
            post.Memo = input.Memo;
            post.SortOrder = input.SortOrder;
            await _postRepository.UpdateAsync(post);
            return WebApiResult<PostDto>.SuccessResult(ObjectMapper.Map<Post, PostDto>(post));
        }

        [Authorize(SystemManagementPermissions.Posts.Update)]
        public async Task<WebApiResult<PostDto>> UpdateStatusAsync(string id, UpdatePostStatusDto updatePostStatusDto)
        {
            var post = await _postRepository.GetAsync(id);
            post.Status = updatePostStatusDto.Status;
            return WebApiResult<PostDto>.SuccessResult(ObjectMapper.Map<Post, PostDto>(post));
        }

    }
}
