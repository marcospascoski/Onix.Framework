using Onix.Framework.RegularExpresions;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Onix.Framework.Extensions
{
    public static class StringExtensions
    {
		public static bool IsValidEmail(this string value)
		{
			var regex = CreateRegEx(RegularExpression.ValidEmail);
			return regex.IsMatch(value);
		}

		private static Regex CreateRegEx(string expression)
		{
			const RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;
			return new Regex(expression, options, TimeSpan.FromSeconds(2.0));
		}

		public static string LastSlashNode(this string value)
		{
			if(string.IsNullOrWhiteSpace(value))
            {
				return value;
            }
			var splitValues = value.Split('/');
            if (splitValues.Length == 0)
            {
				return value;
            }
			return splitValues.Last();
		}

		public static string ToOnlyNumbers(this string value)
		{
			return Regex.Replace(value, "[^0-9]", "");
		}

		public static bool AllAreTheSame(this string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return false;
			}
			return value.Distinct().Count() == 1;
		}
	}
}