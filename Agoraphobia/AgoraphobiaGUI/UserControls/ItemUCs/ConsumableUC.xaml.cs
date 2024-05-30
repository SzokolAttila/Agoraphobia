using AgoraphobiaLibrary;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static AgoraphobiaGUI.UserControls.ItemListUC;

namespace AgoraphobiaGUI.UserControls.ItemUCs
{
    /// <summary>
    /// Interaction logic for ConsumableUC.xaml
    /// </summary>
    public partial class ConsumableUC : UserControl
    {
        Consumable _consumable;
        Player _player;
        public ConsumableUC(Consumable consumable, ref Player player, ListType type)
        {
            InitializeComponent();
            Name.Text = consumable.Name;
            Energy.Text = consumable.Energy.ToString();
            Hp.Text = consumable.Hp.ToString();
            Defense.Text = consumable.Defense.ToString();
            Attack.Text = consumable.Attack.ToString();
            Sanity.Text = consumable.Sanity.ToString();
            Duration.Text = consumable.Duration.ToString();
            _player = player;
            _consumable = consumable;
            switch (type)
            {
                case ListType.Loot:
                    MouseLeftButtonDown += PickupConsumable;
                    break;
                case ListType.Inventory:
                    MouseLeftButtonDown += UseConsumable;
                    MouseRightButtonDown += DropConsumable;
                    break;
            }
        }


        public void HoverStart(object sender, MouseEventArgs e)
        {
            Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        }

        public void HoverEnd(object sender, MouseEventArgs e)
        {
            Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

        public void UseConsumable(object sender, MouseButtonEventArgs e)
        {
            
        }

        public void PickupConsumable(object sender, MouseButtonEventArgs e)
        {

        }

        public void DropConsumable(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
