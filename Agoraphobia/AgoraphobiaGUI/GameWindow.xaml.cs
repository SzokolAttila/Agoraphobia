using AgoraphobiaGUI.UserControls;
using AgoraphobiaGUI.UserControls.ItemUCs;
using AgoraphobiaLibrary;
using AgoraphobiaLibrary.JoinTables.Armors;
using AgoraphobiaLibrary.JoinTables.Weapons;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Xaml;
using static AgoraphobiaGUI.UserControls.ItemListUC;

namespace AgoraphobiaGUI
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        readonly Grid container;
        private Player _player;
        private Account _account;
        private MainWindow _window;
        public List<int> Exits;
        Enemy _enemy;
        Dictionary<string, string> infoTxt = new Dictionary<string, string>()
        {
            { "Defense", "Surprisingly if you hit an enemy, they're going to fight you back. Defense reduces the damage you suffer from the hit, so you should keep that number high enough." },
            { "Attack", "The higher attack you have, the harder you hit your target. Building on that stat is fun, believe me!" },
            { "DreamCoins", "This is the official currency of the game. Merchants give you stuff for DreamCoins, it's that simple." },
            { "Door1", "asd" },//Rooms.First(x=>x.Id==Exits[0]).Select(x=>$"{x.Name}\n{x.Description}") },
            { "Door2", "asd" },//Rooms.First(x=>x.Id==Exits[1]).Select(x=>$"{x.Name}\n{x.Description}") },
            { "Door3", "asd" },//Rooms.First(x=>x.Id==Exits[2]).Select(x=>$"{x.Name}\n{x.Description}") },
            { "Loot", "Some tasty loot!"  }
        };

        public GameWindow(Account account, Player player, MainWindow window)
        {
            _account = account;
            _window = window;
            _player = player;
            _enemy = player.Room.Enemy;
                
            InitializeComponent();
            DataContext = new
            {
                player = _player,
                enemy = _enemy
            };
            
            infoTxt.Add("Merchant", $"{_player.Room.Merchant.Name}\n{_player.Room.Merchant.Description}");
            infoTxt.Add("Enemy", $"{_enemy.Name}\n{_player.Room.Enemy.Description}");


            //For starter
            _player.WeaponInventories.Add(new WeaponInventory() { 
                Weapon = new Weapon("fist", "sometimes comes handy", 0, 0, 0.5, 1.5, 0), Quantity=1});
        }
        public void Back(object sender, RoutedEventArgs e)
        {
            new MainWindow(_account).Show();
            Close();
        }

        public void ShowInfo(object sender, RoutedEventArgs e)
        {
            System.Windows.Point senderPos = Mouse.GetPosition(Main);
            TutorialBox tb = new TutorialBox(infoTxt[((FrameworkElement)sender).Name]);
            Main.Children.Add(tb);
            tb.UpdateLayout();
            PlaceUCToMouse(tb);
        }

        public void HideInfo(object sender, RoutedEventArgs e)
        {
            Main.Children.Remove(Main.Children.OfType<TutorialBox>().First());
        }

        public void SettingsWindow(object sender, RoutedEventArgs e)
        {
            SettingsUC settingsWin = new SettingsUC(Main, _account, _window,Colors.Black, Colors.White);
            Main.Children.Add(settingsWin);
            settingsWin.HorizontalAlignment = HorizontalAlignment.Stretch;
            settingsWin.VerticalAlignment = VerticalAlignment.Stretch;
            settingsWin.MinHeight = 350;
        }

        public void EffectsWindow(object sender, RoutedEventArgs e)
        {
            Main.Children.Remove(Main.Children.OfType<ItemNestedListUC>().FirstOrDefault());
            List<UserControl> effects = new List<UserControl>();
            foreach (var effect in _player.Effects)
            {
                effects.Add(new EffectUC(effect));
            }

            ItemListUC items = new ItemListUC(effects, new List<string>() { "Name", "Energy", "Hp", "Defense", "Attack", "Sanity", "Duration" });
            ItemNestedListUC nested = new ItemNestedListUC(new List<ItemListUC>() { items }, Main);
            Main.Children.Add(nested);
            nested.UpdateLayout();

            PlaceUCToMouse(nested);
        }

        public void InventoryWindow(object sender, RoutedEventArgs e)
        {
            Main.Children.Remove(Main.Children.OfType<ItemNestedListUC>().FirstOrDefault());
            List<UserControl> weapons = new List<UserControl>();
            foreach (var weapon in _player.WeaponInventories)
            {
                weapons.Add(new WeaponUC(weapon.Weapon, ref _player, ref _enemy, ListType.Inventory));
            }
            ItemListUC weaponList = new ItemListUC(weapons, new List<string>() { "Name", "Min Atk", "Max Atk", "Energy" });

            List<UserControl> armors = new List<UserControl>();
            foreach (var armor in _player.ArmorInventories)
            {
                armors.Add(new ArmorUC(armor.Armor, ref _player, ListType.Inventory));
            }
            ItemListUC armorList = new ItemListUC(armors, new List<string>() { "Name", "Hp", "Defense", "Type" });

            List<UserControl> consumables = new List<UserControl>();
            foreach (var consumable in _player.ConsumableInventories)
            {
                consumables.Add(new ConsumableUC(consumable.Consumable, ref _player, ListType.Inventory));
            }
            ItemListUC consumableList = new ItemListUC(consumables, new List<string>() { "Name", "Energy", "Hp", "Defense", "Atk", "Sanity", "Duration" });

            ItemNestedListUC nested = new ItemNestedListUC(new List<ItemListUC>() { weaponList, armorList, consumableList}, Main);

            Main.Children.Add(nested);
            nested.UpdateLayout();

            PlaceUCToMouse(nested);
        }

        public void TradeWindow(object sender, MouseButtonEventArgs e)
        {
            Main.Children.Remove(Main.Children.OfType<ItemNestedListUC>().FirstOrDefault());

        }

        public void FightWindow(object sender, MouseButtonEventArgs e)
        {
            Main.Children.Remove(Main.Children.OfType<ItemNestedListUC>().FirstOrDefault());
            List<UserControl> weapons = new List<UserControl>();
            foreach (var weapon in _player.WeaponInventories)
            {
                weapons.Add(new WeaponUC(weapon.Weapon, ref _player, ref _enemy, ListType.Enemy));
            }

            ItemListUC items = new ItemListUC(weapons, new List<string>() { "Name", "Min Atk", "Max Atk", "Energy"});
            ItemNestedListUC nested = new ItemNestedListUC(new List<ItemListUC>(){ items}, Main);
            
            Main.Children.Add(nested);
            nested.UpdateLayout();

            PlaceUCToMouse(nested);
        }

        public void LootWindow(object sender, MouseButtonEventArgs e)
        {
            Main.Children.Remove(Main.Children.OfType<ItemNestedListUC>().FirstOrDefault());
            List<UserControl> weapons = new List<UserControl>();
            foreach (var weapon in _player.Room.Weapons)
            {
                weapons.Add(new WeaponUC(weapon.Weapon, ref _player, ref _enemy, ListType.Loot));
            }
            ItemListUC weaponList = new ItemListUC(weapons, new List<string>() { "Name", "Min Atk", "Max Atk", "Energy" });

            List<UserControl> armors = new List<UserControl>();
            foreach (var armor in _player.Room.Armors)
            {
                armors.Add(new ArmorUC(armor.Armor, ref _player, ListType.Loot));
            }
            ItemListUC armorList = new ItemListUC(armors, new List<string>() { "Name", "Hp", "Defense", "Type" });

            List<UserControl> consumables = new List<UserControl>();
            foreach (var consumable in _player.Room.Consumables)
            {
                consumables.Add(new ConsumableUC(consumable.Consumable, ref _player, ListType.Loot));
            }
            ItemListUC consumableList = new ItemListUC(consumables, new List<string>() { "Name", "Energy", "Hp", "Defense", "Atk", "Sanity", "Duration" });

            ItemNestedListUC nested = new ItemNestedListUC(new List<ItemListUC>() { weaponList, armorList, consumableList }, Main);

            Main.Children.Add(nested);
            nested.UpdateLayout();

            PlaceUCToMouse(nested);
        }

        public void CheckEnemyAlive(object sender, RoutedEventArgs e)
        {
            if (_enemy.Hp <= 0)
            {
                Enemy.Visibility = Visibility.Hidden;
                ItemNestedListUC itemList = Main.Children.OfType<ItemNestedListUC>().First();
                Main.Children.Remove(itemList);
            }
        }

        //Intro
        int introWait; // in millisec
        CancellationTokenSource skipIntro = new();
        List<string> intro = new List<string>()
        {
            "John Merta, a writer known all around the globe, is currently suffering from writer's block. ",
            "This leaves him panicking, for he is in great need of a new book, due to his nearness to bankruptcy.",
            "After a long day full of effort, he defeatedly lays is head onto his pillow whilst his notebook lays empty atop his desk, with dozens of crumpled pages scattered on the floor.",
            "However, no matter how hopeless he might feel, for a last ray of hope seems to shine upon him.",
            "After finally falling asleep, he finds himself in a queer dream full of bizarre creatures and uncanny but exhilarating adventures... and, most importantly, numerous things to write about.",
            "As his mind starts producing countless ideas, each better than the last, his wretchedness starts to fade away, replaced by a welcomed feeling of triumph.",
            "For it is this strange dream that might help him prevail and come up with an idea worthy of his name... he needs only survive."
        };


        public async void PlayIntro()
        {
            IntroBox.Visibility = Visibility.Visible;
            Main.Visibility = Visibility.Hidden;
            foreach (string row in intro)
            {
                introWait = 50;
                IntroText.Text = "";
                foreach (char letter in row)
                {
                    IntroText.Text += letter;
                    try
                    {
                        await Task.Delay(introWait, skipIntro.Token);
                    }catch 
                    {
                        IntroText.Text = row;
                        break;
                    }
                }
                skipIntro.Dispose();
                skipIntro = new();

                introWait = 10000;
                try
                {
                    await Task.Delay(introWait, skipIntro.Token);
                }
                catch
                {
                    skipIntro.Dispose();
                    skipIntro = new();
                }
            }
            IntroBox.Visibility = Visibility.Hidden;
            Main.Visibility = Visibility.Visible;
        }

        public void IntroClicked(object sender, MouseButtonEventArgs e)
        {
            skipIntro.Cancel();
        }

        public void PlaceUCToMouse(FrameworkElement uc)
        {
            System.Windows.Point senderPos = Mouse.GetPosition(Main);
            if (uc.ActualWidth + senderPos.X < Main.ActualWidth)
            {
                Canvas.SetLeft(uc, senderPos.X);
            }
            else
            {
                Canvas.SetLeft(uc, senderPos.X - uc.ActualWidth);
            }

            if (uc.ActualHeight + senderPos.Y < Main.ActualHeight)
            {
                Canvas.SetTop(uc, senderPos.Y);
            }
            else
            {
                Canvas.SetTop(uc, senderPos.Y - uc.ActualHeight);
            }
        }
    }
}
