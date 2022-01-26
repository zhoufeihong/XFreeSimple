using System;
using System.Collections.Generic;
using System.Text;
using XFree.Simple.Application.Contracts.SystemManagement.Permission;

namespace XFree.Simple.Application.Contracts.SystemManagement.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemManagementSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public const string GroupName = SystemManagementPermissions.GroupName;

        /// <summary>
        /// Maximum allowed page size for paged list requests.
        /// </summary>
        public const string MaxPageSize = GroupName + ".MaxPageSize";
    }
}
