﻿using System;
using System.Text.Json;
using AndiSoft.Utilities.Internals;

namespace AndiSoft.Utilities
{
    /// <summary>
    /// Useful Conversions
    /// </summary>
    public class AndiConvert
    {
        ///<sumary>
        ///Transform string into dateTime. Returns null if parse is not successful
        ///</sumary>
        public static bool TryParseDate(string date, out DateTime dateTime, string format = "yyyy/MM/dd")
        {
            try
            {
                dateTime = Convertions.ToDateTime(date, format);
                return true;
            }
            catch
            {
                dateTime = new DateTime();
                return false;
            }
        }

        ///<summary>
        ///Returns a float number.
        ///</summary>
        public static float ToFloat(string text)
        {
            return float.Parse(text.Replace('.', ','));
        }

        ///<summary>
        ///Returns a double number.
        ///</summary>
        public static double ToDouble(string text)
        {
            return double.Parse(text.Replace('.', ','));
        }

        ///<summary>
        ///Returns a decimal number.
        ///</summary>
        public static decimal ToDecimal(string text)
        {
            return decimal.Parse(text.Replace('.', ','));
        }

        ///<summary>
        ///Returns a integer number.
        ///</summary>
        public static int ToInt(string text)
        {
            return int.Parse(text);
        }

        /// <summary>
        /// Converts the object to the given type.
        /// </summary>
        /// <param name="obj">Object to be converted.</param>
        /// <returns>New object of the given type.</returns>
        public static T Parse<T>(object obj)
        {
            if (obj is string)
                return JsonSerializer.Deserialize<T>(obj.ToString());

            var json = JsonSerializer.Serialize(obj);
            return JsonSerializer.Deserialize<T>(json);
        }

        /// <summary>
        /// Try to parse object to the given type.
        /// </summary>
        /// <param name="obj">Object to be parsed.</param>
        /// <param name="newObj">New parsed object.</param>
        /// <returns>True if sucessful. False otherwise.</returns>
        public static bool TryParse<T>(object obj, out T newObj)
        {
            try
            {
                if (obj is string)
                    newObj = JsonSerializer.Deserialize<T>(obj.ToString());

                var json = JsonSerializer.Serialize(obj);
                newObj = JsonSerializer.Deserialize<T>(json);
            }
            catch
            {
                newObj = JsonSerializer.Deserialize<T>("{}");
                return false;
            }

            return true;
        }
    }
}