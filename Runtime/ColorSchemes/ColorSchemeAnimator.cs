using System.Collections;
using Mane.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mane.UI
{
    [RequireComponent(typeof(ColorSchemeComponent))]
    public class ColorSchemeAnimator : UIBehaviour
    {
        [SerializeField] private ColorSchemeComponent _colorSchemeComponent;
        [SerializeField] private AnimationCurve _animationCurve =
            AnimationCurve.Linear(0f, 0f, 1f, 1f);

        #if UNITY_EDITOR
        protected override void Reset()
        {
            base.Reset();

            _colorSchemeComponent = gameObject.GetRequiredComponent<ColorSchemeComponent>();
        }
        #endif
        
        public void Animate(ColorScheme colorScheme, float duration)
        {
            if (duration <= 0f || _colorSchemeComponent.ColorScheme == null)
            {
                _colorSchemeComponent.ColorScheme = colorScheme;
                
                return;
            }
            
            if (_colorSchemeComponent.ColorScheme.Length != colorScheme.Length)
                return;
            
            _colorSchemeComponent.SetColorSchemeWithoutRefresh(colorScheme);

            StopAllCoroutines();
            StartCoroutine(AnimateCoroutine(colorScheme, duration));
        }
        
        private IEnumerator AnimateCoroutine(ColorScheme targetScheme, float duration)
        {
            float time = 0f;
            Color[] from = new Color[targetScheme.Length];
            for (int i = 0; i < targetScheme.Length; i++)
                from[i] = _colorSchemeComponent.GetGraphicColor(i);
            while (time < duration)
            {
                float t = _animationCurve.Evaluate(time / duration);
                for (int i = 0; i < targetScheme.Length; i++)
                {
                    Color color = Color.Lerp(from[i], targetScheme[i], t);
                    _colorSchemeComponent.SetGraphicsColor(i, color);
                }

                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}