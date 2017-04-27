﻿using System;
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
using RsaCryptoExample2;

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

            
            MessageBox.Show(hybrid.Decrypt(hybrid.Encrypt("Hallo Wereld!")));

        }
    }
}
