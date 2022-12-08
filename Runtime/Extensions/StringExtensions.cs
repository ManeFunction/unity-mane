using System;
using UnityEngine;

namespace Mane.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveLastPathComponent(this string str)
        {
            if (str.Length < 2)
                return string.Empty;
            
            int index = str.LastIndexOf('/', str.Length - 2);
            
            return index > 0 ? str.Remove(index) : string.Empty;
        }


        public static float ParseFloat(this string str, float defaultValue = 0f)
        {
            if (!float.TryParse(str, out float result))
                result = defaultValue;

            return result;
        }

        public static int ParseInt(this string str, int defaultValue = 0)
        {
            if (!int.TryParse(str, out int result))
                result = defaultValue;

            return result;
        }

        public static ushort ParseUShort(this string str, ushort defaultValue = 0)
        {
            if (!ushort.TryParse(str, out ushort result))
                result = defaultValue;

            return result;
        }
        
        public static string Reverse(this string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            
            return new string(charArray);
        }
        
        public static string ToUpperFirst(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            
            if (str.Length == 1)
                return char.ToUpper(str[0]).ToString(); 
            
            return char.ToUpper(str[0]) + str.Substring(1);
        }
        

        public static string FormatColored(this string str, string value, Color color) =>
            string.Format(str, $"<color={color.ToHex()}>{value}</color>");
        
        public static string FormatColored(this string str,
            string v1, Color c1,
            string v2, Color c2) =>
            string.Format(str,
                $"<color={c1.ToHex()}>{v1}</color>",
                $"<color={c2.ToHex()}>{v2}</color>");
        
        public static string FormatColored(this string str, 
            string v1, Color c1,
            string v2, Color c2,
            string v3, Color c3) =>
            string.Format(str,
                $"<color={c1.ToHex()}>{v1}</color>",
                $"<color={c2.ToHex()}>{v2}</color>",
                $"<color={c3.ToHex()}>{v3}</color>");

        public static string FormatColored(this string str,
            params (string value, Color color)[] args)
        {
            if (args == null || args.Length == 0)
                return str;
            
            object[] formattedArgs = new object[args.Length];
            
            for (int i = 0; i < args.Length; i++)
            {
                (string value, Color color) = args[i];
                formattedArgs[i] = $"<color={color.ToHex()}>{value}</color>";
            }

            return string.Format(str, formattedArgs);
        }
        
        
        public static string ColorizeRich(this string str, Color color) =>
            $"<color={color.ToHex()}>{str}</color>";
        
        
        public static string GetCountedString(this int count, string one, string many, string more)
        {
            int tens = count % 100;
            if (tens > 5 && tens < 20)
                return many;
            
            int units = count % 10;
            if (units == 1)
                return one;
            if (units > 1 && units < 5)
                return many;
            return more;
        }
    }
}