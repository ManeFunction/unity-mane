using System;
using UnityEngine;

namespace Mane
{
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public class RectTransformChangedCatcher : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        
        public event Action<RectTransform> OnRectTransformChanged;
        
#if UNITY_EDITOR
        private void Reset() => _rectTransform = gameObject.GetComponent<RectTransform>();
#endif

        private void Update()
        {
            if (_rectTransform.hasChanged)
                OnRectTransformChanged?.Invoke(_rectTransform);
        }
    }
}
