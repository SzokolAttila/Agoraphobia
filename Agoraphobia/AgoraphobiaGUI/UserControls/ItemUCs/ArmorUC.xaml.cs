﻿using AgoraphobiaAPI.Repositories;
using AgoraphobiaLibrary;
using AgoraphobiaLibrary.Exceptions.Armor;
using AgoraphobiaLibrary.JoinTables.Armors;
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
using AgoraphobiaAPI.HttpClients;
using static AgoraphobiaGUI.UserControls.ItemListUC;

namespace AgoraphobiaGUI.UserControls.ItemUCs
{
    /// <summary>
    /// Interaction logic for ArmorUC.xaml
    /// </summary>
    public partial class ArmorUC : UserControl
    {
        Player _player;
        Armor _armor;
        int _qty;
        public ArmorUC(Armor armor, ref Player player, ListType type, int qty)
        {
            InitializeComponent();
            Name.Text = armor.Name;
            Hp.Text = armor.Hp.ToString();
            Defense.Text = armor.Defense.ToString();
            Type.Text = armor.ArmorType.ToString();
            Price.Text = armor.Price.ToString();
            _armor = armor;
            _player = player;
            _qty = qty;
            switch (type)
            {
                case ListType.Loot:
                    MouseLeftButtonDown += PickupArmor;
                    HaveQty();
                    break;
                case ListType.Inventory:
                    MouseRightButtonDown += DropArmor;
                    HaveQty();
                    break;
                case ListType.Merchant:
                    MouseLeftButtonDown += BuyArmor;
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

        public async void BuyArmor(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (_player.Room!.Merchant!.BuyArmor(_armor, _player))
                {
                    Qty.Text = (int.Parse(Qty.Text) - 1).ToString();
                }
                else
                {
                    Visibility = Visibility.Collapsed;
                }
                await ArmorSaleStatusHttpClient
                    .RemoveItem(_player.Id, _armor.Id, _player.RoomId, _player.Room!.MerchantId);
                await ArmorInventoryHttpClient.AddItem(_player.Id, _armor.Id);
                await PlayerHttpClient.Save(_player);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void PickupArmor(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var picked = new ArmorInventory
                {
                    ArmorId = _armor.Id,
                    Armor = _armor,
                    PlayerId = _player.Id,
                    Quantity = 1
                };
                _player += picked;

                await ArmorLootStatusHttpClient.RemoveItem(_player.Id, _armor.Id, _player.RoomId);
                await ArmorInventoryHttpClient.AddItem(_player.Id, _armor.Id);
                await PlayerHttpClient.Save(_player);
                if (_qty > 1)
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

        public async void DropArmor(object sender, MouseButtonEventArgs e)
        {
            try
            {
                await ArmorInventoryHttpClient.RemoveItem(_player.Id, _armor.Id);
                await ArmorLootStatusHttpClient.AddItem(_player.Id, _armor.Id, _player.RoomId);
                if (_player.DropArmor(_armor))
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
    }
}
