using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Linq;
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.Application.Common
{
    /// <summary>
    ///  分页查询辅助类
    /// </summary>
    public static class GetListPagedExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static async Task<WebApiResult<PagedEResultDto<TSource>>> GetListPaged<TSource>(this IQueryable<TSource> source, PagedRequestDto input)
        {
            var dtos = await source
                .Skip((input.PageIndex - 1) * input.PageSize)
                .Take(input.PageSize)
                .ToListAsync();

            var totalCount = await source.LongCountAsync();

            //var dtos = ObjectMapper.Map<List<User>, List<UserDto>>(users);

            return WebApiResult<PagedEResultDto<TSource>>.SuccessResult(new PagedEResultDto<TSource>(totalCount, dtos));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="objectMapper"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static async Task<WebApiResult<PagedEResultDto<TDestination>>> GetListPaged<TSource, TDestination>(this IQueryable<TSource> source, Volo.Abp.ObjectMapping.IObjectMapper objectMapper, PagedRequestDto input)
        {
            var sourceResults = await source
                .Skip((input.PageIndex - 1) * input.PageSize)
                .Take(input.PageSize)
                .ToListAsync();

            var totalCount = await source.LongCountAsync();

            var dtos = objectMapper.Map<List<TSource>, List<TDestination>>(sourceResults);
            return WebApiResult<PagedEResultDto<TDestination>>.SuccessResult(new PagedEResultDto<TDestination>(totalCount, dtos));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="objectMapper"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static async Task<WebApiResult<PagedEResultDto<TDestination>>> GetDtoListPaged<TDestination>(this IQueryable<TDestination> source, PagedRequestDto input)
        {
            var dtoResults = await source
                .Skip((input.PageIndex - 1) * input.PageSize)
                .Take(input.PageSize)
                .ToListAsync();

            var totalCount = await source.LongCountAsync();

            return WebApiResult<PagedEResultDto<TDestination>>.SuccessResult(new PagedEResultDto<TDestination>(totalCount, dtoResults));
        }

    }
}
