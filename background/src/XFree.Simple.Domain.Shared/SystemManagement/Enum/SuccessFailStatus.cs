using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XFree.Simple.Domain.Shared.SystemManagement.Enum
{
    /// <summary>
    /// 成功失败状态
    /// </summary>
    public enum SuccessFailStatus
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        Unknown = 0,
        /// <summary>
        ///  成功
        /// </summary>
        [Description("成功")]
        Successful = 1,
        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Failed = 2
    }
}
