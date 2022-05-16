using System.Security.Cryptography;
using System.Text;
using System;

namespace AndiSoft.Utilities
{
    // todo

    internal class Encryption
    {
        
        public static string EncryptAes(string text, string key)
        {
            var objArray = Encoding.UTF8.GetBytes(text);
            var objT = Aes.Create();
            var objCrypt = new MD5CryptoServiceProvider();
            var secretArray = objCrypt.ComputeHash(Encoding.UTF8.GetBytes(key));

            objCrypt.Clear();
            objT.Key = secretArray;
            objT.Mode = CipherMode.ECB;
            objT.Padding = PaddingMode.PKCS7;

            var encryption = objT.CreateEncryptor();
            var resArray = encryption.TransformFinalBlock(objArray, 0, objArray.Length);
            objT.Clear();

            return Convert.ToBase64String(resArray, 0, resArray.Length);
        }

        public static string EncryptTripleDes(string text, string key)
        {
            var objArray = Encoding.UTF8.GetBytes(text);
            var objT = new TripleDESCryptoServiceProvider();
            var objCrypt = new MD5CryptoServiceProvider();
            var secretArray = objCrypt.ComputeHash(Encoding.UTF8.GetBytes(key));

            objCrypt.Clear();
            objT.Key = secretArray;
            objT.Mode = CipherMode.ECB;
            objT.Padding = PaddingMode.PKCS7;

            var encryption = objT.CreateEncryptor();
            var resArray = encryption.TransformFinalBlock(objArray, 0, objArray.Length);
            objT.Clear();

            return Convert.ToBase64String(resArray, 0, resArray.Length);
        }

        public static string DecryptTripleDes(string text, string key)
        {
            var directArray = Convert.FromBase64String(text);
            var objT = new TripleDESCryptoServiceProvider();
            var objMdCrypto = new MD5CryptoServiceProvider();

            var secretArray = objMdCrypto.ComputeHash(Encoding.UTF8.GetBytes(key));
            objMdCrypto.Clear();
            objT.Key = secretArray;
            objT.Mode = CipherMode.ECB;
            objT.Padding = PaddingMode.PKCS7;

            var decryption = objT.CreateDecryptor();
            var resArray = decryption.TransformFinalBlock(directArray, 0, directArray.Length);
            objT.Clear();

            return Encoding.UTF8.GetString(resArray);
        }
    }
}