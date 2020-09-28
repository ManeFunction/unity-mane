using UnityEngine;
using UnityEngine.UI;

namespace Mane.Extensions
{
    public static class UiExtensions
    {
        // Based on this discussion:
        // https://stackoverflow.com/questions/30766020/how-to-scroll-to-a-specific-element-in-scrollrect-with-unity-ui
        public static void SnapTo(this ScrollRect scroller, Transform child)
        {
            Canvas.ForceUpdateCanvases();

            Vector2 contentPos = scroller.transform.InverseTransformPoint(scroller.content.position);
            Vector2 childPos = scroller.transform.InverseTransformPoint(child.position);
            Vector2 endPos = contentPos - childPos;

            if (!scroller.horizontal) endPos.x = contentPos.x;
            if (!scroller.vertical) endPos.y = contentPos.y;
            scroller.content.anchoredPosition = endPos;
        }

        // Optimized versions
        public static void SnapXTo(this ScrollRect scroller, Transform child)
        {
            Canvas.ForceUpdateCanvases();

            Vector2 contentPos = scroller.transform.InverseTransformPoint(scroller.content.position);
            float childPos = scroller.transform.InverseTransformPoint(child.position).x;
            float x = contentPos.x - childPos;

            scroller.content.anchoredPosition = new Vector2(x, contentPos.y);
        }
        public static void SnapYTo(this ScrollRect scroller, Transform child)
        {
            Canvas.ForceUpdateCanvases();

            Vector2 contentPos = scroller.transform.InverseTransformPoint(scroller.content.position);
            float childPos = scroller.transform.InverseTransformPoint(child.position).y;
            float y = contentPos.y - childPos;

            scroller.content.anchoredPosition = new Vector2(contentPos.x, y);
        }
    }
}