﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
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
using AgoraphobiaAPI.HttpClients;
using AgoraphobiaLibrary;

namespace AgoraphobiaGUI.UserControls
{
    
    public partial class NewGameUC : UserControl
    {
        private readonly Grid _container;
        private readonly Account _account;
        private readonly MainWindow _window;
        public NewGameUC(Grid container, Account account, MainWindow window)
        {
            InitializeComponent();
            this._container = container;
            _account = account;
            _window = window;
        }

        public async void SlotSelect(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show($"Do you really want to overwrite this slot?",
                "New Game", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    var slotId = Convert.ToInt32(char.GetNumericValue((sender as Button)!.Content.ToString()![0]));
                    var player = await PlayerHttpClient.AddNewPlayer(_account.Id, slotId);
                    var gameWindow = new GameWindow(_account, player, _window);
                    _container.Children.Remove(this);
                    _window.Close();
                    gameWindow.Show();
                    gameWindow.PlayIntro();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void Back(object sender, RoutedEventArgs e)
        {
            _container.Children.Add(new MainMenuUC(_container, _account, _window));
            _container.Children.Remove(this);
        }
    }
}
