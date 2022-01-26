using System;
using System.Collections.Generic;
using System.Text;

namespace XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Permission
{
    /// <summary>
    /// 接口资源
    /// </summary>
    public class BackgroundApiDto
    {
        /// <summary>
        ///  一级节点
        /// </summary>
        public bool PrimaryNode { get; set; }

        /// <summary>
        ///  模块
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 父节点权限编码
        /// </summary>
        public string ParentPermissionCode { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        public string PermissionCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnName { get; set; }

        /// <summary>
        /// 接口路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortOrder { get; set; }
    }
}
