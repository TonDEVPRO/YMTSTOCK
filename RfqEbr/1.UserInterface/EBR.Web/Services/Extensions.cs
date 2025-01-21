using System;
using System.Collections.Generic;
using System.Linq;

namespace EBR.Web.Services
{

	public static class Extensions
	{
		private const string DateFormat = "dd/MM/yyy";
		private const string TimeFormat = "HH:mm:ss";
		private const string StandardFormat = "yyyy-MM-dd HH:mm:ss";

		public static string ToStringNullSafe(object obj)
		{
			return obj != null ? obj.ToString() : null;
		}

		public static string ToJoinQuote<T>(List<T> lists)
		{
			return lists.Aggregate("", (a, b) => a + (((a != "") ? "," : "") + "'" + b + "'"));
		}

		public static string ToJoin<T>(List<T> lists)
		{
			return lists.Aggregate("", (a, b) => a + (((a != "") ? "," : "") + b));
		}

		public static string ToJoinHtmlNewLine<T>(List<T> lists)
		{
			return lists.Aggregate("", (a, b) => a + (((a != "") ? "<br/>" : "") + b));
		}

		public static string ToJoinHtmlDiv<T>(List<T> lists)
		{
			return lists.Aggregate("", (a, b) => a + ("<div>" + b + "</div>"));
		}

		public static string ToFormat(DateTime value)
		{
			return value.ToString(DateFormat + " " + TimeFormat);
		}
		public static string ToStandardFormat(DateTime value)
		{
			return value.ToString(StandardFormat);
		}
		public static string ToDateFormat(DateTime value)
		{
			return value.ToString(DateFormat);
		}
		public static string ToTimeFormat(DateTime value)
		{
			return value.ToString(TimeFormat);
		}

		public static int? ToInt(object obj, bool throwExceptionIfFailed = false)
		{
			int result;
			var valid = int.TryParse(ToStringNullSafe(obj), out result);

			if (!valid)
			{
				if (throwExceptionIfFailed)
					throw new FormatException(string.Format("'{0}' cannot be converted as int", ToStringNullSafe(obj)));
				return null;
			}

			return result;
		}

		public static int? ToInt(string str, bool throwExceptionIfFailed = false)
		{
			int result;
			var valid = int.TryParse(str, out result);
			if (!valid)
			{
				if (throwExceptionIfFailed)
					throw new FormatException(string.Format("'{0}' cannot be converted as int", str));
				return null;
			}

			return result;
		}

		public static decimal? ToDecimal(object obj)
		{
			decimal result;
			var valid = Decimal.TryParse(ToStringNullSafe(obj), out result);
			return !valid ? (decimal?)null : result;
		}

		public static decimal? ToDecimal(string str)
		{
			decimal result;
			var valid = Decimal.TryParse(str, out result);
			return !valid ? (decimal?)null : result;
		}


	}
}