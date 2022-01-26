using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XFree.SimpleService.Host.Common.Attributes
{
    /// <summary>
    /// 不需要统一返回结果特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NoWrapResultAttribute : Attribute
    {
    }
}
