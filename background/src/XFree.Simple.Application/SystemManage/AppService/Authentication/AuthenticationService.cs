using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;
using XFree.Simple.Application.Common;
using XFree.Simple.Application.Contracts.Common;
using XFree.Simple.Application.Contracts.Options;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Authentication;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Authentication.Dto;
using XFree.Simple.Application.Contracts.SystemManagement.Permission;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.Shared.SystemManagement;
using XFree.Simple.Domain.Shared.SystemManagement.Domain;
using XFree.Simple.Domain.SystemManagement.Organization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Uow;

namespace XFree.Simple.Application.SystemManage.AppService.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(SystemManagementPermissions.Base.WithToken)]
    public class AuthenticationAppService : ApplicationService, IAuthenticationAppService
    {

        private readonly IRepository<User, string> _userRepository;

        private readonly IRepository<Tenant, string> _tenantRepository;

        private readonly IRSASecurityKeyFacotry _rsaSecurityKeyFacotry;

        private readonly IOptions<AuthenticationJwtOptions> _authenticationJwtOptions;

        private readonly ErrorMessageService _errorMessageService;

        public AuthenticationAppService(IRepository<User, string> userRepository,
            IOptions<AuthenticationJwtOptions> authenticationJwtOptions,
            IRSASecurityKeyFacotry rsaSecurityKeyFacotry,
            ErrorMessageService errorMessageService,
            IRepository<Tenant, string> tenantRepository)
        {
            _userRepository = userRepository;
            _authenticationJwtOptions = authenticationJwtOptions;
            _rsaSecurityKeyFacotry = rsaSecurityKeyFacotry;
            _errorMessageService = errorMessageService;
            _tenantRepository = tenantRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<WebApiResult<UserLoginVo>> Login(UserLoginDto userLoginDto)
        {
            Guid? tenantGuid = null;
            string tenantLanguage = null;
            if (!string.IsNullOrEmpty(userLoginDto.TenantCode))
            {
                var tenant = await _tenantRepository.FirstOrDefaultAsync(f => f.Code == userLoginDto.TenantCode);
                if (tenant == null)
                {
                    _errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.LoginError110);
                }
                if (tenant.Status == Domain.Shared.SystemManagement.Enum.NormalLockedStatus.Locked)
                {
                    _errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.LoginError111);
                }
                tenantGuid = Guid.Parse(tenant.Id);
                tenantLanguage = tenant.Language;
            }
            using (CurrentTenant.Change(tenantGuid))
            {
                using var unitOfWork = UnitOfWorkManager.Begin(true);
                var userRepository = unitOfWork.ServiceProvider.GetService<IRepository<User, string>>();
                var user = await userRepository.FirstOrDefaultAsync(s => s.LoginName == userLoginDto.LoginName);
                if (user == null)
                {
                    _errorMessageService.ThrowMessageParam(SystemFriendlyExceptionCode.LoginError101, userLoginDto.LoginName);
                }
                user.Login(_errorMessageService, userLoginDto.Password);
                var resultDto = new UserLoginVo
                {
                    TenantLanguage = Const.LanguageCultureName.ToBackCulture(tenantLanguage) ?? Const.LanguageCultureName.ZH_CN_FRONT
                };
                if (userLoginDto.ReturnUserToken)
                {
                    resultDto.UserToken = CreateToken(user, tenantLanguage);
                }
                user.LoginDate = DateTime.Now;
                await unitOfWork.CompleteAsync();
                return WebApiResult<UserLoginVo>.SuccessResult(resultDto);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<WebApiResult<UserLoginVo>> RefreshToken()
        {
            var tenantLanguage = CurrentUser.FindClaim(Const.ClaimType.TENANT_LANGUAGE)?.Value;
            var user = await _userRepository.GetAsync(CurrentUser.Id.ToString());
            var resultDto = new UserLoginVo
            {
                UserToken = CreateToken(user, tenantLanguage),
                TenantLanguage = tenantLanguage
            };
            return WebApiResult<UserLoginVo>.SuccessResult(resultDto);
        }

        #region Private

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="tenantLanguage"></param>
        /// <returns></returns>
        private string CreateToken(User user, string tenantLanguage)
        {
            var rsaSecurityKey = _rsaSecurityKeyFacotry.Get(_authenticationJwtOptions.Value.BearerPrivateKey);
            // jwtHeader
            var signcreds = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);
            var jwtHeader = new JwtHeader(signcreds);
            // jwtPayload
            string issuer = "xfree.auth";
            string audience = "xfree.api.auth";
            var claims = new List<Claim>
            {
                    new Claim(Const.ClaimType.USER_ID,user.Id),
                    new Claim(Const.ClaimType.NAME,user.Nickname ?? string.Empty),
                    new Claim(Const.ClaimType.LOGIN_NAME,user.LoginName ?? string.Empty),
                    new Claim(Const.ClaimType.DEPART_ID,user.DepartId?? string.Empty),
                    new Claim(Const.ClaimType.TENANT_LANGUAGE,tenantLanguage ?? Const.LanguageCultureName.ZH_CN_FRONT)
            };
            if (user.TenantId.HasValue)
            {
                claims.Add(new Claim(Const.ClaimType.USER_TENANT_ID, user.TenantId.Value.ToString()));
            }
            var notBefore = DateTime.UtcNow;
            var expires = DateTime.UtcNow.AddMinutes(60);
            var jwtPayload = new JwtPayload(issuer, audience, claims, notBefore, expires);
            // build Token
            var jwt = new JwtSecurityToken(jwtHeader, jwtPayload);
            var JwtHander = new JwtSecurityTokenHandler();
            var token = JwtHander.WriteToken(jwt);
            return token;
        }

        #endregion

    }
}
