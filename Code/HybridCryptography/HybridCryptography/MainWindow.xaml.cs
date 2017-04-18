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
using Encrypt_Decrypt_Program;

namespace HybridCryptography
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            HybridCryptograpyHelper hybrid = new HybridCryptograpyHelper();
            TripleDESHelper tdes = new TripleDESHelper();

            
            //MessageBox.Show(hybrid.Decrypt(hybrid.Encrypt("Hallo Wereld!")));

            
            string geheimeText = tdes.Decrypt(String.Join(".", tdes.Encrypt("Hallo Wereld!")), String.Join(".", tdes.GetKey()));
            MessageBox.Show(geheimeText);

            /*//encrypt
            Dictionary<string, byte[]> encryptResult = TripleDESHelper.Encrypt("Hallo Wereld!");
            string encryptedText = UTF8Encoding.UTF8.GetString(encryptResult["text"]);
            string key = String.Join(".", encryptResult["key"]);
            string geheimeText = encryptedText;
            string geheimeTextBytes = String.Join(".", encryptResult["text"]);

            //decrypt
            string decryptedText = TripleDESHelper.Decrypt(geheimeTextBytes, key);
            MessageBox.Show(decryptedText);*/
        }
    }
}
