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
    /// Interaction logic for ItemListUC.xaml
    /// </summary>
    public partial class ItemListUC : UserControl
    {
        public enum ListType
        {
            Inventory,
            Loot,
            Enemy, //Only for weapons
            Merchant
        }
        public ItemListUC(List<UserControl> items, List<string> headers)
        {
            InitializeComponent();

            for (int i = 0; i<headers.Count; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(1, GridUnitType.Star);
                TextBlock header = new TextBlock();
                Header.ColumnDefinitions.Add(cd);
                header.Text = headers[i];
                header.Margin = new Thickness(5);
                Grid.SetColumn(header, i);
                Header.Children.Add(header);
            }
            foreach (var item in items)
            {
                Items.Children.Add(item);
            }
        }
    }
}
