using System;
using System.Globalization;

namespace AndiSoft.Utilities.Internals
{
    internal class Convertions
    {
        internal static DateTime ToDateTime(string date, string format)
        {
            format = format.Replace("MM", "M").Replace("dd", "d");
            return DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
        }
    }
}