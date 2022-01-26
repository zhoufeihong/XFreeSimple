using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace XFree.Simple.Domain.SystemManagement.Permission
{
    /// <summary>
    /// 用户角色管理者
    /// </summary>
    public class UserRoleManager : DomainService
    {

        private readonly IRepository<UserRole, string> _userRoleRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRoleRepository"></param>
        public UserRoleManager(IRepository<UserRole, string> userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <param name="isCreate"></param>
        /// <returns></returns>
        public async Task SetUserRole(string userId, IEnumerable<string> roleIds,bool isCreate = false)
        {
            if (!isCreate)
            {
                var deleteUserRoles = await _userRoleRepository.Where(w => w.UserId == userId).ToListAsync();
                if (deleteUserRoles.Count > 0)
                {
                    await _userRoleRepository.DeleteManyAsync(deleteUserRoles.Select(s => s.Id));
                }
            }
            if (roleIds != null && roleIds.Any())
            {
                await _userRoleRepository.InsertManyAsync(roleIds.Distinct().Select(s => new UserRole(GuidGenerator.Create().ToString())
                {
                    RoleId = s,
                    UserId = userId
                }));
            }
        }

        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public async Task<List<UserRole>> GetUserRoles(IEnumerable<string> userIds)
        {
            return await _userRoleRepository.Where(w => userIds.Contains(w.UserId)).ToListAsync();
        }
    }
}
