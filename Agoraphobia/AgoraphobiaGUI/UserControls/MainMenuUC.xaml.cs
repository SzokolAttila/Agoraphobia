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

namespace AgoraphobiaGUI.UserControls
{
    /// <summary>
    /// Interaction logic for MainMenuUC.xaml
    /// </summary>
    public partial class MainMenuUC : UserControl
    {
        Grid container;
        public MainMenuUC(Grid container)
        {
            InitializeComponent();
            this.container = container;
        }

        public void ContinueWindow(object sender, RoutedEventArgs e)
        {
            container.Children.Add(new ContinueUC(container));
            container.Children.Remove(this);
        }

        public void NewGameWindow(object sender, RoutedEventArgs e)
        {
            container.Children.Add(new NewGameUC(container));
            container.Children.Remove(this);
        }

        public void SettingsWindow(object sender, RoutedEventArgs e)
        {
            container.Children.Add(new SettingsUC(container));
            container.Children.Remove(this);
        }

        public void TutorialWindow(object sender, RoutedEventArgs e)
        {
            container.Children.Add(new SettingsUC(container));
            container.Children.Remove(this);
        }

        public void CreditsWindow(object sender, RoutedEventArgs e)
        {
            container.Children.Add(new CreditsUC(container));
            container.Children.Remove(this);
        }
    }
}
