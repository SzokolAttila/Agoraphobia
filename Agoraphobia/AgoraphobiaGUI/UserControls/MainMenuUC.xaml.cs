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
using AgoraphobiaLibrary;

namespace AgoraphobiaGUI.UserControls
{
    /// <summary>
    /// Interaction logic for MainMenuUC.xaml
    /// </summary>
    public partial class MainMenuUC : UserControl
    {
        readonly Grid container;
        private readonly Account _account;
        private MainWindow _window;
        public MainMenuUC(Grid container, Account account, MainWindow window)
        {
            InitializeComponent();
            this.container = container;
            _account = account;
            _window = window;
        }

        public void ContinueWindow(object sender, RoutedEventArgs e)
        {
            container.Children.Add(new ContinueUC(container, _account, _window));
            container.Children.Remove(this);
        }

        public void NewGameWindow(object sender, RoutedEventArgs e)
        {
            container.Children.Add(new NewGameUC(container, _account, _window));
            container.Children.Remove(this);
        }

        public void SettingsWindow(object sender, RoutedEventArgs e)
        {
            container.Children.Add(new SettingsUC(container, _account, _window));
            container.Children.Remove(this);
        }

        public void TutorialWindow(object sender, RoutedEventArgs e)
        {
            container.Children.Add(new TutorialUC(container, _account, _window));
            container.Children.Remove(this);
        }

        public void CreditsWindow(object sender, RoutedEventArgs e)
        {
            container.Children.Add(new CreditsUC(container, _account, _window));
            container.Children.Remove(this);
        }

        public void Quit(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you really want to exit the game?", "Quit Confirmation", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
