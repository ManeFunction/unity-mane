using System.Linq;
using Mane.Extensions;
using UnityEngine;

namespace Mane
{
    public class Vector3HistoryCache : HistoryCache<Vector3>
    {
        public Vector3HistoryCache(int length) : base(length) { }

        public override Vector3 GetAverage() => History.Average();
    }


    public class Vector2HistoryCache : HistoryCache<Vector2>
    {
        public Vector2HistoryCache(int length) : base(length) { }

        public override Vector2 GetAverage() => History.Average();
    }


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

        
        public void Append(T value)
        {
            History[_idx++] = value;
            if (_idx == History.Length)
                _idx = 0;
        }

        public void Clear()
        {
            History.Clear();
            _idx = 0;
        }
    }
}