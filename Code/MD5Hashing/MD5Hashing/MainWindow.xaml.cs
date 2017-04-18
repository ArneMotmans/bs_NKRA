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
        MD5Helper md5helper = new MD5Helper();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void hashButton_Click(object sender, RoutedEventArgs e)
        {
            hashTextBox.Text = md5helper.GenerateHash(inputTextBox.Text, saltTextBox.Text);
        }

        private void generateSaltButton_Click(object sender, RoutedEventArgs e)
        {
            saltTextBox.Text = md5helper.GenerateSalt(32);
        }

        private void disableSaltCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            generateSaltButton.IsEnabled = false;
            saltTextBox.Text = String.Empty;
           // Salt = "";
            saltTextBox.IsEnabled = false;
        }

        private void disableSaltCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            generateSaltButton.IsEnabled = true;
            saltTextBox.IsEnabled = true;
        }
    }
}
