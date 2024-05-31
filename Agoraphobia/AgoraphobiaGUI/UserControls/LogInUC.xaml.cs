using System;
using System.Collections.Generic;
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

namespace AgoraphobiaGUI.UserControls
{
    /// <summary>
    /// Interaction logic for LogInUC.xaml
    /// </summary>
    public partial class LogInUC : UserControl
    {
        private readonly Grid _container;
        private MainWindow _window;
        public LogInUC(Grid container, MainWindow window)
        {
            InitializeComponent();
            _container = container;
            _window = window;
        }
        private async void LogIn(object sender, RoutedEventArgs e)
        {
            var client = new AccountHttpClient(new HttpClient());
            try
            {
                var account = await client.LogIn(Username.Text, PasswordBox.Password, false);
                MessageBox.Show("Logging in was successful, good luck on your journey!", "Successful login", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                _container.Children.Add(new MainMenuUC(_container, account, _window));
                _container.Children.Remove(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void RegisterWindow(object sender, RoutedEventArgs e)
        {
            _container.Children.Add(new RegisterUC(_container, _window));
            _container.Children.Remove(this);
        }
    }
}
