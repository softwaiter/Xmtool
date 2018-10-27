using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CodeM.Common.Tools.Security
{
    public class CryptoUtils
    {
        public static string Base64Encode(string text, string encoding = "utf-8") {
            byte[] bytes = Encoding.GetEncoding(encoding).GetBytes(text);
            return Convert.ToBase64String(bytes);
        }

        public static string Base64Decode(string base64Text, string encoding = "utf-8") {
            byte[] bytes = Convert.FromBase64String(base64Text);
            return Encoding.GetEncoding(encoding).GetString(bytes);
        }

        private static void GenerateAESKeyIV(string aesKey, out byte[] key, out byte[] iv, string encoding) {
            byte[] bytes = Encoding.GetEncoding(encoding).GetBytes(aesKey);

            using (SHA256Managed sha256 = new SHA256Managed()) {
                sha256.ComputeHash(bytes);
                key = sha256.Hash;
                sha256.Clear();
            }

            using (MD5 md5 = MD5.Create()) {
                md5.ComputeHash(bytes);
                iv = md5.Hash;
                md5.Clear();
            }
        }

        public static string AESEncode(string text, string key, string encoding = "utf-8") {
            byte[] source = Encoding.GetEncoding(encoding).GetBytes(text);

            byte[] _key;
            byte[] _iv;
            GenerateAESKeyIV(key, out _key, out _iv, encoding);

            byte[] target;

            using (AesManaged aes = new AesManaged()) 
            {
                aes.Key = _key;
                aes.IV = _iv;

                using (MemoryStream ms = new MemoryStream()) 
                {
                    using (CryptoStream cs = new CryptoStream(ms,
                        aes.CreateEncryptor(), CryptoStreamMode.Write)) 
                    {
                        cs.Write(source, 0, source.Length);
                        cs.FlushFinalBlock();
                        target = (byte[])ms.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(target);
        }

        public static string AESDecode(string aesText, string key, string encoding = "utf-8") {
            byte[] source = Convert.FromBase64String(aesText);

            byte[] _key;
            byte[] _iv;
            GenerateAESKeyIV(key, out _key, out _iv, encoding);

            byte[] target;
            using (AesManaged aes = new AesManaged())
            {
                aes.Key = _key;
                aes.IV = _iv;

                using (MemoryStream ms = new MemoryStream()) 
                {
                    using (CryptoStream cs = new CryptoStream(ms,
                        aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(source, 0, source.Length);
                        cs.FlushFinalBlock();
                        target = (byte[])ms.ToArray();
                    }
                }                
            }
            
            return Encoding.GetEncoding(encoding).GetString(target);
        }
    }
}