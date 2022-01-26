using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XFree.Simple.Domain.Shared.SystemManagement.Enum
{
    /// <summary>
    /// 正常、锁定状态枚举
    /// </summary>
    public enum NormalLockedStatus
    {
        /// <summary>
        /// 正常状态
        /// </summary>
        [Description("正常")]
        Normal = 1,
        /// <summary>
        /// 锁定状态
        /// </summary>
        [Description("锁定")]
        Locked = 2
    }
}
