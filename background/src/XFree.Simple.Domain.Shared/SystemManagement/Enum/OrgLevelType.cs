using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XFree.Simple.Domain.Shared.SystemManagement.Enum
{
    /// <summary>
    /// 部门层级
    /// </summary>
    public enum OrgLevelType
    {
        /// <summary>
        /// 一级部门
        /// </summary>
        [Description("一级部门")]
        PrimaryDepartment = 1,
        /// <summary>
        /// 子部门
        /// </summary>
        [Description("子部门")]
        SubsidiaryDepartment = 2
    }
}
