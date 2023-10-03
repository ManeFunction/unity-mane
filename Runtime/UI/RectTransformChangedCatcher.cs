using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mane.UI
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public class RectTransformChangedCatcher : UIBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        
        public event Action<RectTransform> OnRectTransformChanged;
        
#if UNITY_EDITOR
        protected override void Reset() => _rectTransform = gameObject.GetComponent<RectTransform>();
#endif

        private void Update()
        {
            if (_rectTransform.hasChanged)
                OnRectTransformChanged?.Invoke(_rectTransform);
        }
    }
}
