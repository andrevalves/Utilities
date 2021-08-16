using System;
using System.Globalization;

namespace AndiSoft.Utilities.Converters
{
    /// <summary>
    /// DateTime Converter
    /// </summary>
    internal static class ConvertDateTime
    {
        ///<sumary>
        ///Transform string into dateTime. Returns null if parse is not successful
        ///</sumary>
        public static bool TryParse(string date, out DateTime dateTime, string format = "yyyy/MM/dd")
        {
            try
            {
                dateTime = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
                return true;
            }
            catch
            {
                dateTime = new DateTime();
                return false;
            }
        }
    }
}