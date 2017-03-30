using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Security.Cryptography;

namespace MD5Hashing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Salt { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void hashButton_Click(object sender, RoutedEventArgs e)
        {
            hashTextBox.Text = GenerateHash(inputTextBox.Text, Salt);
        }

        private void generateSaltButton_Click(object sender, RoutedEventArgs e)
        {
            saltTextBox.Text = GenerateSalt(32);
        }

        private string GenerateHash(string text, string salt = "")
        {
            MD5 md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedTextBytes = md5Hasher.ComputeHash(Encoding.Unicode.GetBytes(salt+text));
            return ByteArrayToString(hashedTextBytes);
        }

        private string GenerateSalt(int size)
        {
            RNGCryptoServiceProvider salter = new RNGCryptoServiceProvider();   //Genereert random bytes
            byte[] saltBytes = new byte[size];
            salter.GetBytes(saltBytes);
            Salt = ByteArrayToString(saltBytes);
            return Salt;
        }

        private string ByteArrayToString(byte[] array)
        {
            return BitConverter.ToString(array).Replace("-", String.Empty).ToLower();
        }

        private void disableSaltCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            generateSaltButton.IsEnabled = false;
            saltTextBox.Text = String.Empty;
            Salt = "";
            saltTextBox.IsEnabled = false;
        }

        private void disableSaltCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            generateSaltButton.IsEnabled = true;
            saltTextBox.IsEnabled = true;
        }
    }
}
