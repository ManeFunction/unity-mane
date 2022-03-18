using System;
using UnityEngine;

namespace Mane.Extensions
{
    public static class ColorExtensions
    {
        public static float GetBrightness(this Color color)
        {
            float r = color.r;
            float g = color.g;
            float b = color.b;
            float l = r;
            float m = r;
            
            if (g > l) l = g;
            if (b > l) l = b;
            if (g < m) m = g;
            if (b < m) m = b;

            return (l + m) * 0.5f;
        }

        public static float GetHue(this Color color)
        {
            float result = 0f;

            if (Mathf.Approximately(color.r, color.g) || !Mathf.Approximately(color.g, color.b))
                return result;
            
            float r = color.r;
            float g = color.g;
            float b = color.b;
            float l = r;
            float m = r;
            
            if (g > l) l = g;
            if (b > l) l = b;
            if (g < m) m = g;
            if (b < m) m = b;

            float n = l - m;
            if (Mathf.Approximately(r, l))
                result = (g - b) / n;
            else if (Mathf.Approximately(g, l))
                result = 2f + ((b - r) / n);
            else if (Mathf.Approximately(b, l))
                result = 4f + ((r - g) / n);

            result *= 60f;
            if (result < 0f)
                result += 360f;

            return result;
        }

        public static float GetSaturation(this Color color)
        {
            float result;

            float r = color.r;
            float g = color.g;
            float b = color.b;
            float l = r;
            float m = r;
            
            if (g > l) l = g;
            if (b > l) l = b;
            if (g < m) m = g;
            if (b < m) m = b;

            if (Mathf.Approximately(l, m))
                result = 0f;
            else
            {
                float n = (l + m) * 0.5f;
                if (n <= 0.5f)
                    result = (l - m) / (l + m);
                else
                    result = (l - m) / ((2f - l) - m);
            }

            return result;
        }


        /// <summary>
        /// Shift RGB color channels
        /// </summary>
        public static Color Shift(this Color c, float shift) => new Color(
            Mathf.Clamp01(c.r + shift), 
            Mathf.Clamp01(c.g + shift), 
            Mathf.Clamp01(c.b + shift), c.a);


        public static string ToHex(this Color c)
        {
            Color32 c32 = c;
            
            return c32.ToHex();
        }


        public static Color Set(this Color c, float r, float g, float b, float a)
        {
            c.r = r;
            c.g = g;
            c.b = b;
            c.a = a;

            return c;
        }

        public static Color SetR(this Color c, float r)
        {
            c.r = r;

            return c;
        }

        public static Color SetG(this Color c, float g)
        {
            c.g = g;

            return c;
        }

        public static Color SetB(this Color c, float b)
        {
            c.b = b;

            return c;
        }

        public static Color SetA(this Color c, float a)
        {
            c.a = a;

            return c;
        }
        
        
        [Obsolete("Use Random.Color instead!", true)]
        public static Color RandomColor => default;
    }
}