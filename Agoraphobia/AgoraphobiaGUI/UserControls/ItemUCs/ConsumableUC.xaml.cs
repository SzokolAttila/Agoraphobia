using AgoraphobiaLibrary;
using AgoraphobiaLibrary.JoinTables.Armors;
using AgoraphobiaLibrary.JoinTables.Consumables;
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
        int _qty;
        public ConsumableUC(Consumable consumable, ref Player player, ListType type, int qty)
        {
            InitializeComponent();
            Name.Text = consumable.Name;
            Energy.Text = consumable.Energy.ToString();
            Hp.Text = consumable.Hp.ToString();
            Defense.Text = consumable.Defense.ToString();
            Attack.Text = consumable.Attack.ToString();
            Sanity.Text = consumable.Sanity.ToString();
            Duration.Text = consumable.Duration.ToString();
            Price.Text = consumable.Price.ToString();
            _player = player;
            _consumable = consumable;
            _qty = qty;
            switch (type)
            {
                case ListType.Loot:
                    MouseLeftButtonDown += PickupConsumable;
                    HaveQty();
                    break;
                case ListType.Inventory:
                    MouseLeftButtonDown += UseConsumable;
                    MouseRightButtonDown += DropConsumable;
                    HaveQty();
                    break;
                case ListType.Merchant:
                    MouseLeftButtonDown += BuyConsumable;
                    HaveQty();
                    break;
            }
        }

        private void HaveQty()
        {
            Qty.Visibility = Visibility.Visible;
            Qty.Text = _qty.ToString();
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

        public void BuyConsumable(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int _idx = _player.Room.Merchant.ConsumableSales.FindIndex(x => x.Consumable.Id == _consumable.Id);
                _player.DreamCoins -= _consumable.Price;

                ConsumableInventory bought = new ConsumableInventory();
                bought.ConsumableId = _consumable.Id;
                bought.Consumable = _player.Room.Merchant.BuyConsumable(_idx);
                bought.PlayerId = _player.Id;
                bought.Player = _player;
                bought.Quantity = 1;
                _player += bought;

                if (_player.Room.Merchant.ConsumableSales.Select(x => x.Consumable.Id).Contains(_consumable.Id))
                {
                    Qty.Text = (int.Parse(Qty.Text) - 1).ToString();
                }
                else
                {
                    Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void PickupConsumable(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int _idx = _player.Room.Consumables.FindIndex(x => x.Consumable.Id == _consumable.Id);

                ConsumableInventory picked = new ConsumableInventory();
                picked.ConsumableId = _consumable.Id;
                picked.Consumable = _player.Room.PickupConsumable(_idx);
                picked.PlayerId = _player.Id;
                picked.Player = _player;
                picked.Quantity = 1;
                _player += picked;

                if (_player.Room.Consumables.Select(x => x.Consumable.Id).Contains(_consumable.Id))
                {
                    Qty.Text = (int.Parse(Qty.Text) - 1).ToString();
                }
                else
                {
                    Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DropConsumable(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
