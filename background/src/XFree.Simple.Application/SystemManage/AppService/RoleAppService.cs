using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using XFree.Simple.Application.Contracts.SystemManagement;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.SystemManagement.Permission;
using System.Linq.Dynamic.Core;
using XFree.Simple.Application.Common;
using XFree.Simple.Application.Contracts.Common;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using XFree.Simple.Domain.SystemManagement.Organization;
using Microsoft.AspNetCore.Authorization;
using XFree.Simple.Domain.Shared.SystemManagement.Enum;
using XFree.Simple.Application.Contracts.SystemManagement.Permission;
using XFree.Simple.Application.Contracts.SystemManagement.AppService;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Permission;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization;

namespace XFree.Simple.Application.SystemManage.AppService
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(SystemManagementPermissions.Base.WithToken)]
    public class RoleAppService : ApplicationService, IRoleAppService
    {

        private readonly IRepository<Role, string> _roleRepository;

        private readonly IRepository<RoleUiPermission, string> _roleUiPermissionRepository;

        private readonly IRepository<User, string> _userRepository;
        private readonly IRepository<Post, string> _postRepository;
        private readonly IRepository<Depart, string> _departRepository;
        private readonly IRepository<UserRole, string> _userRoleRepository;
        private readonly ICheckUserPermission _checkUserPermission;

        private readonly ICodeGenerator _codeGenerator;

        private readonly RoleManager _roleManager;

        private readonly string RoleCodePrefix = "R";

        public RoleAppService(IRepository<Role, string> roleRepository,
            ICodeGenerator codeGenerator,
            RoleManager roleManager,
            IRepository<RoleUiPermission, string> roleUiPermissionRepository,
            IRepository<User, string> userRepository,
            IRepository<Post, string> postRepository,
            IRepository<Depart, string> departRepository,
            IRepository<UserRole, string> userRoleRepository,
            ICheckUserPermission checkUserPermission)
        {
            _roleRepository = roleRepository;
            _codeGenerator = codeGenerator;
            _roleManager = roleManager;
            _roleUiPermissionRepository = roleUiPermissionRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _departRepository = departRepository;
            _userRoleRepository = userRoleRepository;
            _checkUserPermission = checkUserPermission;
        }

        [Authorize(SystemManagementPermissions.Roles.Create)]
        public async Task<WebApiResult<RoleDto>> CreateAsync(CreateRoleDto input)
        {
            var newRole = new Role(GuidGenerator.Create().ToString())
            {
                Code = input.Code
            };
            if (string.IsNullOrEmpty(newRole.Code))
            {
                newRole.Code = await _codeGenerator.Create(RoleCodePrefix);
            }
            newRole.Memo = input.Memo;
            newRole.Name = input.Name;
            newRole.RoleAccessType = input.RoleAccessType;
            newRole.AccessValue = input.AccessValue;
            await _roleManager.CreateRoleAsync(newRole);
            return WebApiResult<RoleDto>.SuccessResult(ObjectMapper.Map<Role, RoleDto>(newRole));
        }

        public async Task<WebApiResult<ListResultDto<RoleDto>>> GetListAsync()
        {
            var userId = CurrentUser.Id.ToString();
            var currentUser = await _userRepository.GetAsync(userId);
            var items = await _roleRepository.GetListAsync();
            List<Role> tempItems;
            if (currentUser.SupperUser)
            {
                tempItems = items;
            }
            else
            {
                var roleIds = await _userRoleRepository.Where(w => w.UserId == userId).Select(s => s.RoleId).ToListAsync();
                // 返回可以访问的角色信息
                tempItems = new();
                foreach (var item in items)
                {
                    if (item.RoleAccessType == RoleAccessType.Public || roleIds.Contains(item.Id))
                    {
                        tempItems.Add(item);
                    }
                    else if (item.RoleAccessType == RoleAccessType.WithAccessCode && !string.IsNullOrEmpty(item.AccessValue))
                    {
                        if (await _checkUserPermission.CheckUiPermission(userId, item.AccessValue))
                        {
                            tempItems.Add(item);
                        }
                    }
                }
            }

            var itemDtos = ObjectMapper.Map<List<Role>, List<RoleDto>>(tempItems);

            return WebApiResult<ListResultDto<RoleDto>>.SuccessResult(new ListResultDto<RoleDto>(itemDtos));
        }

        [Authorize(SystemManagementPermissions.Roles.Delete)]
        public async Task<WebApiResult> DeleteAsync(string id)
        {
            await _roleRepository.DeleteAsync(id);
            return WebApiResult.SuccessResult();
        }

        public async Task<WebApiResult<RoleDto>> GetAsync(string id)
        {
            var role = await _roleRepository.GetAsync(id);

            return WebApiResult<RoleDto>.SuccessResult(ObjectMapper.Map<Role, RoleDto>(role));
        }

        public async Task<WebApiResult<PagedEResultDto<RoleDto>>> GetListPagedAsync(RolePagedAndSortedRequestDto input)
        {
            var resultQuery = _roleRepository
                              .WhereIf(!string.IsNullOrEmpty(input.Code), w => w.Code == input.Code)
                              .WhereIf(!string.IsNullOrEmpty(input.Name), w => w.Name == input.Name)
                              .OrderBy(input.Sorting ?? "CreationTime");

            return await resultQuery.GetListPaged<Role, RoleDto>(ObjectMapper, input);
        }

        [Authorize(SystemManagementPermissions.Roles.Update)]
        public async Task<WebApiResult> GrantUiPermission(string id, string[] permissionIds)
        {
            await _roleUiPermissionRepository.DeleteAsync(d => d.RoleId == id);
            if (permissionIds != null)
            {
                var permissions = permissionIds.Distinct().Select(s => new RoleUiPermission(GuidGenerator.Create().ToString())
                {
                    RoleId = id,
                    UiPermissionId = s
                });
                await _roleUiPermissionRepository.InsertManyAsync(permissions);
            }
            return WebApiResult.SuccessResult();
        }

        public async Task<WebApiResult<List<string>>> QueryUiPermissionIds(string id)
        {
            var permissionIds = await _roleUiPermissionRepository.Where(w => w.RoleId == id).Select(s => s.UiPermissionId).ToListAsync();
            return WebApiResult<List<string>>.SuccessResult(permissionIds);
        }

        [Authorize(SystemManagementPermissions.Roles.Update)]
        public async Task<WebApiResult<RoleDto>> UpdateAsync(string id, UpdateRoleDto input)
        {
            var role = await _roleRepository.GetAsync(id);
            role.Name = input.Name;
            role.Memo = input.Memo;
            role.RoleAccessType = input.RoleAccessType;
            role.AccessValue = input.AccessValue;
            await _roleManager.UpdateRoleAsync(role);
            return WebApiResult<RoleDto>.SuccessResult(ObjectMapper.Map<Role, RoleDto>(role));
        }

        [Authorize(SystemManagementPermissions.Roles.Update)]
        public async Task<WebApiResult<RoleDto>> UpdateStatusAsync(string id, UpdateRoleStatusDto updateUserStatusDto)
        {
            var role = await _roleRepository.GetAsync(id);
            role.Status = updateUserStatusDto.Status;
            return WebApiResult<RoleDto>.SuccessResult(ObjectMapper.Map<Role, RoleDto>(role));
        }

        public async Task<WebApiResult<List<UserDto>>> QueryUsers(string roleId)
        {
            var userId = this.CurrentUser.Id.ToString();
            var query = from u in _userRepository
                        join p in _postRepository on u.PostId equals p.Id into grouping
                        from p in grouping.DefaultIfEmpty()
                        join d in _departRepository on u.DepartId equals d.Id into grouping1
                        from d in grouping1.DefaultIfEmpty()
                        select new UserDto
                        {
                            Id = u.Id,
                            Avatar = u.Avatar,
                            Birthday = u.Birthday,
                            CreationTime = u.CreationTime,
                            DepartId = u.DepartId,
                            Email = u.Email,
                            EmployeeIDNumber = u.EmployeeIDNumber,
                            SupperUser = u.SupperUser,
                            LockLogin = u.LockLogin,
                            LoginDate = u.LoginDate,
                            LoginIp = u.LoginIp,
                            LoginName = u.LoginName,
                            Memo = u.Memo,
                            Nickname = u.Nickname,
                            Phone = u.Phone,
                            PostId = u.PostId,
                            PwdUpdateDate = u.PwdUpdateDate,
                            Sex = u.Sex,
                            Status = u.Status,
                            TenantId = u.TenantId,
                            UserType = u.UserType,
                            PostName = p.Name,
                            DepartName = d.OrgName
                        };
            var resultQuery = query.Where(w => _userRoleRepository.Any(userRole => userRole.UserId == w.Id && userRole.RoleId == roleId))
                .OrderBy("CreationTime");

            var result = await resultQuery.ToListAsync();

            return WebApiResult<List<UserDto>>.SuccessResult(result); ;
        }

    }
}
