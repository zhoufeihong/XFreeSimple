using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace XFree.Simple.Application.Contracts.Common
{
    /// <summary>
    ///  业务编码生成
    /// </summary>
    public interface ICodeGenerator : ISingletonDependency
    {
        /// <summary>
        /// 生成业务编码
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        Task<string> Create(string prefix);
    }
}
