using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RsaCryptoExample2
{
    public class RSAHelper
    {

        private UnicodeEncoding ByteConverter = new UnicodeEncoding();

        public RSACryptoServiceProvider RSA { get; set; }

        private RSAParameters publicKey;

        public RSAParameters PublicKey
        {
            get { return RSA.ExportParameters(false); }
            set { publicKey = value; }
        }

        private RSAParameters privateKey;

        public RSAParameters PrivateKey
        {
            get { return RSA.ExportParameters(true); }
            set { privateKey = value; }
        }

        public RSAHelper()
        {
            RSA = new RSACryptoServiceProvider();
        }

        public byte[] Encryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey); encryptedData = RSA.Encrypt(Data, DoOAEPPadding);
                }
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public byte[] Decryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    decryptedData = RSA.Decrypt(Data, DoOAEPPadding);
                }
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }

   
}
