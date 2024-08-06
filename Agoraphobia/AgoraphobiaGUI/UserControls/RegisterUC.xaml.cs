using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
using AgoraphobiaAPI.Dtos.Account;
using AgoraphobiaAPI.HttpClients;
using AgoraphobiaLibrary;
using Newtonsoft.Json;

namespace AgoraphobiaGUI.UserControls
{
    /// <summary>
    /// Interaction logic for RegisterUC.xaml
    /// </summary>
    public partial class RegisterUC : UserControl
    {
        private readonly Dictionary<int, UIElement> _levels = new();
        private readonly Brush _red = new SolidColorBrush(Colors.Red);
        private readonly Brush _green = new SolidColorBrush(Colors.Green);
        private readonly Grid _container;
        private MainWindow _window;
        public RegisterUC(Grid container, MainWindow window)
        {
            InitializeComponent();
            _container = container;
            _window = window;
            _levels.Add(16, Long);
            _levels.Add(8, Special);
            _levels.Add(4, Digit);
            _levels.Add(2, Upper);
            _levels.Add(1, Lower);
        }

        private void LoginWindow(object sender, RoutedEventArgs e)
        {
            _container.Children.Add(new LogInUC(_container, _window));
            _container.Children.Remove(this);
        }
        private async void Register(object sender, RoutedEventArgs e)
        {
            try
            {
                await AccountHttpClient.Register(Username.Text, PasswordBox.Password, false);
                MessageBox.Show("Account successfully created, time to log in!", "Account created", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Account could not be created", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _container.Children.Add(new LogInUC(_container, _window));
            _container.Children.Remove(this);
        }
        private void StrengthLevel(object sender, RoutedEventArgs e)
        {
            Lower.Foreground = _red;
            Upper.Foreground = _red;
            Digit.Foreground = _red;
            Special.Foreground = _red;
            Long.Foreground = _red;
            var strengthLevel = Password.CheckSecurityLevel(PasswordBox.Password);

            foreach (var level in _levels)
            {
                if (strengthLevel >= level.Key)
                {
                    ((TextBlock)level.Value).Foreground = _green;
                    strengthLevel -= level.Key;
                }
            }
        }
    }
}
