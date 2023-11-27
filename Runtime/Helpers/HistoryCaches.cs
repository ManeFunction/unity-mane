using System.Linq;
using Mane.Extensions;
using UnityEngine;

namespace Mane
{
    /// <summary>
    /// This class is used to cache Vector3 values history and calculate average value.
    /// </summary>
    public class Vector3HistoryCache : HistoryCache<Vector3>
    {
        public Vector3HistoryCache(int length) : base(length) { }

        public override Vector3 GetAverage() => History.Average();
    }


    /// <summary>
    /// This class is used to cache Vector2 values history and calculate average value.
    /// </summary>
    public class Vector2HistoryCache : HistoryCache<Vector2>
    {
        public Vector2HistoryCache(int length) : base(length) { }

        public override Vector2 GetAverage() => History.Average();
    }


    /// <summary>
    /// This class is used to cache float values history and calculate average value.
    /// </summary>
    public class FloatHistoryCache : HistoryCache<float>
    {
        public FloatHistoryCache(int length) : base(length) { }

        public override float GetAverage() => History.Average();
    }

    
    public abstract class HistoryCache<T>
    {
        protected readonly T[] History;
        private int _idx;


        protected HistoryCache(int length) => History = new T[length];


        public abstract T GetAverage();

        
        /// <summary>
        /// Appends value to the history cache. If cache is full, oldest value will be replaced.
        /// </summary>
        /// <param name="value">Value to append.</param>
        public void Append(T value)
        {
            History[_idx++] = value;
            if (_idx == History.Length)
                _idx = 0;
        }

        /// <summary>
        /// Clears the history cache.
        /// </summary>
        public void Clear()
        {
            History.Clear();
            _idx = 0;
        }
    }
}