using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Encrypt_Decrypt_Program
{
    public class TripleDESHelper
    {
        public TripleDESCryptoServiceProvider tdes { get; set; }

        public TripleDESHelper()
        {
            tdes = new TripleDESCryptoServiceProvider();
        }

        public byte[] Encrypt(string toEncrypt)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            // Generate safe key (never weak key)
            tdes.GenerateKey();

            keyArray = tdes.Key;

            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();

            //Return the encrypted data in byte array
            return resultArray;

        }

        /*public static Dictionary<string, byte[]> Encrypt(string toEncrypt)
        {
            Dictionary<string, byte[]> output = new Dictionary<string, byte[]>();
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            // Generate safe key (never weak key)
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.GenerateKey();

            keyArray = tdes.Key;

            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();

            //Add key and encrypted data to output Dictionary
            output.Add("text", resultArray);
            output.Add("key", keyArray);

            //Return the encrypted data into unreadable string format
            return output;

        }

        public static string Decrypt(string cipherString, string key)
        {

            byte[] keyArray;
            //get the byte code of the string

            List<byte> encryptedBytes = new List<byte>();
            foreach (string b in cipherString.Split('.'))
            {
                encryptedBytes.Add(Convert.ToByte(b));
            }
            byte[] toDecryptArray = encryptedBytes.ToArray();

            //Get your key from config file to open the lock!
            string[] stringBytes = key.Split('.');
            List<byte> listBytes = new List<byte>();
            foreach (string b in stringBytes)
            {
                listBytes.Add(Convert.ToByte(b));
            }
            keyArray = listBytes.ToArray();


            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toDecryptArray, 0, toDecryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }*/

        public string Decrypt(string cipherString, string key)
        {

            byte[] keyArray;
            //get the byte code of the string

            List<byte> encryptedBytes = new List<byte>();
            foreach (string b in cipherString.Split('.'))
            {
                encryptedBytes.Add(Convert.ToByte(b));
            }
            byte[] toDecryptArray = encryptedBytes.ToArray();

            //Get your key from config file to open the lock!
            string[] stringBytes = key.Split('.');
            List<byte> listBytes = new List<byte>();
            foreach (string b in stringBytes)
            {
                listBytes.Add(Convert.ToByte(b));
            }
            keyArray = listBytes.ToArray();

            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toDecryptArray, 0, toDecryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public byte[] GetKey()
        {
            return tdes.Key;
        }
    }
}
