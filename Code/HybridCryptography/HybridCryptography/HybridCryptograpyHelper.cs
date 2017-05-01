using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Encrypt_Decrypt_Program;
using MD5Hashing;
using RsaCryptoExample2;

namespace HybridCryptography
{
    public class HybridCryptograpyHelper
    {
        private MD5Helper md5helper = new MD5Helper();
        private TripleDESHelper tripleDesHelper = new TripleDESHelper();
        private RSAHelper RsaHelper = new RSAHelper();
        public RSAParameters PublicKey { get; }

        public HybridCryptograpyHelper()
        {
            PublicKey = RsaHelper.PublicKey;
        }

        public Dictionary<string, byte[]> Encrypt(byte[] bytesToEncrypt, RSAParameters publicKeyReceiver)
        {
            Dictionary<string,byte[]> output = new Dictionary<string, byte[]>();
            Dictionary<string, byte[]> tdes = TripleDESHelper.Encrypt(bytesToEncrypt);
            output.Add("text",tdes["text"]); //file 1: het origineel geencrypteerd met triple DES. Het gene wat geencrypteerd wordt is text (uit de parameter van deze functie)
            output.Add("key",RsaHelper.Encryption(tdes["key"], publicKeyReceiver, false)); //File 2: triple des sleutel encrypteren met de public van andere persoon
            output.Add("hash", RsaHelper.SignData(md5helper.GenerateHash(bytesToEncrypt) , RsaHelper.PrivateKey)); // file 3: maak een hash en encrypteer die met eigen privé sleutel
            return output;
        }

        public Dictionary<string, byte[]> Decrypt(Dictionary<string, byte[]> data, RSAParameters publicKeySender)
        {
            Dictionary<string, byte[]> output = new Dictionary<string, byte[]>();
            try
            {
                output.Add("key", RsaHelper.Decryption(data["key"], RsaHelper.PrivateKey, false));
                //file 2: decrypteren met eigen prive -> geeft tripledes sleutel
                output.Add("text", TripleDESHelper.Decrypt(data["text"], output["key"]));
                //file 1: decrypteren met de zo juist verkregen tripledes sleutel
                byte[] hashComparison = new byte[]
                {
                    Convert.ToByte(RsaHelper.VerifyData(md5helper.GenerateHash(output["text"]), data["hash"],
                        publicKeySender))
                }; //decrypteer meegestuurde hash met public key van zender en vergelijk
                output.Add("hash", hashComparison);
                return output;
            }
            catch (NullReferenceException ex)
            {
                throw new NullReferenceException(ex.Message);
            }
        }

        public RSAParameters ConvertStringToKey(string keyString)
        {
            //get a stream from the string
            var sr = new System.IO.StringReader(keyString);
            //we need a deserializer
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            //get the object back from the stream
            try
            {
                return (RSAParameters) xs.Deserialize(sr);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public string ConvertKeyToString(RSAParameters key)
        {
            //we need some buffer
            var sw = new System.IO.StringWriter();
            //we need a serializer
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            //serialize the key into the stream
            xs.Serialize(sw, key);
            //get the string from the stream
            return sw.ToString();
        }

        private byte[] StringToByteArray(string text)
        {
            return Encoding.ASCII.GetBytes(text);
        }
    }
}
