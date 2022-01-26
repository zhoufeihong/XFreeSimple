using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XFree.Simple.Domain.Shared.SystemManagement.Enum
{
    /// <summary>
    /// 部门类型
    /// </summary>
    public enum OrgCategory
    {
        /// <summary>
        ///  组织架构
        /// </summary>
        [Description("组织架构")]
        Structure = 1,
        /// <summary>
        ///  岗位
        /// </summary>
        [Description("岗位")]
        Post = 2
    }
}
