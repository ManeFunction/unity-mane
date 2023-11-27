using System;
using UnityEngine;

namespace Mane.Extensions
{
    /// <summary>
    /// Provides extension methods for numeric types.
    /// </summary>
    public static class NumericExtensions
    {
        /// <summary>
        /// Clamps a value between a minimum and maximum value.
        /// </summary>
        /// <typeparam name="T">The type of the value to clamp. Must implement IComparable.</typeparam>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value. If the value is less than this, the minimum value is returned.</param>
        /// <param name="max">The maximum value. If the value is greater than this, the maximum value is returned.</param>
        /// <returns>The clamped value.</returns>
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
        
        /// <summary>
        /// Clamps a value to a minimum value.
        /// </summary>
        /// <typeparam name="T">The type of the value to clamp. Must implement IComparable.</typeparam>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value. If the value is less than this, the minimum value is returned.</param>
        /// <returns>The clamped value.</returns>
        public static T ClampMin<T>(this T value, T min) where T : IComparable<T> => 
            value.CompareTo(min) < 0 ? min : value;
        
        /// <summary>
        /// Clamps a value to a maximum value.
        /// </summary>
        /// <typeparam name="T">The type of the value to clamp. Must implement IComparable.</typeparam>
        /// <param name="value">The value to clamp.</param>
        /// <param name="max">The maximum value. If the value is greater than this, the maximum value is returned.</param>
        /// <returns>The clamped value.</returns>
        public static T ClampMax<T>(this T value, T max) where T : IComparable<T> => 
            value.CompareTo(max) > 0 ? max : value;

        /// <summary>
        /// Clamps a float value between 0 and 1.
        /// </summary>
        /// <param name="value">The float value to clamp.</param>
        /// <returns>The clamped float value.</returns>
        public static float Clamp01(this float value) => value.Clamp(0f, 1f);

        /// <summary>
        /// Clamps a double value between 0 and 1.
        /// </summary>
        /// <param name="value">The double value to clamp.</param>
        /// <returns>The clamped double value.</returns>
        public static double Clamp01(this double value) => value.Clamp(0d, 1d);

        /// <summary>
        /// Clamps a decimal value between 0 and 1.
        /// </summary>
        /// <param name="value">The decimal value to clamp.</param>
        /// <returns>The clamped decimal value.</returns>
        public static decimal Clamp01(this decimal value) => value.Clamp(0m, 1m);


        /// <summary>
        /// Truncates a float value to a specified number of decimal places.
        /// </summary>
        /// <param name="value">The float value to truncate.</param>
        /// <param name="tail">The number of decimal places to keep.</param>
        /// <returns>The truncated float value.</returns>
        public static float Cut(this float value, int tail)
        {
            float t = Mathf.Pow(10, tail);
            int intValue = (int)(value * t);

            return intValue / t;
        }
        
        /// <summary>
        /// Rounds a float value to the nearest multiple of a specified integer.
        /// </summary>
        /// <param name="value">The float value to round.</param>
        /// <param name="n">The integer to which the value should be rounded.</param>
        /// <returns>The rounded integer value.</returns>
        public static int RoundTo(this float value, int n) => Mathf.RoundToInt(value / n) * n;
        
        /// <summary>
        /// Rounds an integer value to the nearest multiple of a specified integer.
        /// </summary>
        /// <param name="value">The integer value to round.</param>
        /// <param name="n">The integer to which the value should be rounded.</param>
        /// <returns>The rounded integer value.</returns>
        public static int RoundTo(this int value, int n) => Mathf.RoundToInt((float)value / n) * n;


        /// <summary>
        /// Determines whether an integer is even.
        /// </summary>
        /// <param name="value">The integer to check.</param>
        /// <returns>True if the integer is even, false otherwise.</returns>
        public static bool IsEven(this int value) => (value >> 1) << 1 == value;

        /// <summary>
        /// Determines whether an integer is odd.
        /// </summary>
        /// <param name="value">The integer to check.</param>
        /// <returns>True if the integer is odd, false otherwise.</returns>
        public static bool IsOdd(this int value) => !IsEven(value);

        /// <summary>
        /// Maps a value from one range to another.
        /// </summary>
        /// <param name="value">The value to map.</param>
        /// <param name="sourceFrom">The lower bound of the source range.</param>
        /// <param name="sourceTo">The upper bound of the source range.</param>
        /// <param name="destFrom">The lower bound of the destination range.</param>
        /// <param name="destTo">The upper bound of the destination range.</param>
        /// <returns>The value mapped to the destination range.</returns>
        public static float Map(this float value, float sourceFrom, float sourceTo, float destFrom, float destTo) => 
            sourceFrom == 0f && destFrom == 0f ?
                value == 0f ? 0f : value * (destTo / sourceTo) :
                (value - sourceFrom) / (sourceTo - sourceFrom) * (destTo - destFrom) + destFrom;

        /// <summary>
        /// Converts a float to a Vector2 with both components set to the float value.
        /// </summary>
        /// <param name="value">The float value to convert.</param>
        /// <returns>A Vector2 with both components set to the float value.</returns>
        public static Vector2 ToVector2(this float value) => new Vector2(value, value);

        /// <summary>
        /// Converts a float to a Vector3 with all three components set to the float value.
        /// </summary>
        /// <param name="value">The float value to convert.</param>
        /// <returns>A Vector2 with both components set to the float value.</returns>
        public static Vector3 ToVector3(this float value) => new Vector3(value, value, value);

        /// <summary>
        /// Converts a float to a Vector4 with all four components set to the float value.
        /// </summary>
        /// <param name="value">The float value to convert.</param>
        /// <returns>A Vector2 with both components set to the float value.</returns>
        public static Vector4 ToVector4(this float value) => new Vector4(value, value, value, value);


        /// <summary>
        /// Safely adds two integers together. If the operation would cause an overflow, it returns the maximum or minimum integer value instead.
        /// </summary>
        /// <param name="value">The first integer to add.</param>
        /// <param name="add">The second integer to add.</param>
        /// <returns>The sum of the two integers, or the maximum or minimum integer value if the operation would cause an overflow.</returns>
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
        
        /// <summary>
        /// Safely subtracts one integer from another. If the operation would cause an overflow, it returns the maximum or minimum integer value instead.
        /// </summary>
        /// <param name="value">The integer from which to subtract.</param>
        /// <param name="remove">The integer to subtract.</param>
        /// <returns>The difference of the two integers, or the maximum or minimum integer value if the operation would cause an overflow.</returns>
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
        
        /// <summary>
        /// Safely multiplies two integers together. If the operation would cause an overflow, it returns the maximum integer value instead.
        /// </summary>
        /// <param name="value">The first integer to multiply.</param>
        /// <param name="multiplier">The second integer to multiply.</param>
        /// <returns>The product of the two integers, or the maximum integer value if the operation would cause an overflow.</returns>
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