using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using XFree.Simple.Application.Common;
using XFree.Simple.Application.Common.Impl;
using XFree.Simple.Application.Contracts.SystemManagement;
using XFree.Simple.Application.Contracts.SystemManagement.AppService;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Common;
using XFree.Simple.Application.Contracts.SystemManagement.AppService.Dto.Organization;
using XFree.Simple.Application.Contracts.SystemManagement.Permission;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.Shared.SystemManagement;
using XFree.Simple.Domain.SystemManagement;
using XFree.Simple.Domain.SystemManagement.Common;
using XFree.Simple.Domain.SystemManagement.Organization;

namespace XFree.Simple.Application.SystemManage.AppService
{
    [Authorize(PlatformPermissions.DatabaseConnections.Default)]
    public class DatabaseConnectionAppService : ApplicationService, IDatabaseConnectionAppService
    {
        private readonly IRepository<DatabaseConnection, string> _databaseConnectionRepository;

        private readonly ErrorMessageService _errorMessageService;

        private readonly DatabaseConnectionStringEncryption _databaseConnectionStringEncryption;

        public DatabaseConnectionAppService(IRepository<DatabaseConnection, string> databaseConnectionRepository,
            ErrorMessageService errorMessageService,
            DatabaseConnectionStringEncryption databaseConnectionStringEncryption)
        {
            _databaseConnectionRepository = databaseConnectionRepository;
            _errorMessageService = errorMessageService;
            _databaseConnectionStringEncryption = databaseConnectionStringEncryption;
        }

        public async Task<WebApiResult<PagedEResultDto<DatabaseConnectionDto>>> GetListPagedAsync(DatabaseConnectionPagedAndSortedRequestDto input)
        {
            var resultQuery = _databaseConnectionRepository
                .WhereIf(!string.IsNullOrEmpty(input.Name), w => w.Name == input.Name)
                .OrderBy(input.Sorting ?? "CreationTime");

            var resultData = await resultQuery.GetListPaged<DatabaseConnection, DatabaseConnectionDto>(ObjectMapper, input);
            foreach (var resultDto in resultData.Data.Data)
            {
                resultDto.ConnectionString = _databaseConnectionStringEncryption.Decrypt(resultDto.ConnectionString);
            }
            return resultData;
        }

        public async Task<WebApiResult<ListResultDto<DatabaseConnectionDto>>> GetListAsync()
        {
            var databaseConnections = await _databaseConnectionRepository.GetListAsync();

            var databaseConnectionList = ObjectMapper.Map<List<DatabaseConnection>, List<DatabaseConnectionDto>>(databaseConnections);

            return WebApiResult<ListResultDto<DatabaseConnectionDto>>.SuccessResult(new ListResultDto<DatabaseConnectionDto>(databaseConnectionList));
        }

        public async Task<WebApiResult<DatabaseConnectionDto>> GetAsync(string id)
        {
            var databaseConnection = await _databaseConnectionRepository.GetAsync(id);
            var resultDto = ObjectMapper.Map<DatabaseConnection, DatabaseConnectionDto>(databaseConnection);
            resultDto.ConnectionString = _databaseConnectionStringEncryption.Decrypt(resultDto.ConnectionString);
            return WebApiResult<DatabaseConnectionDto>.SuccessResult(ObjectMapper.Map<DatabaseConnection, DatabaseConnectionDto>(databaseConnection));
        }

        [Authorize(PlatformPermissions.DatabaseConnections.Create)]
        public async Task<WebApiResult<DatabaseConnectionDto>> CreateAsync(CreateDatabaseConnectionDto input)
        {
            var existsDatabaseConnection = await _databaseConnectionRepository.FirstOrDefaultAsync(f => f.Name == input.Name);
            if (existsDatabaseConnection != null)
            {
                _errorMessageService.ThrowMessage(SystemFriendlyExceptionCode.DuplicateDatabaseConnectionName601);
            }
            var databaseConnection = new DatabaseConnection(GuidGenerator.Create().ToString())
            {
                DatabaseProviderType = input.DatabaseProviderType,
                Memo = input.Memo,
                Name = input.Name,
                ConnectionString = _databaseConnectionStringEncryption.Encrypt(input.ConnectionString),
                RangeTenantIds = input.RangeTenantIds
            };
            await _databaseConnectionRepository.InsertAsync(databaseConnection);
            return WebApiResult<DatabaseConnectionDto>.SuccessResult(ObjectMapper.Map<DatabaseConnection, DatabaseConnectionDto>(databaseConnection));
        }

        [Authorize(PlatformPermissions.DatabaseConnections.Update)]
        public async Task<WebApiResult<DatabaseConnectionDto>> UpdateAsync(string id, UpdateDatabaseConnectionDto input)
        {
            var databaseConnection = await _databaseConnectionRepository.GetAsync(id);
            databaseConnection.ConnectionString = _databaseConnectionStringEncryption.Encrypt(input.ConnectionString);
            databaseConnection.DatabaseProviderType = input.DatabaseProviderType;
            databaseConnection.Memo = input.Memo;
            databaseConnection.RangeTenantIds = input.RangeTenantIds;
            await _databaseConnectionRepository.UpdateAsync(databaseConnection);
            return WebApiResult<DatabaseConnectionDto>.SuccessResult(ObjectMapper.Map<DatabaseConnection, DatabaseConnectionDto>(databaseConnection));
        }

        [Authorize(PlatformPermissions.DatabaseConnections.Delete)]
        public async Task<WebApiResult> DeleteAsync(string id)
        {
            await _databaseConnectionRepository.DeleteAsync(id);
            return WebApiResult.SuccessResult();
        }

        [Authorize(PlatformPermissions.DatabaseConnections.Update)]
        public async Task<WebApiResult<DatabaseConnectionDto>> UpdateStatusAsync(string id, UpdateDatabaseConnectionStatusDto input)
        {
            var entity = await _databaseConnectionRepository.GetAsync(id);
            entity.Status = input.Status;
            return WebApiResult<DatabaseConnectionDto>.SuccessResult(ObjectMapper.Map<DatabaseConnection, DatabaseConnectionDto>(entity));
        }

    }
}
