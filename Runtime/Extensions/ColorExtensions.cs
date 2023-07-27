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

        public static int GetHue(this Color color)
        {
            float r = color.r / 255f;
            float g = color.g / 255f;
            float b = color.b / 255f;
            
            float max = Mathf.Max(r, Mathf.Max(g, b));
            float min = Mathf.Min(r, Mathf.Min(g, b));
            float delta = max - min;

            float hue = 0f;

            if (delta != 0f)
            {
                if (Math.Abs(max - r) < float.Epsilon)
                    hue = (g - b) / delta;
                else if (Math.Abs(max - g) < float.Epsilon)
                    hue = 2f + (b - r) / delta;
                else
                    hue = 4f + (r - g) / delta;

                hue *= 60f;

                if (hue < 0f)
                    hue += 360f;
            }

            return Mathf.RoundToInt(hue);
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

        public static float GetLight(this Color color) => color.r * .2f + color.g * .7f + color.b * .1f;


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