using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.Shared.SystemManagement.Enum;
using XFree.Simple.Domain.SystemManagement.Organization;
using XFree.Simple.Domain.SystemManagement.Permission;

namespace XFree.Simple.Application.Common.Impl
{
    /// <summary>
    /// 用户接口资源权限校验
    /// </summary>
    public class CheckUserPermission : ICheckUserPermission
    {

        private readonly IMemoryCache _memoryCache;

        private readonly IRepository<User, string> _userRepository;

        private readonly IRepository<Role, string> _roleRepository;

        private readonly IRepository<UserRole, string> _userRoleRepository;

        private readonly IRepository<RoleUiPermission, string> _roleUiPermissionRepository;

        private readonly IRepository<UiPermission, string> _uiPermissionRepository;

        private readonly IRepository<UiWithApi, string> _uiWithApiRepository;

        private readonly IRepository<BackgroundApi, string> _backgroundApiRepository;

        private const string CachePrefix = Const.CacheKeyPrefix.USER;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memoryCache"></param>
        /// <param name="roleRepository"></param>
        /// <param name="userRoleRepository"></param>
        /// <param name="roleUiPermissionRepository"></param>
        /// <param name="uiPermissionRepository"></param>
        /// <param name="uiWithApiRepository"></param>
        public CheckUserPermission(IMemoryCache memoryCache, IRepository<Role, string> roleRepository, IRepository<UserRole, string> userRoleRepository, IRepository<RoleUiPermission, string> roleUiPermissionRepository, IRepository<UiPermission, string> uiPermissionRepository, IRepository<UiWithApi, string> uiWithApiRepository, IRepository<User, string> userRepository, IRepository<BackgroundApi, string> backgroundApiRepository)
        {
            _memoryCache = memoryCache;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _roleUiPermissionRepository = roleUiPermissionRepository;
            _uiPermissionRepository = uiPermissionRepository;
            _uiWithApiRepository = uiWithApiRepository;
            _userRepository = userRepository;
            _backgroundApiRepository = backgroundApiRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissionCode"></param>
        /// <returns></returns>
        public async Task<bool> Check(string userId, string permissionCode)
        {
            var hashPermissionCodes = await _memoryCache.GetOrCreateAsync($"{CachePrefix}:{userId}:PermissionCodes", async (item) =>
            {
                // 设置缓存绝对过期时间
                item.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(180);
                var hashPermissionCodes = new HashSet<string>();
                var user = await _userRepository.GetAsync(userId);
                if (user.Status != NormalLockedStatus.Normal)
                {
                    return hashPermissionCodes;
                }
                // 管理员拥有全部权限
                if (user.SupperUser)
                {
                    var backgroundApis = await _backgroundApiRepository.Select(s => s.PermissionCode).ToListAsync();
                    backgroundApis.ForEach(f => hashPermissionCodes.Add(f));
                    return hashPermissionCodes;
                }
                var permissionCodesQuery = _userRoleRepository.Where(w => w.UserId == userId)
                                                             .Join(_roleRepository.Where(w => w.Status == NormalLockedStatus.Normal), ur => ur.RoleId, r => r.Id, (ur, r) => ur)
                                                             .Join(_roleUiPermissionRepository, ur => ur.RoleId, ru => ru.RoleId, (ur, ru) => ru)
                                                             .Join(_uiPermissionRepository.Where(w => w.Enabled == true), ru => ru.UiPermissionId, u => u.Id, (ru, u) => u)
                                                             .Join(_uiWithApiRepository, u => u.Id, ua => ua.UiPermissionId, (u, ua) => ua).Select(ua => ua.PermissionCode).Distinct();
                var permissionCodes = await permissionCodesQuery.ToListAsync();
                permissionCodes.ForEach(f => hashPermissionCodes.Add(f));
                return hashPermissionCodes;
            });
            return hashPermissionCodes.Contains(permissionCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="perms"></param>
        /// <returns></returns>
        public async Task<bool> CheckUiPermission(string userId, string perms)
        {
            var hashPerms = await _memoryCache.GetOrCreateAsync($"{CachePrefix}:{userId}:UiPerms", async (item) =>
            {
                // 设置缓存绝对过期时间
                item.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(180);
                var hashPerms = new HashSet<string>();
                var user = await _userRepository.GetAsync(userId);
                if (user.Status != NormalLockedStatus.Normal)
                {
                    return hashPerms;
                }
                // 管理员拥有全部权限
                if (user.SupperUser)
                {
                    var backgroundApis = await _uiPermissionRepository.Where(w => w.Enabled == true && w.Perms != null).Select(s => s.Perms).ToListAsync();
                    backgroundApis.ForEach(f => hashPerms.Add(f));
                    return hashPerms;
                }
                var perms = await (from ur in _userRoleRepository.Where(w => w.UserId == userId)
                                   join ru in _roleUiPermissionRepository on ur.RoleId equals ru.RoleId
                                   join r in _roleRepository.Where(w => w.Status == NormalLockedStatus.Normal) on ru.RoleId equals r.Id
                                   join u in _uiPermissionRepository.Where(w => w.Enabled == true && w.Perms != null) on ru.UiPermissionId equals u.Id
                                   select u).Select(s => s.Perms).Distinct().ToListAsync();
                perms.ForEach(f => hashPerms.Add(f));
                return hashPerms;
            });
            return hashPerms.Contains(perms);
        }

    }
}
