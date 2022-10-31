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

        /// <summary>
        /// Both inclusive!
        /// </summary>
        public int RandomBetween => UnityEngine.Random.Range(Min, Max + 1);
    }
}