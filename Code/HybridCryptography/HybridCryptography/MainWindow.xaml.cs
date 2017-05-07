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
        private string encryptionFilePath;
        private byte[] encryptionFileContents;
        private string fileToEncodePath;
        private string fileToEncodeContents;
        private string fileToDecodePath;
        private string fileToDecodeContents;
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
            try
            {
                fileToEncodeContents = File.ReadAllText(fileToEncodePath);
                img = new PictureSteganographyHelper().embedText(fileToEncodeContents, img);
                SetEncodingStatusMessage("Success", Brushes.Green);
            }
            catch (ArgumentNullException ex)
            {
                SetEncodingStatusMessage("Failed: No file selected", Brushes.Red);
            }
            catch (NullReferenceException ex)
            {
                SetEncodingStatusMessage(ex.Message, Brushes.Red);
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Image Files (*.png, *.bnp) | *.png; *.bnp";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if ((bool)dialog.ShowDialog())
            {
                string name = dialog.FileName;
                img.Save(name);
            }
        }

        private void decodeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                fileToDecodePath = pathTextBox.Text;
                imageUri = new Uri(fileToDecodePath);
                img = new Bitmap(imageUri.AbsolutePath);
                fileToDecodeContents = new PictureSteganographyHelper().extractText(img);
                decodedMessageTextBox.Text = fileToDecodeContents;
                SetDecodingStatusMessage("Success", Brushes.Green);
            }
            catch (UriFormatException ex)
            {
                SetDecodingStatusMessage("Failed: No image selected", Brushes.Red);
            }
        }

        private void selectFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if ((bool)dialog.ShowDialog())
            {
                encryptionFilePath = dialog.FileName;
                filePathTextBox.Text = encryptionFilePath;
                FileStream stream = File.OpenRead(encryptionFilePath);
                encryptionFileContents = new byte[stream.Length];
                stream.Read(encryptionFileContents, 0, encryptionFileContents.Length);
                stream.Close();
            }
        }

        private void encryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (key != null)
            {
                try
                {
                    encryptedFileContents =
                        hybrid.Encrypt(encryptionFileContents, hybrid.ConvertStringToKey(receiverPublicKeyTextBox.Text));
                    SetEncryptionStatusMessage("Success", Brushes.Green);
                }
                catch (InvalidOperationException ex)
                {
                    SetEncryptionStatusMessage("Failed: Invalid public key", Brushes.Red);
                }
                catch (NullReferenceException ex)
                {
                    SetEncryptionStatusMessage("Failed: Invalid file", Brushes.Red);
                }        
            }
            else
            {
                SetEncryptionStatusMessage("Failed: No valid public key found", Brushes.Red);
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
                encryptionFilePath = dialog.FileName;
                fileToDecryptTextBox.Text = encryptionFilePath;
                FileStream stream = File.OpenRead(encryptionFilePath);
                encryptionFileContents = new byte[stream.Length];
                Dictionary<string, byte[]> test = EncryptedDataHelper.ToDictionary(File.ReadAllText(encryptionFilePath));
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
                    encryptionFileContents =
                        hybrid.Decrypt(encryptedFileContents,
                            hybrid.ConvertStringToKey(senderPublicKeyToDecryptTextBox.Text))["text"];
                    SetDecryptionStatusMessage("Success", Brushes.Green);
                    resultTextBox.Text = Encoding.Default.GetString(encryptionFileContents);
                }
                catch (InvalidOperationException ex)
                {
                    SetDecryptionStatusMessage("Failed: Invalid public key", Brushes.Red);
                }
                catch (NullReferenceException ex)
                {
                    SetDecryptionStatusMessage("Failed: Invalid file", Brushes.Red);
                }
                catch (ArgumentNullException ex)
                {
                    SetDecryptionStatusMessage("Failed: Unable to decrypt the file", Brushes.Red);
                }
            }
            else
            {
                SetDecryptionStatusMessage("Failed: No valid public key found", Brushes.Red);
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

        private void SetEncodingStatusMessage(String message, SolidColorBrush color)
        {
            encodingStatusMessageLabel.Content = message;
            encodingStatusMessageLabel.Foreground = color;
        }

        private void SetDecodingStatusMessage(String message, SolidColorBrush color)
        {
            decodingStatusMessageLabel.Content = message;
            decodingStatusMessageLabel.Foreground = color;
        }

        private void saveDecryptedFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if ((bool)dialog.ShowDialog())
            {
                File.WriteAllText(dialog.FileName, Encoding.Default.GetString(encryptionFileContents));
            }
        }

        private void NewWindowMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow window2 = new MainWindow();
            window2.Show();
        }

        private void selectFileToEncodeButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if ((bool)dialog.ShowDialog())
            {
                fileToEncodePath = dialog.FileName;
                filePathToEncodeTextBox.Text = fileToEncodePath;
            }
        }

        private void saveDecodedFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if ((bool)dialog.ShowDialog())
            {
                File.WriteAllText(dialog.FileName, fileToDecodeContents);
            }
        }
    }
}
