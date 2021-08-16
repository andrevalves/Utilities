using System.Linq;
using AndiSoft.Utilities.Extensions;

namespace AndiSoft.Utilities
{
    /// <summary>
    /// Some helpers to treat string and other data
    /// </summary>
    public static class AndiHelper
    {
        /// <summary>
        /// Returns only numbers of a string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetNumbersOnly(string text)
        {
            if (text.IsNullOrEmpty()) return string.Empty;
            
            return new string(text.Where(char.IsDigit).ToArray());
        }

        /// <summary>
        /// Returns only letters of a tring
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetLettersOnly(string text)
        {
            if (text.IsNullOrEmpty()) return string.Empty;
            return new string(text.Where(char.IsLetter).ToArray());
        }

        /// <summary>
        /// Returns only letter or numbers of a tring
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetLettersAndNumbers(string text)
        {
            if (text.IsNullOrEmpty()) return string.Empty;
            return new string(text.Where(char.IsLetterOrDigit).ToArray());
        }

        /// <summary>
        /// For brazilian phone numbers. Returns a valid phone number if possible.
        /// If the given string cannot be parsed to a valid phone number, returns null.
        /// If phone is a mobile number (starts with 8 or 9), and has 8 digits apart from the area code, the leading 9 will be inserted.
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static string GetMobileNumber(string phone)
        {
            phone = GetNumbersOnly(phone);

            if (phone.IsNullOrEmpty()) return string.Empty;


            // Se inicia com 0 (ex: 051), remove o 0
            if (phone.Substring(0, 1).Equals("0"))
                phone = phone.Remove(0, 1);

            // Se não tem o 9 na frente, adiciona o 9
            if (phone.Length == 10 && (phone.Substring(2, 1).Equals("9") || phone.Substring(2, 1).Equals("8")))
                phone = $"{phone.Substring(0, 2)}9{phone.Substring(2)}";
            
            if (phone.Length == 10 || phone.Length == 11)
                return phone;

            return null;
        }
    }
}