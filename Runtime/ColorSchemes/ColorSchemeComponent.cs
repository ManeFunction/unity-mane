using System;
using UnityEngine;
using UnityEngine.UI;

namespace Mane
{
    public class ColorSchemeComponent : MonoBehaviour
    {
        [SerializeField] protected GraphicCollection[] _graphic;

        [SerializeField] private ColorScheme _colorScheme;

        protected void Awake() => RefreshColorScheme();

        public void RefreshColorScheme(ColorScheme colorScheme = null)
        {
            if (colorScheme != null)
                _colorScheme = colorScheme;

            if (_colorScheme == null || _graphic.Length == 0) return;

            for (int i = 0; i < _graphic.Length; i++)
            {
                for (int j = 0; j < _graphic[i].Length; j++)
                {
                    MaskableGraphic graphic = _graphic[i][j];
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


        [Serializable]
        public class GraphicCollection
        {
            [SerializeField] private MaskableGraphic[] _graphic;
            
            public MaskableGraphic this[int index] => _graphic[index];
            public int Length => _graphic.Length;
        }
    }
}