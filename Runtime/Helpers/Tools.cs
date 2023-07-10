using System;
using System.Collections.Generic;
using System.Linq;


namespace Mane
{
    public static class Enum
    {
        public static IEnumerable<T> GetValues<T>() => 
            System.Enum.GetValues(typeof(T)).Cast<T>();
        
        public static bool IsObsolete(System.Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (ObsoleteAttribute[])
                fi.GetCustomAttributes(typeof(ObsoleteAttribute), false);
            return attributes.Any();
        }
    }


    public static class Collection
    {
        public static bool IsNullOrEmpty<T>(IEnumerable<T> collection) => 
            collection == null || !collection.Any();
    }
}