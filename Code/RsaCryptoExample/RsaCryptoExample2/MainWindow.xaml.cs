using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace RsaCryptoExample2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();  // dit is uw RSA --> RSA.exportparameters(false) = public key --> visa versa voor private key
        byte[] plaintext;
        byte[] encryptedtext;

        public MainWindow()
        {
            InitializeComponent();

        }

        public byte[] Encryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData; 
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey); //RSA sleutel die meegegeven wordt importeren in de nieuwe RSA variable IN DIT GEVAL DE PUBLIC KEY VAN DE ONTVANGER
                    encryptedData = RSA.Encrypt(Data, DoOAEPPadding); //doOAEPPading (geen idee maar is altijd false) - Data is het gene wat je wilt encrypteren
                }
                return encryptedData; //geef de encrypteerde data terug in bytes
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
                    RSA.ImportParameters(RSAKey); //import de rsa key uit de parameter
                    decryptedData = RSA.Decrypt(Data, DoOAEPPadding); // import  
                }
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        private void buttonEncrypt_Click(object sender, RoutedEventArgs e)
        {

            plaintext = ByteConverter.GetBytes(textBoxTekst.Text);
            encryptedtext = Encryption(plaintext, RSA.ExportParameters(false), false);
            textBoxEncrypted.Text = ByteConverter.GetString(encryptedtext);
        
     }

        private void buttonDecrypt_Click(object sender, RoutedEventArgs e)
        {
            byte[] decryptedtex = Decryption(encryptedtext, RSA.ExportParameters(true), false);
            textBoxDecrypted.Text = ByteConverter.GetString(decryptedtex);
        }
    }
}
