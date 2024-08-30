using AgoraphobiaLibrary;
using AgoraphobiaLibrary.JoinTables.Armors;
using AgoraphobiaLibrary.JoinTables.Weapons;
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
using AgoraphobiaAPI.HttpClients;
using static AgoraphobiaGUI.UserControls.ItemListUC;

namespace AgoraphobiaGUI.UserControls.ItemUCs
{
    /// <summary>
    /// Interaction logic for WeaponUC.xaml
    /// </summary>
    public partial class WeaponUC : UserControl
    {
        Player _player;
        Weapon _weapon;
        Enemy _enemy;
        int _qty;
        public WeaponUC(Weapon weapon, ref Player player, ref Enemy enemy, ListType type, int qty)
        {
            InitializeComponent();
            Name.Text = weapon.Name;
            Min.Text = (weapon.MinMultiplier*player.Attack).ToString("#.##");
            Max.Text = (weapon.MaxMultiplier*player.Attack).ToString("#.##");
            Energy.Text = weapon.Energy.ToString();
            Price.Text = weapon.Price.ToString();
            _weapon = weapon;
            _player = player;
            _enemy = enemy;
            _qty = qty;
            switch (type)
            {
                case ListType.Enemy:
                    MouseLeftButtonDown += UseWeapon;
                    break;
                case ListType.Loot:
                    MouseLeftButtonDown += PickupWeapon;
                    HaveQty();
                    break;
                case ListType.Inventory:
                    MouseRightButtonDown += DropWeapon;
                    HaveQty();
                    break;
                case ListType.Merchant:
                    MouseLeftButtonDown += BuyWeapon;
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

        public async void UseWeapon(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!_player.AttackEnemy(_enemy, _weapon))
                {
                    await EffectHttpClient.DecreaseDuration(_player.Id);
                }

                if (_enemy.Hp <= 0)
                {
                    await WeaponDroprateHttpClient.DropWeapons(_enemy.Id, _player.Id, _player.RoomId);
                    await ConsumableDroprateHttpClient.DropConsumables(_enemy.Id, _player.Id, _player.RoomId);
                    await ArmorDroprateHttpClient.DropArmors(_enemy.Id, _player.Id, _player.RoomId);
                }
                await PlayerHttpClient.Save(_player);
                await RoomEnemyStatusHttpClient.UpdateEnemyHealth(_player.Id, _player.RoomId, _enemy.Hp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void BuyWeapon(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (_player.Room.Merchant.BuyWeapon(_weapon, _player))
                {
                    Qty.Text = (int.Parse(Qty.Text) - 1).ToString();
                }
                else
                {
                    Visibility = Visibility.Collapsed;
                }
                await WeaponSaleStatusHttpClient
                    .RemoveItem(_player.Id, _weapon.Id, _player.RoomId, _player.Room!.MerchantId);
                await WeaponInventoryHttpClient.AddItem(_player.Id, _weapon.Id);
                await PlayerHttpClient.Save(_player);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void PickupWeapon(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var picked = new WeaponInventory()
                {
                    WeaponId = _weapon.Id,
                    PlayerId = _player.Id,
                    Quantity = 1,
                    Weapon = _weapon
                };
                _player += picked;
                await RoomWeaponLootStatusHttpClient.RemoveItem(_player.Id, _weapon.Id, _player.RoomId);
                await WeaponInventoryHttpClient.AddItem(_player.Id, _weapon.Id);

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

        public async void DropWeapon(object sender, MouseButtonEventArgs e)
        {
            try
            {
                await WeaponInventoryHttpClient.RemoveItem(_player.Id, _weapon.Id);
                await RoomWeaponLootStatusHttpClient.AddItem(_player.Id, _weapon.Id, _player.RoomId);
                if (_player.DropWeapon(_weapon))
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
