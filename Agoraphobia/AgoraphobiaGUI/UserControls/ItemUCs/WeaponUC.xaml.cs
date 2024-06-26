﻿using AgoraphobiaLibrary;
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
using static AgoraphobiaGUI.UserControls.ItemListUC;

namespace AgoraphobiaGUI.UserControls.ItemUCs
{
    /// <summary>
    /// Interaction logic for WeaponUC.xaml
    /// </summary>
    public partial class WeaponUC : UserControl
    {
        Player player;
        Weapon weapon;
        Enemy enemy;
        public WeaponUC(Weapon weapon, ref Player player, ref Enemy enemy, ListType type)
        {
            InitializeComponent();
            Name.Text = weapon.Name;
            Min.Text = (weapon.MinMultiplier*player.Attack).ToString();
            Max.Text = (weapon.MaxMultiplier*player.Attack).ToString();
            Energy.Text = weapon.Energy.ToString();
            this.weapon = weapon;
            this.player = player;
            this.enemy = enemy;
            switch (type)
            {
                case ListType.Enemy:
                    MouseLeftButtonDown += UseWeapon;
                    break;
                case ListType.Loot:
                    MouseLeftButtonDown += PickupWeapon;
                    break;
                case ListType.Inventory:
                    MouseRightButtonDown += DropWeapon;
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

        public void UseWeapon(object sender, MouseButtonEventArgs e)
        {
            player.AttackEnemy(enemy, weapon);
        }

        public void PickupWeapon(object sender, MouseButtonEventArgs e)
        {
            
        }

        public void DropWeapon(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
