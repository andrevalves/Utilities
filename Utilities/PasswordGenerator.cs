﻿using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AndiSoft.Utilities
{
    /// <summary>
    /// Password Generator
    /// </summary>
    public static class PasswordGenerator
    {
        private static readonly char[] Numbers = "1234567890".ToCharArray();
        private static readonly char[] SenhaAlfaNumerica = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        private static readonly char[] SenhaForte = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%&*()".ToCharArray();

        public static string GenerateAlphaNumeric(int size)
        {
            return GeneratePassword(SenhaAlfaNumerica, size);
        }

        public static string GenerateNumeric(int size)
        {
            return GeneratePassword(Numbers, size);
        }

        public static string GenerateStrongPassword(int size)
        {
            string senhaForte;

            do
            {
                senhaForte = GeneratePassword(SenhaForte, size);
            } while (!AndiValidation.IsStrongPassword(senhaForte));

            return senhaForte;
        }

        private static string GeneratePassword(IReadOnlyList<char> colecao, int size)
        {
            var data = new byte[size];
            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }

            var sb = new StringBuilder(size);
            foreach (var b in data)
            {
                sb.Append(colecao[b % (colecao.Count)]);
            }
            var result = sb.ToString();

            return result;
        }
    }
}