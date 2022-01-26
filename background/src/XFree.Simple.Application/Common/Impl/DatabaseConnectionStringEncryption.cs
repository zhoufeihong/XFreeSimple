using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Encryption;
using XFree.Simple.Application.Contracts.Options;

namespace XFree.Simple.Application.Common.Impl
{

    public class DatabaseConnectionStringEncryption : IScopedDependency
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IStringEncryptionService _stringEncryptionService;

        /// <summary>
        /// 
        /// </summary>
        private readonly IOptions<StringEncryptionOptions> _stringEncryptionOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringEncryptionService"></param>
        /// <param name="stringEncryptionOptions"></param>
        public DatabaseConnectionStringEncryption(IStringEncryptionService stringEncryptionService,
            IOptions<StringEncryptionOptions> stringEncryptionOptions)
        {
            _stringEncryptionService = stringEncryptionService;
            _stringEncryptionOptions = stringEncryptionOptions;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public string Decrypt(string cipherText)
        {
            return _stringEncryptionService.Decrypt(cipherText, _stringEncryptionOptions.Value.DatabaseConnectionStringPassPhrase);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string Encrypt(string plainText)
        {
            return _stringEncryptionService.Encrypt(plainText, _stringEncryptionOptions.Value.DatabaseConnectionStringPassPhrase);
        }
    }
}
