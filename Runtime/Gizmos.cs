using UnityEngine;

namespace Mane.Extensions
{
    public static class Gizmos
    {
        public static void DrawPlane(Plane plane, Vector3 drawPosition) =>
            DrawPlane(plane.ClosestPointOnPlane(drawPosition), plane.normal);

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