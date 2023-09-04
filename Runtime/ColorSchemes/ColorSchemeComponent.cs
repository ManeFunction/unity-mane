using UnityEngine;
using UnityEngine.UI;

namespace Mane
{
    public class ColorSchemeComponent : MonoBehaviour
    {
        [SerializeField] protected MaskableGraphic[] _graphics;

        [SerializeField] private ColorScheme _colorScheme;

        protected void Awake() => RefreshColorScheme();

        public void RefreshColorScheme(ColorScheme colorScheme = null)
        {
            if (colorScheme != null)
                _colorScheme = colorScheme;

            if (_colorScheme == null || _graphics.Length == 0) return;

            for (int i = 0; i < _graphics.Length; i++)
            {
                MaskableGraphic graphic = _graphics[i];
                if (graphic && i < _colorScheme.Length)
                {
#if UNITY_EDITOR
                    if (graphic.color != _colorScheme[i])
                        UnityEditor.EditorUtility.SetDirty(graphic);
#endif
                    graphic.color = _colorScheme[i];
                }
            }
        }
    }
}