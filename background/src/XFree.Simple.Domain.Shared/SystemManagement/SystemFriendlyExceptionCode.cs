using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Volo.Abp;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.Shared.SystemManagement.Domain;

namespace XFree.Simple.Domain.Shared.SystemManagement
{

    /// <summary>
    /// 友好异常返回枚举类型
    /// 可以通过枚举扩展返回抛出<see cref="Volo.Abp.UserFriendlyException"/>异常，支持多语言
    /// <para> 枚举int值: 作为返回结果<see cref="XFree.Simple.Domain.Shared.Common.WebApiResult">的code </para> 
    /// <para> "EnumException:枚举名称:枚举值字符串" : 作为多语言资源文件的key </para> 
    /// </summary>
    [EnumResource(ResourceType = typeof(SystemManagementResource))]
    public enum SystemFriendlyExceptionCode
    {
        /// <summary>
        ///  商户名称重复
        /// </summary>
        [Description("商户名称重复")]
        DuplicateMerchantName = 100001,
        /// <summary>
        ///  商户编码重复
        /// </summary>
        [Description("商户编码重复:{0}")]
        DuplicateMerchantCode = 100002,
        /// <summary>
        ///  字典编码重复
        /// </summary>
        [Description("字典编码重复:{0}")]
        DuplicateDictCode = 100003,
        /// <summary>
        ///  字典项编码重复
        /// </summary>
        [Description("字典项编码重复:{0}")]
        DuplicateDictItemValue = 100004,
        /// <summary>
        ///  商户数据已经进行初始化，数据库信息无法修改
        /// </summary>
        [Description("商户数据已经进行初始化，数据库信息无法修改")]
        MerchantError005 = 100005,
        /// <summary>
        ///  登录接口-用户不存在
        /// </summary>
        [Description("用户不存在:{0}")]
        LoginError101 = 100101,
        /// <summary>
        ///  登录接口-密码错误
        /// </summary>
        [Description("密码错误11")]
        LoginError102 = 100102,
        /// <summary>
        ///  登录接口-用户被禁用
        /// </summary>
        [Description("用户被禁用")]
        LoginError103 = 100103,
        /// <summary>
        ///  登录接口-用户被锁定
        /// </summary>
        [Description("用户被锁定")]
        LoginError104 = 100104,
        /// <summary>
        ///  登录接口-无效Token
        /// </summary>
        [Description("无效Token")]
        LoginError106 = 100106,
        /// <summary>
        ///  登录接口-Token失效
        /// </summary>
        [Description("Token失效")]
        LoginError107 = 100107,
        /// <summary>
        ///  登录接口-过期请重新登录
        /// </summary>
        [Description("过期请重新登录")]
        LoginError108 = 100108,
        /// <summary>
        ///  
        /// </summary>
        [Description("没有访问权限")]
        LoginError109 = 100109,
        /// <summary>
        ///  商户不存在
        /// </summary>
        [Description("商户不存在")]
        LoginError110 = 100110,
        /// <summary>
        ///  商户被禁用
        /// </summary>
        [Description("商户被禁用")]
        LoginError111 = 100111,
        /// <summary>
        ///  用户接口-用户账户重复
        /// </summary>
        [Description("用户账户重复")]
        UserError114 = 100114,
        /// <summary>
        ///  用户接口-用户工号重复
        /// </summary>
        [Description("用户工号重复")]
        UserError115 = 100115,
        /// <summary>
        ///  部门编码不存在
        /// </summary>
        [Description("部门编码不存在")]
        UserError116 = 100116,
        /// <summary>
        ///  职务编码不存在
        /// </summary>
        [Description("职务编码不存在")]
        UserError117 = 100117,
        /// <summary>
        ///  角色编码不存在
        /// </summary>
        [Description("角色编码不存在")]
        UserError118 = 100118,
        /// <summary>
        ///  管理员用户不能删除
        /// </summary>
        [Description("管理员用户不能删除")]
        UserError119 = 100119,
        /// <summary>
        ///  部门接口
        /// </summary>
        [Description("部门编码重复:{0}")]
        DuplicateDepartCode201 = 100201,
        /// <summary>
        ///  角色接口
        /// </summary>
        [Description("角色编码重复:{0}")]
        DuplicateRoleCode301 = 100301,
        /// <summary>
        ///  职务接口
        /// </summary>
        [Description("职务编码重复:{0}")]
        DuplicatePostCode501 = 100501,
        /// <summary>
        ///  数据库连接名称重复
        /// </summary>
        [Description("数据库连接名称重复")]
        DuplicateDatabaseConnectionName601 = 100601
    }

}
