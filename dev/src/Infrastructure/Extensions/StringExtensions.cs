namespace Perficient.Infrastructure.Extensions
{
	public static class StringExtensions
	{
		public static string TrimStart(this string sourceString, string trimString, bool trimAll = false)
		{
			if (!sourceString.StartsWith(trimString)) { return sourceString; }

			var resultString = sourceString.Substring(trimString.Length);

			while (trimAll && resultString.StartsWith(trimString))
			{
				resultString = resultString.Substring(trimString.Length);
			}

			return resultString;
		}

		public static string TrimEnd(this string sourceString, string trimString, bool trimAll = false)
		{
			if (!sourceString.EndsWith(trimString)) { return sourceString; }

			var resultString = sourceString.Substring(0, sourceString.Length - trimString.Length);

			while (trimAll && resultString.EndsWith(trimString))
			{
				resultString = resultString.Substring(0, sourceString.Length - trimString.Length);
			}

			return resultString;
		}
	}
}
