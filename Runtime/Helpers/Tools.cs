using System;
using System.Collections.Generic;
using System.Linq;


namespace Mane.Extensions
{
    public static class Tools
    {
        public static IEnumerable<T> GetEnumValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}