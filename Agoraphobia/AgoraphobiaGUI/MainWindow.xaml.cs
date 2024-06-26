﻿using AgoraphobiaGUI.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using System.Threading;
using AgoraphobiaAPI.HttpClients;
using System.Net.Http;
using AgoraphobiaLibrary;

namespace AgoraphobiaGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Container.Children.Add(new LogInUC(Container, this));
        }      
        public MainWindow(Account account)
        {
            InitializeComponent();
            Container.Children.Add(new MainMenuUC(Container, account, this));
        }
    }
}
