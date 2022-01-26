using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Common;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Permission;
using XFree.Simple.Domain.SystemManagement;
using XFree.Simple.Domain.SystemManagement.Common;
using XFree.Simple.Domain.SystemManagement.Organization;
using XFree.Simple.Domain.SystemManagement.Permission;

namespace XFree.Simple.Application.SystemManage.MapProfiles
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemManagementApplicationAutoMapperProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public SystemManagementApplicationAutoMapperProfile()
        {
            CreateMap<Tenant, TenantDto>(MemberList.None);
            CreateMap<CreateTenantDto, Tenant>(MemberList.None);
            CreateMap<UpdateTenantDto, Tenant>(MemberList.None);
            CreateMap<DatabaseConnection, DatabaseConnectionDto>();
            CreateMap<Dict, DictDto>();
            CreateMap<DictItem, DictItemDto>();
            CreateMap<User, UserDto>(MemberList.None);
            CreateMap<OperationInfo, OperationInfoDto>().ForMember(f => f.StatusName,s => s.Ignore());
            CreateMap<Role, RoleDto>();
            CreateMap<Post, PostDto>();
            CreateMap<Depart, DepartDto>();
            CreateMap<UiPermission, UiPermissionDto>(MemberList.None);
            CreateMap<CreateUiPermissionDto, UiPermission>(MemberList.None);
            CreateMap<UpdateUiPermissionDto, UiPermission>(MemberList.None);
            CreateMap<BackgroundApi, BackgroundApiDto>(MemberList.None);
        }
    }
}
