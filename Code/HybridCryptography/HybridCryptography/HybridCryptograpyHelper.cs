using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encrypt_Decrypt_Program;
using MD5Hashing;
using RsaCryptoExample2;

namespace HybridCryptography
{
    public class HybridCryptograpyHelper
    {
        MD5Helper md5helper = new MD5Helper();
        TripleDESHelper tripleDesHelper = new TripleDESHelper();
        RSAHelper RsaHelper = new RSAHelper();

        public Dictionary<string, byte[]> Encrypt(string text)
        {
            Dictionary<string,byte[]> output = new Dictionary<string, byte[]>();
            output.Add("hash",StringToByteArray(md5helper.GenerateHash(text)));
            output.Add("text",tripleDesHelper.Encrypt(text));
            output.Add("key",RsaHelper.Encryption(tripleDesHelper.GetKey(),RsaHelper.PublicKey,false));
            return output;
        }

        private byte[] StringToByteArray(string text)
        {
            return Encoding.ASCII.GetBytes(text);
        }
    }
}
