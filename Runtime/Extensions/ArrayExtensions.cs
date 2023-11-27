using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mane.Extensions
{
    /// <summary>
    /// Provides extension methods for arrays and lists.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Initializes a list with a specified value for a given count.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="TContent">The type of the elements in the list.</typeparam>
        /// <param name="list">The list to initialize.</param>
        /// <param name="with">The value to fill the list with.</param>
        /// <param name="count">The number of elements to add to the list.</param>
        /// <returns>The initialized list.</returns>
        public static TList InitWith<TList, TContent>(this TList list, TContent with, int count) where TList : IList<TContent>
        {
            list.Clear();

            for (int i = 0; i < count; i++)
                list.Add(with);

            return list;
        }

        /// <summary>
        /// Initializes a list with a specified value for the list's capacity.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <param name="list">The list to initialize.</param>
        /// <param name="with">The value to fill the list with.</param>
        /// <returns>The initialized list.</returns>
        public static List<T> InitWith<T>(this List<T> list, T with) => 
            list.InitWith(with, list.Capacity);

        /// <summary>
        /// Initializes a list with the result of a function for a given count.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="TContent">The type of the elements in the list.</typeparam>
        /// <param name="list">The list to initialize.</param>
        /// <param name="with">A function that generates the value to fill the list with.</param>
        /// <param name="count">The number of elements to add to the list.</param>
        /// <returns>The initialized list.</returns>
        public static TList InitWith<TList, TContent>(this TList list, Func<TContent> with, int count) where TList : IList<TContent>
        {
            list.Clear();

            for (int i = 0; i < count; i++)
                list.Add(with());

            return list;
        }
        
        /// <summary>
        /// Initializes a list with the result of a function for the list's capacity.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <param name="list">The list to initialize.</param>
        /// <param name="with">A function that generates the value to fill the list with.</param>
        /// <returns>The initialized list.</returns>
        public static List<T> InitWith<T>(this List<T> list, Func<T> with) => 
            list.InitWith(with, list.Capacity);

        /// <summary>
        /// Initializes a list with the result of a function that takes an index for a given count.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="TContent">The type of the elements in the list.</typeparam>
        /// <param name="list">The list to initialize.</param>
        /// <param name="with">A function that generates the value to fill the list with. The function takes an index as a parameter.</param>
        /// <param name="count">The number of elements to add to the list.</param>
        /// <returns>The initialized list.</returns>
        public static TList InitWith<TList, TContent>(this TList list, Func<int, TContent> with, int count) where TList : IList<TContent>
        {
            list.Clear();

            for (int i = 0; i < count; i++)
                list.Add(with(i));

            return list;
        }
        
        /// <summary>
        /// Initializes a list with the result of a function that takes an index for the list's capacity.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <param name="list">The list to initialize.</param>
        /// <param name="with">A function that generates the value to fill the list with. The function takes an index as a parameter.</param>
        /// <returns>The initialized list.</returns>
        public static List<T> InitWith<T>(this List<T> list, Func<int, T> with) => 
            list.InitWith(with, list.Capacity);


        /// <summary>
        /// Fills an array with a specified value.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the array.</typeparam>
        /// <param name="array">The array to fill.</param>
        /// <param name="with">The value to fill the array with.</param>
        /// <returns>The filled array.</returns>
        public static T[] FillWith<T>(this T[] array, T with)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = with;

            return array;
        }

        /// <summary>
        /// Fills an array with the result of a function.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the array.</typeparam>
        /// <param name="array">The array to fill.</param>
        /// <param name="with">A function that generates the value to fill the array with.</param>
        /// <returns>The filled array.</returns>
        public static T[] FillWith<T>(this T[] array, Func<T> with)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = with();

            return array;
        }

        /// <summary>
        /// Fills an array with the result of a function that takes an index.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the array.</typeparam>
        /// <param name="array">The array to fill.</param>
        /// <param name="with">A function that generates the value to fill the array with. The function takes an index as a parameter.</param>
        /// <returns>The filled array.</returns>
        public static T[] FillWith<T>(this T[] array, Func<int, T> with)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = with(i);

            return array;
        }

        /// <summary>
        /// Fills a 2D array with a specified value.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the array.</typeparam>
        /// <param name="array">The 2D array to fill.</param>
        /// <param name="with">The value to fill the array with.</param>
        /// <returns>The filled 2D array.</returns>
        public static T[,] FillWith<T>(this T[,] array, T with)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);
            for (int j = 0; j < h; j++)
                for (int i = 0; i < w; i++)
                    array[i, j] = with;

            return array;
        }

        /// <summary>
        /// Fills a 2D array with the result of a function.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the array.</typeparam>
        /// <param name="array">The 2D array to fill.</param>
        /// <param name="with">A function that generates the value to fill the array with.</param>
        /// <returns>The filled 2D array.</returns>
        public static T[,] FillWith<T>(this T[,] array, Func<T> with)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);
            for (int j = 0; j < h; j++)
                for (int i = 0; i < w; i++)
                    array[i, j] = with();

            return array;
        }

        /// <summary>
        /// Fills a 2D array with the result of a function that takes two indices.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the array.</typeparam>
        /// <param name="array">The 2D array to fill.</param>
        /// <param name="with">A function that generates the value to fill the array with. The function takes two indices as parameters.</param>
        /// <returns>The filled 2D array.</returns>
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
        /// Clears an array by setting all elements to their default value.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the array.</typeparam>
        /// <param name="array">The array to clear.</param>
        /// <returns>The cleared array.</returns>
        public static T[] Clear<T>(this T[] array)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = default;

            return array;
        }


        /// <summary>
        /// Shuffles a list in place.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <param name="list">The list to shuffle.</param>
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
        /// Adds an element to a list if it is not already present.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <param name="list">The list to add the element to.</param>
        /// <param name="element">The element to add.</param>
        /// <returns>True if the element was added, false if the element was already present in the list.</returns>
        public static bool AddExclusive<T>(this IList<T> list, T element)
        {
            if (list.Contains(element))
                return false;
            
            list.Add(element);

            return true;
        }

        /// <summary>
        /// Adds a range of elements to a list, excluding any that are already present.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <param name="list">The list to add the elements to.</param>
        /// <param name="elements">The elements to add.</param>
        public static void AddRangeExclusive<T>(this IList<T> list, IList<T> elements)
        {
            foreach (T element in elements)
                list.AddExclusive(element);
        }

        /// <summary>
        /// Inserts an element into a list at a specified index if it is not already present.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <param name="list">The list to insert the element into.</param>
        /// <param name="index">The index at which to insert the element.</param>
        /// <param name="element">The element to insert.</param>
        /// <returns>True if the element was inserted, false if the element was already present in the list.</returns>
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
        /// Executes a function for each element in a list until the function returns false.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <param name="list">The list to iterate over.</param>
        /// <param name="breakFunc">A function to execute for each element. If the function returns false, the iteration is stopped.</param>
        public static void ForEachCancellable<T>(this IEnumerable<T> list, Func<T, bool> breakFunc)
        {
            if (breakFunc == null || list == null) return;

            foreach (T element in list)
                if (!breakFunc(element))
                    break;
        }
        
        
        /// <summary>
        /// Executes an action for each element in a list.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <param name="list">The list to iterate over.</param>
        /// <param name="action">The action to execute for each element.</param>
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            if (action == null || list == null) return;

            foreach (T element in list)
                action(element);
        }
        
        
        /// <summary>
        /// Returns a random element from a list and its index.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <param name="list">The list to get a random element from.</param>
        /// <param name="selectedIdx">The index of the selected element.</param>
        /// <returns>The randomly selected element from the list.</returns>
        public static T GetRandom<T>(this IReadOnlyList<T> list, out int selectedIdx)
        {
            selectedIdx = UnityEngine.Random.Range(0, list.Count);
        
            return list[selectedIdx];
        }
        
        /// <summary>
        /// Returns a random element from a list.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <param name="list">The list to get a random element from.</param>
        /// <returns>The randomly selected element from the list.</returns>
        public static T GetRandom<T>(this IReadOnlyList<T> list) => 
            GetRandom(list, out int _);

        /// <summary>
        /// Returns a list of random elements from a collection without duplicates.
        /// If the collection has fewer elements than the requested count and getMaxWithDuplicates is true,
        /// the function will duplicate elements from the collection to meet the count.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the collection.</typeparam>
        /// <param name="collection">The collection to get random elements from.</param>
        /// <param name="count">The number of random elements to get.</param>
        /// <param name="getMaxWithDuplicates">Whether to allow duplicates when the collection has fewer elements than the requested count.</param>
        /// <returns>A list of random elements from the collection.</returns>
        public static List<T> GetRandom<T>(this IEnumerable<T> collection, int count,
            bool getMaxWithDuplicates = false)
        {
            List<T> result = new(count);
            List<T> listCopy = new(collection);

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
        /// Returns a random element from a collection or the default value if the collection is empty.
        /// !! Be aware that this method will enumerate the entire collection multiple times,
        /// so results can be wrong if the collection is modified during enumeration.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the collection.</typeparam>
        /// <param name="collection">The collection to get a random element from.</param>
        /// <returns>The randomly selected element from the collection or the default value if the collection is empty.</returns>
        public static T RandomOrDefault<T>(this IEnumerable<T> collection) => 
            collection.ElementAtOrDefault(UnityEngine.Random.Range(0, collection.Count()));


        /// <summary>
        /// Returns the index of an element in a list, or -1 if the element is not present.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="element">The element to find the index of.</param>
        /// <returns>The index of the element in the list, or -1 if the element is not present.</returns>
        public static int GetIndexOf<T>(this IReadOnlyList<T> list, T element)
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i].Equals(element))
                    return i;

            return -1;
        }

        /// <summary>
        /// Returns a new sequence that concatenates the source sequence with itself a specified number of times.
        /// !! Be aware that this method will enumerate the entire collection multiple times,
        /// so results can be wrong if the collection is modified during enumeration.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="list">The sequence to concatenate.</param>
        /// <param name="times">The number of times to concatenate the sequence.</param>
        /// <returns>A new sequence that concatenates the source sequence with itself a specified number of times.</returns>
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
        /// Returns the last elements from a sequence.
        /// !! Be aware that this method will enumerate the entire collection multiple times,
        /// so results can be wrong if the collection is modified during enumeration.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="collection">The sequence to get the last elements from.</param>
        /// <param name="count">The number of elements to return.</param>
        /// <returns>A sequence that contains the last elements from the input sequence.</returns>
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> collection, int count)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            
            return collection.Skip(Math.Max(0, collection.Count() - count));
        }
        
        /// <summary>
        /// Determines whether a sequence contains a specified number of elements that satisfy a condition.
        /// Works exactly as an Enumerable.Any, but search for the multiple entries
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the sequence.</typeparam>
        /// <param name="source">The sequence to check.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="count">The number of elements the sequence should contain that satisfy the condition.</param>
        /// <returns>True if the sequence contains the specified number of elements that satisfy the condition, otherwise false.</returns>
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

        /// <summary>
        /// Converts a dictionary to another dictionary with different key and value types.
        /// </summary>
        /// <typeparam name="TInKey">The type of the keys in the input dictionary.</typeparam>
        /// <typeparam name="TInValue">The type of the values in the input dictionary.</typeparam>
        /// <typeparam name="TOutKey">The type of the keys in the output dictionary.</typeparam>
        /// <typeparam name="TOutValue">The type of the values in the output dictionary.</typeparam>
        /// <param name="source">The input dictionary to convert.</param>
        /// <param name="converter">A function that takes a key-value pair from the input dictionary and returns a new key-value pair for the output dictionary.</param>
        /// <returns>A new dictionary with the keys and values converted using the provided function. If the converter function is null, returns null.</returns>
        public static Dictionary<TOutKey, TOutValue> Convert<TInKey, TInValue, TOutKey, TOutValue>(
            this IReadOnlyDictionary<TInKey, TInValue> source,
            Func<TInKey, TInValue, (TOutKey, TOutValue)> converter)
        {
            if (converter == null) return null;
            
            Dictionary<TOutKey, TOutValue> result = new Dictionary<TOutKey, TOutValue>(source.Count);
            foreach (KeyValuePair<TInKey, TInValue> pair in source)
            {
                (TOutKey key, TOutValue value) = converter(pair.Key, pair.Value);
                result.Add(key, value);
            }

            return result;
        }
    }
}