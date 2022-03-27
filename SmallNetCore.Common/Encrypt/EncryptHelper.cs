using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Common.Encrypt
{
    public class EncryptHelper
    {
        /// <summary>
        /// Aes加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string AESEncrypt(string source, string aesKey = "2335d6187704a7c4", string aesIV = "6e9fa9545949b9f9")
        {
            using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
            {
                aesProvider.Key = Encoding.UTF8.GetBytes(aesKey);
                aesProvider.IV = Encoding.UTF8.GetBytes(aesIV);
                aesProvider.Mode = CipherMode.CBC;
                aesProvider.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor())
                {
                    byte[] inputBuffers = Encoding.UTF8.GetBytes(source);
                    byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    aesProvider.Clear();
                    aesProvider.Dispose();
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }
        /// <summary>
        /// Aes解密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string AESDecrypt(string source, string aesKey = "2335d6187704a7c4", string aesIV = "6e9fa9545949b9f9")
        {
            using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
            {
                //source = source.Replace(" ", "+");

                aesProvider.Key = Encoding.UTF8.GetBytes(aesKey);
                aesProvider.IV = Encoding.UTF8.GetBytes(aesIV);
                aesProvider.Mode = CipherMode.CBC;
                aesProvider.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor())
                {
                    byte[] inputBuffers = Convert.FromBase64String(source);
                    byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    aesProvider.Clear();
                    return Encoding.UTF8.GetString(results);
                }
            }
        }
    }
}
