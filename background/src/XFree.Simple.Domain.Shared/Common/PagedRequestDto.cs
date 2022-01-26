using System;
using System.Collections.Generic;
using System.Text;

namespace XFree.Simple.Domain.Shared.Common
{

    /// <summary>
    ///  分页接口请求实体
    /// </summary>
    public class PagedRequestDto
    {
        /// <summary>
        ///  每页返回结果数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        ///  当前页号，从1开始
        /// </summary>
        public int PageIndex { get; set; }
    }

    /// <summary>
    /// 带排序的分页接口请求实体
    /// </summary>
    public class PagedAndSortedRequestDto : PagedRequestDto
    {
        /// <summary>
        ///  排序请求参数
        ///  服务依赖<see cref="System.Linq.Dynamic.Core.DynamicQueryableExtensions"/>实现动态排序
        /// </summary>
        public virtual string Sorting { get; set; }
    }

    /// <summary>
    ///  分页接口结果
    /// </summary>
    public class PagedEResultDto<T>
    {

        /// <summary>
        /// 结果总数量
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// 当前页结果数据
        /// </summary>
        public IReadOnlyList<T> Data
        {
            get { return _items ?? (_items = new List<T>()); }
            set { _items = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private IReadOnlyList<T> _items;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalCount">总数量</param>
        /// <param name="items">当前页数据</param>
        public PagedEResultDto(long totalCount, IReadOnlyList<T> items)
        {
            Total = totalCount;
            _items = items;
        }

    }

}
