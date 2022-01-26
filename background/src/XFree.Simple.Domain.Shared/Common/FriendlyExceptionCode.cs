using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Volo.Abp;
using XFree.Simple.Domain.Shared.Common.Localization;

namespace XFree.Simple.Domain.Shared.Common
{

    /// <summary>
    /// 友好异常返回枚举类型
    /// 可以通过枚举扩展返回抛出<see cref="Volo.Abp.UserFriendlyException"/>异常，支持多语言
    /// <para> 枚举int值: 作为返回结果<see cref="XFree.Simple.Domain.Shared.Common.WebApiResult">的code </para> 
    /// <para> "EnumException:枚举名称:枚举值字符串" : 作为多语言资源文件的key </para> 
    /// </summary>
    [EnumResource(ResourceType = typeof(DomainSharedCommonResource))]
    public enum FriendlyExceptionCode
    {
        /// <summary>
        ///  导入文件缺少列
        /// </summary>
        [Description("导入文件缺少列:{0}")]
        ExcelImport001 = 10001,
        /// <summary>
        ///  导入文件为空
        /// </summary>
        [Description("导入文件为空")]
        ExcelImport002 = 10002,
        /// <summary>
        ///  行格式不正确
        /// </summary>
        [Description("{0}行'{1}'列格式不正确")]
        ExcelImport003 = 10003,
        /// <summary>
        ///  一次最多导入{0}行数据
        /// </summary>
        [Description("一次最多导入{0}行数据")]
        ExcelImport004 = 10004,
        /// <summary>
        ///  请选择上传文件
        /// </summary>
        [Description("请选择上传文件")]
        ExcelImport005 = 10005,
        /// <summary>
        ///  操作超时
        /// </summary>
        [Description("操作超时,请稍后重试.")]
        TimeOut10061 = 10061,
        /// <summary>
        ///  等待超时
        /// </summary>
        [Description("等待超时,请稍后重试.")]
        TimeOut10062 = 10062
    }

}
