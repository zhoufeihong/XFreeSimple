using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using XFree.Simple.Application.Contracts.SystemManagement;
using XFree.Simple.Application.Contracts.SystemManagement.AppService;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Common;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization;
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.HttpApi.SystemManage.Controllers
{
    /// <summary>
    /// 数据库连接信息接口
    /// </summary>
    [RemoteService]
    [Area("systemManagement")]
    [Route("api/systemManagement/databaseConnection")]
    public class DatabaseConnectionController : AbpController, IDatabaseConnectionAppService
    {
        private readonly IDatabaseConnectionAppService _databaseConnectionAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseConnectionAppService"></param>
        public DatabaseConnectionController(IDatabaseConnectionAppService databaseConnectionAppService)
        {
            _databaseConnectionAppService = databaseConnectionAppService;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<WebApiResult<PagedEResultDto<DatabaseConnectionDto>>> GetListPagedAsync(DatabaseConnectionPagedAndSortedRequestDto input)
        {
            return await _databaseConnectionAppService.GetListPagedAsync(input);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<WebApiResult<ListResultDto<DatabaseConnectionDto>>> GetListAsync()
        {
            return await _databaseConnectionAppService.GetListAsync();
        }

        /// <summary>
        /// 主键获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<WebApiResult<DatabaseConnectionDto>> GetAsync(string id)
        {
            return await _databaseConnectionAppService.GetAsync(id);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiResult<DatabaseConnectionDto>> CreateAsync(CreateDatabaseConnectionDto input)
        {
            return await _databaseConnectionAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/status")]
        public async Task<WebApiResult<DatabaseConnectionDto>> UpdateStatusAsync(string id, UpdateDatabaseConnectionStatusDto input)
        {
            return await _databaseConnectionAppService.UpdateStatusAsync(id, input);
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<WebApiResult<DatabaseConnectionDto>> UpdateAsync(string id, UpdateDatabaseConnectionDto input)
        {
            return await _databaseConnectionAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<WebApiResult> DeleteAsync(string id)
        {
            return await _databaseConnectionAppService.DeleteAsync(id);
        }
    }
}
