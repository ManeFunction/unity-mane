using Mane.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Mane
{
    [ExecuteAlways]
    [RequireComponent(typeof(Text))]
    [RequireComponent(typeof(LayoutElement))]
    public class MaxTextSize : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private LayoutElement _layoutElement;
        
        [SerializeField] private float _maxWidth = -1;
        [SerializeField] private float _maxHeight = -1;

        private string _oldValue = string.Empty;
        

#if UNITY_EDITOR
        private void Reset()
        {
            _text = gameObject.GetRequiredComponent<Text>();
            _layoutElement = gameObject.GetRequiredComponent<LayoutElement>();
            
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
            _layoutElement.preferredWidth = _maxWidth >= 0f ? Mathf.Min(_text.preferredWidth, _maxWidth) : -1f;
            _layoutElement.preferredHeight = _maxHeight >= 0f ? Mathf.Min(_text.preferredHeight, _maxHeight) : -1f;
        }
    }
}