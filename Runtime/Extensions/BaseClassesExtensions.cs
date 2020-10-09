using System;

namespace Mane.Extensions
{
    public static class BaseClassesExtensions
    {
        public static Version ToVersion(this string v)
        {
            return new Version(v);
        }
    }
}