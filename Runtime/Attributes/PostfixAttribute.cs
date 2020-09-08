using UnityEngine;

namespace Mane.Extensions
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