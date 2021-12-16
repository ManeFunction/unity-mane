using UnityEngine;

namespace Mane.Inspector
{
    public class PostfixAttribute : PropertyAttribute
    {
        public string Text { get; }
        public float Width { get; }

        public PostfixAttribute(string text, float width = 0f)
        {
            Text = text;
            Width = width;
        }
    }
}