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
    /// Interaction logic for LogInUC.xaml
    /// </summary>
    public partial class LogInUC : UserControl
    {
        private readonly Grid _container;
        public LogInUC(Grid container)
        {
            InitializeComponent();
            _container = container;
        }
        private void LogIn(object sender, RoutedEventArgs e)
        {
            _container.Children.Add(new MainMenuUC(_container));
            _container.Children.Remove(this);
        }
        private void RegisterWindow(object sender, RoutedEventArgs e)
        {
            _container.Children.Add(new RegisterUC(_container));
            _container.Children.Remove(this);
        }
    }
}
