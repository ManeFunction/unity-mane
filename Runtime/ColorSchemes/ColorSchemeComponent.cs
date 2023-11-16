using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mane.UI
{
    public class ColorSchemeComponent : UIBehaviour
    {
        [SerializeField] protected GraphicCollection[] _graphic;

        [SerializeField] private ColorScheme _colorScheme;
        
        public ColorScheme ColorScheme
        {
            get => _colorScheme;
            set
            {
                _colorScheme = value;
                Refresh();
            }
        }

        protected override void Awake() => Refresh();

        public Color GetGraphicColor(int i)
        {
            if (i < _graphic.Length)
            {
                MaskableGraphic graphic = _graphic[i][0];
                if (graphic)
                    return graphic.color;
            }

            return Color.white;
        }

        public void SetGraphicsColor(int i, Color color)
        {
            for (int j = 0; j < _graphic[i].Length; j++)
            {
                MaskableGraphic graphic = _graphic[i][j];
                if (graphic)
                    graphic.color = color;
            }
        }
        
        public void SetColorSchemeWithoutRefresh(ColorScheme colorScheme) => 
            _colorScheme = colorScheme;

        public void Refresh()
        {
            if (_colorScheme == null || _graphic.Length == 0) return;

            for (int i = 0; i < _graphic.Length; i++)
                RefreshColor(i);
        }

        private void RefreshColor(int i)
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


        [Serializable]
        public class GraphicCollection
        {
            [SerializeField] private MaskableGraphic[] _graphic;
            
            public MaskableGraphic this[int index] => _graphic[index];
            public int Length => _graphic.Length;
        }
    }
}