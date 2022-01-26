using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using XFree.Simple.Domain.Shared.SystemManagement.Domain;

namespace XFree.Simple.Domain.SystemManagement.Permission
{
    /// <summary>
    ///  Ui菜单权限管理者
    /// </summary>
    public class UiPermissionManager : DomainService
    {
        private readonly IRepository<UiPermission, string> _uiPermissionRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiPermissionRepository"></param>
        public UiPermissionManager(IRepository<UiPermission, string> uiPermissionRepository)
        {
            _uiPermissionRepository = uiPermissionRepository;
        }

        /// <summary>
        /// 设置父节点的叶子节点状态
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task SetParentIsLeaf(string parentId)
        {
            if (string.IsNullOrEmpty(parentId))
            {
                await ValueTask.CompletedTask;
                return;
            }
            var parentEntity = await _uiPermissionRepository.GetAsync(parentId);
            var isLeaf = false;
            if (parentEntity.IsLeaf != isLeaf)
            {
                parentEntity.IsLeaf = isLeaf;
                await _uiPermissionRepository.UpdateAsync(parentEntity);
            }
        }

        /// <summary>
        /// 设置父节点的叶子节点状态
        /// </summary>
        /// <param name="oldId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task SetParentIsLeaf(string oldId,string parentId)
        {
            if (string.IsNullOrEmpty(parentId))
            {
                await ValueTask.CompletedTask;
                return;
            }
            var parentEntity = await _uiPermissionRepository.GetAsync(parentId);
            var isLeaf = !(await _uiPermissionRepository.AnyAsync(a => a.Id != oldId && a.ParentId == parentId));
            if (parentEntity.IsLeaf != isLeaf)
            {
                parentEntity.IsLeaf = isLeaf;
                await _uiPermissionRepository.UpdateAsync(parentEntity);
            }
        }

    }
}
