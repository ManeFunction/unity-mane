using System;

namespace Mane
{
    /// <summary>
    /// This struct represents a range of float values.
    /// </summary>
    [Serializable]
    public struct MinMax
    {
        public float Min;
        public float Max;

        public MinMax(float min, float max)
        {
            Min = min;
            Max = max;
        }
        
        public static MinMax operator *(MinMax value, float n) => 
            new MinMax(value.Min * n, value.Max * n);
        
        public static MinMax operator *(float n, MinMax value) => value * n;
        
        public static MinMax operator /(MinMax value, float n) => 
            new MinMax(value.Min / n, value.Max / n);
        
        public static MinMax operator +(MinMax a, MinMax b) =>
            new MinMax(a.Min + b.Min, a.Max + b.Max);

        /// <summary>
        /// Gets a random float number between the Min and Max values of the MinMax struct.
        /// </summary>
        public float RandomBetween => UnityEngine.Random.Range(Min, Max);
        
        /// <summary>
        /// Checks if a float value is within the Min and Max values of the MinMax struct.
        /// </summary>
        /// <param name="value">The float value to check.</param>
        /// <returns>True if the value is within the range, false otherwise.</returns>
        public bool Contains(float value) => value >= Min && value <= Max;
        
        /// <summary>
        /// Checks if an integer value is within the Min and Max values of the MinMax struct.
        /// </summary>
        /// <param name="value">The integer value to check.</param>
        /// <returns>True if the value is within the range, false otherwise.</returns>
        public bool Contains(int value) => Contains((float)value);
    }

    /// <summary>
    /// This struct represents a range of int values.
    /// </summary>
    [Serializable]
    public struct MinMaxInt
    {
        public int Min;
        public int Max;

        public MinMaxInt(int min, int max)
        {
            Min = min;
            Max = max;
        }
        
        public static MinMaxInt operator *(MinMaxInt value, int n) => 
            new MinMaxInt(value.Min * n, value.Max * n);
        
        public static MinMaxInt operator *(int n, MinMaxInt value) => value * n;
        
        public static MinMaxInt operator /(MinMaxInt value, int n) => 
            new MinMaxInt(value.Min / n, value.Max / n);
        
        public static MinMaxInt operator +(MinMaxInt a, MinMaxInt b) =>
            new MinMaxInt(a.Min + b.Min, a.Max + b.Max);
        
        /// <summary>
        /// Gets a random integer number between the Min and Max values of the MinMaxInt struct, inclusive.
        /// </summary>
        public int RandomBetween => UnityEngine.Random.Range(Min, Max + 1);
        
        /// <summary>
        /// Checks if a float value is within the Min and Max values of the MinMaxInt struct.
        /// </summary>
        /// <param name="value">The float value to check.</param>
        /// <param name="includeMin">Should the Min value be included in the range?</param>
        /// <param name="includeMax">Should the Max value be included in the range?</param>
        /// <returns>True if the value is within the range, false otherwise.</returns>
        public bool Contains(float value, bool includeMin = true, bool includeMax = true) =>
            (includeMin ? value >= Min : value > Min) && (includeMax ? value <= Max : value < Max);

        /// <summary>
        /// Checks if an integer value is within the Min and Max values of the MinMax struct.
        /// </summary>
        /// <param name="value">The integer value to check.</param>
        /// <param name="includeMin">Should the Min value be included in the range?</param>
        /// <param name="includeMax">Should the Max value be included in the range?</param>
        /// <returns>True if the value is within the range, false otherwise.</returns>
        public bool Contains(int value, bool includeMin = true, bool includeMax = true) =>
            Contains((float)value, includeMin, includeMax);
    }
    
    public static class MinMaxExtensions
    {
        /// <summary>
        /// Checks if a float value is within a specified MinMax range.
        /// </summary>
        /// <param name="value">The float value to check.</param>
        /// <param name="range">The MinMax range to check within.</param>
        /// <returns>True if the value is within the range, false otherwise.</returns>
        public static bool IsInRange(this float value, MinMax range) => range.Contains(value);
        
        /// <summary>
        /// Checks if an int value is within a specified MinMax range.
        /// </summary>
        /// <param name="value">The int value to check.</param>
        /// <param name="range">The MinMax range to check within.</param>
        /// <returns>True if the value is within the range, false otherwise.</returns>
        public static bool IsInRange(this int value, MinMax range) => range.Contains(value);
        
        /// <summary>
        /// Checks if a float value is within a specified MinMaxInt range.
        /// </summary>
        /// <param name="value">The float value to check.</param>
        /// <param name="range">The MinMaxInt range to check within.</param>
        /// <param name="includeMin">Should the Min value be included in the range?</param>
        /// <param name="includeMax">Should the Max value be included in the range?</param>
        /// <returns>True if the value is within the range, false otherwise.</returns>
        public static bool IsInRange(this int value, MinMaxInt range, bool includeMin = true, bool includeMax = true) =>
            range.Contains(value, includeMin, includeMax);
        
        /// <summary>
        /// Checks if an int value is within a specified MinMaxInt range.
        /// </summary>
        /// <param name="value">The int value to check.</param>
        /// <param name="range">The MinMaxInt range to check within.</param>
        /// <param name="includeMin">Should the Min value be included in the range?</param>
        /// <param name="includeMax">Should the Max value be included in the range?</param>
        /// <returns>True if the value is within the range, false otherwise.</returns>
        public static bool IsInRange(this float value, MinMaxInt range, bool includeMin = true, bool includeMax = true) =>
            range.Contains((int)value, includeMin, includeMax);
    }
}