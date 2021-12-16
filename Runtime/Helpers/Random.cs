﻿using UnityEngine;

namespace Mane
{
    public static class Random
    {
        public static int ThrowDice(int d = 6) => 
            (int)(UnityEngine.Random.Range(0f, 1f) / (1f / d));

        public static bool FlipCoin() => 
            UnityEngine.Random.Range(0f, 1f) < .5f;

        public static float Range01() => 
            UnityEngine.Random.Range(0f, 1f);

        public static Vector2 Vector2(float min, float max, bool sameForAllAxes = false)
        {
            if (sameForAllAxes)
            {
                float n = UnityEngine.Random.Range(min, max);
                
                return new Vector2(n, n);
            }
            
            return new Vector2(
                UnityEngine.Random.Range(min, max),
                UnityEngine.Random.Range(min, max));
        }

        public static Vector2 Vector2(Vector2 from, Vector2 to) => new Vector2(
            UnityEngine.Random.Range(@from.x, to.x),
            UnityEngine.Random.Range(@from.y, to.y));

        public static Vector3 Vector3(float min, float max, bool sameForAllAxes = false)
        {
            if (sameForAllAxes)
            {
                float n = UnityEngine.Random.Range(min, max);
                
                return new Vector3(n, n, n);
            }
            
            return new Vector3(
                UnityEngine.Random.Range(min, max),
                UnityEngine.Random.Range(min, max),
                UnityEngine.Random.Range(min, max));
        }

        public static Vector3 Vector3(Vector3 from, Vector3 to) => new Vector3(
            UnityEngine.Random.Range(@from.x, to.x),
            UnityEngine.Random.Range(@from.y, to.y),
            UnityEngine.Random.Range(@from.z, to.z));
    }
}