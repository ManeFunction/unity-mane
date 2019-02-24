using System;


namespace Mane.Utils
{
	public static class TimeSpanExtensions
	{
		public static string ToHHMMSS(this TimeSpan ts)
		{
			string result = ts.TotalSeconds > 0
				? $"{ts.TotalHours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2}"
				: "00:00:00";

			return result;
		}


		public static string ToHHMM(this TimeSpan ts)
		{
			string result = ts.TotalSeconds > 0
				? $"{ts.TotalHours:D2}:{ts.Minutes:D2}"
				: "00:00";

			return result;
		}


		public static string ToMMSS(this TimeSpan ts)
		{
			string result = ts.TotalSeconds > 0
				? $"{ts.TotalMinutes:D2}:{ts.Seconds:D2}"
				: "00:00";

			return result;
		}
	}
}