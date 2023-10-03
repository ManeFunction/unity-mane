using System;
using UnityEngine;

namespace Mane
{
    public class StatusEventSender : MonoBehaviour
    {
        public event Action<GameObject, bool> StatusChanged;

        private void OnEnable() => StatusChanged?.Invoke(gameObject, true);

        private void OnDisable() => StatusChanged?.Invoke(gameObject, false);
    }
}