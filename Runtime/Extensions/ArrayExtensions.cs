using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mane.Extensions
{
    public static class ArrayExtensions
    {
        public static L InitWith<L, T>(this L list, T with, int count) where L : IList<T>
        {
            list.Clear();

            for (int i = 0; i < count; i++)
                list.Add(with);

            return list;
        }
        
        public static List<T> InitWith<T>(this List<T> list, T with) => 
            list.InitWith(with, list.Capacity);

        public static L InitWith<L, T>(this L list, Func<T> with, int count) where L : IList<T>
        {
            list.Clear();

            for (int i = 0; i < count; i++)
                list.Add(with());

            return list;
        }

        public static List<T> InitWith<T>(this List<T> list, Func<T> with) => 
            list.InitWith(with, list.Capacity);

        public static L InitWith<L, T>(this L list, Func<int, T> with, int count) where L : IList<T>
        {
            list.Clear();

            for (int i = 0; i < count; i++)
                list.Add(with(i));

            return list;
        }

        public static List<T> InitWith<T>(this List<T> list, Func<int, T> with) => 
            list.InitWith(with, list.Capacity);


        public static T[] FillWith<T>(this T[] array, T with)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = with;

            return array;
        }

        public static T[] FillWith<T>(this T[] array, Func<T> with)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = with();

            return array;
        }

        public static T[] FillWith<T>(this T[] array, Func<int, T> with)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = with(i);

            return array;
        }

        public static T[,] FillWith<T>(this T[,] array, T with)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);
            for (int j = 0; j < h; j++)
                for (int i = 0; i < w; i++)
                    array[i, j] = with;

            return array;
        }

        public static T[,] FillWith<T>(this T[,] array, Func<T> with)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);
            for (int j = 0; j < h; j++)
                for (int i = 0; i < w; i++)
                    array[i, j] = with();

            return array;
        }

        public static T[,] FillWith<T>(this T[,] array, Func<int, int, T> with)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);
            for (int j = 0; j < h; j++)
                for (int i = 0; i < w; i++)
                    array[i, j] = with(i, j);

            return array;
        }
        
        
        /// <summary>
        /// Fills array with default values
        /// </summary>
        public static T[] Clear<T>(this T[] array)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = default;

            return array;
        }


        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }


        /// <summary>
        /// Add element if it isn't added yet
        /// </summary>
        /// <returns>Indicates added element or not</returns>
        public static bool AddExclusive<T>(this IList<T> list, T element)
        {
            if (list.Contains(element))
                return false;
            
            list.Add(element);

            return true;
        }

        /// <summary>
        /// Good for tiny Lists instead of Distinct()
        /// </summary>
        public static void AddRangeExclusive<T>(this IList<T> list, IList<T> elements)
        {
            foreach (T element in elements)
                list.AddExclusive(element);
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
        /// Return 'false' in func to break the cycle
        /// </summary>
        public static void ForEachCancellable<T>(this IEnumerable<T> list, Func<T, bool> breakFunc)
        {
            if (breakFunc == null || list == null) return;

            foreach (T element in list)
                if (!breakFunc(element))
                    break;
        }
        
        
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            if (action == null || list == null) return;

            foreach (T element in list)
                action(element);
        }
        
        
        public static T GetRandom<T>(this IReadOnlyList<T> list, out int selectedIdx)
        {
            selectedIdx = UnityEngine.Random.Range(0, list.Count);
        
            return list[selectedIdx];
        }
        
        public static T GetRandom<T>(this IReadOnlyList<T> list) => 
            GetRandom(list, out int _);

        /// <summary>
        /// Return List of random elements from the source collection without duplicates 
        /// </summary>
        public static List<T> GetRandom<T>(this IEnumerable<T> collection, int count,
            bool getMaxWithDuplicates = false)
        {
            List<T> result = new List<T>(count);
            List<T> listCopy = new List<T>(collection);

            int limit;
            if (getMaxWithDuplicates)
            {
                while (listCopy.Count < count)
                {
                    result.AddRange(listCopy);
                    count -= listCopy.Count;
                }

                limit = count;
            }
            else
                limit = Mathf.Min(listCopy.Count, count);

            for (int i = 0; i < limit; i++)
            {
                T copy = listCopy.GetRandom();
                result.Add(copy);
                listCopy.Remove(copy);
            }

            return result;
        }

        /// <summary>
        /// DO NOT THREAD-SAFE!
        /// But pretty efficient for single thread apps with LINQ queries
        /// </summary>
        public static T RandomOrDefault<T>(this IEnumerable<T> collection) => 
            collection.ElementAtOrDefault(UnityEngine.Random.Range(0, collection.Count()));


        /// <summary>
        /// Return '-1' if element not in the collection
        /// </summary>
        public static int GetIndexOf<T>(this IReadOnlyList<T> list, T element)
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i].Equals(element))
                    return i;

            return -1;
        }

        /// <summary>
        /// DO NOT THREAD-SAFE!
        /// </summary>
        public static IEnumerable<T> SelfConcat<T>(this IEnumerable<T> list, int times)
        {
            if (times == 1)
                return list;
            
            if (times <= 0)
                return null;

            IEnumerable<T> res = list;
            for (int i = 1; i < times; i++)
                res = res.Concat(list);
            
            return res;
        }

        /// <summary>
        /// DO NOT THREAD-SAFE!
        /// </summary>
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> collection, int count)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            
            return collection.Skip(Math.Max(0, collection.Count() - count));
        }
        
        /// <summary>
        /// Works exactly as an Enumerable.Any, but search for the multiple entries
        /// </summary>
        public static bool Any<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate, int count)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            
            int n = 0;
            
            return source.Any(element => predicate(element) && ++n == count);
        }

        public static Dictionary<KO, VO> Convert<KI, VI, KO, VO>(this Dictionary<KI, KO> source,
            Func<KI, KO, (KO, VO)> converter)
        {
            if (converter == null) return null;
            
            Dictionary<KO, VO> result = new Dictionary<KO, VO>(source.Count);
            foreach (KeyValuePair<KI, KO> pair in source)
            {
                (KO key, VO value) = converter(pair.Key, pair.Value);
                result.Add(key, value);
            }

            return result;
        }
    }
}