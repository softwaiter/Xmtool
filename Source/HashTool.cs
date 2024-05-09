using System;
using System.Security.Cryptography;
using System.Text;

namespace CodeM.Common.Tools
{
    public class HashTool
    {
        private static HashTool sHG = new HashTool();

        private HashTool()
        { 
        }

        internal static HashTool New()
        {
            return sHG;
        }

        public string MD5(string text, string encoding = "utf-8") {
            byte[] bytes = Encoding.GetEncoding(encoding).GetBytes(text);

            using (MD5 md5 = System.Security.Cryptography.MD5.Create()) {
                md5.ComputeHash(bytes);
                byte[] hashBytes = md5.Hash;
                md5.Clear();
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public string SHA1(string text, string encoding = "utf-8") {
            byte[] bytes = Encoding.GetEncoding(encoding).GetBytes(text);

            using (SHA1 sha1 = System.Security.Cryptography.SHA1.Create()) {
                sha1.ComputeHash(bytes);
                byte[] hashBytes = sha1.Hash;
                sha1.Clear();
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public string SHA256(string text, string encoding = "utf-8") {
            byte[] bytes = Encoding.GetEncoding(encoding).GetBytes(text);

            using (SHA256 sha256 = System.Security.Cryptography.SHA256.Create()) {
                sha256.ComputeHash(bytes);
                byte[] hashBytes = sha256.Hash;
                sha256.Clear();
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public string SHA384(string text, string encoding = "utf-8") {
            byte[] bytes = Encoding.GetEncoding(encoding).GetBytes(text);

            using (SHA384 sha384 = System.Security.Cryptography.SHA384.Create()) {
                sha384.ComputeHash(bytes);
                byte[] hashBytes = sha384.Hash;
                sha384.Clear();
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public string SHA512(string text, string encoding = "utf-8") {
            byte[] bytes = Encoding.GetEncoding(encoding).GetBytes(text);

            using (SHA512 sha512 = System.Security.Cryptography.SHA512.Create()) {
                sha512.ComputeHash(bytes);
                byte[] hashBytes = sha512.Hash;
                sha512.Clear();
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}