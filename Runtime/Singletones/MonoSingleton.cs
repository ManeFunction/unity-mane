using UnityEngine;

namespace Mane
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        public static bool IsReady() => Instance;

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.LogError($"{gameObject.name} was Destroyed because it is a duplicate of {Instance.gameObject.name} singleton of type {nameof(T)}.");
                Destroy(gameObject);
            }
        }
    }
}
