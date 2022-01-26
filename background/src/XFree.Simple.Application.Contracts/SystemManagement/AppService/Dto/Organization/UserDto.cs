using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.Shared.SystemManagement.Enum;

namespace XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization
{

    /// <summary>
    /// 分页请求
    /// </summary>
    public class UserPagedAndSortedRequestDto : PagedAndSortedRequestDto 
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string EmployeeIDNumber { get; set; }
    }

    /// <summary>
    /// 用户
    /// </summary>
    public class UserDto
    {
        /// <summary>
        ///  用户Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public string DepartId { get; set; }

        /// <summary>
        /// 职务Id
        /// </summary>
        public string PostId { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 昵称/姓名
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string EmployeeIDNumber { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 性别(0-默认未知,1-男,2-女)
        /// </summary>
        public SexType Sex { get; set; }

        /// <summary>
        /// 账号类型: ( 1: 平台   2:租户用户)
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///  备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 状态(1-正常,2-锁定)
        /// </summary>
        public NormalLockedStatus Status { get; set; }

        /// <summary>
        /// 最后登录ip
        /// </summary>
        public string LoginIp { get; set; }

        /// <summary>
        /// 登录锁定
        /// </summary>
        public bool LockLogin { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LoginDate { get; set; }

        /// <summary>
        /// 最后密码更改时间
        /// </summary>
        public DateTime? PwdUpdateDate { get; set; }

        /// <summary>
        ///  管理员
        /// </summary>
        public bool SupperUser { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string[] RoleIds { get; set; }

        /// <summary>
        /// 角色编码
        /// </summary>
        public string RoleCodes { get; set; }

        /// <summary>
        /// 部门编码
        /// </summary>
        public string DepartCode { get; set; }

        /// <summary>
        /// 职务编码
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        ///  角色名称
        /// </summary>
        public string RoleNames { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        public string PostName { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [JsonIgnore]
        public string Password { get; set; }

    }

    /// <summary>
    /// 创建用户
    /// </summary>
    public class CreateUserDto
    {

        /// <summary>
        /// 租户Id
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public string DepartId { get; set; }

        /// <summary>
        /// 职务Id
        /// </summary>
        public string PostId { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        [Required]
        public string LoginName { get; set; }

        /// <summary>
        /// 昵称/姓名
        /// </summary>
        [Required]
        public string Nickname { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [Required]
        public string EmployeeIDNumber { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 性别(0-默认未知,1-男,2-女)
        /// </summary>
        public SexType Sex { get; set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///  备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        ///  管理员
        /// </summary>
        public bool SupperUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string[] RoleIds { get; set; }

    }

    /// <summary>
    /// 更新用户
    /// </summary>
    public class UpdateUserDto
    {

        /// <summary>
        /// 部门Id
        /// </summary>
        public string DepartId { get; set; }

        /// <summary>
        /// 职务Id
        /// </summary>
        public string PostId { get; set; }

        /// <summary>
        /// 昵称/姓名
        /// </summary>
        [Required]
        public string Nickname { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [Required]
        public string EmployeeIDNumber { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 性别(0-默认未知,1-男,2-女)
        /// </summary>
        public SexType Sex { get; set; }

        /// <summary>
        /// 账号类型: ( 1: 平台   2:租户用户)
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///  备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string[] RoleIds { get; set; }

        /// <summary>
        ///  管理员
        /// </summary>
        public bool SupperUser { get; set; }
    }

    /// <summary>
    /// 更新密码
    /// </summary>
    public class UpdateUserPasswordDto
    {

        /// <summary>
        /// 旧密码
        /// </summary>
        [Required]
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }

    }

    /// <summary>
    /// 更新状态
    /// </summary>
    public class UpdateUserStatusDto
    {

        /// <summary>
        /// 状态
        /// </summary>
        public NormalLockedStatus Status { get; set; }

    }

}
