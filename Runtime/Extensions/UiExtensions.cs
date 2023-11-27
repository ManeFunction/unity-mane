using UnityEngine;
using UnityEngine.UI;

namespace Mane.Extensions
{
    /// <summary>
    /// Provides extension methods for UI elements.
    /// </summary>
    public static class UiExtensions
    {
        /// <summary>
        /// Snaps a ScrollRect to a specific item.
        /// </summary>
        /// <param name="scroll">The ScrollRect to snap.</param>
        /// <param name="item">The item to snap to.</param>
        /// <param name="offset">An optional offset to apply when snapping.</param>
        public static void SnapTo(this ScrollRect scroll, Transform item, Vector2 offset = default)
        {
            // Based on this discussion, but improved:
            // https://stackoverflow.com/questions/30766020/how-to-scroll-to-a-specific-element-in-scrollrect-with-unity-ui
            Canvas.ForceUpdateCanvases();

            Vector2 contentPos = scroll.transform.InverseTransformPoint(scroll.content.position);
            Vector2 childPos = scroll.transform.InverseTransformPoint(item.position);
            Vector2 endPos = contentPos - childPos;

            if (!scroll.horizontal) endPos.x = contentPos.x;
            if (!scroll.vertical) endPos.y = contentPos.y;
            scroll.content.anchoredPosition = endPos + offset;
        }

        /// <summary>
        /// Snaps a ScrollRect to a specific item along the X axis.
        /// </summary>
        /// <param name="scroll">The ScrollRect to snap.</param>
        /// <param name="item">The item to snap to.</param>
        /// <param name="offset">An optional offset to apply when snapping.</param>
        public static void SnapXTo(this ScrollRect scroll, Transform item, float offset = 0f)
        {
            Canvas.ForceUpdateCanvases();

            Vector2 contentPos = scroll.transform.InverseTransformPoint(scroll.content.position);
            float childPos = scroll.transform.InverseTransformPoint(item.position).x;
            float x = contentPos.x - childPos;

            scroll.content.anchoredPosition = new Vector2(x + offset, contentPos.y);
        }
        
        /// <summary>
        /// Snaps a ScrollRect to a specific item along the Y axis.
        /// </summary>
        /// <param name="scroll">The ScrollRect to snap.</param>
        /// <param name="item">The item to snap to.</param>
        /// <param name="offset">An optional offset to apply when snapping.</param>
        public static void SnapYTo(this ScrollRect scroll, Transform item, float offset = 0f)
        {
            Canvas.ForceUpdateCanvases();

            Vector2 contentPos = scroll.transform.InverseTransformPoint(scroll.content.position);
            float childPos = scroll.transform.InverseTransformPoint(item.position).y;
            float y = contentPos.y - childPos;

            scroll.content.anchoredPosition = new Vector2(contentPos.x, y + offset);
        }
    }
}