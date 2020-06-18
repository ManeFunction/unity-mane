using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Mane.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 Translate(this Vector3 v, float dX, float dY, float dZ)
        {
            v.x += dX;
            v.y += dY;
            v.z += dZ;
            
            return v;
        }

        public static Vector3 Translate(this Vector3 v, Vector3 d)
        {
            v.x += d.x;
            v.y += d.y;
            v.z += d.z;
            
            return v;
        }

        public static Vector3 Translate(this Vector3 v, Vector2 d)
        {
            v.x += d.x;
            v.y += d.y;
            
            return v;
        }

        public static Vector2 Translate(this Vector2 v, float dX, float dY)
        {
            v.x += dX;
            v.y += dY;
            
            return v;
        }

        public static Vector2 Translate(this Vector2 v, Vector2 d)
        {
            v.x += d.x;
            v.y += d.y;
            
            return v;
        }
        
        public static Vector3 Translate(this Vector2 v, float dX, float dY, float dZ)
        {
            return new Vector3(v.x + dX, v.y + dY, dZ);
        }
        
        
        public static Vector3 TranslateX(this Vector3 v, float dX)
        {
            v.x += dX;

            return v;
        }

        public static Vector3 TranslateY(this Vector3 v, float dY)
        {
            v.y += dY;

            return v;
        }

        public static Vector3 TranslateZ(this Vector3 v, float dZ)
        {
            v.z += dZ;

            return v;
        }
        
        public static Vector2 TranslateX(this Vector2 v, float dX)
        {
            v.x += dX;

            return v;
        }

        public static Vector2 TranslateY(this Vector2 v, float dY)
        {
            v.y += dY;

            return v;
        }

        
        public static Vector3 SetX(this Vector3 v, float x)
        {
            v.x = x;
            
            return v;
        }

        public static Vector3 SetY(this Vector3 v, float y)
        {
            v.y = y;
            
            return v;
        }

        public static Vector3 SetZ(this Vector3 v, float z)
        {
            v.z = z;
            
            return v;
        }

        public static Vector2 SetX(this Vector2 v, float x)
        {
            v.x = x;
            
            return v;
        }

        public static Vector2 SetY(this Vector2 v, float y)
        {
            v.y = y;
            
            return v;
        }
        

        public static Vector3 FlipX(this Vector3 v)
        {
            v.x *= -1;
            
            return v;
        }

        public static Vector3 FlipY(this Vector3 v)
        {
            v.y *= -1;
            
            return v;
        }

        public static Vector3 FlipZ(this Vector3 v)
        {
            v.z *= -1;
            
            return v;
        }

        public static Vector2 FlipX(this Vector2 v)
        {
            v.x *= -1;
            
            return v;
        }

        public static Vector2 FlipY(this Vector2 v)
        {
            v.y *= -1;
            
            return v;
        }

        public static Vector3 AddZ(this Vector2 v, float z = 0f)
        {
            return new Vector3(v.x, v.y, z);
        }


        public static Vector2 Clamp(this Vector2 v, float a, float b)
        {
            v.x = v.x.Clamp(a, b);
            v.y = v.y.Clamp(a, b);

            return v;
        }

        public static Vector3 Clamp(this Vector3 v, float a, float b)
        {
            v.x = v.x.Clamp(a, b);
            v.y = v.x.Clamp(a, b);
            v.z = v.x.Clamp(a, b);

            return v;
        }
        
        public static Vector2 Project(this Vector2 v, Vector2 onNormal)
        {
            float num1 = Vector2.Dot(onNormal, onNormal);
            if (num1 < Mathf.Epsilon)
            {
                return Vector2.zero;
            }
            float num2 = Vector2.Dot(v, onNormal);
            
            return new Vector2(onNormal.x * num2 / num1, onNormal.y * num2 / num1);
        }

        public static float Volume(this Vector3 size) => size.x * size.y * size.z;

        public static float Area(this Vector2 size) => size.x * size.y;

        // Thanks to bronxbomber92 (https://forum.unity.com/threads/math-problem.8114/#post-59715)
        public static Vector3 ClosestPointOnLine(this Vector3 vPoint, Vector3 vA, Vector3 vB)
        {
            Vector3 vVector1 = vPoint - vA;
            Vector3 vVector2 = (vB - vA).normalized;
 
            float d = Vector3.Distance(vA, vB);
            float t = Vector3.Dot(vVector2, vVector1);
 
            if (t <= 0) return vA;
            if (t >= d) return vB;
 
            Vector3 vVector3 = vVector2 * t;
            Vector3 vClosestPoint = vA + vVector3;
 
            return vClosestPoint;
        }
        
        // Thanks to Saeed Amiri (https://stackoverflow.com/questions/4243042/c-sharp-point-in-polygon)
        /// <summary>
        /// Define polygon with points CW or CCW, works with convex polygons
        /// </summary>
        public static bool IsInPolygon(this Vector3 point, Vector3[] poly)
        {
            List<float> coef = poly.Skip(1)
                                   .Select((p, i) => 
                                           (point.y - poly[i].y) * (p.x - poly[i].x) 
                                         - (point.x - poly[i].x) * (p.y - poly[i].y))
                                   .ToList();

            if (coef.Any(p => Math.Abs(p) < float.Epsilon)) return true;

            for (int i = 1; i < coef.Count; i++)
            {
                if (coef[i] * coef[i - 1] < 0) return false;
            }
            
            return true;
        }
    }
}