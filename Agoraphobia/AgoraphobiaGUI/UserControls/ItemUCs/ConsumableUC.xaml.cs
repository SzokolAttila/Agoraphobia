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
using AgoraphobiaAPI.HttpClients;
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

        public async void UseConsumable(object sender, MouseButtonEventArgs e)
        {
            try
            {
                await EffectHttpClient.ApplyEffect(_player.Id, _consumable.Id);
                if (_player.UseConsumable(_consumable))
                {
                    Qty.Text = (int.Parse(Qty.Text) - 1).ToString();
                }
                else
                {
                    Visibility = Visibility.Collapsed;
                }
                await PlayerHttpClient.Save(_player);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void BuyConsumable(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (_player.Room.Merchant.BuyConsumable(_consumable, _player))
                {
                    Qty.Text = (int.Parse(Qty.Text) - 1).ToString();
                }
                else
                {
                    Visibility = Visibility.Collapsed;
                }

                await ConsumableSaleStatusHttpClient
                    .RemoveItem(_player.Id, _consumable.Id, _player.RoomId, _player.Room!.MerchantId);
                await ConsumableInventoryHttpClient.AddItem(_player.Id, _consumable.Id);
                await PlayerHttpClient.Save(_player);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void PickupConsumable(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var picked = new ConsumableInventory()
                {
                    ConsumableId = _consumable.Id,
                    Consumable = _consumable,
                    PlayerId = _player.Id,
                    Quantity = 1
                };

                _player += picked;
                await ConsumableLootStatusHttpClient.RemoveItem(_player.Id, _consumable.Id, _player.RoomId);
                await ConsumableInventoryHttpClient.AddItem(_player.Id, _consumable.Id);

                if (_qty > 1)
                {
                    Qty.Text = (int.Parse(Qty.Text) - 1).ToString();
                    _qty--;
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

        public async void DropConsumable(object sender, MouseButtonEventArgs e)
        {
            try
            {
                await ConsumableInventoryHttpClient.RemoveItem(_player.Id, _consumable.Id);
                await ConsumableLootStatusHttpClient.AddItem(_player.Id, _consumable.Id, _player.RoomId);
                if (_player.DropConsumable(_consumable))
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
    }
}
