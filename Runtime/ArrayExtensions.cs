using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;


namespace Mane.Extensions
{
    public static class ArrayExtensions
    {
        public static L InitWith<L, T>(this L list, T with, int count) where L : IList<T>
        {
            list.Clear();

            for (int i = 0; i < count; i++)
            {
                list.Add(with);
            }

            return list;
        }

        public static L InitWith<L, T>(this L list, Func<T> with, int count) where L : IList<T>
        {
            list.Clear();

            for (int i = 0; i < count; i++)
            {
                list.Add(with());
            }

            return list;
        }

        public static L InitWith<L, T>(this L list, Func<int, T> with, int count) where L : IList<T>
        {
            list.Clear();

            for (int i = 0; i < count; i++)
            {
                list.Add(with(i));
            }

            return list;
        }


        public static T[] FillWith<T>(this T[] array, T with)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = with;
            }

            return array;
        }

        public static T[] FillWith<T>(this T[] array, Func<T> with)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = with();
            }

            return array;
        }

        public static T[] FillWith<T>(this T[] array, Func<int, T> with)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = with(i);
            }

            return array;
        }

        public static T[,] FillWith<T>(this T[,] array, T with)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    array[i, j] = with;
                }
            }

            return array;
        }

        public static T[,] FillWith<T>(this T[,] array, Func<T> with)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    array[i, j] = with();
                }
            }

            return array;
        }

        public static T[,] FillWith<T>(this T[,] array, Func<int, int, T> with)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    array[i, j] = with(i, j);
                }
            }

            return array;
        }


        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }


        /// <summary>
        /// Add element if it isn't added yet
        /// </summary>
        /// <returns>Indicates added element or not</returns>
        public static bool AddExclusive<T>(this IList<T> list, T element)
        {
            if (list.Contains(element))
            {
                return false;
            }
            
            list.Add(element);

            return true;
        }

        /// <summary>
        /// Good for tiny Lists instead of Distinct()
        /// </summary>
        public static void AddRangeExclusive<T>(this IList<T> list, IList<T> elements)
        {
            foreach (T element in elements)
            {
                list.AddExclusive(element);
            }
        }

        /// <summary>
        /// Add element if it isn't added yet
        /// </summary>
        /// <returns>Indicates added element or not</returns>
        public static bool InsertExclusive<T>(this IList<T> list, int index, T element)
        {
            bool result = false;

            if (!list.Contains(element))
            {
                result = true;

                index = index.Clamp(0, list.Count);
                if (index == list.Count)
                {
                    list.Add(element);
                }
                else
                {
                    list.Insert(index, element);
                }
            }

            return result;
        }


        /// <summary>
        /// Return 'true' in func to break the cycle
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> list, Func<T, bool> breakFunc)
        {
            if (breakFunc == null)
            {
                return;
            }

            foreach (T element in list)
            {
                if (breakFunc(element))
                {
                    break;
                }
            }
        }
    }
}