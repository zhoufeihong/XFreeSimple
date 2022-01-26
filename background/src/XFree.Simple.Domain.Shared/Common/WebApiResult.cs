using System;
using System.Collections.Generic;
using System.Text;

namespace XFree.Simple.Domain.Shared.Common
{

    /// <summary>
    ///  接口统一返回结构实体
    ///  当执行失败时，可以使用<see cref="SystemManagement.SystemFriendlyExceptionCode"/>抛出<see cref="Volo.Abp.UserFriendlyException"/>异常
    /// </summary>
    public class WebApiResult
    {
        /// <summary>
        ///  结果标识
        ///  <para>true: 成功</para>
        ///  <para>false: 失败</para>
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 返回编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///  创建执行成功返回结果
        /// </summary>
        /// <param name="message">返回信息</param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public static WebApiResult SuccessResult(string message = "Success", string code = "200")
        {
            return new WebApiResult
            {
                Success = true,
                Code = code,
                Msg = message
            };
        }

        /// <summary>
        /// 创建执行失败返回结果
        /// </summary>
        /// <param name="message">返回信息</param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public static WebApiResult FailResult(string message = "Fail", string code = "500")
        {
            return new WebApiResult
            {
                Success = false,
                Code = code,
                Msg = message
            };
        }

    }

    /// <summary>
    /// 带返回数据的接口统一返回结构实体
    /// 继承自<see cref="WebApiResult"/>,执行失败处理方式可以参考
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WebApiResult<T> : WebApiResult
    {

        /// <summary>
        ///  返回数据
        /// </summary>
        public T Data { get; set; }

        public WebApiResult()
        {
            Data = default;
        }

        /// <summary>
        /// 创建执行成功返回结果
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static WebApiResult<T> SuccessResult(T data, string message = "Success", string code = "200")
        {
            return new WebApiResult<T>
            {
                Success = true,
                Code = code,
                Msg = message,
                Data = data
            };
        }

        /// <summary>
        /// 创建执行失败返回结果
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public new static WebApiResult<T> FailResult(string message = "Fail", string code = "500")
        {
            return new WebApiResult<T>
            {
                Success = false,
                Code = code,
                Msg = message
            };
        }

    }

    /// <summary>
    ///  导入结果
    /// </summary>
    public class ImportResult
    {
        /// <summary>
        ///  总数量
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        ///  成功数量
        /// </summary>
        public long SuccessfulCount { get; set; }

        /// <summary>
        /// 创建数量
        /// </summary>
        public long CreatedCount { get; set; }

        /// <summary>
        /// 更新数量
        /// </summary>
        public long UpdatedCount { get; set; }

        /// <summary>
        ///  失败数量
        /// </summary>
        public long FailedCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ItemError> ItemErrors { get; set; }
    }

    /// <summary>
    ///  异常项
    /// </summary>
    public class ItemError
    {
        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }
    }

}
