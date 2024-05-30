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
    /// Interaction logic for ItemNestedListUC.xaml
    /// </summary>
    public partial class ItemNestedListUC : UserControl
    {
        Canvas _parent;
        public ItemNestedListUC(List<ItemListUC> itemLists, Canvas parent)
        {
            InitializeComponent();
            _parent = parent;
            foreach (var itemList in itemLists)
            {
                NestedLists.Children.Add(itemList);
            }
        }

        public void CloseList(object sender, MouseButtonEventArgs e)
        {
            _parent.Children.Remove(this);
        }
    }
}
