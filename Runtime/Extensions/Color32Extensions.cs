using System;
using UnityEngine;

namespace Mane.Extensions
{
    public static class Color32Extensions
    {
        public static Color32 Set(this Color32 c, byte r, byte g, byte b, byte a)
        {
            c.r = r;
            c.g = g;
            c.b = b;
            c.a = a;

            return c;
        }

        public static Color32 SetR(this Color32 c, byte r)
        {
            c.r = r;

            return c;
        }

        public static Color32 SetG(this Color32 c, byte g)
        {
            c.g = g;

            return c;
        }

        public static Color32 SetB(this Color32 c, byte b)
        {
            c.b = b;

            return c;
        }

        public static Color32 SetA(this Color32 c, byte a)
        {
            c.a = a;

            return c;
        }


        public static uint ToUInt(this Color32 color) =>
            (uint)(color.a << 24
                 | color.r << 16
                 | color.g << 8
                 | color.b);

        public static Color32 ToColor32(this uint color)
        {
            byte a = (byte)(color >> 24);
            byte r = (byte)(color >> 16);
            byte g = (byte)(color >> 8);
            byte b = (byte)color;

            return new Color32(r, g, b, a);
        }

        public static string ToHex(this Color32 c) => 
            $"#{c.r:X2}{c.g:X2}{c.b:X2}{c.a:X2}";


        public static float GetBrightness(this Color32 color)
        {
            Color c = color;
            
            return c.GetBrightness();
        }

        public static float GetHue(this Color32 color)
        {
            Color c = color;
            
            return c.GetHue();
        }

        public static float GetSaturation(this Color32 color)
        {
            Color c = color;
            
            return c.GetSaturation();
        }

        public static float GetLight(this Color32 color)
        {
            Color c = color;
            
            return c.GetLight();
        }


        /// <summary>
        /// Shift RGB color channels
        /// </summary>
        public static Color32 Shift(this Color32 c, byte shift) => new Color32(
            (byte)(c.r + shift).Clamp(0, 255),
            (byte)(c.g + shift).Clamp(0, 255),
            (byte)(c.b + shift).Clamp(0, 255), c.a);
        
        
        [Obsolete("Use Random.Color32 instead!", true)]
        public static Color32 RandomColor => default;
    }
}