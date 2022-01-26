using System;
using System.Collections.Generic;
using System.Text;
using XFree.Simple.Domain.Shared.SystemManagement.Enum;

namespace XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization
{
    /// <summary>
    /// 操作信息
    /// </summary>
    public class OperationInfoDto
    {

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///  内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// ip地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 登录地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 状态(1-成功 2-失败)
        /// </summary>
        public SuccessFailStatus Status { get; set; }

        /// <summary>
        ///  状态名称
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
