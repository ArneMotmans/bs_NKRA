using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MD5Hashing
{
    public class MD5Helper
    {
        public string Salt { get; set; }

        public string GenerateHash(string text, string salt = "")
        {
            MD5 md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedTextBytes = md5Hasher.ComputeHash(Encoding.Unicode.GetBytes(salt + text));
            return ByteArrayToString(hashedTextBytes);
        }

        public string GenerateSalt(int size)
        {
            RNGCryptoServiceProvider salter = new RNGCryptoServiceProvider();   //Genereert random bytes
            byte[] saltBytes = new byte[size];
            salter.GetBytes(saltBytes);
            Salt = ByteArrayToString(saltBytes);
            return Salt;
        }

        private string ByteArrayToString(byte[] array)
        {
            return BitConverter.ToString(array).Replace("-", String.Empty).ToLower();
        }
    }
}
