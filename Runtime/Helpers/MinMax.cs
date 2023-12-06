using System;

namespace Mane
{
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

        public float GetRandomBetween() => UnityEngine.Random.Range(Min, Max);
        
        public bool Contains(float value) => value >= Min && value <= Max;
        
        public bool Contains(int value) => Contains((float)value);
    }

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
        /// Both inclusive!
        /// </summary>
        public int GetRandomBetween() => UnityEngine.Random.Range(Min, Max + 1);
        
        public bool Contains(float value, bool includeMin = true, bool includeMax = true) =>
            (includeMin ? value >= Min : value > Min) && (includeMax ? value <= Max : value < Max);

        public bool Contains(int value, bool includeMin = true, bool includeMax = true) =>
            Contains((float)value, includeMin, includeMax);
    }
    
    public static class MinMaxExtensions
    {
        public static bool IsInRange(this float value, MinMax range) => range.Contains(value);
        
        public static bool IsInRange(this int value, MinMax range) => range.Contains(value);
        
        public static bool IsInRange(this int value, MinMaxInt range, bool includeMin = true, bool includeMax = true) =>
            range.Contains(value, includeMin, includeMax);
        
        public static bool IsInRange(this float value, MinMaxInt range, bool includeMin = true, bool includeMax = true) =>
            range.Contains((int)value, includeMin, includeMax);
    }
}