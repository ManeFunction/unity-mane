using System;
using UnityEngine;

namespace Mane.Extensions
{
    /// <summary>
    /// Provides extension methods for string.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Removes the last path component of a string.
        /// </summary>
        /// <param name="str">The string to remove the last path component from.</param>
        /// <returns>The string with the last path component removed. If the string is less than 2 characters long, returns an empty string. If there is no path component to remove, returns the original string.</returns>
        public static string RemoveLastPathComponent(this string str)
        {
            if (str.Length < 2)
                return string.Empty;
            
            int index = str.LastIndexOf('/', str.Length - 2);
            
            return index > 0 ? str.Remove(index) : string.Empty;
        }


        /// <summary>
        /// Tries to parse a string to a float. If the parsing fails, returns a default value.
        /// </summary>
        /// <param name="str">The string to parse.</param>
        /// <param name="defaultValue">The default value to return if the parsing fails.</param>
        /// <returns>The parsed float value, or the default value if the parsing fails.</returns>
        public static float ParseFloat(this string str, float defaultValue = 0f)
        {
            if (!float.TryParse(str, out float result))
                result = defaultValue;

            return result;
        }

        /// <summary>
        /// Tries to parse a string to an integer. If the parsing fails, returns a default value.
        /// </summary>
        /// <param name="str">The string to parse.</param>
        /// <param name="defaultValue">The default value to return if the parsing fails.</param>
        /// <returns>The parsed integer value, or the default value if the parsing fails.</returns>
        public static int ParseInt(this string str, int defaultValue = 0)
        {
            if (!int.TryParse(str, out int result))
                result = defaultValue;

            return result;
        }

        /// <summary>
        /// Tries to parse a string to an unsigned short. If the parsing fails, returns a default value.
        /// </summary>
        /// <param name="str">The string to parse.</param>
        /// <param name="defaultValue">The default value to return if the parsing fails.</param>
        /// <returns>The parsed unsigned short value, or the default value if the parsing fails.</returns>
        public static ushort ParseUShort(this string str, ushort defaultValue = 0)
        {
            if (!ushort.TryParse(str, out ushort result))
                result = defaultValue;

            return result;
        }
        
        /// <summary>
        /// Reverses a string.
        /// </summary>
        /// <param name="str">The string to reverse.</param>
        /// <returns>The reversed string.</returns>
        public static string Reverse(this string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            
            return new string(charArray);
        }
        
        /// <summary>
        /// Converts the first character of a string to uppercase.
        /// </summary>
        /// <param name="str">The string to convert.</param>
        /// <returns>The string with the first character converted to uppercase. If the string is null or whitespace, returns the original string. If the string length is 1, returns the string with its character converted to uppercase.</returns>
        public static string ToUpperFirst(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            
            if (str.Length == 1)
                return char.ToUpper(str[0]).ToString(); 
            
            return char.ToUpper(str[0]) + str.Substring(1);
        }
        

        /// <summary>
        /// Formats a string with a specified value and color.
        /// </summary>
        /// <param name="str">The string to format.</param>
        /// <param name="value">The value to insert into the string.</param>
        /// <param name="color">The color to apply to the value.</param>
        /// <returns>The formatted string with the value colored.</returns>
        public static string FormatColored(this string str, string value, Color color) =>
            string.Format(str, $"<color={color.ToHex()}>{value}</color>");
        
        /// <summary>
        /// Formats a string with two specified values and colors.
        /// </summary>
        /// <param name="str">The string to format.</param>
        /// <param name="v1">The first value to insert into the string.</param>
        /// <param name="c1">The color to apply to the first value.</param>
        /// <param name="v2">The second value to insert into the string.</param>
        /// <param name="c2">The color to apply to the second value.</param>
        /// <returns>The formatted string with the values colored.</returns>
        public static string FormatColored(this string str,
            string v1, Color c1,
            string v2, Color c2) =>
            string.Format(str,
                $"<color={c1.ToHex()}>{v1}</color>",
                $"<color={c2.ToHex()}>{v2}</color>");
        
        /// <summary>
        /// Formats a string with three specified values and colors.
        /// </summary>
        /// <param name="str">The string to format.</param>
        /// <param name="v1">The first value to insert into the string.</param>
        /// <param name="c1">The color to apply to the first value.</param>
        /// <param name="v2">The second value to insert into the string.</param>
        /// <param name="c2">The color to apply to the second value.</param>
        /// <param name="v3">The third value to insert into the string.</param>
        /// <param name="c3">The color to apply to the third value.</param>
        /// <returns>The formatted string with the values colored.</returns>
        public static string FormatColored(this string str, 
            string v1, Color c1,
            string v2, Color c2,
            string v3, Color c3) =>
            string.Format(str,
                $"<color={c1.ToHex()}>{v1}</color>",
                $"<color={c2.ToHex()}>{v2}</color>",
                $"<color={c3.ToHex()}>{v3}</color>");

        /// <summary>
        /// Formats a string with multiple specified values and colors.
        /// </summary>
        /// <param name="str">The string to format.</param>
        /// <param name="args">An array of tuples, each containing a value to insert into the string and a color to apply to the value.</param>
        /// <returns>The formatted string with the values colored. If the args array is null or empty, returns the original string.</returns>
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
        
        
        /// <summary>
        /// Applies a color to a string using rich text color tags.
        /// </summary>
        /// <param name="str">The string to colorize.</param>
        /// <param name="color">The color to apply to the string.</param>
        /// <returns>The colorized string.</returns>
        public static string ColorizeRich(this string str, Color color) =>
            $"<color={color.ToHex()}>{str}</color>";
        
        
        /// <summary>
        /// Returns a string based on the count. This is useful for pluralization in certain languages.
        /// </summary>
        /// <param name="count">The count to base the string on.</param>
        /// <param name="one">The string to return if the count is 1.</param>
        /// <param name="many">The string to return if the count is between 2 and 4, or if the last two digits of the count are between 5 and 20.</param>
        /// <param name="more">The string to return if the count is 0, or if the last digit of the count is 0, or between 5 and 9.</param>
        /// <returns>A string based on the count.</returns>
        public static string GetCountedString(this int count, string one, string many, string more)
        {
            int tens = count % 100;
            if (tens is > 5 and < 20)
                return many;
            
            int units = count % 10;
            switch (units)
            {
                case 1: return one;
                case > 1 and < 5: return many;
                default: return more;
            }
        }
    }
}