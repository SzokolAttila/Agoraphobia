using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ContinueUC.xaml
    /// </summary>
    public partial class ContinueUC : UserControl
    {
        readonly Grid container;
        private readonly Account _account;
        private MainWindow _window;
        public ContinueUC(Grid container, Account account, MainWindow window)
        {
            InitializeComponent();
            this.container = container;
            _account = account;
            _window = window;
        }

        public void SlotSelect(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{Grid.GetRow((UIElement)sender)}. slot selected");
        }

        public void Back(object sender, RoutedEventArgs e)
        {
            container.Children.Add(new MainMenuUC(container, _account, _window));
            container.Children.Remove(this);
        }
    }
}
