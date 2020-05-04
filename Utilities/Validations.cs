using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace AndiSoft.Utilities
{
    public static class Validations
    {
        ///<summary>
        ///Checks if a given string contains only number. If any char is not numeric, the return is false.
        ///</summary>
        public static bool IsNumberOnly(this string text)
        {
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
            var rgx = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");
            return rgx.IsMatch(password);
        }

        /// <summary>
        /// Verifies if the given string is a valid CPF.
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static bool IsValidoCpf(string cpf)
        {
            var mt1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var mt2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
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
    }
}