using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Security.Claims;
using XFree.Simple.Application.Contracts.Options;

namespace XFree.SimpleService.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : AbpController
    {
        /// <summary>
        /// 
        /// </summary>
        public HomeController()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return Redirect("/swagger");
        }

    }
}
