using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XFree.Simple.Domain.Shared.SystemManagement.Enum
{
    /// <summary>
    ///  处理状态
    /// </summary>
    public enum ProcessingStatus
    {
        /// <summary>
        /// 初始状态
        /// </summary>
        [Description("初始状态")]
        Initial = 0,
        /// <summary>
        ///  准备完成
        /// </summary>
        [Description("准备完成")]
        Ready = 1,
        /// <summary>
        ///  处理中
        /// </summary>
        [Description("处理中")]
        Processing = 2,
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Successful = 3,
        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Failed = 4
    }
}
