using System.Collections.Generic;
using System.Linq;


namespace Mane
{
    public static class Enum
    {
        public static IEnumerable<T> GetValues<T>() => 
            System.Enum.GetValues(typeof(T)).Cast<T>();
    }


    public static class Collection
    {
        public static bool IsNullOrEmpty<T>(IEnumerable<T> collection) => 
            collection == null || !collection.Any();
    }
}