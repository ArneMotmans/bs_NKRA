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
            /*string text = System.Text.UTF8Encoding.Default.GetString(tdes.Encrypt("Hallo wereld"));
            string key = System.Text.UTF8Encoding.Default.GetString(tdes.GetKey());*/
            string text = tdes.Encrypt("hallo wereld").ToString();
            string key = tdes.GetKey().ToString();
            MessageBox.Show(text);
            MessageBox.Show(tdes.Decrypt(text, key));
            //MessageBox.Show(hybrid.Decrypt(hybrid.Encrypt("Hallo aan mijn Team")));
        }
    }
}
