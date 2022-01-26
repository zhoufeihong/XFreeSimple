using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using XFree.Simple.Domain.Shared.SystemManagement;
using XFree.Simple.Domain.Shared.SystemManagement.Enum;
using XFree.Simple.Domain.SystemManagement.Organization.Events;
using XFree.Simple.Domain.Shared.Common;
using Volo.Abp.MultiTenancy;

namespace XFree.Simple.Domain.SystemManagement.Organization
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : FullAuditedAggregateRoot<string>, IMultiTenant
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public User(string id)
        {
            Id = id;
            Status = NormalLockedStatus.Normal;
        }

        /// <summary>
        /// 租户Id
        /// </summary>
        public virtual Guid? TenantId { get; set; }

        /// <summary>
        ///  管理员用户
        /// </summary>
        public virtual bool SupperUser { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public virtual string DepartId { get; set; }

        /// <summary>
        /// 职务Id
        /// </summary>
        public virtual string PostId { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public virtual string LoginName { get; set; }

        /// <summary>
        /// 昵称/姓名
        /// </summary>
        public virtual string Nickname { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// 密码盐
        /// </summary>
        public virtual string Salt { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public virtual string Avatar { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public virtual string EmployeeIDNumber { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public virtual DateTime? Birthday { get; set; }

        /// <summary>
        /// 性别(0-默认未知,1-男,2-女)
        /// </summary>
        public virtual SexType Sex { get; set; }

        /// <summary>
        /// 账号类型: ( 1: 平台   2:租户用户)
        /// </summary>
        public virtual UserType UserType { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public virtual string Phone { get; set; }

        /// <summary>
        ///  备注
        /// </summary>
        public virtual string Memo { get; set; }

        /// <summary>
        /// 状态(1-正常,2-锁定)
        /// </summary>
        public virtual NormalLockedStatus Status { get; set; }

        /// <summary>
        /// 最后登录ip
        /// </summary>
        public virtual string LoginIp { get; set; }

        /// <summary>
        /// 登录锁定
        /// </summary>
        public virtual bool LockLogin { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 最后密码更改时间
        /// </summary>
        public virtual DateTime? PwdUpdateDate { get; set; }

        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="password"></param>
        public void SetPassword(string password)
        {
            Salt = Guid.NewGuid().ToString();
            Password = GetPasswordSignature(password, Salt);
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="status"></param>
        public void SetStatus(NormalLockedStatus status)
        {
            Status = status;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="password"></param>
        public void UpdatePassword(string password)
        {
            Salt = Guid.NewGuid().ToString();
            Password = GetPasswordSignature(password, Salt);
            PwdUpdateDate = DateTime.Now;
            AddLocalEvent(new OperationEvent
            {
                Title = "修改密码"
            });
        }

        /// <summary>
        /// 判断密码是否正确
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsTruePassword(string password)
        {
            return Password == GetPasswordSignature(password, Salt);
        }

        /// <summary>
        /// 获取密码+盐的签名
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private static string GetPasswordSignature(string password, string salt)
        {
            return (password + salt).ToMd5();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="errorMessageService"></param>
        /// <param name="password"></param>
        public void Login(ErrorMessageService errorMessageService, string password)
        {
            if (LockLogin)
            {
                errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.LoginError104);
            }
            if (!IsTruePassword(password))
            {
                errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.LoginError102);
            }
            if (Status != NormalLockedStatus.Normal)
            {
                errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.LoginError103);
            }
            LoginDate = DateTime.Now;
            AddLocalEvent(new OperationEvent
            {
                Title = "登录",
                UserId = Id
            });
        }

    }
}
