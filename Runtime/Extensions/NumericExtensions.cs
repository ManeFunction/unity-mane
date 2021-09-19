using System;
using UnityEngine;


namespace Mane.Extensions
{
    public static class NumericExtensions
    {
        public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T>
        {
            T result;

            if (value.CompareTo(min) < 0)
            {
                result = min;
            }
            else if (value.CompareTo(max) > 0)
            {
                result = max;
            }
            else
            {
                result = value;
            }

            return result;
        }

        public static float Clamp01(this float value)
        {
            return value.Clamp(0f, 1f);
        }

        public static double Clamp01(this double value)
        {
            return value.Clamp(0d, 1d);
        }

        public static decimal Clamp01(this decimal value)
        {
            return value.Clamp(0m, 1m);
        }


        public static float Cut(this float value, int tail)
        {
            float t = Mathf.Pow(10, tail);
            int intValue = (int)(value * t);

            return intValue / t;
        }


        public static bool IsEven(this int value)
        {
            return (value >> 1) << 1 == value;
        }

        public static bool IsOdd(this int value)
        {
            return !IsEven(value);
        }
        
        public static float Map(this float value, float sourceFrom, float sourceTo, float destFrom, float destTo)
        {
            return (value - sourceFrom) / (sourceTo - sourceFrom) * (destTo - destFrom) + destFrom;
        }

        public static Vector2 ToVector2(this float value)
        {
            return new Vector2(value, value);
        }

        public static Vector3 ToVector3(this float value)
        {
            return new Vector3(value, value, value);
        }

        public static Vector4 ToVector4(this float value)
        {
            return new Vector4(value, value, value, value);
        }
    }
}