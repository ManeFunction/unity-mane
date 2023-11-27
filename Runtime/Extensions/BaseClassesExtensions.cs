using System;

namespace Mane.Extensions
{
    /// <summary>
    /// Provides extension methods for base classes.
    /// </summary>
    public static class BaseClassesExtensions
    {
        /// <summary>
        /// Converts a string to a Version.
        /// </summary>
        /// <param name="v">The string to convert.</param>
        /// <returns>The converted Version.</returns>
        public static Version ToVersion(this string v) => new(v);
    }
}