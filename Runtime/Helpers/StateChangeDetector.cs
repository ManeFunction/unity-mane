using System;
using UnityEngine;

namespace Mane
{
    public class StateChangeDetector : MonoBehaviour
    {
        [Flags]
        public enum State
        {
            Enabled = 1 << 0,
            Disabled = 1 << 1,
            Destroyed = 1 << 2,
        }
        
        public event Action<GameObject, State> OnStateChanged;
        public event Action<GameObject> OnEnabled;
        public event Action<GameObject> OnDisabled;
        public event Action<GameObject> OnDestroyed;
        
        private void OnEnable()
        {
            OnEnabled?.Invoke(gameObject);
            OnStateChanged?.Invoke(gameObject, State.Enabled);
        }
        
        private void OnDisable()
        {
            OnDisabled?.Invoke(gameObject);
            OnStateChanged?.Invoke(gameObject, State.Disabled);
        }
        
        private void OnDestroy()
        {
            OnDestroyed?.Invoke(gameObject);
            OnStateChanged?.Invoke(gameObject, State.Destroyed);
        }
    }
}
