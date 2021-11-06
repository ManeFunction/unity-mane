using System;

namespace Mane.Extensions
{
	public static class TimeSpanExtensions
	{
		public static string ToHHMMSS(this TimeSpan ts) => 
			ts.TotalSeconds > 0
				? $"{ts.TotalHours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2}"
				: "00:00:00";


		public static string ToHHMM(this TimeSpan ts) => 
			ts.TotalSeconds > 0
				? $"{ts.TotalHours:D2}:{ts.Minutes:D2}"
				: "00:00";


		public static string ToMMSS(this TimeSpan ts) => 
			ts.TotalSeconds > 0
				? $"{ts.TotalMinutes:D2}:{ts.Seconds:D2}"
				: "00:00";
	}
}