using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Settings;

namespace XFree.Simple.Application.Contracts.SystemManagement.Setting
{
    /// <summary>
    /// 默认配置
    /// </summary>
    public class SystemManagementSettingDefinitionProvider : SettingDefinitionProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
                new SettingDefinition(
                    SystemManagementSettings.MaxPageSize,
                    "100",
                    isVisibleToClients: true
                )
            );
        }
    }
}
