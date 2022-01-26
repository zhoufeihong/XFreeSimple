using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Authentication.Dto;
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.Application.Contracts.SystemManagement.AppService.Authentication
{
    /// <summary>
    ///  权限接口
    /// </summary>
    public interface IAuthenticationAppService : IApplicationService
    {

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns></returns>
        Task<WebApiResult<UserLoginVo>> Login(UserLoginDto userLoginDto);

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns></returns>
        Task<WebApiResult<UserLoginVo>> RefreshToken();

    }
}
