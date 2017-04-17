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

        RSAHelper RSAhelper = new RSAHelper();
        private UnicodeEncoding ByteConverter = new UnicodeEncoding();
        private byte[] encryptedtext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonEncrypt_Click(object sender, RoutedEventArgs e)
        {
            byte [] plaintext = ByteConverter.GetBytes(textBoxTekst.Text);
            encryptedtext = RSAhelper.Encryption(plaintext, RSAhelper.PublicKey, false);
            textBoxEncrypted.Text = ByteConverter.GetString(encryptedtext);
        }

        private void buttonDecrypt_Click(object sender, RoutedEventArgs e)
        {
            byte[] decryptedtex = RSAhelper.Decryption(encryptedtext, RSAhelper.PrivateKey, false);
            textBoxDecrypted.Text = ByteConverter.GetString(decryptedtex);
        }
    }
}
