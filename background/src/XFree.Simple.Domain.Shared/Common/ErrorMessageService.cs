using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace XFree.Simple.Domain.Shared.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorMessageService : ISingletonDependency
    {
        /// <summary>
        /// 
        /// </summary>
        public IStringLocalizerFactory _stringLocalizerFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringLocalizerFactory"></param>
        public ErrorMessageService(IStringLocalizerFactory stringLocalizerFactory)
        {
            _stringLocalizerFactory = stringLocalizerFactory;
        }

        /// <summary>
        /// 抛出业务异常
        /// </summary>
        /// <param name="enum"></param>
        /// <param name="logLevel"></param>
        public void ThrowMessage(System.Enum @enum, LogLevel logLevel = LogLevel.Error)
        {
            IStringLocalizer stringLocalizer = GetStringLocalizer(@enum);
            @enum.ThrowMessage(stringLocalizer, logLevel);
        }

        /// <summary>
        /// 获取抛出业务异常信息
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public string GetMessage(System.Enum @enum)
        {
            IStringLocalizer stringLocalizer = GetStringLocalizer(@enum);
            return @enum.GetExceptionMessage(stringLocalizer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        private IStringLocalizer GetStringLocalizer(System.Enum @enum)
        {
            var resourceType = @enum.GetType().GetCustomAttributes(true).OfType<EnumResourceAttribute>().First().ResourceType;
            var stringLocalizer = _stringLocalizerFactory.Create(resourceType);
            return stringLocalizer;
        }

        /// <summary>
        /// 抛出业务异常，带有参数信息
        /// </summary>
        /// <param name="enum"></param>
        /// <param name="param"></param>
        public void ThrowMessageParam(System.Enum @enum, params string[] param)
        {
            IStringLocalizer stringLocalizer = GetStringLocalizer(@enum);
            @enum.ThrowMessageParam(stringLocalizer, LogLevel.Error, param);
        }

        /// <summary>
        /// 抛出业务异常，带有参数信息
        /// </summary>
        /// <param name="enum"></param>
        /// <param name="logLevel"></param>
        /// <param name="param"></param>
        public void ThrowMessageParam(System.Enum @enum, LogLevel logLevel, params string[] param)
        {
            IStringLocalizer stringLocalizer = GetStringLocalizer(@enum);
            @enum.ThrowMessageParam(stringLocalizer, logLevel, param);
        }

    }
}
