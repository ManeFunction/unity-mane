using System;
using UnityEngine;

namespace Mane.Extensions
{
    /// <summary>
    /// Provides extension methods for Color.
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Gets the brightness of a Color.
        /// </summary>
        /// <param name="color">The Color to get the brightness of.</param>
        /// <returns>The brightness of the Color.</returns>
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

        /// <summary>
        /// Gets the hue of a Color.
        /// </summary>
        /// <param name="color">The Color to get the hue of.</param>
        /// <returns>The hue of the Color.</returns>
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

        /// <summary>
        /// Gets the saturation of a Color.
        /// </summary>
        /// <param name="color">The Color to get the saturation of.</param>
        /// <returns>The saturation of the Color.</returns>
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
        /// Gets the lightness of a Color.
        /// </summary>
        /// <param name="color">The Color to get the lightness of.</param>
        /// <returns>The lightness of the Color.</returns>
        public static float GetLight(this Color color) => color.r * .2f + color.g * .7f + color.b * .1f;


        /// <summary>
        /// Shifts the RGB components of a Color by a specified amount.
        /// </summary>
        /// <param name="c">The Color to shift.</param>
        /// <param name="shift">The amount to shift the RGB components by.</param>
        /// <returns>The Color with the shifted RGB components.</returns>
        public static Color Shift(this Color c, float shift) => new(
            Mathf.Clamp01(c.r + shift), 
            Mathf.Clamp01(c.g + shift), 
            Mathf.Clamp01(c.b + shift), c.a);


        /// <summary>
        /// Converts a Color to a hexadecimal string.
        /// </summary>
        /// <param name="c">The Color to convert.</param>
        /// <returns>The hexadecimal string representation of the Color.</returns>
        public static string ToHex(this Color c)
        {
            Color32 c32 = c;
            
            return c32.ToHex();
        }


        /// <summary>
        /// Sets the RGBA values of a Color.
        /// </summary>
        /// <param name="c">The Color to set the values of.</param>
        /// <param name="r">The red component value.</param>
        /// <param name="g">The green component value.</param>
        /// <param name="b">The blue component value.</param>
        /// <param name="a">The alpha component value.</param>
        /// <returns>The Color with the set values.</returns>
        public static Color Set(this Color c, float r, float g, float b, float a)
        {
            c.r = r;
            c.g = g;
            c.b = b;
            c.a = a;

            return c;
        }

        /// <summary>
        /// Sets the red component of a Color.
        /// </summary>
        /// <param name="c">The Color to set the red component of.</param>
        /// <param name="r">The red component value.</param>
        /// <returns>The Color with the set red component.</returns>
        public static Color SetR(this Color c, float r)
        {
            c.r = r;

            return c;
        }

        /// <summary>
        /// Sets the green component of a Color.
        /// </summary>
        /// <param name="c">The Color to set the green component of.</param>
        /// <param name="g">The green component value.</param>
        /// <returns>The Color with the set green component.</returns>
        public static Color SetG(this Color c, float g)
        {
            c.g = g;

            return c;
        }

        /// <summary>
        /// Sets the blue component of a Color.
        /// </summary>
        /// <param name="c">The Color to set the blue component of.</param>
        /// <param name="b">The blue component value.</param>
        /// <returns>The Color with the set blue component.</returns>
        public static Color SetB(this Color c, float b)
        {
            c.b = b;

            return c;
        }
        
        /// <summary>
        /// Sets the alpha component of a Color.
        /// </summary>
        /// <param name="c">The Color to set the alpha component of.</param>
        /// <param name="a">The alpha component value.</param>
        /// <returns>The Color with the set alpha component.</returns>
        public static Color SetA(this Color c, float a)
        {
            c.a = a;

            return c;
        }
        
        
        [Obsolete("Use Random.Color instead!")]
        public static Color RandomColor => Random.Color;
    }
}