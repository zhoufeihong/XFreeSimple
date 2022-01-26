using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.Authorization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Http;
using Volo.Abp.Json;
using Volo.Abp.Validation;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.Shared.Common.Localization;
using XFree.SimpleService.Host.Common.Attributes;

namespace XFree.SimpleService.Host.Common.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class ResultExceptionFilter : IFilterMetadata, IAsyncExceptionFilter, ITransientDependency, IResultFilter
    {
        private ILogger<ResultExceptionFilter> Logger { get; set; }
        private readonly IExceptionToErrorInfoConverter _errorInfoConverter;
        private readonly IHttpExceptionStatusCodeFinder _statusCodeFinder;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly AbpExceptionHandlingOptions _exceptionHandlingOptions;
        private readonly IStringLocalizerFactory _stringLocalizerFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorInfoConverter"></param>
        /// <param name="statusCodeFinder"></param>
        /// <param name="jsonSerializer"></param>
        /// <param name="exceptionHandlingOptions"></param>
        /// <param name="stringLocalizerFactory"></param>
        public ResultExceptionFilter(
            IExceptionToErrorInfoConverter errorInfoConverter,
            IHttpExceptionStatusCodeFinder statusCodeFinder,
            IJsonSerializer jsonSerializer,
            IOptions<AbpExceptionHandlingOptions> exceptionHandlingOptions,
            IStringLocalizerFactory stringLocalizerFactory)
        {
            _errorInfoConverter = errorInfoConverter;
            _statusCodeFinder = statusCodeFinder;
            _jsonSerializer = jsonSerializer;
            _exceptionHandlingOptions = exceptionHandlingOptions.Value;
            Logger = NullLogger<ResultExceptionFilter>.Instance;
            _stringLocalizerFactory = stringLocalizerFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            if (!ShouldHandleException(context))
            {
                return;
            }
            await HandleAndWrapException(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual bool ShouldHandleException(ExceptionContext context)
        {
            if (!context.ActionDescriptor.AsControllerActionDescriptor().ControllerTypeInfo.GetCustomAttributes(typeof(NoWrapResultAttribute), true).Any())
            {
                return true;
            }
            if (!context.ActionDescriptor.GetMethodInfo().GetCustomAttributes(typeof(NoWrapResultAttribute), true).Any())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual async Task HandleAndWrapException(ExceptionContext context)
        {
            // 处理异常信息
            context.HttpContext.Response.Headers.Add(AbpHttpConsts.AbpErrorFormat, "true");
            var statusCode = (int)_statusCodeFinder.GetStatusCode(context.HttpContext, context.Exception);
            context.HttpContext.Response.StatusCode = 200;
            var remoteServiceErrorInfo = _errorInfoConverter.Convert(context.Exception, _exceptionHandlingOptions.SendExceptionsDetailsToClients);
            remoteServiceErrorInfo.Code = context.HttpContext.TraceIdentifier;
            var message = SimplifyMessage(context.Exception);
            remoteServiceErrorInfo.Message = message.Item1;
            remoteServiceErrorInfo.Code = message.Item2;
            // 返回格式统一
            var result = WebApiResult<object>.FailResult(remoteServiceErrorInfo.Message, remoteServiceErrorInfo.Code);
            // HttpResponse
            context.Result = new ObjectResult(result);
            // 写日志
            var logLevel = context.Exception.GetLogLevel();
            var remoteServiceErrorInfoBuilder = new StringBuilder();
            remoteServiceErrorInfoBuilder.AppendLine($"---------- {nameof(RemoteServiceErrorInfo)} ----------");
            remoteServiceErrorInfoBuilder.AppendLine(_jsonSerializer.Serialize(remoteServiceErrorInfo, indented: true));
            Logger.LogWithLevel(logLevel, remoteServiceErrorInfoBuilder.ToString());
            Logger.LogException(context.Exception, logLevel);
            await context.HttpContext
                .RequestServices
                .GetRequiredService<IExceptionNotifier>()
                .NotifyAsync(
                    new ExceptionNotificationContext(context.Exception)
                );
            context.Exception = null; //Handled!
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        private (string, string) SimplifyMessage(Exception error)
        {
            return error switch
            {
                AbpAuthorizationException => (LS("100106").Localize(_stringLocalizerFactory), "100106"),
                AbpValidationException e => (GetErrorMessage(e), "400"),
                EntityNotFoundException => (LS("404").Localize(_stringLocalizerFactory), "404"),
                BusinessException e => ($"{e.Message}", e.Code),
                NotImplementedException => (LS("5001").Localize(_stringLocalizerFactory), "5001"),
                _ => (LS("500").Localize(_stringLocalizerFactory), "500"),
            };
        }

        private string GetErrorMessage(AbpValidationException abpValidationException)
        {
            if (!abpValidationException.ValidationErrors.IsNullOrEmpty())
            {
                return $"{LS("400").Localize(_stringLocalizerFactory)}:{string.Join(":", abpValidationException.ValidationErrors.Select(s => s.ErrorMessage))}";
            }
            return LS("400").Localize(_stringLocalizerFactory);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuting(ResultExecutingContext context)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        private static Volo.Abp.Localization.LocalizableString LS(string name, string prefix = "HttpException:")
        {
            return Volo.Abp.Localization.LocalizableString.Create<DomainSharedCommonResource>($"{prefix}{name}");
        }
    }
}
