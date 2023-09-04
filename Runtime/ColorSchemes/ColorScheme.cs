using UnityEngine;

namespace Mane
{
    [CreateAssetMenu(fileName = "ColorScheme", menuName = "Mane/Color Scheme")]
    public class ColorScheme : ScriptableObject
    {
        [SerializeField] private Color[] _colors = { Color.white };

        public int Length => _colors.Length;

        public Color this[int index] =>
            index < 0 || index >= _colors.Length ? Color.white : _colors[index];

        private void SetColor(int i, Color color)
        {
            if (i >= 0 && i < _colors.Length)
                _colors[i] = color;
        }
    }
}