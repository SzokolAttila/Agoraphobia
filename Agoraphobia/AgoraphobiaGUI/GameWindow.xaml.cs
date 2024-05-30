using AgoraphobiaGUI.UserControls;
using AgoraphobiaGUI.UserControls.ItemUCs;
using AgoraphobiaLibrary;
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

namespace AgoraphobiaGUI
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        readonly Grid container;
        Player _player = new Player(1, 1);
        public List<int> Exits;
        Enemy _enemy = new Enemy("Kitten", "Just a cute kitten", 10, 1, 1, 4, 0);
        Dictionary<string, string> infoTxt = new Dictionary<string, string>()
        {
            { "Defense", "Surprisingly if you hit an enemy, they're going to fight you back. Defense reduces the damage you suffer from the hit, so you should keep that number high enough." },
            { "Attack", "The higher attack you have, the harder you hit your target. Building on that stat is fun, believe me!" },
            { "DreamCoins", "This is the official currency of the game. Merchants give you stuff for DreamCoins, it's that simple." },
            { "Door1", "asd" },//Rooms.First(x=>x.Id==Exits[0]).Select(x=>$"{x.Name}\n{x.Description}") },
            { "Door2", "asd" },//Rooms.First(x=>x.Id==Exits[1]).Select(x=>$"{x.Name}\n{x.Description}") },
            { "Door3", "asd" },//Rooms.First(x=>x.Id==Exits[2]).Select(x=>$"{x.Name}\n{x.Description}") },
            { "Merchant", "asd" },//$"{player.CurrentRoom.Merchant.Name}\n{player.CurrentRoom.Merchant.Description}"  },
            { "Enemy", "asd" },//$"{player.CurrentRoom.Enemy.Name}\n{player.CurrentRoom.Enemy.Description}"  },
            { "Loot", "Some tasty loot!"  }
        };

        public GameWindow() //Player player as param
        {
            InitializeComponent();
            DataContext = new
            {
                player = _player,
                enemy = _enemy
            };
            //_player = player;
            //_enemy = player.Room.Enemy;
            PlayIntro();

            //Just for testing
            _player.WeaponInventories.Add(new WeaponInventory() { 
                Weapon = new Weapon("stick", "just a stick", 0, 1, 0.8, 1.8, 1), Quantity=1});
        }
        public void Back(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
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

        public void Save(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Your progress have been saved!");
        }

        public void SettingsWindow(object sender, RoutedEventArgs e)
        {
            SettingsUC settingsWin = new SettingsUC(Main, Colors.Black, Colors.White);
            Main.Children.Add(settingsWin);
            settingsWin.HorizontalAlignment = HorizontalAlignment.Stretch;
            settingsWin.VerticalAlignment = VerticalAlignment.Stretch;
            settingsWin.MinHeight = 350;
        }

        public void EffectsWindow(object sender, RoutedEventArgs e)
        {

        }

        public void InventoryWindow(object sender, RoutedEventArgs e)
        {

        }

        public void TradeWindow(object sender, MouseButtonEventArgs e)
        {

        }

        public void FightWindow(object sender, MouseButtonEventArgs e)
        {
            List<UserControl> weapons = new List<UserControl>();
            foreach (var weapon in _player.WeaponInventories)
            {
                weapons.Add(new WeaponUC(weapon.Weapon, ref _player, ref _enemy));
            }

            ItemListUC items = new ItemListUC(weapons, new List<string>() { "Name", "Min Atk", "Max Atk", "Energy"}, Main);
            Main.Children.Add(items);
            items.UpdateLayout();

            PlaceUCToMouse(items);
        }

        public void LootWindow(object sender, MouseButtonEventArgs e)
        {

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

        public void PlaceUCToMouse(UserControl uc)
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
