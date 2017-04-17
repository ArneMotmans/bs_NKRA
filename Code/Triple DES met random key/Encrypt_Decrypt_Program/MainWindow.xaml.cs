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

        private void EncryptText(object sender, RoutedEventArgs e)
        {
            Dictionary<string, byte[]> encryptResult = TripleDESHelper.Encrypt(InputTextBox.Text);
            string encryptedText = UTF8Encoding.UTF8.GetString(encryptResult["text"]);
            string key = String.Join(".", encryptResult["key"]);
            EncryptTextBox.Text = encryptedText;
            KeyTextBox.Text = key;
            EncryptedBytesTextBox.Text = String.Join(".", encryptResult["text"]);
        }

        private void DEcryptText(object sender, RoutedEventArgs e)
        {
            string decryptedText = TripleDESHelper.Decrypt(EncryptToDecryptTextBox.Text,KeyToDecryptTextBox.Text);
            OutputTextBox.Text = decryptedText;
        }
    }
}
