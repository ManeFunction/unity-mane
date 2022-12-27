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
                result = min;
            else if (value.CompareTo(max) > 0)
                result = max;
            else
                result = value;

            return result;
        }
        
        public static T ClampMin<T>(this T value, T min) where T : IComparable<T> => 
            value.CompareTo(min) < 0 ? min : value;
        
        public static T ClampMax<T>(this T value, T max) where T : IComparable<T> => 
            value.CompareTo(max) > 0 ? max : value;

        public static float Clamp01(this float value) => value.Clamp(0f, 1f);

        public static double Clamp01(this double value) => value.Clamp(0d, 1d);

        public static decimal Clamp01(this decimal value) => value.Clamp(0m, 1m);


        public static float Cut(this float value, int tail)
        {
            float t = Mathf.Pow(10, tail);
            int intValue = (int)(value * t);

            return intValue / t;
        }
        
        public static int RoundTo(this float value, int n) => Mathf.RoundToInt(value / n) * n;
        
        public static int RoundTo(this int value, int n) => Mathf.RoundToInt((float)value / n) * n;


        public static bool IsEven(this int value) => (value >> 1) << 1 == value;

        public static bool IsOdd(this int value) => !IsEven(value);

        public static float Map(this float value, float sourceFrom, float sourceTo, float destFrom, float destTo) => 
            sourceFrom == 0f && destFrom == 0f ?
                value == 0f ? 0f : value * (destTo / sourceTo) :
                (value - sourceFrom) / (sourceTo - sourceFrom) * (destTo - destFrom) + destFrom;

        public static Vector2 ToVector2(this float value) => new Vector2(value, value);

        public static Vector3 ToVector3(this float value) => new Vector3(value, value, value);

        public static Vector4 ToVector4(this float value) => new Vector4(value, value, value, value);


        public static int SafePlus(this int value, int add)
        {
            try
            {
                return checked(value + add);
            }
            catch (OverflowException)
            {
                return add > 0 ? int.MaxValue : int.MinValue;
            }
        }
        
        public static int SafeMinus(this int value, int remove)
        {
            try
            {
                return checked(value - remove);
            }
            catch (OverflowException)
            {
                return remove > 0 ? int.MinValue : int.MaxValue;
            }
        }
        
        public static int SafeMultiply(this int value, int multiplier)
        {
            try
            {
                return checked(value * multiplier);
            }
            catch (OverflowException)
            {
                return int.MaxValue;
            }
        }
    }
}