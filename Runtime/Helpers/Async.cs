using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace Mane
{
    public static class Async
    {
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
        
        public static async Task WaitForCondition(Func<bool> condition, int delayMilliseconds = 100,
            CancellationToken cancellationToken = default)
        {
            while (!condition())
            {
                await Task.Delay(delayMilliseconds, cancellationToken);
            }
        }
        
        public static async void Synchronize(Task task, Action<bool> callback)
        {
            await task;
    
            callback?.Invoke(task.IsCompletedSuccessfully);
        }

        public static async void Synchronize<T>(Task<T> task, Action<bool, T> callback)
        {
            await task;
    
            callback?.Invoke(task.IsCompletedSuccessfully, task.Result);
        }
    }
}