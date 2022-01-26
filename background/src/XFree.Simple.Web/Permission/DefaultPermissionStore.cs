using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using XFree.Simple.Application.Common;
using XFree.Simple.Domain.Shared.SystemManagement;
using XFree.Simple.Domain.Shared.SystemManagement.Domain;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Application.Contracts.SystemManagement.Permission;
using Volo.Abp.MultiTenancy;

namespace XFree.SimpleService.Host.Permission
{
    /// <summary>
    ///  默认权限校验实现
    /// </summary>
    public class DefaultPermissionStore : IPermissionStore, IScopedDependency
    {

        private readonly ICheckUserPermission _checkUserPermission;

        private const string PROVIDER_NAME_U = "U";

        private readonly ErrorMessageService _errorMessageService;

        private readonly ICurrentTenant _currentTenant;


        public DefaultPermissionStore(ICheckUserPermission checkUserPermission,
            ErrorMessageService errorMessageService,
            ICurrentTenant currentTenant)
        {
            _checkUserPermission = checkUserPermission;
            _errorMessageService = errorMessageService;
            _currentTenant = currentTenant;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        public async Task<bool> IsGrantedAsync(string name, string providerName, string providerKey)
        {
            if (name == SystemManagementPermissions.Base.WithToken)
            {
                return true;
            }
            if (name == PlatformPermissions.Base.WithToken)
            {
                // 租户用户没有权限访问
                if (_currentTenant.GetMultiTenancySide() != MultiTenancySides.Host)
                {
                    _errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.LoginError109);
                }
                return true;
            }
            if (providerName == PROVIDER_NAME_U)
            {
                var checkResult = await _checkUserPermission.Check(providerKey, name);
                if (!checkResult)
                {
                    _errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.LoginError109);
                }
                return checkResult;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="names"></param>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        public async Task<MultiplePermissionGrantResult> IsGrantedAsync(string[] names, string providerName, string providerKey)
        {
            var result = new MultiplePermissionGrantResult();
            if (providerName == PROVIDER_NAME_U)
            {
                foreach (var name in names)
                {
                    result.Result.Add(name, await _checkUserPermission.Check(providerKey, name)
                        ? PermissionGrantResult.Granted
                        : PermissionGrantResult.Prohibited);
                }
            }
            return await Task.FromResult(result);
        }

    }
}
