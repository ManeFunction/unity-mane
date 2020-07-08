﻿using System;
using UnityEngine;


namespace Mane.Extensions
{
    public static class NumericExtensions
    {
        public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T>
        {
            T result;

            if (value.CompareTo(min) < 0)
            {
                result = min;
            }
            else if (value.CompareTo(max) > 0)
            {
                result = max;
            }
            else
            {
                result = value;
            }

            return result;
        }


        public static float Cut(this float value, int tail)
        {
            float t = Mathf.Pow(10, tail);
            int intValue = (int)(value * t);

            return intValue / t;
        }


        public static bool IsEven(this int value)
        {
            return value % 2 == 0;
        }

        public static bool IsOdd(this int value)
        {
            return !IsEven(value);
        }
        
        public static float Map(this float value, float sourceFrom, float sourceTo, float destFrom, float destTo)
        {
            return (value - sourceFrom) / (sourceTo - sourceFrom) * (destTo - destFrom) + destFrom;
        }
    }
}