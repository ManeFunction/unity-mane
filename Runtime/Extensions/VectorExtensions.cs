using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mane.Extensions
{
    /// <summary>
    /// Provides extension methods for Vector2 and Vector3.
    /// </summary>
    public static class VectorExtensions
    {
        /// <summary>
        /// Translates a Vector3 by adding specified values to its x, y, and z components.
        /// </summary>
        /// <param name="v">The Vector3 to translate.</param>
        /// <param name="dX">The value to add to the x component.</param>
        /// <param name="dY">The value to add to the y component.</param>
        /// <param name="dZ">The value to add to the z component.</param>
        /// <returns>The translated Vector3.</returns>
        public static Vector3 Translate(this Vector3 v, float dX, float dY, float dZ)
        {
            v.x += dX;
            v.y += dY;
            v.z += dZ;
            
            return v;
        }

        /// <summary>
        /// Translates a Vector3 by adding another Vector3 to it.
        /// </summary>
        /// <param name="v">The Vector3 to translate.</param>
        /// <param name="d">The Vector3 to add to the original Vector3.</param>
        /// <returns>The translated Vector3.</returns>
        public static Vector3 Translate(this Vector3 v, Vector3 d)
        {
            v.x += d.x;
            v.y += d.y;
            v.z += d.z;
            
            return v;
        }

        /// <summary>
        /// Translates a Vector3 by adding a Vector2 to its x and y components.
        /// </summary>
        /// <param name="v">The Vector3 to translate.</param>
        /// <param name="d">The Vector2 to add to the original Vector3's x and y components.</param>
        /// <returns>The translated Vector3.</returns>
        public static Vector3 Translate(this Vector3 v, Vector2 d)
        {
            v.x += d.x;
            v.y += d.y;
            
            return v;
        }

        /// <summary>
        /// Translates a Vector2 by adding specified values to its x and y components.
        /// </summary>
        /// <param name="v">The Vector2 to translate.</param>
        /// <param name="dX">The value to add to the x component.</param>
        /// <param name="dY">The value to add to the y component.</param>
        /// <returns>The translated Vector2.</returns>
        public static Vector2 Translate(this Vector2 v, float dX, float dY)
        {
            v.x += dX;
            v.y += dY;
            
            return v;
        }

        /// <summary>
        /// Translates a Vector2 by adding another Vector2 to it.
        /// </summary>
        /// <param name="v">The Vector2 to translate.</param>
        /// <param name="d">The Vector2 to add to the original Vector2.</param>
        /// <returns>The translated Vector2.</returns>
        public static Vector2 Translate(this Vector2 v, Vector2 d)
        {
            v.x += d.x;
            v.y += d.y;
            
            return v;
        }
        
        /// <summary>
        /// Translates a Vector2 to a Vector3 by adding specified values to its x, y, and z components.
        /// </summary>
        /// <param name="v">The Vector2 to translate.</param>
        /// <param name="dX">The value to add to the x component.</param>
        /// <param name="dY">The value to add to the y component.</param>
        /// <param name="dZ">The z component of the new Vector3.</param>
        /// <returns>The translated Vector3.</returns>
        public static Vector3 Translate(this Vector2 v, float dX, float dY, float dZ) => 
            new(v.x + dX, v.y + dY, dZ);

        /// <summary>
        /// Translates a Vector3 along the x-axis by adding a specified value to its x component.
        /// </summary>
        /// <param name="v">The Vector3 to translate.</param>
        /// <param name="dX">The value to add to the x component.</param>
        /// <returns>The translated Vector3.</returns>
        public static Vector3 TranslateX(this Vector3 v, float dX)
        {
            v.x += dX;

            return v;
        }

        /// <summary>
        /// Translates a Vector3 along the y-axis by adding a specified value to its y component.
        /// </summary>
        /// <param name="v">The vector3 to translate.</param>
        /// <param name="dY">The value to add to the y component.</param>
        /// <returns>The translated Vector3.</returns>
        public static Vector3 TranslateY(this Vector3 v, float dY)
        {
            v.y += dY;

            return v;
        }

        /// <summary>
        /// Translates a Vector3 along the z-axis by adding a specified value to its z component.
        /// </summary>
        /// <param name="v">The Vector3 to translate.</param>
        /// <param name="dZ">The value to add to the z component.</param>
        /// <returns>The translated Vector3.</returns>
        public static Vector3 TranslateZ(this Vector3 v, float dZ)
        {
            v.z += dZ;

            return v;
        }
        
        /// <summary>
        /// Translates a Vector2 along the x-axis by adding a specified value to its x component.
        /// </summary>
        /// <param name="v">The Vector2 to translate.</param>
        /// <param name="dX">The value to add to the x component.</param>
        /// <returns>The translated Vector2.</returns>
        public static Vector2 TranslateX(this Vector2 v, float dX)
        {
            v.x += dX;

            return v;
        }

        /// <summary>
        /// Translates a Vector2 along the y-axis by adding a specified value to its y component.
        /// </summary>
        /// <param name="v">The Vector2 to translate.</param>
        /// <param name="dY">The value to add to the y component.</param>
        /// <returns>The translated Vector2.</returns>
        public static Vector2 TranslateY(this Vector2 v, float dY)
        {
            v.y += dY;

            return v;
        }

        
        /// <summary>
        /// Sets the x component of a Vector3 to a specified value.
        /// </summary>
        /// <param name="v">The Vector3 to modify.</param>
        /// <param name="x">The new x component value.</param>
        /// <returns>The modified Vector3.</returns>
        public static Vector3 SetX(this Vector3 v, float x)
        {
            v.x = x;
            
            return v;
        }

        /// <summary>
        /// Sets the y component of a Vector3 to a specified value.
        /// </summary>
        /// <param name="v">The Vector3 to modify.</param>
        /// <param name="y">The new y component value.</param>
        /// <returns>The modified Vector3.</returns>
        public static Vector3 SetY(this Vector3 v, float y)
        {
            v.y = y;
            
            return v;
        }

        /// <summary>
        /// Sets the z component of a Vector3 to a specified value.
        /// </summary>
        /// <param name="v">The Vector3 to modify.</param>
        /// <param name="z">The new z component value.</param>
        /// <returns>The modified Vector3.</returns>
        public static Vector3 SetZ(this Vector3 v, float z)
        {
            v.z = z;
            
            return v;
        }

        /// <summary>
        /// Sets the x component of a Vector2 to a specified value.
        /// </summary>
        /// <param name="v">The Vector2 to modify.</param>
        /// <param name="x">The new x component value.</param>
        /// <returns>The modified Vector2.</returns>
        public static Vector2 SetX(this Vector2 v, float x)
        {
            v.x = x;
            
            return v;
        }

        /// <summary>
        /// Sets the y component of a Vector2 to a specified value.
        /// </summary>
        /// <param name="v">The Vector2 to modify.</param>
        /// <param name="y">The new y component value.</param>
        /// <returns>The modified Vector2.</returns>
        public static Vector2 SetY(this Vector2 v, float y)
        {
            v.y = y;
            
            return v;
        }
        

        /// <summary>
        /// Flips a Vector3 along the x-axis.
        /// </summary>
        /// <param name="v">The Vector3 to flip.</param>
        /// <returns>The flipped Vector3.</returns>
        public static Vector3 FlipX(this Vector3 v)
        {
            v.x *= -1;
            
            return v;
        }

        /// <summary>
        /// Flips a Vector3 along the y-axis.
        /// </summary>
        /// <param name="v">The Vector3 to flip.</param>
        /// <returns>The flipped Vector3.</returns>
        public static Vector3 FlipY(this Vector3 v)
        {
            v.y *= -1;
            
            return v;
        }

        /// <summary>
        /// Flips a Vector3 along the z-axis.
        /// </summary>
        /// <param name="v">The Vector3 to flip.</param>
        /// <returns>The flipped Vector3.</returns>
        public static Vector3 FlipZ(this Vector3 v)
        {
            v.z *= -1;
            
            return v;
        }

        /// <summary>
        /// Flips a Vector2 along the x-axis.
        /// </summary>
        /// <param name="v">The Vector2 to flip.</param>
        /// <returns>The flipped Vector2.</returns>
        public static Vector2 FlipX(this Vector2 v)
        {
            v.x *= -1;
            
            return v;
        }

        /// <summary>
        /// Flips a Vector2 along the y-axis.
        /// </summary>
        /// <param name="v">The Vector2 to flip.</param>
        /// <returns>The flipped Vector2.</returns>
        public static Vector2 FlipY(this Vector2 v)
        {
            v.y *= -1;
            
            return v;
        }

        
        /// <summary>
        /// Adds a z component to a Vector2.
        /// </summary>
        /// <param name="v">The Vector2 to add a z component to.</param>
        /// <param name="z">The z component value.</param>
        /// <returns>The Vector3 with the z component added.</returns>
        public static Vector3 AddZ(this Vector2 v, float z = 0f) => new(v.x, v.y, z);


        /// <summary>
        /// Clamps the x and y components of a Vector2 between two specified values.
        /// </summary>
        /// <param name="v">The Vector2 to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped Vector2.</returns>
        public static Vector2 Clamp(this Vector2 v, float min, float max)
        {
            v.x = v.x.Clamp(min, max);
            v.y = v.y.Clamp(min, max);

            return v;
        }

        /// <summary>
        /// Clamps the x, y, and z components of a Vector3 between two specified values.
        /// </summary>
        /// <param name="v">The Vector3 to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped Vector3.</returns>
        public static Vector3 Clamp(this Vector3 v, float min, float max)
        {
            v.x = v.x.Clamp(min, max);
            v.y = v.x.Clamp(min, max);
            v.z = v.x.Clamp(min, max);

            return v;
        }
        
        /// <summary>
        /// Projects a Vector2 onto another Vector2.
        /// </summary>
        /// <param name="v">The Vector2 to project.</param>
        /// <param name="onNormal">The Vector2 to project onto.</param>
        /// <returns>The projected Vector2. If the dot product of onNormal with itself is less than a very small positive number, returns a zero Vector2.</returns>
        public static Vector2 Project(this Vector2 v, Vector2 onNormal)
        {
            float num1 = Vector2.Dot(onNormal, onNormal);
            if (num1 < Mathf.Epsilon)
                return Vector2.zero;
            
            float num2 = Vector2.Dot(v, onNormal);
            
            return new Vector2(onNormal.x * num2 / num1, onNormal.y * num2 / num1);
        }

        /// <summary>
        /// Calculates the volume of a 3D space defined by a Vector3.
        /// </summary>
        /// <param name="size">The Vector3 representing the dimensions of the 3D space.</param>
        /// <returns>The volume of the 3D space.</returns>
        public static float Volume(this Vector3 size) => size.x * size.y * size.z;

        /// <summary>
        /// Calculates the area of a 2D space defined by a Vector2.
        /// </summary>
        /// <param name="size">The Vector2 representing the dimensions of the 2D space.</param>
        /// <returns>The area of the 2D space.</returns>
        public static float Area(this Vector2 size) => size.x * size.y;

        /// <summary>
        /// Finds the closest point on a line defined by two points to a given point.
        /// </summary>
        /// <param name="vPoint">The point to find the closest point on the line to.</param>
        /// <param name="vA">The first point defining the line.</param>
        /// <param name="vB">The second point defining the line.</param>
        /// <returns>The closest point on the line to the given point.</returns>
        public static Vector3 ClosestPointOnLine(this Vector3 vPoint, Vector3 vA, Vector3 vB)
        {
            // Thanks to bronxbomber92 (https://forum.unity.com/threads/math-problem.8114/#post-59715)
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
        
        /// <summary>
        /// Determines whether a Vector2 point is inside a rectangle.
        /// </summary>
        /// <param name="p">The Vector2 point to check.</param>
        /// <param name="rect">The rectangle to check if the point is inside.</param>
        /// <returns>True if the point is inside the rectangle, false otherwise.</returns>
        public static bool IsInsideRectangle(this Vector2 p, Rect rect) =>
            p.IsInsideRectangle(rect.min, new Vector2(rect.xMin, rect.yMax),
                                rect.max, new Vector2(rect.xMax, rect.yMin));
        
        /// <summary>
        /// Determines whether a Vector2 point is inside a rectangle defined by an array of four Vector2 points.
        /// </summary>
        /// <param name="p">The Vector2 point to check.</param>
        /// <param name="rect">An array of four Vector2 points defining the rectangle.</param>
        /// <returns>True if the point is inside the rectangle, false otherwise.</returns>
        public static bool IsInsideRectangle(this Vector2 p, params Vector2[] rect)
        {
            if (rect.Length != 4)
                throw new ArgumentOutOfRangeException(nameof(rect), "The rect argument must be a 4 point array!");

            return IsInsideRectangle(p, rect[0], rect[1], rect[2], rect[3]);
        }
    
        /// <summary>
        /// Determines whether a Vector2 point is inside a rectangle defined by four Vector2 points.
        /// </summary>
        /// <param name="p">The Vector2 point to check.</param>
        /// <param name="p1">The first point defining the rectangle.</param>
        /// <param name="p2">The second point defining the rectangle.</param>
        /// <param name="p3">The third point defining the rectangle.</param>
        /// <param name="p4">The fourth point defining the rectangle.</param>
        /// <returns>True if the point is inside the rectangle, false otherwise.</returns>
        public static bool IsInsideRectangle(this Vector2 p, Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
        {
            float dot1 = Vector2.Dot(p - p1, p2 - p1);
            float dot2 = Vector2.Dot(p - p1, p4 - p1);
            float dot3 = Vector2.Dot(p - p3, p4 - p3);
            float dot4 = Vector2.Dot(p - p3, p2 - p3);

            return dot1 >= 0 && dot1 <= (p2 -p1).sqrMagnitude &&
                   dot2 >= 0 && dot2 <= (p4 -p1).sqrMagnitude &&
                   dot3 >= 0 && dot3 <= (p4 -p3).sqrMagnitude &&
                   dot4 >= 0 && dot4 <= (p2 -p3).sqrMagnitude;
        }
        
        /// <summary>
        /// Determines whether a Vector3 point is inside a polygon defined by an array of Vector3 points placed clockwise or counter-clockwise.
        /// </summary>
        /// <param name="point">The Vector3 point to check.</param>
        /// <param name="poly">An array of Vector3 points defining the polygon.</param>
        /// <returns>True if the point is inside the polygon, false otherwise.</returns>
        public static bool IsInPolygon(this Vector3 point, Vector3[] poly)
        {
            // Thanks to Saeed Amiri (https://stackoverflow.com/questions/4243042/c-sharp-point-in-polygon)
            float[] coef = new float[poly.Length];
            for (int i = 0; i < poly.Length; i++)
            {
                Vector3 prev = poly[i == 0 ? poly.Length - 1 : i - 1];
                Vector3 cur = poly[i];
                coef[i] = (point.y - cur.y) * (prev.x - cur.x) 
                        - (point.x - cur.x) * (prev.y - cur.y);
            }

            if (coef.Any(p => Math.Abs(p) < Single.Epsilon)) return true;

            for (int i = 1; i < coef.Length; i++)
                if (coef[i] * coef[i - 1] < 0) return false;
            
            return true;
        }
        
        
        /// <summary>
        /// Calculates the average of a collection of Vector3 values.
        /// Be aware of multiple enumerations!
        /// </summary>
        /// <param name="values">The collection of Vector3 values to average.</param>
        /// <returns>The average Vector3. If the collection is empty, returns Vector3.zero.</returns>
        public static Vector3 Average(this IEnumerable<Vector3> values)
        {
            if (!values.Any()) return Vector3.zero;
        
            Vector3 sum = Vector3.zero;
            int total = 0;
            values.ForEach(v =>
            {
                sum += v;
                total++;
            });

            return sum / total;
        }

        /// <summary>
        /// Calculates the average of a collection of Vector2 values.
        /// Be aware of multiple enumerations!
        /// </summary>
        /// <param name="values">The collection of Vector2 values to average.</param>
        /// <returns>The average Vector2. If the collection is empty, returns Vector2.zero.</returns>
        public static Vector2 Average(this IEnumerable<Vector2> values)
        {
            if (!values.Any()) return Vector2.zero;
            
            Vector2 sum = Vector2.zero;
            int total = 0;
            values.ForEach(v =>
            {
                sum += v;
                total++;
            });

            return sum / total;
        }

        /// <summary>
        /// Returns a random float number between the x and y components of a Vector2.
        /// Very neat feature to use Vector2 as a min/max range!
        /// </summary>
        /// <param name="value">The Vector2 to get the range from.</param>
        /// <returns>A random float number between the x and y components of the Vector2.</returns>
        public static float RandomBetween(this Vector2 value) => 
            UnityEngine.Random.Range(value.x, value.y);

        /// <summary>
        /// Returns a random int number between the x and y components of a Vector2Int.
        /// Very neat feature to use Vector2Int as a min/max range!
        /// </summary>
        /// <param name="value">The Vector2Int to get the range from.</param>
        /// <param name="inclusiveMax">Whether to include the maximum value in the range.</param>
        /// <returns>A random int number between the x and y components of the Vector2Int.</returns>
        public static int RandomBetween(this Vector2Int value, bool inclusiveMax = false) => 
            UnityEngine.Random.Range(value.x, inclusiveMax ? value.y + 1 : value.y);

        /// <summary>
        /// Divides a Vector2 by another Vector2 on a component-wise basis.
        /// </summary>
        /// <param name="dividend">The Vector2 to divide.</param>
        /// <param name="divisor">The Vector2 to divide by.</param>
        /// <returns>A new Vector2 where each component is the result of dividing the corresponding component of the dividend by the corresponding component of the divisor.</returns>
        public static Vector2 Divide(this Vector2 dividend, Vector2 divisor) => new()
        {
            x = dividend.x / divisor.x,
            y = dividend.y / divisor.y
        };

        /// <summary>
        /// Divides a Vector3 by another Vector3 on a component-wise basis.
        /// </summary>
        /// <param name="dividend">The Vector3 to divide.</param>
        /// <param name="divisor">The Vector3 to divide by.</param>
        /// <returns>A new Vector3 where each component is the result of dividing the corresponding component of the dividend by the corresponding component of the divisor.</returns>
        public static Vector3 Divide(this Vector3 dividend, Vector3 divisor) => new()
        {
            x = dividend.x / divisor.x,
            y = dividend.y / divisor.y,
            z = dividend.z / divisor.z
        };
    }
}