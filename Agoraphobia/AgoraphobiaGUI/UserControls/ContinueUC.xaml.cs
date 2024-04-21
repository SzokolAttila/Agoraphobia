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

namespace AgoraphobiaGUI.UserControls
{
    /// <summary>
    /// Interaction logic for ContinueUC.xaml
    /// </summary>
    public partial class ContinueUC : UserControl
    {
        Grid container;
        public ContinueUC(Grid container)
        {
            InitializeComponent();
            this.container = container;
        }

        public void SlotSelect(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{Grid.GetRow((UIElement)sender)}. slot selected");
        }

        public void Back(object sender, RoutedEventArgs e)
        {
            container.Children.Add(new MainMenuUC(container));
            container.Children.Remove(this);
        }
    }
}
