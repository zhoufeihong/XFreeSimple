using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Volo.Abp;

namespace XFree.Simple.Domain.Shared.Common
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum)]
    public class EnumResourceAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public Type ResourceType { get; set; }
    }

    /// <summary>
    ///  枚举扩展类型
    /// </summary>
    public static class EnumExtession
    {
        /// <summary>
        ///  抛出业务异常
        /// </summary>
        /// <param name="enum"></param>
        /// <param name="stringLocalizer"></param>
        internal static void ThrowMessage(this System.Enum @enum, IStringLocalizer stringLocalizer, LogLevel logLevel = LogLevel.Error)
        {
            throw new UserFriendlyException(GetExceptionMessage(@enum, stringLocalizer), Convert.ToInt32(@enum).ToString(), logLevel: logLevel);
        }

        /// <summary>
        /// 抛出业务异常，带有参数信息
        /// </summary>
        /// <param name="enum"></param>
        /// <param name="stringLocalizer"></param>
        internal static void ThrowMessageParam(this System.Enum @enum, IStringLocalizer stringLocalizer, params string[] param)
        {
            ThrowMessageParam(@enum, stringLocalizer, LogLevel.Error, param);
        }

        /// <summary>
        /// 抛出业务异常，带有参数信息
        /// </summary>
        /// <param name="enum"></param>
        /// <param name="stringLocalizer"></param>
        internal static void ThrowMessageParam(this System.Enum @enum, IStringLocalizer stringLocalizer, LogLevel logLevel, params string[] param)
        {
            throw new UserFriendlyException(string.Format(GetExceptionMessage(@enum, stringLocalizer), param), Convert.ToInt32(@enum).ToString(), logLevel: logLevel);
        }

        /// <summary>
        /// 获取枚举抛出异常信息
        /// </summary>
        /// <param name="enum"></param>
        /// <param name="stringLocalizer"></param>
        /// <returns></returns>
        public static string GetExceptionMessage(this System.Enum @enum, IStringLocalizer stringLocalizer)
        {
            if (stringLocalizer == null)
            {
                return @enum.GetDescription()?.Description;
            }
            var resourceName = $"EnumException:{@enum.GetType().Name}:{@enum}";
            var localizedString = stringLocalizer[resourceName];
            return localizedString.ResourceNotFound ? @enum.GetDescription()?.Description : localizedString;
        }

        /// <summary>
        ///  获取枚举Description
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static DescriptionAttribute GetDescription(this System.Enum @enum)
        {
            return @enum.GetType().GetField(@enum.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault() as DescriptionAttribute;
        }
    }
}
