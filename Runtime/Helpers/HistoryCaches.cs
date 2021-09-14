using System.Linq;
using Mane.Extensions;
using UnityEngine;

namespace Mane
{
    public class Vector3HistoryCache : HistoryCache<Vector3>
    {
        public Vector3HistoryCache(int length) : base(length) { }

        public override Vector3 GetAverage()
        {
            return _history.Average();
        }
    }


    public class Vector2HistoryCache : HistoryCache<Vector2>
    {
        public Vector2HistoryCache(int length) : base(length) { }

        public override Vector2 GetAverage()
        {
            return _history.Average();
        }
    }


    public class FloatHistoryCache : HistoryCache<float>
    {
        public FloatHistoryCache(int length) : base(length) { }

        public override float GetAverage()
        {
            return _history.Average();
        }
    }


    public abstract class HistoryCache<T>
    {
        protected readonly T[] _history;
        private int _idx;


        protected HistoryCache(int length)
        {
            _history = new T[length];
        }


        public abstract T GetAverage();

        
        public void Append(T value)
        {
            _history[_idx++] = value;
            if (_idx == _history.Length)
            {
                _idx = 0;
            }
        }

        public void Clear()
        {
            _history.Clear();
            _idx = 0;
        }
    }
}