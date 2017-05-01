using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridCryptography
{
    public class EncryptedDataHelper
    {
        public static string ToFileFormat(Dictionary<string, byte[]> data)
        {
            StringBuilder dataString = new StringBuilder();
            foreach (var key in data.Keys)
            {
                foreach (var dataByte in data[key])
                {
                    dataString.Append(dataByte);
                    dataString.Append('.');
                }
                dataString.Append("-");
            }
            return dataString.ToString();
        }

        public static Dictionary<string, byte[]> ToDictionary(string data)
        {
            Dictionary<string, byte[]> dataDictionary = new Dictionary<string, byte[]>();
            string[] byteStrings = data.Split('-');
            dataDictionary.Add("text",StringToByteArray(byteStrings[0]));
            dataDictionary.Add("key", StringToByteArray(byteStrings[1]));
            dataDictionary.Add("hash", StringToByteArray(byteStrings[2]));
            return dataDictionary;
        }

        private static byte[] StringToByteArray(string byteString)
        {
            List<byte> bytes = new List<byte>();
            foreach (var nr in byteString.Split('.'))
            {
                if (nr != "")
                bytes.Add(Convert.ToByte(nr));
            }
            return bytes.ToArray();
        }
    }
}
