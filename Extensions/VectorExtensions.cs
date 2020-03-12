using UnityEngine;


namespace Mane.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 Translate(this Vector3 vector, float deltaX, float deltaY, float deltaZ)
        {
            vector.x += deltaX;
            vector.y += deltaY;
            vector.z += deltaZ;
            
            return vector;
        }

        public static Vector3 Translate(this Vector3 vector, Vector3 offset)
        {
            vector.x += offset.x;
            vector.y += offset.y;
            vector.z += offset.z;
            
            return vector;
        }

        public static Vector3 Translate(this Vector3 vector, Vector2 offset)
        {
            vector.x += offset.x;
            vector.y += offset.y;
            
            return vector;
        }

        public static Vector2 Translate(this Vector2 vector, float deltaX, float deltaY)
        {
            vector.x += deltaX;
            vector.y += deltaY;
            
            return vector;
        }

        public static Vector2 Translate(this Vector2 vector, Vector2 offset)
        {
            vector.x += offset.x;
            vector.y += offset.y;
            
            return vector;
        }
        
        public static Vector3 Translate(this Vector2 vector, float deltaX, float deltaY, float deltaZ)
        {
            return new Vector3(vector.x + deltaX, vector.y + deltaY, deltaZ);
        }

        
        public static Vector3 SetX(this Vector3 vector, float x)
        {
            vector.x = x;
            
            return vector;
        }

        public static Vector3 SetY(this Vector3 vector, float y)
        {
            vector.y = y;
            
            return vector;
        }

        public static Vector3 SetZ(this Vector3 vector, float z)
        {
            vector.z = z;
            
            return vector;
        }

        public static Vector2 SetX(this Vector2 vector, float x)
        {
            vector.x = x;
            
            return vector;
        }

        public static Vector2 SetY(this Vector2 vector, float y)
        {
            vector.y = y;
            
            return vector;
        }
        

        public static Vector3 FlipX(this Vector3 vector)
        {
            vector.x *= -1;
            
            return vector;
        }

        public static Vector3 FlipY(this Vector3 vector)
        {
            vector.y *= -1;
            
            return vector;
        }

        public static Vector3 FlipZ(this Vector3 vector)
        {
            vector.z *= -1;
            
            return vector;
        }

        public static Vector2 FlipX(this Vector2 vector)
        {
            vector.x *= -1;
            
            return vector;
        }

        public static Vector2 FlipY(this Vector2 vector)
        {
            vector.y *= -1;
            
            return vector;
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
        
        public static Vector2 Project(this Vector2 vector, Vector2 onNormal)
        {
            float num1 = Vector2.Dot(onNormal, onNormal);
            if (num1 < Mathf.Epsilon)
            {
                return Vector2.zero;
            }
            float num2 = Vector2.Dot(vector, onNormal);
            
            return new Vector2(onNormal.x * num2 / num1, onNormal.y * num2 / num1);
        }
    }
}