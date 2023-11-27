using UnityEngine;

namespace Mane.Extensions
{
    /// <summary>
    /// Provides extension methods for RectTransform.
    /// </summary>
    public static class RectTransformExtensions
    {
        /// <summary>
        /// Sets the left offset of a RectTransform.
        /// </summary>
        /// <param name="rt">The RectTransform to set the offset of.</param>
        /// <param name="left">The left offset value.</param>
        public static void SetLeftOffset(this RectTransform rt, float left) => 
            rt.offsetMin = new Vector2(left, rt.offsetMin.y);

        /// <summary>
        /// Sets the right offset of a RectTransform.
        /// </summary>
        /// <param name="rt">The RectTransform to set the offset of.</param>
        /// <param name="right">The right offset value.</param>
        public static void SetRightOffset(this RectTransform rt, float right) => 
            rt.offsetMax = new Vector2(-right, rt.offsetMax.y);

        /// <summary>
        /// Sets the top offset of a RectTransform.
        /// </summary>
        /// <param name="rt">The RectTransform to set the offset of.</param>
        /// <param name="top">The top offset value.</param>
        public static void SetTopOffset(this RectTransform rt, float top) => 
            rt.offsetMax = new Vector2(rt.offsetMax.x, -top);

        /// <summary>
        /// Sets the bottom offset of a RectTransform.
        /// </summary>
        /// <param name="rt">The RectTransform to set the offset of.</param>
        /// <param name="bottom">The bottom offset value.</param>
        public static void SetBottomOffset(this RectTransform rt, float bottom) => 
            rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
        
        
        /// <summary>
        /// Gets the world coordinates of a RectTransform as a Rect.
        /// </summary>
        /// <param name="uiElement">The RectTransform to get the world coordinates of.</param>
        /// <returns>A Rect representing the world coordinates of the RectTransform. If the RectTransform is null, returns a Rect with all components set to zero.</returns>
        public static Rect GetWorldCoordinates(this RectTransform uiElement)
        {
            if (uiElement == null)
                return Rect.zero;
            
            var worldCorners = new Vector3[4];
            uiElement.GetWorldCorners (worldCorners);
            
            return new Rect(
                worldCorners[0].x,
                worldCorners[0].y,
                worldCorners[2].x - worldCorners[0].x,
                worldCorners[2].y - worldCorners[0].y);
        }
    }
}