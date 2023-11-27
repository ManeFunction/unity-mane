using UnityEngine;

namespace Mane.Extensions
{
    /// <summary>
    /// Provides extension methods for Rect.
    /// </summary>
    public static class RectExtensions
    {
        /// <summary>
        /// Scales the size of a Rect by a specified scale factor, using the center of the Rect as the pivot point.
        /// </summary>
        /// <param name="rect">The Rect to scale.</param>
        /// <param name="scale">The scale factor.</param>
        /// <returns>The scaled Rect.</returns>
        public static Rect ScaleSizeBy(this Rect rect, float scale) => 
            rect.ScaleSizeBy(scale, rect.center);

        /// <summary>
        /// Scales the size of a Rect by a specified scale factor, using a provided pivot point.
        /// </summary>
        /// <param name="rect">The Rect to scale.</param>
        /// <param name="scale">The scale factor.</param>
        /// <param name="pivotPoint">The pivot point for the scaling operation.</param>
        /// <returns>The scaled Rect.</returns>
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