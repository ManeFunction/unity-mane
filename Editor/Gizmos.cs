using UnityEngine;

namespace Mane.Editor
{
    /// <summary>
    /// Provides a set of tools for drawing Gizmos in the Unity Editor.
    /// </summary>
    public static class Gizmos
    {
        /// <summary>
        /// Draws a plane Gizmo at the specified position.
        /// </summary>
        /// <param name="plane">The plane to draw.</param>
        /// <param name="drawPosition">The position at which to draw the plane.</param>
        public static void DrawPlane(Plane plane, Vector3 drawPosition) => 
            DrawPlane(plane.ClosestPointOnPlane(drawPosition), plane.normal);

        /// <summary>
        /// Draws a plane Gizmo at the specified position and with the specified normal.
        /// </summary>
        /// <param name="position">The position at which to draw the plane.</param>
        /// <param name="normal">The normal of the plane.</param>
        public static void DrawPlane(Vector3 position, Vector3 normal)
        {
            Vector3 v3 = normal.normalized != Vector3.forward
                ? Vector3.Cross(normal, Vector3.forward).normalized * normal.magnitude
                : Vector3.Cross(normal, Vector3.up).normalized * normal.magnitude;

            Vector3 corner0 = position + v3;
            Vector3 corner2 = position - v3;
            Quaternion q = Quaternion.AngleAxis(90f, normal);
            v3 = q * v3;
            Vector3 corner1 = position + v3;
            Vector3 corner3 = position - v3;

            Debug.DrawLine(corner0, corner2, Color.green);
            Debug.DrawLine(corner1, corner3, Color.green);
            Debug.DrawLine(corner0, corner1, Color.green);
            Debug.DrawLine(corner1, corner2, Color.green);
            Debug.DrawLine(corner2, corner3, Color.green);
            Debug.DrawLine(corner3, corner0, Color.green);
            Debug.DrawRay(position, normal, Color.red);
        }
    }
}