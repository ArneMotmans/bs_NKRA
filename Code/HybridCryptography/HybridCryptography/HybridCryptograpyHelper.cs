﻿using System;
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
        private MD5Helper md5helper = new MD5Helper();
        private TripleDESHelper tripleDesHelper = new TripleDESHelper();
        private RSAHelper RsaHelperA = new RSAHelper();
        private RSAHelper RsaHelperB = new RSAHelper();

        public Dictionary<string, byte[]> Encrypt(string text)
        {
            Dictionary<string,byte[]> output = new Dictionary<string, byte[]>();
            output.Add("text",tripleDesHelper.Encrypt(text)); //file 1: het origineel geencrypteerd met triple DES. Het gene wat geencrypteerd wordt is text (uit de parameter van deze functie)
            output.Add("key",RsaHelperB.Encryption(tripleDesHelper.GetKey(),RsaHelperB.PublicKey,false)); //File 2: triple des sleutel encrypteren met de public van B
            output.Add("hash", RsaHelperA.Encryption(StringToByteArray(md5helper.GenerateHash(text)) , RsaHelperA.PrivateKey , false)); // file 3: maak een hash en encrypteer die met de privé sleutel van A
            return output;
        }

        public string Decrypt(Dictionary<string, byte[]> input)
        {
            byte[] tripleDesKey = RsaHelperB.Decryption(input["key"], RsaHelperB.PrivateKey, false); //file 2: decrypteren met de prive van B --> geeft tripledes sleutel
            string geheimeText = tripleDesHelper.Decrypt(input["text"].ToString(), tripleDesKey.ToString()); //file 1: decrypteren met de zo juist verkregen tripledes sleutel
            string hash = RsaHelperA.Decryption(input["hash"], RsaHelperA.PublicKey, false).ToString();
            string outputText;
            if (md5helper.GenerateHash(geheimeText).Equals(hash))
            {
                outputText = "De hash is in orde \n\n" + geheimeText;
            }
            else
            {
                outputText = "De hash is NIET IN ORDE! \n\n" + geheimeText;
            }

            return outputText;
        }

        private byte[] StringToByteArray(string text)
        {
            return Encoding.ASCII.GetBytes(text);
        }
    }
}