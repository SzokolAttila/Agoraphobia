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
    
    public partial class NewGameUC : UserControl
    {
        readonly Grid container;
        public NewGameUC(Grid container)
        {
            InitializeComponent();
            this.container = container;
        }

        public void SlotSelect(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show($"Do you really want to overwrite this slot?",
                "New Game", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MessageBox.Show($"New game to {Grid.GetRow((UIElement)sender)}. slot");
            }
        }

        public void Back(object sender, RoutedEventArgs e)
        {
            container.Children.Add(new MainMenuUC(container));
            container.Children.Remove(this);
        }
    }
}
