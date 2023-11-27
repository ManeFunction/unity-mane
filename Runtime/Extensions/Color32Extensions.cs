using System;
using UnityEngine;

namespace Mane.Extensions
{
    /// <summary>
    /// Provides extension methods for Color32.
    /// </summary>
    public static class Color32Extensions
    {
        /// <summary>
        /// Sets the RGBA values of a Color32.
        /// </summary>
        /// <param name="c">The Color32 to set the values of.</param>
        /// <param name="r">The red component value.</param>
        /// <param name="g">The green component value.</param>
        /// <param name="b">The blue component value.</param>
        /// <param name="a">The alpha component value.</param>
        /// <returns>The Color32 with the set values.</returns>
        public static Color32 Set(this Color32 c, byte r, byte g, byte b, byte a)
        {
            c.r = r;
            c.g = g;
            c.b = b;
            c.a = a;

            return c;
        }

        /// <summary>
        /// Sets the red component of a Color32.
        /// </summary>
        /// <param name="c">The Color32 to set the red component of.</param>
        /// <param name="r">The red component value.</param>
        /// <returns>The Color32 with the set red component.</returns>
        public static Color32 SetR(this Color32 c, byte r)
        {
            c.r = r;

            return c;
        }

        /// <summary>
        /// Sets the green component of a Color32.
        /// </summary>
        /// <param name="c">The Color32 to set the green component of.</param>
        /// <param name="g">The green component value.</param>
        /// <returns>The Color32 with the set green component.</returns>
        public static Color32 SetG(this Color32 c, byte g)
        {
            c.g = g;

            return c;
        }

        /// <summary>
        /// Sets the blue component of a Color32.
        /// </summary>
        /// <param name="c">The Color32 to set the blue component of.</param>
        /// <param name="b">The blue component value.</param>
        /// <returns>The Color32 with the set blue component.</returns>
        public static Color32 SetB(this Color32 c, byte b)
        {
            c.b = b;

            return c;
        }

        /// <summary>
        /// Sets the alpha component of a Color32.
        /// </summary>
        /// <param name="c">The Color32 to set the alpha component of.</param>
        /// <param name="a">The alpha component value.</param>
        /// <returns>The Color32 with the set alpha component.</returns>
        public static Color32 SetA(this Color32 c, byte a)
        {
            c.a = a;

            return c;
        }

        
        /// <summary>
        /// Converts a Color32 to a uint by shifting the color components into the correct positions.
        /// </summary>
        /// <param name="color">The Color32 to convert.</param>
        /// <returns>The uint representation of the Color32.</returns>
        public static uint ToUInt(this Color32 color) =>
            (uint)(color.a << 24
                 | color.r << 16
                 | color.g << 8
                 | color.b);
        
        /// <summary>
        /// Converts a uint to a Color32 by shifting the color components into the correct positions.
        /// </summary>
        /// <param name="color">The uint to convert.</param>
        /// <returns>The Color32 representation of the uint.</returns>
        public static Color32 ToColor32(this uint color)
        {
            byte a = (byte)(color >> 24);
            byte r = (byte)(color >> 16);
            byte g = (byte)(color >> 8);
            byte b = (byte)color;

            return new Color32(r, g, b, a);
        }

        /// <summary>
        /// Converts a Color32 to a hexadecimal string.
        /// </summary>
        /// <param name="c">The Color32 to convert.</param>
        /// <returns>The hexadecimal string representation of the Color32.</returns>
        public static string ToHex(this Color32 c) => 
            $"#{c.r:X2}{c.g:X2}{c.b:X2}{c.a:X2}";


        /// <summary>
        /// Gets the brightness of a Color32.
        /// </summary>
        /// <param name="color">The Color32 to get the brightness of.</param>
        /// <returns>The brightness of the Color32.</returns>
        public static float GetBrightness(this Color32 color)
        {
            Color c = color;
            
            return c.GetBrightness();
        }

        /// <summary>
        /// Gets the hue of a Color32.
        /// </summary>
        /// <param name="color">The Color32 to get the hue of.</param>
        /// <returns>The hue of the Color32.</returns>
        public static float GetHue(this Color32 color)
        {
            Color c = color;
            
            return c.GetHue();
        }

        /// <summary>
        /// Gets the saturation of a Color32.
        /// </summary>
        /// <param name="color">The Color32 to get the saturation of.</param>
        /// <returns>The saturation of the Color32.</returns>
        public static float GetSaturation(this Color32 color)
        {
            Color c = color;
            
            return c.GetSaturation();
        }

        /// <summary>
        /// Gets the lightness of a Color32.
        /// </summary>
        /// <param name="color">The Color32 to get the lightness of.</param>
        /// <returns>The lightness of the Color32.</returns>
        public static float GetLight(this Color32 color)
        {
            Color c = color;
            
            return c.GetLight();
        }

        /// <summary>
        /// Shifts the RGB components of a Color32 by a specified amount.
        /// </summary>
        /// <param name="c">The Color32 to shift.</param>
        /// <param name="shift">The amount to shift the RGB components by.</param>
        /// <returns>The Color32 with the shifted RGB components.</returns>
        public static Color32 Shift(this Color32 c, byte shift) => new(
            (byte)(c.r + shift).Clamp(0, 255),
            (byte)(c.g + shift).Clamp(0, 255),
            (byte)(c.b + shift).Clamp(0, 255), c.a);
        
        
        [Obsolete("Use Random.Color32 instead!")]
        public static Color32 RandomColor => Random.Color32;
    }
}