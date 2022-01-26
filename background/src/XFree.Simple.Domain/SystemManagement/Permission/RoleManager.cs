using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XFree.Simple.Domain.Shared.Common;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using XFree.Simple.Domain.Shared.SystemManagement;
using XFree.Simple.Domain.Shared.SystemManagement.Domain;

namespace XFree.Simple.Domain.SystemManagement.Permission
{
    /// <summary>
    /// 角色管理者
    /// </summary>
    public class RoleManager : DomainService
    {

        private readonly IRepository<Role, string> _roleRepository;

        private readonly ErrorMessageService _errorMessageService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleRepository"></param>
        /// <param name="errorMessageService"></param>
        public RoleManager(IRepository<Role, string> roleRepository,
            ErrorMessageService errorMessageService)
        {
            _roleRepository = roleRepository;
            _errorMessageService = errorMessageService;
        }

        /// <summary>
        ///  创建角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<Role> CreateRoleAsync(Role role)
        {
            var existingRole = await _roleRepository.FirstOrDefaultAsync(p => p.Code == role.Code);
            if (existingRole != null)
            {
                _errorMessageService.ThrowMessageParam(SystemFriendlyExceptionCode.DuplicateRoleCode301, existingRole.Code);
            }
            return await _roleRepository.InsertAsync(role);
        }

        /// <summary>
        ///  更新角色信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<Role> UpdateRoleAsync(Role role)
        {
            var existingUsers = await AsyncExecuter.ToListAsync(_roleRepository.Where(p => p.Code == role.Code));
            if (existingUsers != null && existingUsers.Any(a => a.Id != role.Id))
            {
                _errorMessageService.ThrowMessageParam(SystemFriendlyExceptionCode.DuplicateRoleCode301, role.Code);
            }
            return await _roleRepository.UpdateAsync(role);
        }

    }
}
