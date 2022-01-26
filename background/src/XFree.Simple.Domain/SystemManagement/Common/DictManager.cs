using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using XFree.Simple.Domain.Shared.SystemManagement;
using XFree.Simple.Domain.Shared.SystemManagement.Domain;
using XFree.Simple.Domain.Shared.Common;

namespace XFree.Simple.Domain.SystemManagement.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class DictManager : DomainService
    {

        private readonly IRepository<Dict, string> _dictRepository;

        private readonly IRepository<DictItem, string> _dictItemRepository;

        private readonly ErrorMessageService _errorMessageService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictRepository"></param>
        /// <param name="dictItemRepository"></param>
        /// <param name="errorMessageService"></param>
        public DictManager(IRepository<Dict, string> dictRepository,
            IRepository<DictItem, string> dictItemRepository, 
            ErrorMessageService errorMessageService)
        {
            _dictRepository = dictRepository;
            _dictItemRepository = dictItemRepository;
            _errorMessageService = errorMessageService;
        }

        /// <summary>
        ///  添加字典项
        /// </summary>
        /// <param name="dictItem"></param>
        /// <returns></returns>
        public async Task<DictItem> AddDictItem(DictItem dictItem)
        {
            var dict = await _dictRepository.GetAsync(dictItem.DictId, false);
            if (dict == null)
            {
                throw new ArgumentException(nameof(dictItem.DictId));
            }
            // 重复编码
            if (await _dictItemRepository.AnyAsync(f => f.DictId == dict.Id && f.ItemValue == dictItem.ItemValue))
            {
                _errorMessageService.ThrowMessageParam(SystemFriendlyExceptionCode.DuplicateDictItemValue, dictItem.ItemValue);
            }
            await _dictItemRepository.InsertAsync(dictItem);
            return dictItem;
        }

        /// <summary>
        /// 更新字典项
        /// </summary>
        /// <param name="dictItemId"></param>
        /// <param name="newItemValue"></param>
        /// <param name="newItemText"></param>
        /// <param name="newDescription"></param>
        /// <param name="sortOrder"></param>
        /// <param name="newItemEnText"></param>
        /// <returns></returns>
        public async Task<DictItem> UpdateDictItem(string dictItemId,
            string newItemValue,
            string newItemText,
            string newDescription,
            int? sortOrder,
            string newItemEnText)
        {
            var dictItem = await _dictItemRepository.FirstOrDefaultAsync(f => f.Id == dictItemId);
            if (dictItem == null)
            {
                throw new ArgumentException(null, nameof(dictItemId));
            }
            if (await _dictItemRepository.AnyAsync(a => a.Id != dictItemId && a.DictId == dictItem.DictId && a.ItemValue == newItemValue))
            {
                _errorMessageService.ThrowMessageParam(SystemFriendlyExceptionCode.DuplicateDictItemValue, newItemValue);
            }
            dictItem.ItemValue = newItemValue;
            dictItem.ItemText = newItemText;
            dictItem.Description = newDescription;
            dictItem.SortOrder = sortOrder;
            dictItem.ItemEnText = newItemEnText;
            return dictItem;
        }

    }
}
