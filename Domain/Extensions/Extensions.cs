using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Extensions
{
	public static class Extensions
	{
		public static string Join(this IEnumerable<string> source, string separator) =>
			string.Join(separator, source);

		public static string Join(this string[] source, string separator) =>
			string.Join(separator, source);

		public static string JoinAsStrings<T>(this IEnumerable<T> source, string separator) =>
			string.Join(separator, source);

		public static string JoinAsStrings<T>(this T[] source, string separator) =>
			string.Join(separator, source);
	}
}
