using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mane
{
    public class MainThreadDispatcher : MonoBehaviour
    {
        private static MainThreadDispatcher _instance;
        private readonly Queue<Action> _actions = new Queue<Action>();

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static void RunOnMainThread(Action action)
        {
            if (_instance == null)
            {
                Debug.LogError("MainThreadDispatcher is not present in the scene.");
                return;
            }

            lock (_instance._actions)
            {
                _instance._actions.Enqueue(action);
            }
        }

        private void Update()
        {
            while (_actions.Count > 0)
            {
                Action action;
                lock (_actions)
                {
                    action = _actions.Dequeue();
                }
                action();
            }
        }
    }
}