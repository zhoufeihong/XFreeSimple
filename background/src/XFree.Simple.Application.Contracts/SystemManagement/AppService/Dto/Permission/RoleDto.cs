using System;
using System.Collections.Generic;
using System.Text;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.Shared.SystemManagement.Enum;

namespace XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Permission
{

    /// <summary>
    /// 角色
    /// </summary>
    public class RoleDto
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 角色编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public NormalLockedStatus Status { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 可访问权限类型
        /// </summary>
        public RoleAccessType RoleAccessType { get; set; }

        /// <summary>
        ///  访问权限编码
        /// </summary>
        public string AccessValue { get; set; }
    }

    /// <summary>
    /// 创建角色
    /// </summary>
    public class CreateRoleDto
    {

        /// <summary>
        /// 角色编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 可访问权限类型
        /// </summary>
        public RoleAccessType RoleAccessType { get; set; }

        /// <summary>
        ///  访问权限编码
        /// </summary>
        public string AccessValue { get; set; }
    }

    /// <summary>
    /// 更新角色
    /// </summary>
    public class UpdateRoleDto
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 可访问权限类型
        /// </summary>
        public RoleAccessType RoleAccessType { get; set; }

        /// <summary>
        ///  访问权限编码
        /// </summary>
        public string AccessValue { get; set; }
    }

    /// <summary>
    /// 更新状态
    /// </summary>
    public class UpdateRoleStatusDto
    {
        /// <summary>
        /// 
        /// </summary>
        public NormalLockedStatus Status { get; set; }
    }

    /// <summary>
    /// 分页请求
    /// </summary>
    public class RolePagedAndSortedRequestDto : PagedAndSortedRequestDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
    }

}
