using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using XFree.Simple.Application.Contracts.SystemManage;
using XFree.Simple.Application.Contracts.SystemManagement;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Authentication;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Authentication.Dto;
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.HttpApi.SystemManage.Controllers
{
    /// <summary>
    /// 登录接口
    /// </summary>
    [RemoteService]
    [Area("systemManagement")]
    [Route("api/systemManagement/login")]
    public class LoginController : AbpController, IAuthenticationAppService
    {
        private readonly IAuthenticationAppService _authenticationAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationAppService"></param>
        public LoginController(IAuthenticationAppService authenticationAppService)
        {
            _authenticationAppService = authenticationAppService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiResult<UserLoginVo>> Login(UserLoginDto userLoginDto)
        {
            return await _authenticationAppService.Login(userLoginDto);
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns></returns>
        [HttpPost("refreshToken")]
        public async Task<WebApiResult<UserLoginVo>> RefreshToken()
        {
            return await _authenticationAppService.RefreshToken();
        }
    }
}
