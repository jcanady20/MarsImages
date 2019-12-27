using System;
using System.Globalization;

namespace MarsImages.Internal.Extensions
{
    public static class StringExtensions
    {
        private static readonly string[] _formats = {
            "M/d/yy",
            "MM/dd/yy",
            "M/d/yyyy",
            "MM/dd/yyyy",
            "MMMM d, yyyy",
            "MMMM dd, yyyy",
            "MMM-d-yyyy",
            "MMM-dd-yyyy",
            "yyyy-MM-dd"
        };
        //  Validate and parse input string as DateTime objects
        public static DateTime? ToDateTime(this string input)
        {
            if (DateTime.TryParseExact(input, _formats, new CultureInfo("en-US"), DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            return null;
        }
    }
}