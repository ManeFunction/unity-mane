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
        
        public static string ToIntString<T>(this T value) where T : struct, System.Enum =>
            ((int)(ValueType)value).ToString();
    
        public static T ToIntEnum<T>(this string value) where T : struct, System.Enum =>
            (T)System.Enum.ToObject(typeof(T), int.Parse(value));
        
        public static bool TryParseToIntEnum<T>(this string value, out T result, T defaultValue = default) where T : struct, System.Enum
        {
            if (int.TryParse(value, out int intValue) && System.Enum.IsDefined(typeof(T), intValue))
            {
                result = (T)System.Enum.ToObject(typeof(T), intValue);
                return true;
            }
            result = defaultValue;
            return false;
        }
    }


    public static class Collection
    {
        public static bool IsNullOrEmpty<T>(IEnumerable<T> collection) => 
            collection == null || !collection.Any();
    }
}