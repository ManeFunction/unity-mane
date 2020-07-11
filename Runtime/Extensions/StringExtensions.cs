using System;

namespace Mane.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveLastPathComponent(this string str)
        {
            if (str.Length < 2)
            {
                return string.Empty;
            }
            
            int index = str.LastIndexOf('/', str.Length - 2);
            
            return index > 0 ? str.Remove(index) : string.Empty;
        }


        public static float ParseFloat(this string str, float defaultValue = 0f)
        {
            if (!float.TryParse(str, out float result))
            {
                result = defaultValue;
            }

            return result;
        }

        public static int ParseInt(this string str, int defaultValue = 0)
        {
            if (!int.TryParse(str, out int result))
            {
                result = defaultValue;
            }

            return result;
        }

        public static ushort ParseUShort(this string str, ushort defaultValue = 0)
        {
            if (!ushort.TryParse(str, out ushort result))
            {
                result = defaultValue;
            }

            return result;
        }
        
        public static string Reverse(this string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            
            return new string(charArray);
        }
    }
}