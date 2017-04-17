using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Encrypt_Decrypt_Program
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private static byte[] GenerateKey()
        {
            RNGCryptoServiceProvider generator = new RNGCryptoServiceProvider();
            byte[] keyBytes = new byte[16];
            generator.GetBytes(keyBytes);
            return keyBytes;
        }

        public static Dictionary<string, byte[]> Encrypt(string toEncrypt)
        {
            Dictionary<string, byte[]> output = new Dictionary<string, byte[]>();
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            // Generate safe 24 bit key
            keyArray = GenerateKey();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
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
        }

        private void EncryptText(object sender, RoutedEventArgs e)
        {
            Dictionary<string, byte[]> encryptResult = Encrypt(InputTextBox.Text);
            string encryptedText = UTF8Encoding.UTF8.GetString(encryptResult["text"]);
            string key = String.Join(".", encryptResult["key"]);
            EncryptTextBox.Text = encryptedText;
            KeyTextBox.Text = key;
            EncryptedBytesTextBox.Text = String.Join(".", encryptResult["text"]);
        }

        private void DEcryptText(object sender, RoutedEventArgs e)
        {
            string decryptedText = Decrypt(EncryptToDecryptTextBox.Text,KeyToDecryptTextBox.Text);
            OutputTextBox.Text = decryptedText;
        }
    }
}
