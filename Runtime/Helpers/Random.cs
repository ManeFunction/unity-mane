using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        public static Color Color => new Color(
            UnityEngine.Random.value,
            UnityEngine.Random.value,
            UnityEngine.Random.value);

        public static Color32 Color32 => new Color32(
            (byte)UnityEngine.Random.Range(0, 256),
            (byte)UnityEngine.Random.Range(0, 256),
            (byte)UnityEngine.Random.Range(0, 256), 255);


        /// <summary>
        /// Returns index of an element, selected from the list of weights,
        /// based on the weights from min to max (from the rarest).
        /// Expects already sorted list!
        /// </summary>
        public static int SelectFrom(IReadOnlyList<float> odds)
        {
            float totalWeight = odds.Sum();
            
            float sum = 0f;
            float d = UnityEngine.Random.Range(0f, totalWeight);
            for (int i = 0; i < odds.Count - 1; i++)
            {
                sum += odds[i];
                if (d <= sum)
                    return i;
            }

            return odds.Count - 1;
        }

        /// <summary>
        /// Try to select an element from the list of 0-1 odds, from min to max.
        /// Highly recommended to use sorted list starting from the minimal chances!
        /// If no element was selected, returns -1.
        /// </summary>
        public static int TrySelectFrom(IReadOnlyList<float> odds)
        {
            for (int i = 0; i < odds.Count; i++)
            {
                if (Range01() < odds[i])
                    return i;
            }

            return -1;
        }
    }
}