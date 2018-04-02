using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DES_encryption_decryption
{
    public class DES_encrypt_decrypt
    {

        /// <comment>C# DES解密方法 </summary>  
        /// <param name="encryptedValue">待解密的字符串</param>  
        /// <param name="key">密钥</param>  
        /// <param name="iv">向量</param>  
        /// <returns>解密后的字符串</returns>  
        public static string DESEncrypt(string originalValue, string key, string iv)
        {
            using (DESCryptoServiceProvider sa
                = new DESCryptoServiceProvider { Key = Encoding.UTF8.GetBytes(key), IV = Encoding.UTF8.GetBytes(iv) })
            {
                using (ICryptoTransform ct = sa.CreateEncryptor())
                {
                    byte[] by = Encoding.UTF8.GetBytes(originalValue);
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, ct,CryptoStreamMode.Write))
                        {
                            cs.Write(by, 0, by.Length);
                            cs.FlushFinalBlock();
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        /// <comment> C# DES解密方法 </comment>  
        /// <param name="encryptedValue">待解密的字符串</param>  
        /// <param name="key">密钥</param>  
        /// <param name="iv">向量</param>  
        /// <returns>解密后的字符串</returns>  
        public static string DESDecrypt(string encryptedValue, string key, string iv)
        {
            using (DESCryptoServiceProvider sa =
                new DESCryptoServiceProvider
                { Key = Encoding.UTF8.GetBytes(key), IV = Encoding.UTF8.GetBytes(iv) })
            {
                using (ICryptoTransform ct = sa.CreateDecryptor())
                {
                    byte[] byt = Convert.FromBase64String(encryptedValue);

                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                        {
                            cs.Write(byt, 0, byt.Length);
                            cs.FlushFinalBlock();
                        }
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("请输入明文:");
            string plaintext = Console.ReadLine();
            Console.WriteLine("请输入秘钥:");
            string key = Console.ReadLine();
            Console.WriteLine("请输入向量:");
            string vector = Console.ReadLine();
            Console.WriteLine("加密过程如下:");
            string DES_encrypted_result = DESEncrypt(plaintext, key, vector);
            Console.WriteLine(plaintext + "+" + key + "+" + vector + "=" + DES_encrypted_result);
            Console.WriteLine("解密过程如下:");
            string plaintext_check = DESDecrypt(DES_encrypted_result, key, vector);
            Console.WriteLine(DES_encrypted_result + "+" + key + "+" + vector + "=" + plaintext_check);
            Console.ReadKey();
        }
    }
}
