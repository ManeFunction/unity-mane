using UnityEngine;

namespace Mane.Extensions
{
    public static class RectTransformExtensions
    {
        public static void SetLeftOffset(this RectTransform rt, float left) => 
            rt.offsetMin = new Vector2(left, rt.offsetMin.y);

        public static void SetRightOffset(this RectTransform rt, float right) => 
            rt.offsetMax = new Vector2(-right, rt.offsetMax.y);

        public static void SetTopOffset(this RectTransform rt, float top) => 
            rt.offsetMax = new Vector2(rt.offsetMax.x, -top);

        public static void SetBottomOffset(this RectTransform rt, float bottom) => 
            rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }
}