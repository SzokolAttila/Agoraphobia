using AgoraphobiaAPI.Repositories;
using AgoraphobiaLibrary;
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
        public ArmorUC(Armor armor, ref Player player, ListType type)
        {
            InitializeComponent();
            Name.Text = armor.Name;
            Hp.Text = armor.Hp.ToString();
            Defense.Text = armor.Defense.ToString();
            Type.Text = armor.ArmorType.ToString();
            _armor = armor;
            _player = player;
            switch (type)
            {
                case ListType.Loot:
                    MouseLeftButtonDown += PickupArmor;
                    break;
                case ListType.Inventory:
                    MouseLeftButtonDown += UseArmor;
                    MouseRightButtonDown += DropArmor;
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

        public void UseArmor(object sender, MouseButtonEventArgs e)
        {
        }

        public void PickupArmor(object sender, MouseButtonEventArgs e)
        {
        }

        public void DropArmor(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
