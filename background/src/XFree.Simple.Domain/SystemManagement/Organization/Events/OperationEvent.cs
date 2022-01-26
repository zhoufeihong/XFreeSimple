using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFree.Simple.Domain.Shared.SystemManagement.Enum;

namespace XFree.Simple.Domain.SystemManagement.Organization.Events
{
    /// <summary>
    /// 用户操作事件消息实体
    /// </summary>
    public class OperationEvent
    {

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///  内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 
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
        /// 时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
