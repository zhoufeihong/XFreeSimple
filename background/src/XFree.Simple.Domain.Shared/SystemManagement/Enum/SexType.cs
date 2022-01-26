using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XFree.Simple.Domain.Shared.SystemManagement.Enum
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum SexType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        Unknown = 0,
        /// <summary>
        ///  男
        /// </summary>
        [Description("男")]
        Male = 1,
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Female = 2
    }
}
