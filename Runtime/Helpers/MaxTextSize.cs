using Mane.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Mane
{
    [ExecuteAlways]
    [RequireComponent(typeof(Text))]
    [RequireComponent(typeof(LayoutElement))]
    [RequireComponent(typeof(ContentSizeFitter))]
    public class MaxTextSize : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private LayoutElement _layoutElement;
        [SerializeField] private ContentSizeFitter _contentSizeFitter;
        
        [SerializeField] private float _maxWidth = -1;
        [SerializeField] private float _maxHeight = -1;

        private string _oldValue = string.Empty;
        

#if UNITY_EDITOR
        private void Reset()
        {
            _text = gameObject.GetRequiredComponent<Text>();
            _layoutElement = gameObject.GetRequiredComponent<LayoutElement>();
            _contentSizeFitter = gameObject.GetRequiredComponent<ContentSizeFitter>();
            
            ReCalculateLayout();
        }

        private void OnValidate() => ReCalculateLayout();
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
                _contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            }
            else
            {
                _layoutElement.preferredWidth = -1f;
                _contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            }

            // height
            if (_maxHeight >= 0f)
            {
                _layoutElement.preferredHeight = Mathf.Min(_text.preferredHeight, _maxHeight);
                _contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            }
            else
            {
                _layoutElement.preferredHeight = -1f;
                _contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            }
        }
    }
}