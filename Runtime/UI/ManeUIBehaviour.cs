using UnityEngine;
using UnityEngine.EventSystems;

namespace Mane.UI
{
    public class ManeUIBehaviour : UIBehaviour
    {
        private Transform _transform;
        private RectTransform _rectTransform;

        public new Transform transform => _transform;
        public RectTransform rectTransform => _rectTransform;

        protected override void Awake()
        {
            base.Awake();

            _transform = base.transform;
            _rectTransform = transform as RectTransform;
        }
    }
}
