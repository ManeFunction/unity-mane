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
        
        public static MinMax operator /(float n, MinMax value) => value / n;
        
        public static MinMax operator +(MinMax a, MinMax b) =>
            new MinMax(a.Min + b.Min, a.Max + b.Max);

        public float RandomBetween => UnityEngine.Random.Range(Min, Max);
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
        
        public static MinMaxInt operator /(int n, MinMaxInt value) => value / n;
        
        public static MinMaxInt operator +(MinMaxInt a, MinMaxInt b) =>
            new MinMaxInt(a.Min + b.Min, a.Max + b.Max);

        /// <summary>
        /// Both inclusive!
        /// </summary>
        public int RandomBetween => UnityEngine.Random.Range(Min, Max + 1);
    }
}