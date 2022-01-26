using JetBrains.Annotations;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.Domain.Shared.SystemManagement;

namespace XFree.Simple.Domain.Shared.Common
{

    /// <summary>
    /// This class can be used to provide an action when
    /// Dipose method is called.
    /// </summary>
    public class LockResultAction : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly Action _action;

        /// <summary>
        /// 
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// Creates a new <see cref="DisposeAction"/> object.
        /// </summary>
        /// <param name="action">Action to be executed when this object is disposed.</param>
        public LockResultAction([NotNull] Action action)
        {
            Check.NotNull(action, nameof(action));

            _action = action;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _action();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class SemaphoreSlimExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="semaphoreSlim"></param>
        /// <param name="millisecondsTimeout"></param>
        /// <returns></returns>
        public static async Task<LockResultAction> LockResultAsync(this SemaphoreSlim semaphoreSlim, int millisecondsTimeout)
        {
            var lockResult = await semaphoreSlim.WaitAsync(millisecondsTimeout);
            return new LockResultAction(() =>
            {
                if (lockResult)
                {
                    semaphoreSlim.Release();
                }
            })
            {
                Result = lockResult
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="semaphoreSlim"></param>
        /// <param name="millisecondsTimeout"></param>
        /// <param name="stringLocalizer"></param>
        /// <returns></returns>
        public static async Task<LockResultAction> LockResultTimeOutAsync(this SemaphoreSlim semaphoreSlim, int millisecondsTimeout, IStringLocalizer stringLocalizer)
        {
            var lockResult = await semaphoreSlim.WaitAsync(millisecondsTimeout);
            if (!lockResult)
            {
                FriendlyExceptionCode.TimeOut10062.ThrowMessage(stringLocalizer);
            }
            return new LockResultAction(() =>
            {
                if (lockResult)
                {
                    semaphoreSlim.Release();
                }
            })
            {
                Result = lockResult
            };
        }

    }

}
