using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XFree.Simple.Domain.Shared.SystemManagement.Enum
{
    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    {
        /// <summary>
        ///  租户用户
        /// </summary>
        [Description("租户用户")]
        Tenant = 1,
        /// <summary>
        ///  平台用户
        /// </summary>
        [Description("平台用户")]
        Platform = 2
    }
}
