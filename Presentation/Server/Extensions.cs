using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Velvetech.Presentation.Server
{
	public static class Extensions
	{
		public static string Join(this IEnumerable<string> source, string separator) =>
			string.Join(separator, source);

		public static string Join(this string[] source, string separator) =>
			string.Join(separator, source);

		public static string Join(this IEnumerable<string> source, char separator) =>
		string.Join(separator, source);

		public static string Join(this string[] source, char separator) =>
			string.Join(separator, source);

		public static IEnumerable<string> ToStrings<T>(this IEnumerable<T> source, string separator) =>
			source.Select(item => item.ToString());
	}
}
