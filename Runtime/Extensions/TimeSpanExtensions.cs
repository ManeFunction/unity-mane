using System;

namespace Mane.Extensions
{
	/// <summary>
	/// Provides extension methods for TimeSpan.
	/// </summary>
	public static class TimeSpanExtensions
	{
		/// <summary>
		/// Converts a TimeSpan to a string in the format "HH:MM:SS".
		/// </summary>
		/// <param name="ts">The TimeSpan to convert.</param>
		/// <returns>A string representing the TimeSpan in the format "HH:MM:SS". If the TimeSpan is zero, returns "00:00:00".</returns>
		public static string ToHHMMSS(this TimeSpan ts) => 
			ts.TotalSeconds > 0
				? $"{(int)ts.TotalHours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2}"
				: "00:00:00";

		/// <summary>
		/// Converts a TimeSpan to a string in the format "HH:MM".
		/// </summary>
		/// <param name="ts">The TimeSpan to convert.</param>
		/// <returns>A string representing the TimeSpan in the format "HH:MM". If the TimeSpan is zero, returns "00:00".</returns>
		public static string ToHHMM(this TimeSpan ts) => 
			ts.TotalSeconds > 0
				? $"{(int)ts.TotalHours:D2}:{ts.Minutes:D2}"
				: "00:00";

		/// <summary>
		/// Converts a TimeSpan to a string in the format "MM:SS".
		/// </summary>
		/// <param name="ts">The TimeSpan to convert.</param>
		/// <returns>A string representing the TimeSpan in the format "MM:SS". If the TimeSpan is zero, returns "00:00".</returns>
		public static string ToMMSS(this TimeSpan ts) => 
			ts.TotalSeconds > 0
				? $"{(int)ts.TotalMinutes:D2}:{ts.Seconds:D2}"
				: "00:00";
	}
}