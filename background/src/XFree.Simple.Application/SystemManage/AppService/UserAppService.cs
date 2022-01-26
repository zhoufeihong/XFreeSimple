using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;
using XFree.Simple.Application.Common;
using XFree.Simple.Application.Contracts.SystemManage;
using XFree.Simple.Application.Contracts.SystemManage.AppService;
using XFree.Simple.Application.Contracts.SystemManagement;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Authentication.Dto;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Permission;
using XFree.Simple.Application.Contracts.SystemManagement.Permission;
using XFree.Simple.Application.Contracts.SystemManagement.Setting;
using XFree.Simple.Application.SystemManage.DataFilter;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.Shared.SystemManagement;
using XFree.Simple.Domain.Shared.SystemManagement.Domain;
using XFree.Simple.Domain.Shared.SystemManagement.Enum;
using XFree.Simple.Domain.SystemManagement;
using XFree.Simple.Domain.SystemManagement.Organization;
using XFree.Simple.Domain.SystemManagement.Permission;

namespace XFree.Simple.Application.SystemManage.AppService
{
    [Authorize(SystemManagementPermissions.Base.WithToken)]
    public class UserAppService : ApplicationService, IUserAppService
    {
        private readonly UserDataFilterService _userDataFilterService;
        private readonly UserManager _userManager;
        private readonly IRepository<User, string> _userRepository;
        private readonly IRepository<Post, string> _postRepository;
        private readonly IRepository<Depart, string> _departRepository;
        private readonly IRepository<UserRole, string> _userRoleRepository;
        private readonly IRepository<Role, string> _roleRepository;
        private readonly IRepository<RoleUiPermission, string> _roleUiPermissionRepository;
        private readonly IRepository<UiPermission, string> _uiPermissionRepository;
        private readonly IRepository<OperationInfo, string> _operationInfoRepository;
        private readonly string DefaultAvatar = "";
        private readonly UserRoleManager _userRoleManager;
        private readonly IDistributedCache<WebApiResult<List<UiPermissionDto>>> _distributedCache;
        private readonly ErrorMessageService _errorMessageService;

        public UserAppService(UserManager userManager,
            IRepository<User, string> userRepository,
            IRepository<OperationInfo, string> operationInfoRepository,
            UserDataFilterService userDataFilterService,
            UserRoleManager userRoleManager,
            IRepository<Post, string> postRepository,
            IRepository<Depart, string> departRepository,
            IRepository<UserRole, string> userRoleRepository,
            IRepository<Role, string> roleRepository,
            IRepository<RoleUiPermission, string> roleUiPermissionRepository,
            IRepository<UiPermission, string> uiPermissionRepository,
            IDistributedCache<WebApiResult<List<UiPermissionDto>>> distributedCache,
            ErrorMessageService errorMessageService)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _operationInfoRepository = operationInfoRepository;
            _userDataFilterService = userDataFilterService;
            _userRoleManager = userRoleManager;
            _postRepository = postRepository;
            _departRepository = departRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _roleUiPermissionRepository = roleUiPermissionRepository;
            _uiPermissionRepository = uiPermissionRepository;
            _distributedCache = distributedCache;
            _errorMessageService = errorMessageService;
        }

        public async Task<WebApiResult<PagedEResultDto<UserDto>>> GetListPagedAsync(UserPagedAndSortedRequestDto input)
        {
            await NormalizeMaxResultCountAsync(input);
            return await GetNoMaxListPagedAsync(input);
        }

        [Authorize(SystemManagementPermissions.Users.Export)]
        public async Task<WebApiResult<PagedEResultDto<UserDto>>> GetExportListPagedAsync(UserPagedAndSortedRequestDto input)
        {
            input.PageIndex = 1;
            input.PageSize = int.MaxValue;
            return await GetNoMaxListPagedAsync(input);
        }

        /// <summary>
        /// 
        /// </summary>
        private readonly static SemaphoreSlim ImportSemaphore = new(1, 1);

        [Authorize(SystemManagementPermissions.Users.Import)]
        public async Task<WebApiResult<ImportResult>> ImportAsync(List<UserDto> inputs)
        {
            // 加锁
            using var lockResult = await ImportSemaphore.LockResultTimeOutAsync(30000, L);
            var maxLimit = 300;
            if (inputs.Count > maxLimit)
            {
                _errorMessageService.ThrowMessageParam(FriendlyExceptionCode.ExcelImport004, maxLimit.ToString());
            }
            var checkResult = await CheckParamAndSetIds(inputs);
            if (checkResult != null)
            {
                return checkResult;
            }
            foreach (var input in inputs)
            {
                var newUser = _userManager.New();
                newUser.LoginName = input.LoginName;
                newUser.Birthday = input.Birthday;
                newUser.DepartId = input.DepartId;
                newUser.Email = input.Email;
                newUser.EmployeeIDNumber = input.EmployeeIDNumber;
                newUser.Memo = input.Memo;
                newUser.Nickname = input.Nickname;
                newUser.Phone = input.Phone;
                newUser.PostId = input.PostId;
                newUser.Sex = input.Sex;
                newUser.UserType = UserType.Tenant;
                newUser.SupperUser = input.SupperUser;
                newUser.SetPassword(input.Password);
                await _userRoleManager.SetUserRole(newUser.Id, input.RoleIds, true);
                await _userRepository.InsertAsync(newUser);
            }
            return WebApiResult<ImportResult>.SuccessResult(new()
            {
                Total = inputs.Count,
                SuccessfulCount = inputs.Count,
                UpdatedCount = 0,
                CreatedCount = inputs.Count,
                FailedCount = 0
            });
        }

        public async Task<WebApiResult<UserDto>> GetAsync(string id)
        {
            var user = await _userRepository.GetAsync(id);

            return WebApiResult<UserDto>.SuccessResult(ObjectMapper.Map<User, UserDto>(user));
        }

        public async Task<WebApiResult<UserDto>> GetByTokenAsync()
        {
            var user = await _userRepository.GetAsync(this.CurrentUser.Id.ToString());
            var userDto = ObjectMapper.Map<User, UserDto>(user);
            userDto.Avatar = DefaultAvatar;
            return WebApiResult<UserDto>.SuccessResult(userDto);
        }

        [Authorize(SystemManagementPermissions.Users.Create)]
        public async Task<WebApiResult<UserDto>> CreateAsync(CreateUserDto input)
        {
            var supperUser = input.SupperUser;
            await TrySetSuperUser(supperUser);
            var newUser = _userManager.New();
            newUser.LoginName = input.LoginName;
            newUser.Birthday = input.Birthday;
            newUser.DepartId = input.DepartId;
            newUser.Email = input.Email;
            newUser.EmployeeIDNumber = input.EmployeeIDNumber;
            newUser.Memo = input.Memo;
            newUser.Nickname = input.Nickname;
            newUser.Phone = input.Phone;
            newUser.PostId = input.PostId;
            newUser.Sex = input.Sex;
            newUser.UserType = input.UserType;
            newUser.SupperUser = input.SupperUser;
            newUser.SetPassword(input.Password);
            var user = await _userManager.CreateUserAsync(newUser);
            await _userRoleManager.SetUserRole(newUser.Id, input.RoleIds, true);
            return WebApiResult<UserDto>.SuccessResult(ObjectMapper.Map<User, UserDto>(user));
        }

        [Authorize(SystemManagementPermissions.Users.Update)]
        public async Task<WebApiResult<UserDto>> UpdateAsync(string id, UpdateUserDto input)
        {
            var supperUser = input.SupperUser;
            await TrySetSuperUser(supperUser);
            var user = await _userRepository.GetAsync(id);
            user.Avatar = input.Avatar;
            user.Birthday = input.Birthday;
            user.DepartId = input.DepartId;
            user.Email = input.Email;
            user.EmployeeIDNumber = input.EmployeeIDNumber;
            user.Memo = input.Memo;
            user.Nickname = input.Nickname;
            user.Phone = input.Phone;
            user.PostId = input.PostId;
            user.Sex = input.Sex;
            user.UserType = input.UserType;
            await _userManager.UpdateUserAsync(user);
            await _userRoleManager.SetUserRole(user.Id, input.RoleIds);
            return WebApiResult<UserDto>.SuccessResult(ObjectMapper.Map<User, UserDto>(user));
        }

        [Authorize(SystemManagementPermissions.Users.Update)]
        public async Task<WebApiResult<UserDto>> UpdateStatusAsync(string id, UpdateUserStatusDto updateUserStatusDto)
        {
            var user = await _userRepository.GetAsync(id);
            user.SetStatus(updateUserStatusDto.Status);
            return WebApiResult<UserDto>.SuccessResult(ObjectMapper.Map<User, UserDto>(user));
        }

        [Authorize(SystemManagementPermissions.Base.ChangePassword)]
        public async Task<WebApiResult<UserDto>> UpdatePasswordAsync(string id, UpdateUserPasswordDto updateUserPasswordDto)
        {
            var user = await _userRepository.GetAsync(id);
            if (!user.IsTruePassword(updateUserPasswordDto.OldPassword))
            {
                _errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.LoginError102);
            }
            user.UpdatePassword(updateUserPasswordDto.NewPassword.Trim());
            return WebApiResult<UserDto>.SuccessResult(ObjectMapper.Map<User, UserDto>(user));
        }

        [Authorize(SystemManagementPermissions.Users.ResetPassword)]
        public async Task<WebApiResult> ResetPassword(string id)
        {
            var userId = this.CurrentUser.Id.ToString();
            var (supperUser, departIds) = await _userDataFilterService.GetDepartIds(userId);
            var user = await _userRepository.GetAsync(id);
            // 验证权限
            if (!supperUser && !departIds.Contains(user.DepartId))
            {
                _errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.LoginError109);
            }
            user.UpdatePassword("123456");
            return WebApiResult<UserDto>.SuccessResult(ObjectMapper.Map<User, UserDto>(user));
        }

        [Authorize(SystemManagementPermissions.Users.Delete)]
        public async Task<WebApiResult> DeleteAsync(string id)
        {
            var entity = await _userRepository.GetAsync(id);
            if (entity.SupperUser)
            {
                _errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.UserError119);
            }
            await _userRepository.DeleteAsync(id);
            return WebApiResult.SuccessResult();
        }

        public async Task<WebApiResult<UserPermisionInfo>> GetUserPermisionInfo()
        {
            var userId = CurrentUser.Id.ToString();
            var user = await _userRepository.GetAsync(userId);
            var userRoles = await _userRoleRepository.Where(w => w.UserId == userId).Join(_roleRepository, u => u.RoleId, r => r.Id, (u, r) => r).ToListAsync();
            var tenantLanguage = CurrentUser.FindClaim(Const.ClaimType.TENANT_LANGUAGE)?.Value;
            return WebApiResult<UserPermisionInfo>.SuccessResult(new UserPermisionInfo
            {
                Id = userId,
                DepartId = user.DepartId,
                Name = user.Nickname,
                SupperUser = user.SupperUser,
                Roles = userRoles.Select(s => s.Name).ToArray(),
                Avatar = DefaultAvatar,
                TenantLanguage = tenantLanguage
            });
        }

        public async Task<WebApiResult<List<UiPermissionDto>>> GetUiPermission()
        {
            var userId = CurrentUser.Id.ToString();
            return await _distributedCache.GetOrAddAsync($"{nameof(UserAppService)}_{nameof(GetUiPermission)}_{userId}", async () =>
             {
                 var user = await _userRepository.GetAsync(userId);
                 if (user.SupperUser)
                 {
                     return await ToUiPermissionResult(_uiPermissionRepository.Where(w => w.Enabled == true));
                 }
                 var uiPermissionQuery = (from ur in _userRoleRepository.Where(w => w.UserId == userId)
                                          join ru in _roleUiPermissionRepository on ur.RoleId equals ru.RoleId
                                          join r in _roleRepository.Where(w => w.Status == NormalLockedStatus.Normal) on ru.RoleId equals r.Id
                                          join u in _uiPermissionRepository.Where(w => w.Enabled == true) on ru.UiPermissionId equals u.Id
                                          select u).Distinct();
                 return await ToUiPermissionResult(uiPermissionQuery);
             }, () => new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions
             {
                 AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
             });
        }

        public async Task<WebApiResult<PagedEResultDto<OperationInfoDto>>> GetOperationInfoListPagedAsync(PagedAndSortedRequestDto input)
        {
            var resultQuery = _operationInfoRepository
                .Where(w => w.UserId == CurrentUser.Id.ToString())
                .OrderBy(input.Sorting ?? "CreationTime desc");
            return await resultQuery.GetListPaged<OperationInfo, OperationInfoDto>(ObjectMapper, input);
        }

        #region private

        private async Task TrySetSuperUser(bool supperUser)
        {
            if (supperUser)
            {
                var currentUser = await _userRepository.GetAsync(CurrentUser.Id.ToString());
                // 只有管理员能设置管理员权限
                if (!currentUser.SupperUser)
                {
                    _errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.LoginError109);
                }
            }
        }

        private async Task<WebApiResult<ImportResult>> CheckParamAndSetIds(List<UserDto> inputs)
        {
            var loginNames = inputs.Select(s => s.LoginName).Distinct();
            var employeeIDNumbers = inputs.Select(s => s.EmployeeIDNumber).Distinct();
            if (inputs.Count != loginNames.Count())
            {
                _errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.UserError114);
            }
            if (inputs.Count != employeeIDNumbers.Count())
            {
                _errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.UserError115);
            }
            // 账号重复
            var existingUsers = await _userRepository.Where(w => loginNames.Contains(w.LoginName)).ToListAsync();
            if (existingUsers.Count > 0)
            {
                return WebApiResult<ImportResult>.SuccessResult(new()
                {
                    Total = inputs.Count,
                    SuccessfulCount = 0,
                    UpdatedCount = 0,
                    CreatedCount = 0,
                    FailedCount = inputs.Count,
                    ItemErrors = existingUsers.Select(s => new ItemError { Key = s.LoginName, Message = _errorMessageService.GetMessage(SystemFriendlyExceptionCode.UserError114) }).ToList()
                });
            }
            // 工号重复
            existingUsers = await _userRepository.Where(w => employeeIDNumbers.Contains(w.EmployeeIDNumber)).ToListAsync();
            if (existingUsers.Count > 0)
            {
                return WebApiResult<ImportResult>.SuccessResult(new()
                {
                    Total = inputs.Count,
                    SuccessfulCount = 0,
                    UpdatedCount = 0,
                    CreatedCount = 0,
                    FailedCount = inputs.Count,
                    ItemErrors = existingUsers.Select(s => new ItemError { Key = s.LoginName, Message = _errorMessageService.GetMessage(SystemFriendlyExceptionCode.UserError115) }).ToList()
                });
            }
            // 职务编码
            var postCodes = inputs.Select(s => s.PostCode).Where(w => !string.IsNullOrEmpty(w)).Distinct().ToList();
            // 部门编码
            var departCodes = inputs.Select(s => s.DepartCode).Where(w => !string.IsNullOrEmpty(w)).Distinct().ToList();
            // 角色编码
            var roleCodes = inputs.SelectMany(s => s.RoleCodes?.Split(',') ?? Array.Empty<string>()).Where(w => !string.IsNullOrEmpty(w)).Distinct().ToList();
            var posts = await _postRepository.Where(w => postCodes.Contains(w.Code)).ToListAsync();
            var departs = await _departRepository.Where(w => departCodes.Contains(w.OrgCode)).ToListAsync();
            var roles = await _roleRepository.Where(w => roleCodes.Contains(w.Code)).ToListAsync();
            var itemErrors = new List<ItemError>();
            inputs.ForEach(input =>
            {
                // 职务编码校验
                if (!string.IsNullOrEmpty(input.PostCode))
                {
                    var post = posts.FirstOrDefault(f => f.Code == input.PostCode);
                    if (post == null)
                    {
                        itemErrors.Add(new ItemError { Key = input.LoginName, Message = _errorMessageService.GetMessage(SystemFriendlyExceptionCode.UserError117) });
                        return;
                    }
                    input.PostId = post.Id;
                }
                // 角色编码校验
                if (!string.IsNullOrEmpty(input.RoleCodes))
                {
                    var roleIds = new List<string>();
                    foreach (var roleCode in input.RoleCodes.Split(',').Where(w => !string.IsNullOrEmpty(w)))
                    {
                        var role = roles.FirstOrDefault(f => f.Code == roleCode);
                        if (role == null)
                        {
                            itemErrors.Add(new ItemError { Key = input.LoginName, Message = _errorMessageService.GetMessage(SystemFriendlyExceptionCode.UserError118) });
                            return;
                        }
                        roleIds.Add(role.Id);
                    }
                    input.RoleIds = roleIds.ToArray();
                }
                // 部门编码
                var depart = departs.FirstOrDefault(f => f.OrgCode == input.DepartCode);
                if (depart == null)
                {
                    itemErrors.Add(new ItemError { Key = input.LoginName, Message = _errorMessageService.GetMessage(SystemFriendlyExceptionCode.UserError116) });
                    return;
                }
                input.DepartId = depart.Id;
            });
            if (itemErrors.Count > 0)
            {
                return WebApiResult<ImportResult>.SuccessResult(new()
                {
                    Total = inputs.Count,
                    SuccessfulCount = 0,
                    UpdatedCount = 0,
                    CreatedCount = 0,
                    FailedCount = inputs.Count,
                    ItemErrors = itemErrors
                });
            }
            return null;
        }

        private async Task<WebApiResult<PagedEResultDto<UserDto>>> GetNoMaxListPagedAsync(UserPagedAndSortedRequestDto input)
        {
            var userId = this.CurrentUser.Id.ToString();
            var (supperUser, departIds) = await _userDataFilterService.GetDepartIds(userId);
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
                            DepartName = d.OrgName,
                            DepartCode = d.OrgCode,
                            PostCode = p.Code
                        };
            var resultQuery = query
                .WhereIf(!string.IsNullOrEmpty(input.LoginName), w => w.LoginName.Contains(input.LoginName))
                .WhereIf(!string.IsNullOrEmpty(input.Nickname), w => w.Nickname.Contains(input.Nickname))
                .WhereIf(!string.IsNullOrEmpty(input.EmployeeIDNumber), w => w.EmployeeIDNumber == input.EmployeeIDNumber)
                .WhereIf(!supperUser, w => departIds.Contains(w.DepartId))
                .OrderBy(input.Sorting ?? "CreationTime");

            var result = await resultQuery.GetDtoListPaged(input);

            if (result != null && result.Data.Data != null && result.Data.Data.Count > 0)
            {
                var userRoles = await _userRoleManager.GetUserRoles(result.Data.Data.Select(userDto => userDto.Id));
                var userRoleIds = userRoles.Select(s => s.RoleId).Distinct();
                var roles = await _roleRepository.Where(w => userRoleIds.Contains(w.Id)).ToListAsync();
                foreach (var tempResultItem in result.Data.Data)
                {
                    var userRoleList = userRoles.Where(w => w.UserId == tempResultItem.Id);
                    tempResultItem.RoleIds = userRoleList.Select(s => s.RoleId).ToArray();
                    tempResultItem.RoleCodes = string.Join(",", roles.Where(w => userRoleList.Any(ur => ur.RoleId == w.Id)).Select(s => s.Code));
                }
            }
            return result;
        }

        private async Task<WebApiResult<List<UiPermissionDto>>> ToUiPermissionResult(IQueryable<UiPermission> uiPermissionQuery)
        {
            var uiPermissions = await uiPermissionQuery.ToListAsync();
            var resultDtos = ObjectMapper.Map<List<UiPermission>, List<UiPermissionDto>>(uiPermissions);
            return WebApiResult<List<UiPermissionDto>>.SuccessResult(resultDtos);
        }

        private async Task NormalizeMaxResultCountAsync(PagedAndSortedRequestDto input)
        {
            var maxPageSize = (await SettingProvider.GetOrNullAsync(SystemManagementSettings.MaxPageSize))?.To<int>();
            if (maxPageSize.HasValue && input.PageSize > maxPageSize.Value)
            {
                input.PageSize = maxPageSize.Value;
            }
        }

        #endregion
    }
}
