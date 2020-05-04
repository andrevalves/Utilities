using System;
using System.Globalization;

namespace AndiSoft.Utilities.Converters
{
    public static class ConvertDateTime
    {
        ///<sumary>
        ///Transform string into dateTime. Returns null if parse is not successful
        ///</sumary>
        public static DateTime? ToDateTime(this string text, string format = "dd/MM/yyyy")
        {
            try
            {
                var dateTime = DateTime.ParseExact(text, format, CultureInfo.InvariantCulture);
                return dateTime;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}