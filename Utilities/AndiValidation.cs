using System;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using AndiSoft.Utilities.Extensions;

namespace AndiSoft.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public static class AndiValidation
    {
        ///<summary>
        ///Checks if a given string contains only number. If any char is not numeric, the return is false.
        ///</summary>
        public static bool IsNumberOnly(string text)
        {
            if (text.IsNullOrEmpty()) return false;

            return text.All(c => c >= '0' && c <= '9');
        }

        /// <summary>
        /// Verifies if password has at least::
        /// - 1 Uppercase character
        /// - 1 Lowercase character
        /// - 1 Number
        /// - 1 Special character (!@#$%^&*)
        /// </summary>
        /// <param name="password"></param>
        /// <returns>True if password is strong</returns>
        public static bool IsStrongPassword(string password)
        {
            if (password.IsNullOrEmpty()) return false;

            var rgx = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");
            return rgx.IsMatch(password);
        }

        /// <summary>
        /// Verifies if the given string is a valid CPF.
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static bool IsValidCpf(string cpf)
        {
            if (cpf.IsNullOrEmpty()) 
                return false;

            var mt1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var mt2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "").Replace(" ","");

            if (cpf.Length != 11 || !IsNumberOnly(cpf))
                return false;

            var tempCpf = cpf.Substring(0, 9);
            var soma = 0;
            for (var i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * mt1[i];

            var resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            var digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * mt2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }

        /// <summary>
        /// Verifies if the given string is a valid CNPJ
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public static bool IsValidCnpj(string cnpj)
        {
            if (cnpj.IsNullOrEmpty()) return false;

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);

            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;

            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            if (cnpj.EndsWith(digito))
            {
                return true;
            }

            return false;
        }

        ///<summary>
        ///Check if the given string is a valid email address
        ///</summary>
        public static bool IsValidEmail(string text)
        {
            try {
                var addr = new MailAddress(text);
                return addr.Address == text;
            }
            catch {
                return false;
            }
        }

        /// <summary>
        /// Check if given string is a valid date
        /// </summary>
        /// <param name="date">Date (dd/MM/yyyy)</param>
        /// <returns></returns>
        public static bool IsValidDate(string date)
        {
            try
            {
                const string pattern = @"(0[1-9]|[1-2][0-9]|3[0-1])/(0[1-9]|1[0-2])/[0-9]{4}";
                var rx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

                if (!rx.Match(date).Success)
                    return false;
                
                Convert.ToDateTime(date);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if given string is a valid date
        /// </summary>
        /// <param name="date">Date string</param>
        /// <param name="format">Date format. Default: dd/MM/yyyy</param>
        /// <returns></returns>
        public static bool IsValidDate(string date, string format)
        {
            var parts = format.ToLower().Split('/', '-');
            var dates = date.Split('/', '-');

            if(parts.Length != 3)
                throw new Exception("Invalid format!");
            
            try
            {
                int day = 0, mon = 0, year = 0;

                for (var i = 0; i < 3; i++)
                {
                    if (parts[i].Contains("d"))
                        day = Convert.ToInt32(dates[i]);
                    else if (parts[i].Contains("m"))
                        mon = Convert.ToInt32(dates[i]);
                    else if (parts[i].Contains("y"))
                        year = Convert.ToInt32(dates[i]);
                    else
                        return false;
                }

                if (day == 0 || mon == 0 || year == 0)
                    throw new Exception("Invalid format!");
                    
                new DateTime(year, mon, day);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if given string is a valid mobile number in Brazil
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool IsValidMobileNumber(string phone)
        {
            if (phone.IsNullOrEmpty()) return false;

            var pattern = @"^[1-9]{2}9[1-9][0-9]{7}$";
            var rx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return rx.IsMatch(phone);
        }

        /// <summary>
        /// Check if given string is a valid mobile number and removes special characters
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool IsValidMobileNumber(ref string phone)
        {
            if (phone.IsNullOrEmpty()) return false;

            phone = AndiHelper.GetMobileNumber(phone);

            var pattern = @"^[1-9]{2}9[1-9][0-9]{7}$";
            var rx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return rx.IsMatch(phone);
        }
    }
}