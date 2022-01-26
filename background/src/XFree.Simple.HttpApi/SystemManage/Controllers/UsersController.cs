using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using XFree.Simple.Application.Contracts.SystemManage;
using XFree.Simple.Application.Contracts.SystemManage.AppService;
using XFree.Simple.Application.Contracts.SystemManagement;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Authentication.Dto;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Permission;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.Shared.SystemManagement.Domain;
using XFree.Simple.Domain.Shared.SystemManagement.Enum;
using XFree.Simple.HttpApi.Common;

namespace XFree.Simple.HttpApi.SystemManage.Controllers
{
    /// <summary>
    /// 用户接口
    /// </summary>
    [RemoteService]
    [Area("systemManagement")]
    [Route("api/systemManagement/user")]
    public class UsersController : AbpController, IUserAppService
    {
        private readonly IUserAppService _userAppService;

        private readonly ExcelService _excelService;

        private readonly ErrorMessageService _errorMessageService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAppService"></param>
        /// <param name="excelService"></param>
        /// <param name="errorMessageService"></param>
        public UsersController(IUserAppService userAppService,
            ExcelService excelService,
            ErrorMessageService errorMessageService)
        {
            _userAppService = userAppService;
            _excelService = excelService;
            _errorMessageService = errorMessageService;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<WebApiResult<PagedEResultDto<UserDto>>> GetListPagedAsync(UserPagedAndSortedRequestDto input)
        {
            return await _userAppService.GetListPagedAsync(input);
        }

        /// <summary>
        /// 主键获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<WebApiResult<UserDto>> GetAsync(string id)
        {
            return await _userAppService.GetAsync(id);
        }

        /// <summary>
        /// 通过Token获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getByToken")]
        public async Task<WebApiResult<UserDto>> GetByTokenAsync()
        {
            return await _userAppService.GetByTokenAsync();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiResult<UserDto>> CreateAsync(CreateUserDto input)
        {
            return await _userAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<WebApiResult<UserDto>> UpdateAsync(string id, UpdateUserDto input)
        {
            return await _userAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/status")]
        public async Task<WebApiResult<UserDto>> UpdateStatusAsync(string id, UpdateUserStatusDto input)
        {
            return await _userAppService.UpdateStatusAsync(id, input);
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/password")]
        public async Task<WebApiResult<UserDto>> UpdatePasswordAsync(string id, UpdateUserPasswordDto input)
        {
            return await _userAppService.UpdatePasswordAsync(id, input);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<WebApiResult> DeleteAsync(string id)
        {
            return await _userAppService.DeleteAsync(id);
        }

        /// <summary>
        ///  重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/resetPassword")]
        public async Task<WebApiResult> ResetPassword(string id)
        {
            return await _userAppService.ResetPassword(id);
        }

        /// <summary>
        /// 
        /// </summary>
        private readonly static List<ColumnItem<UserDto>> ColumnItems = new()
        {
            new()
            {
                ColumnDisplayName = LS("*账号"),
                ImportSet = (obj, val) => { Check.NotNullOrWhiteSpace(val, "code"); obj.LoginName = val; },
                ExportGet = obj => obj.LoginName,
            },
            new()
            {
                ColumnDisplayName = LS("*姓名"),
                ImportSet = (obj, val) => { Check.NotNullOrWhiteSpace(val, "name"); obj.Nickname = val; },
                ExportGet = obj => obj.Nickname,
            },
            new()
            {
                ColumnDisplayName = LS("*工号"),
                ImportSet = (obj, val) => { Check.NotNullOrWhiteSpace(val, "employeeIDNumber"); obj.EmployeeIDNumber = val; },
                ExportGet = obj => obj.EmployeeIDNumber,
            },
            new()
            {
                ColumnDisplayName = LS("性别(0:未知 1:男 2:女)"),
                ImportSet = (obj, val) => { obj.Sex = Enum.Parse<SexType>(val); },
                ExportGet = obj => ((int)obj.Sex).ToString(),
            },
            new()
            {
                ColumnDisplayName = LS("*部门编码"),
                ImportSet = (obj, val) => { obj.DepartCode = val; },
                ExportGet = obj => obj.DepartCode,
            },
            new()
            {
                ColumnDisplayName = LS("部门名称"),
                ImportSet = (obj, val) => { obj.DepartName = val; },
                ExportGet = obj => obj.DepartName,
            },
            new()
            {
                ColumnDisplayName = LS("职务编码"),
                ImportSet = (obj, val) => { obj.PostCode = val; },
                ExportGet = obj => obj.PostCode,
            },
            new()
            {
                ColumnDisplayName = LS("职务名称"),
                ImportSet = (obj, val) => { obj.PostName = val; },
                ExportGet = obj => obj.PostName,
            },
            new()
            {
                ColumnDisplayName = LS("角色编码"),
                ImportSet = (obj, val) => { obj.RoleCodes = val; },
                ExportGet = obj => obj.RoleCodes,
            },
            new()
            {
                ColumnDisplayName = LS("角色名称"),
                ImportSet = (obj, val) => { obj.RoleNames = val; },
                ExportGet = obj => obj.RoleNames,
            },
            new()
            {
                ColumnDisplayName = LS("*状态(1:正常 2:禁用)"),
                ImportSet = (obj, val) => { obj.Status = Enum.Parse<NormalLockedStatus>(val); },
                ExportGet = obj => ((int)obj.Status).ToString(),
            },
            new()
            {
                ColumnDisplayName = LS("*密码"),
                ImportSet = (obj, val) => { obj.Password = val; },
                IgnoreExport = true
            },
            new()
            {
                ColumnDisplayName = LS("创建时间"),
                IgnoreImport = true,
                ExportGet = obj => obj.CreationTime.ToString(Const.DateTimeConfig.DEFAULT_FORMAT),
            }
        };

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("export")]
        public async Task<FileContentResult> Export(UserPagedAndSortedRequestDto input)
        {
            var users = await _userAppService.GetExportListPagedAsync(input);
            return new FileContentResult(_excelService.Export(users.Data.Data, ColumnItems), "application/octet-stream")
            {
                FileDownloadName = $"{DateTime.Now:yyyyMMddHHmm}{LS("Title").Localize(StringLocalizerFactory)}.xls"
            };
        }

        /// <summary>
        ///  下载模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("downLoadTemplate")]
        public FileContentResult DownLoadTemplate()
        {
            return new FileContentResult(_excelService.Export(new List<UserDto>(), ColumnItems, ExcelDataType.Template), "application/octet-stream")
            {
                FileDownloadName = $"{LS("TemplateTitle").Localize(StringLocalizerFactory)}.xls"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("import")]
        public async Task<WebApiResult<ImportResult>> ImportAsync([FromForm] IFormCollection formCollection)
        {
            if (formCollection == null || formCollection.Files == null || formCollection.Files.Count == 0)
            {
                _errorMessageService.ThrowMessage(FriendlyExceptionCode.ExcelImport005);
            }
            var vehicleModelDtos = _excelService.ImportExcel(formCollection.Files[0], ColumnItems);
            return await _userAppService.ImportAsync(vehicleModelDtos);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("getUserPermisionInfo")]
        public async Task<WebApiResult<UserPermisionInfo>> GetUserPermisionInfo()
        {
            return await _userAppService.GetUserPermisionInfo();
        }

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <returns></returns>
        [HttpGet("getUiPermission")]
        public async Task<WebApiResult<List<UiPermissionDto>>> GetUiPermission()
        {
            return await _userAppService.GetUiPermission();
        }

        /// <summary>
        /// 分页查询用户操作记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getOperationInfoListPaged")]
        public async Task<WebApiResult<PagedEResultDto<OperationInfoDto>>> GetOperationInfoListPagedAsync(PagedAndSortedRequestDto input)
        {
            return await _userAppService.GetOperationInfoListPagedAsync(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getExportListPaged")]
        public async Task<WebApiResult<PagedEResultDto<UserDto>>> GetExportListPagedAsync(UserPagedAndSortedRequestDto input)
        {
            return await _userAppService.GetExportListPagedAsync(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        private static Volo.Abp.Localization.LocalizableString LS(string name, string prefix = "Excel:User:")
        {
            return Volo.Abp.Localization.LocalizableString.Create<SystemManagementResource>($"{prefix}{name}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [NonAction]
        public Task<WebApiResult<ImportResult>> ImportAsync(List<UserDto> inputs)
        {
            throw new NotImplementedException();
        }

    }
}
