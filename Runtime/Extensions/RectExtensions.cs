using UnityEngine;

namespace Mane.Extensions
{
    public static class RectExtensions
    {
        public static Rect ScaleSizeBy(this Rect rect, float scale) => 
            rect.ScaleSizeBy(scale, rect.center);


        public static Rect ScaleSizeBy(this Rect rect, float scale, Vector2 pivotPoint)
        {
            Rect result = rect;
            result.x -= pivotPoint.x;
            result.y -= pivotPoint.y;

            result.xMin *= scale;
            result.xMax *= scale;
            result.yMin *= scale;
            result.yMax *= scale;

            result.x += pivotPoint.x;
            result.y += pivotPoint.y;

            return result;
        }
    }
}