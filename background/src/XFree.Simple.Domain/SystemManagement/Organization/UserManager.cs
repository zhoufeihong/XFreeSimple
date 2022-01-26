using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using XFree.Simple.Domain.Shared.SystemManagement;
using XFree.Simple.Domain.Shared.SystemManagement.Domain;
using XFree.Simple.Domain.SystemManagement.Permission;
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.Domain.SystemManagement.Organization
{
    /// <summary>
    ///  用户管理者
    /// </summary>
    public class UserManager : DomainService
    {
        private readonly IRepository<User, string> _userRepository;

        private readonly ErrorMessageService _errorMessageService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="errorMessageService"></param>
        public UserManager(IRepository<User, string> userRepository,
            ErrorMessageService errorMessageService)
        {
            _userRepository = userRepository;
            _errorMessageService = errorMessageService;
        }

        /// <summary>
        /// 新建用户实例
        /// </summary>
        /// <returns></returns>
        public User New()
        {
            return new User(GuidGenerator.Create().ToString());
        }

        /// <summary>
        ///  新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> CreateUserAsync(User user)
        {
            var existingUser = await _userRepository.FirstOrDefaultAsync(p => p.EmployeeIDNumber == user.EmployeeIDNumber || p.LoginName == user.LoginName);
            if (existingUser != null)
            {
                if (existingUser.EmployeeIDNumber == user.EmployeeIDNumber)
                    _errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.UserError115);
                if (existingUser.LoginName == user.LoginName)
                    _errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.UserError114);
            }
            return await _userRepository.InsertAsync(user);
        }

        /// <summary>
        ///  更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> UpdateUserAsync(User user)
        {
            var existingUser = await _userRepository.FirstOrDefaultAsync(p => p.Id != user.Id && (p.EmployeeIDNumber == user.EmployeeIDNumber || p.LoginName == user.LoginName));
            if (existingUser != null)
            {
                if (existingUser.EmployeeIDNumber == user.EmployeeIDNumber)
                    _errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.UserError115);
                if (existingUser.LoginName == user.LoginName)
                    _errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.UserError114);
            }
            return await _userRepository.UpdateAsync(user);
        }
    }
}
