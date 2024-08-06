using System;
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
    /// <summary>
    /// Interaction logic for ContinueUC.xaml
    /// </summary>
    public partial class ContinueUC : UserControl
    {
        readonly Grid _container;
        private readonly Account _account;
        private MainWindow _window;
        public ContinueUC(Grid container, Account account, MainWindow window)
        {
            InitializeComponent();
            _container = container;
            _account = account;
            _window = window;
        }

        public async void SlotSelect(object sender, RoutedEventArgs e)
        {
            try
            {
                var slotId = Convert.ToInt32(char.GetNumericValue((sender as Button)!.Content.ToString()![0]));
                var player = await PlayerHttpClient.LoadPlayer(_account.Id, slotId);
                var gameWindow = new GameWindow(_account, player, _window);
                _container.Children.Remove(this);
                _window.Close();
                gameWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Back(object sender, RoutedEventArgs e)
        {
            _container.Children.Add(new MainMenuUC(_container, _account, _window));
            _container.Children.Remove(this);
        }
    }
}
