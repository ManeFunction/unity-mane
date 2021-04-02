using UnityEngine;

namespace Mane.Inspector
{
    public class PostfixAttribute : PropertyAttribute
    {
        public string Text { get; private set; }
        public float Width { get; private set; }

        public PostfixAttribute(string text, float width = 0f)
        {
            Text = text;
            Width = width;
        }
    }
}