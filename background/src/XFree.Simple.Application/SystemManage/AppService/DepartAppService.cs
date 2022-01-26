using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using XFree.Simple.Application.Common;
using XFree.Simple.Application.Contracts.Common;
using XFree.Simple.Application.Contracts.SystemManagement;
using XFree.Simple.Application.Contracts.SystemManagement.AppService;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization;
using XFree.Simple.Application.Contracts.SystemManagement.Permission;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.Shared.SystemManagement;
using XFree.Simple.Domain.SystemManagement.Organization;

namespace XFree.Simple.Application.SystemManage.AppService
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(SystemManagementPermissions.Base.WithToken)]
    public class DepartAppService : ApplicationService, IDepartAppService
    {

        private readonly IRepository<Depart, string> _departRepository;

        private readonly IRepository<User, string> _userRepository;

        private readonly IRepository<Post, string> _postRepository;

        private readonly ICodeGenerator _codeGenerator;

        private readonly string DepartCodePrefix = "D";

        private readonly ErrorMessageService _errorMessageService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departRepository"></param>
        /// <param name="codeGenerator"></param>
        /// <param name="userRepository"></param>
        /// <param name="postRepository"></param>
        /// <param name="errorMessageService"></param>
        public DepartAppService(IRepository<Depart, string> departRepository,
            ICodeGenerator codeGenerator,
            IRepository<User, string> userRepository,
            IRepository<Post, string> postRepository, 
            ErrorMessageService errorMessageService)
        {
            _departRepository = departRepository;
            _codeGenerator = codeGenerator;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _errorMessageService = errorMessageService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(SystemManagementPermissions.Departs.Create)]
        public async Task<WebApiResult<DepartDto>> CreateAsync(CreateDepartDto input)
        {
            var newDepart = new Depart(GuidGenerator.Create().ToString())
            {
                OrgCode = input.OrgCode
            };
            if (string.IsNullOrEmpty(newDepart.OrgCode))
            {
                newDepart.OrgCode = await _codeGenerator.Create(DepartCodePrefix);
            }
            newDepart.Address = input.Address;
            newDepart.Contact = input.Contact;
            newDepart.Memo = input.Memo;
            newDepart.OrgCategory = input.OrgCategory;
            newDepart.OrgLevelType = input.OrgLevelType;
            newDepart.OrgName = input.OrgName;
            newDepart.OrgNameEn = input.OrgNameEn;
            newDepart.OrgType = input.OrgType;
            newDepart.ParentId = input.ParentId;
            newDepart.SortOrder = input.SortOrder;
            newDepart.Status = input.Status;
            var existingDepart = await _departRepository.FirstOrDefaultAsync(p => p.OrgCode == input.OrgCode);
            if (existingDepart != null)
            {
               _errorMessageService.ThrowMessageParam(SystemFriendlyExceptionCode.DuplicateDepartCode201, existingDepart.OrgCode);
            }
            await _departRepository.InsertAsync(newDepart);
            return WebApiResult<DepartDto>.SuccessResult(ObjectMapper.Map<Depart, DepartDto>(newDepart));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<WebApiResult<ListResultDto<DepartDto>>> GetListAsync()
        {
            var items = await _departRepository.GetListAsync();

            var itemDtos = ObjectMapper.Map<List<Depart>, List<DepartDto>>(items);

            return WebApiResult<ListResultDto<DepartDto>>.SuccessResult(new ListResultDto<DepartDto>(itemDtos));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(SystemManagementPermissions.Departs.Delete)]
        public async Task<WebApiResult> DeleteAsync(string id)
        {
            await _departRepository.DeleteAsync(id);
            return WebApiResult.SuccessResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<WebApiResult<DepartDto>> GetAsync(string id)
        {
            var depart = await _departRepository.GetAsync(id);

            return WebApiResult<DepartDto>.SuccessResult(ObjectMapper.Map<Depart, DepartDto>(depart));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<WebApiResult<PagedEResultDto<DepartDto>>> GetListPagedAsync(DepartPagedAndSortedRequestDto input)
        {
            var resultQuery = _departRepository
                              .WhereIf(!string.IsNullOrEmpty(input.OrgCode), w => w.OrgCode == input.OrgCode)
                              .WhereIf(!string.IsNullOrEmpty(input.OrgName), w => w.OrgName == input.OrgName)
                              .OrderBy(input.Sorting ?? "CreationTime");

            return await resultQuery.GetListPaged<Depart, DepartDto>(ObjectMapper, input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(SystemManagementPermissions.Departs.Update)]
        public async Task<WebApiResult<DepartDto>> UpdateAsync(string id, UpdateDepartDto input)
        {
            var depart = await _departRepository.GetAsync(id);
            depart.Address = input.Address;
            depart.Contact = input.Contact;
            depart.Memo = input.Memo;
            depart.OrgCategory = input.OrgCategory;
            depart.OrgLevelType = input.OrgLevelType;
            depart.OrgName = input.OrgName;
            depart.OrgNameEn = input.OrgNameEn;
            depart.OrgType = input.OrgType;
            depart.SortOrder = input.SortOrder;
            depart.Status = input.Status;
            await _departRepository.UpdateAsync(depart);
            return WebApiResult<DepartDto>.SuccessResult(ObjectMapper.Map<Depart, DepartDto>(depart));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize(SystemManagementPermissions.Departs.QueryUser)]
        public async Task<WebApiResult<PagedEResultDto<UserDto>>> GetUserListPagedAsync(string departId, PagedAndSortedRequestDto input)
        {
            var resultQuery = (from u in _userRepository
                               join d in _departRepository.Where(w => w.Id == departId) on u.DepartId equals d.Id
                               join p in _postRepository on u.PostId equals p.Id into grouping
                               from p in grouping.DefaultIfEmpty()
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
                               })
                  .OrderBy(input.Sorting ?? "CreationTime");

            return await resultQuery.GetListPaged(input);
        }

    }
}
