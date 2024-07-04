#if UNITY_TMPRO
using Mane.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mane.UI
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TextMeshProUGUI))]
    [RequireComponent(typeof(LayoutElement))]
    public class MaxTMProSize : UIBehaviour
    {
        [Header("Required components")]
        [SerializeField] private TextMeshProUGUI _text;

        [SerializeField] private LayoutElement _layoutElement;

        [Header("Optional components")]
        [SerializeField] private ContentSizeFitter _contentSizeFitter;

        [SerializeField] private float _maxWidth = -1;
        [SerializeField] private float _maxHeight = -1;

#if UNITY_EDITOR
        public const string TextPropertyName = nameof(_text);
        public const string LayoutElementPropertyName = nameof(_layoutElement);
        public const string ContentSizeFitterPropertyName = nameof(_contentSizeFitter);

        public const string MaxWidthPropertyName = nameof(_maxWidth);
        public const string MaxHeightPropertyName = nameof(_maxHeight);
#endif

        private string _oldValue = string.Empty;


        public float MaxWidth
        {
            get => _maxWidth;
            set
            {
                _maxWidth = value;
                ReCalculateLayout();
            }
        }

        public float MaxHeight
        {
            get => _maxHeight;
            set
            {
                _maxHeight = value;
                ReCalculateLayout();
            }
        }


#if UNITY_EDITOR
        protected override void Reset()
        {
            _text = gameObject.GetRequiredComponent<TextMeshProUGUI>();
            _layoutElement = gameObject.GetRequiredComponent<LayoutElement>();

            _contentSizeFitter = gameObject.GetComponent<ContentSizeFitter>();

            ReCalculateLayout();
        }

        protected override void OnValidate() => ReCalculateLayout();
#endif

        protected void Update()
        {
            if (!_text || _text.text == _oldValue) return;

            _oldValue = _text.text;
            ReCalculateLayout();
        }

        private void ReCalculateLayout()
        {
            // width
            if (_maxWidth >= 0f)
            {
                _layoutElement.preferredWidth = Mathf.Min(_text.preferredWidth, _maxWidth);
                TrySetContentSizeFitter(ContentSizeFitter.FitMode.PreferredSize, true);
            }
            else
            {
                _layoutElement.preferredWidth = -1f;
                TrySetContentSizeFitter(ContentSizeFitter.FitMode.Unconstrained, true);
            }

            // height
            if (_maxHeight >= 0f)
            {
                _layoutElement.preferredHeight = Mathf.Min(_text.preferredHeight, _maxHeight);
                TrySetContentSizeFitter(ContentSizeFitter.FitMode.PreferredSize, false);
            }
            else
            {
                _layoutElement.preferredHeight = -1f;
                TrySetContentSizeFitter(ContentSizeFitter.FitMode.Unconstrained, false);
            }

            return;


            void TrySetContentSizeFitter(ContentSizeFitter.FitMode fitMode, bool horizontal)
            {
                if (!_contentSizeFitter) return;

                if (horizontal)
                    _contentSizeFitter.horizontalFit = fitMode;
                else
                    _contentSizeFitter.verticalFit = fitMode;
            }
        }
    }
}
#endif