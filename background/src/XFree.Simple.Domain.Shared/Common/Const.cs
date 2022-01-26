using System;
using System.Collections.Generic;
using System.Text;

namespace XFree.Simple.Domain.Shared.Common
{
    /// <summary>
    /// 常量
    /// </summary>
    public static class Const
    {
        /// <summary>
        /// 默认日期格式
        /// </summary>
        public static class DateTimeConfig
        {
            public const string DEFAULT_FORMAT = "yyyy-MM-dd HH:mm:ss";
        }

        /// <summary>
        ///  语言
        /// </summary>
        public static class LanguageCultureName
        {
            /// <summary>
            ///  中文
            /// </summary>
            public const string ZH_CN = "zh-Hans";
            /// <summary>
            ///  英文
            /// </summary>
            public const string EN = "en";
            /// <summary>
            ///  中文
            /// </summary>
            public const string ZH_CN_FRONT = "zh";
        }

        /// <summary>
        /// 
        /// </summary>
        public static class CacheKeyPrefix
        {
            /// <summary>
            /// 
            /// </summary>
            public const string USER = "User";

            /// <summary>
            /// 
            /// </summary>
            public const string TENANT = "Tenant";
        }

        /// <summary>
        /// 
        /// </summary>
        public static class ClaimType
        {
            /// <summary>
            /// 
            /// </summary>
            public const string USER_ID = "userId";

            /// <summary>
            /// 
            /// </summary>
            public const string NAME = "name";

            /// <summary>
            /// 
            /// </summary>
            public const string LOGIN_NAME = "loginName";

            /// <summary>
            /// 
            /// </summary>
            public const string DEPART_ID = "departId";

            /// <summary>
            /// 
            /// </summary>
            public const string TENANT_LANGUAGE = "tenantLanguage";

            /// <summary>
            /// 
            /// </summary>
            public const string USER_TENANT_ID = "userTenantId";
        }
    }
}
