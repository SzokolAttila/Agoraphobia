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

        public void UseWeapon(object sender, MouseButtonEventArgs e)
        {
            _player.AttackEnemy(_enemy, _weapon);
        }

        public void BuyWeapon(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int _idx = _player.Room.Merchant.WeaponSales.FindIndex(x => x.Weapon.Id == _weapon.Id);
                _player.DreamCoins -= _weapon.Price;

                WeaponInventory bought = new WeaponInventory();
                bought.WeaponId = _weapon.Id;
                bought.Weapon = _player.Room.Merchant.BuyWeapon(_idx);
                bought.PlayerId = _player.Id;
                bought.Player = _player;
                bought.Quantity = 1;
                _player += bought;

                if (_player.Room.Merchant.WeaponSales.Select(x => x.Weapon.Id).Contains(_weapon.Id))
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

        public async void PickupWeapon(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int _idx = _player.Room.Weapons.FindIndex(x => x.Weapon.Id == _weapon.Id);

                WeaponInventory picked = new WeaponInventory();
                picked.WeaponId = _weapon.Id;
                picked.Weapon = _player.Room.PickupWeapon(_idx);
                picked.PlayerId = _player.Id;
                picked.Player = _player;
                picked.Quantity = 1;
                _player += picked;
                await WeaponInventoryHttpClient.AddItem(_player.Id, _weapon.Id);

                if (_player.Room.Weapons.Select(x => x.Weapon.Id).Contains(_weapon.Id))
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

        public async void DropWeapon(object sender, MouseButtonEventArgs e)
        {
            await WeaponInventoryHttpClient.RemoveItem(_player.Id, _weapon.Id);
            if (_player.DropWeapon(_weapon))
            {
                Qty.Text = (int.Parse(Qty.Text) - 1).ToString();
            }
            else
            {
                Visibility = Visibility.Collapsed;
            }
        }
    }
}
