using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace XFree.Simple.Domain.SystemManagement.Permission
{
    /// <summary>
    ///  Ui关联接口资源
    /// </summary>
    public class UiWithApi : Entity<string>, IMultiTenant
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public UiWithApi(string id)
        {
            Id = id;
        }

        /// <summary>
        /// 租户Id
        /// </summary>
        public virtual Guid? TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string UiPermissionId { get; set; }

        /// <summary>
        /// 接口资源编码
        /// </summary>
        public virtual string PermissionCode { get; set; }
    }
}
