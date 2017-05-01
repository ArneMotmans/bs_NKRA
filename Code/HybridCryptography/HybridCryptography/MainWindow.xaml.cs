using System;
using System.Collections.Generic;
using System.Drawing;
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
using Microsoft.Win32;
using System.Drawing;
using System.Drawing.Imaging;
using Path = System.IO.Path;

namespace HybridCryptography
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Uri imageUri;
        private Bitmap img;
        private int textLength;

        public MainWindow()
        {
            InitializeComponent();

            HybridCryptograpyHelper hybrid = new HybridCryptograpyHelper();
            TripleDESHelper tdes = new TripleDESHelper();

            
            MessageBox.Show(hybrid.Decrypt(hybrid.Encrypt("Hallo Wereld!")));

        }

        private void selectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files (*.png, *.bnp) | *.png; *.bnp";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if ((bool)dialog.ShowDialog())
            {
                imageUri = new Uri(dialog.FileName);
                pathTextBox.Text = imageUri.AbsolutePath;
                image.Source = new BitmapImage(imageUri);
                img = new Bitmap(imageUri.AbsolutePath);
            }
        }

        private void encodeButton_Click(object sender, RoutedEventArgs e)
        {
            img = new PictureSteganographyHelper().embedText(inputTextBox.Text, img);
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Image Files (*.png, *.bnp) | *.png; *.bnp";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if ((bool)dialog.ShowDialog())
            {
                string name = dialog.SafeFileName;
                pathTextBox.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), name);
                img.Save(pathTextBox.Text);
            }
        }

        private void decodeButton_Click(object sender, RoutedEventArgs e)
        {
            outputTextBox.Text = new PictureSteganographyHelper().extractText(img);
        }
    }
}
