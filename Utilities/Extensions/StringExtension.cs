﻿using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace AndiSoft.Utilities.Extensions
{
    /// <summary>
    /// String Extensions
    /// </summary>
    public static class StringExtension
    {
        private static readonly string[] Preposicoes = { "e", "de", "da", "das", "do", "dos", "com", "na", "nas", "no", "nos" };

        private const string ComAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
        private const string SemAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";
        
        ///<summary>
        ///Converts the first char of a string to UpperCase
        ///</summary>
        public static string Capitalize(this string text)
        {
            if (text.IsNullOrEmpty()) return "";
            if (text.Length == 1) return text.ToUpper();
            var fisrtLetter = text[0].ToString().ToUpper();
            var otherLetters = text.Substring(1).ToLower();
            return fisrtLetter + otherLetters;
        }

        ///<summary>
        ///Capitalizes every word on a given string.
        ///<para/> If param <c>wordsLowerCase</c> is used, every word on array <c>wordsLowerCase</c> won't be Capitalized, but as lower case
        ///<para/> If param <c>wordsUpperCase</c> is used, every word on array <c>wordsUpperCase</c> won't be Capitalized, but as upper case
        ///</summary>
        public static string CapitalizeSentence(this string text, string[] wordsLowerCase = null, string[] wordsUpperCase = null)
        {
            if (text.IsNullOrEmpty()) return text;
            var sentence = "";
            var words = text.Split(' ');
            foreach (var word in words)
            {
                if (wordsUpperCase != null && wordsUpperCase.ContainsCaseIgnored(word))
                    sentence += word.ToUpper() + " ";
                else if (wordsLowerCase != null && wordsLowerCase.ContainsCaseIgnored(word))
                    sentence += word.ToLower() + " ";
                else sentence += word.Capitalize() + " ";
            }
            return sentence.TrimEnd();
        }

        ///<summary>
        ///Capitalizes every word on a given string, excluding PT prepositions
        ///<para/> If param <c>wordsLowerCase</c> is used, every word on array <c>wordsLowerCase</c> won't be Capitalized, but as lower case
        ///<para/> If param <c>wordsUpperCase</c> is used, every word on array <c>wordsUpperCase</c> won't be Capitalized, but as upper case
        ///</summary>
        public static string CapitalizeTitlePt(this string text, string[] wordsLowerCase = null, string[] wordsUpperCase = null)
        {
            if (text.IsNullOrEmpty()) return text;
            if (wordsLowerCase == null)
                wordsLowerCase = Preposicoes;
            else
            {
                var wordsLowerCaseList = wordsLowerCase.ToList();
                wordsLowerCaseList.AddRange(Preposicoes);
                wordsLowerCase = wordsLowerCaseList.ToArray();
            }
            return text.CapitalizeSentence(wordsLowerCase: wordsLowerCase, wordsUpperCase: wordsUpperCase);
        }

        ///<summary>
        ///Check if the list of strings contains a given string. It is not case-sensitive
        ///</summary>
        public static bool ContainsCaseIgnored(this string[] textList, string value)
        {
            return textList.Any(x => string.Equals(x, value, StringComparison.CurrentCultureIgnoreCase));
        }

        ///<summary>
        ///Check if the string contains a given string. It is not case-sensitive
        ///</summary>
        public static bool ContainsCaseIgnored(this string text, string value) {
            return text.ToUpper().Contains(value.ToUpper());
        }

        ///<summary>
        ///Check if the string is equal to the given value. It is not case-sensitive
        ///</summary>
        public static bool EqualsCaseIgnored(this string text, string value)
        {
            return text.Equals(value, StringComparison.CurrentCultureIgnoreCase);
        }

        ///<summary>
        ///A simpler way to check if a string is null, empty or white-space only.
        ///</summary>
        public static bool IsNullOrEmpty(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        /// <summary>
        /// Converts string to DateTime
        /// </summary>
        /// <param name="date"></param>
        /// <param name="format">Date format. Default: dd/MM/yyyy</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string date, string format = "dd/MM/yyyy")
        {
            return DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Remove diacritics
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveAccents(this string text)
        {
            for (var i = 0; i < ComAcentos.Length; i++)
            {
                text = text.Replace(ComAcentos[i].ToString(), SemAcentos[i].ToString());
            }
            return text;
        }

        ///<summary>
        ///Remove all chars that are not letters or numbers
        ///</summary>
        public static string RemoveSpecialCharacters(this string text) 
        {
            var rgx = new Regex("[^a-zA-Z0-9 -]");
            return rgx.Replace(text, ""); 
        }
    }
}