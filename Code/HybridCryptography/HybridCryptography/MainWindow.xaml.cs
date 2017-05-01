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
using System.IO;
using Brushes = System.Windows.Media.Brushes;
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
        private string filePath;
        private byte[] fileContents;
        private string key;
        private Dictionary<string, byte[]> encryptedFileContents;
        private HybridCryptograpyHelper hybrid;

        public MainWindow()
        {
            InitializeComponent();
            hybrid = new HybridCryptograpyHelper();
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

        private void selectFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if ((bool)dialog.ShowDialog())
            {
                filePath = dialog.FileName;
                filePathTextBox.Text = filePath;
                fileContents = File.ReadAllBytes(filePath);
            }
        }

        private void encryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (key != null)
            {
                try
                {
                    encryptedFileContents =
                        hybrid.Encrypt(fileContents, hybrid.ConvertStringToKey(receiverPublicKeyTextBox.Text));
                    SetEncryptionStatusMessage("Succes", Brushes.Green);
                }
                catch (InvalidOperationException ex)
                {
                    SetEncryptionStatusMessage("Failed: Ongeldige publieke sleutel", Brushes.Red);
                }
                catch (NullReferenceException ex)
                {
                    SetEncryptionStatusMessage("Failed: Ongeldig bestand", Brushes.Red);
                }        
            }
            else
            {
                SetEncryptionStatusMessage("Failed: Geen publieke sleutel gevonden", Brushes.Red);
            }
        }

        private void selectKeyButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if ((bool)dialog.ShowDialog())
            {
                string keyPath = dialog.FileName;
                byte[] keyBytes = File.ReadAllBytes(keyPath);
                key = Encoding.UTF8.GetString(keyBytes);
                receiverPublicKeyTextBox.Text = key;
            }
        }

        private void saveFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if ((bool)dialog.ShowDialog())
            {
                File.WriteAllText(dialog.FileName, EncryptedDataHelper.ToFileFormat(encryptedFileContents));
            }
        }

        private void selectFileToDecryptButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if ((bool)dialog.ShowDialog())
            {
                filePath = dialog.FileName;
                fileToDecryptTextBox.Text = filePath;
                Dictionary<string, byte[]> test = EncryptedDataHelper.ToDictionary(File.ReadAllText(filePath));
                encryptedFileContents = test;
            }
        }

        private void selectPublicKeyToDecryptButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if ((bool)dialog.ShowDialog())
            {
                string keyPath = dialog.FileName;
                byte[] keyBytes = File.ReadAllBytes(keyPath);
                key = Encoding.UTF8.GetString(keyBytes);
                senderPublicKeyToDecryptTextBox.Text = key;
            }
        }

        private void decryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (key != null)
            {
                try
                {
                    fileContents =
                        hybrid.Decrypt(encryptedFileContents,
                            hybrid.ConvertStringToKey(senderPublicKeyToDecryptTextBox.Text))["text"];
                    SetDecryptionStatusMessage("Succes", Brushes.Green);
                    resultTextBox.Text = Encoding.UTF8.GetString(fileContents);
                }
                catch (InvalidOperationException ex)
                {
                    SetDecryptionStatusMessage("Failed: Ongeldige publieke sleutel", Brushes.Red);
                }
                catch (NullReferenceException ex)
                {
                    SetDecryptionStatusMessage("Failed: Ongeldig bestand", Brushes.Red);
                }
                catch (ArgumentNullException ex)
                {
                    SetDecryptionStatusMessage("Failed: Kan bestand niet decrypteren", Brushes.Red);
                }
            }
            else
            {
                SetDecryptionStatusMessage("Failed: Geen publieke sleutel gevonden", Brushes.Red);
            }
        }

        private void CloseMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CopyPublicKeyMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if ((bool)dialog.ShowDialog())
            {
                File.WriteAllText(dialog.FileName, hybrid.ConvertKeyToString(hybrid.PublicKey));
            }
        }

        private void SetEncryptionStatusMessage(String message, SolidColorBrush color)
        {
            encryptStatusMessageLabel.Content = message;
            encryptStatusMessageLabel.Foreground = color;
        }

        private void SetDecryptionStatusMessage(String message, SolidColorBrush color)
        {
            decryptStatusMessageLabel.Content = message;
            decryptStatusMessageLabel.Foreground = color;
        }
    }
}
