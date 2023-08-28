using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Mane
{
    public static class Async
    {
        public static IEnumerator ToCoroutine<T>(Task<T> task, Action<bool, T> callback)
        {
            yield return new WaitUntil(() => task.IsCompleted);
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
    }
}