using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace Mane
{
    /// <summary>
    /// Provides utility methods for working with asynchronous tasks.
    /// </summary>
    public static class Async
    {
        /// <summary>
        /// Converts a Task to a Coroutine.
        /// </summary>
        /// <typeparam name="T">The type of the task result.</typeparam>
        /// <param name="task">The task to convert.</param>
        /// <param name="callback">The callback to invoke when the task is completed. The first parameter indicates whether the task completed successfully. The second parameter is the result of the task.</param>
        /// <returns>An IEnumerator that can be used as a Coroutine.</returns>
        public static IEnumerator ToCoroutine<T>(Task<T> task, Action<bool, T> callback)
        {
            while (!task.IsCompleted)
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    callback?.Invoke(false, default);

                    yield break;
                }

                yield return null;
            }

            callback?.Invoke(true, task.Result);
        }

        /// <summary>
        /// Waits for a condition to be met using an asynchronous task.
        /// </summary>
        /// <param name="condition">The condition to wait for.</param>
        /// <param name="delayMilliseconds">The delay in milliseconds between each check of the condition.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the task.</param>
        /// <returns>A Task that completes when the condition is met.</returns>
        public static async Task WaitForCondition(Func<bool> condition, int delayMilliseconds = 100,
            CancellationToken cancellationToken = default)
        {
            while (!condition())
            {
                await Task.Delay(delayMilliseconds, cancellationToken);
            }
        }
    }
}