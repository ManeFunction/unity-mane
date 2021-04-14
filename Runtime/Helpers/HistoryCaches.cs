using System.Linq;
using Mane.Extensions;
using UnityEngine;

namespace Mane
{
    public class Vector3HistoryCache
    {
        private readonly Vector3[] _history;
        private int _idx;


        public Vector3HistoryCache(int length) => _history = new Vector3[length];


        public void Append(Vector3 value)
        {
            _history[_idx++] = value;
            if (_idx == _history.Length)
            {
                _idx = 0;
            }
        }

        public Vector3 GetAverage() => _history.Average();
    }


    public class Vector2HistoryCache
    {
        private readonly Vector2[] _history;
        private int _idx;


        public Vector2HistoryCache(int length) => _history = new Vector2[length];


        public void Append(Vector2 value)
        {
            _history[_idx++] = value;
            if (_idx == _history.Length)
            {
                _idx = 0;
            }
        }

        public Vector2 GetAverage() => _history.Average();
    }


    public class FloatHistoryCache
    {
        private readonly float[] _history;
        private int _idx;


        public FloatHistoryCache(int length) => _history = new float[length];


        public void Append(float value)
        {
            _history[_idx++] = value;
            if (_idx == _history.Length)
            {
                _idx = 0;
            }
        }

        public float GetAverage() => _history.Average();
    }
}