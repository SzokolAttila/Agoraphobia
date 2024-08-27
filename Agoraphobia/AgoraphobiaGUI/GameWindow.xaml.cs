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
using System.Net.Http;
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
using AgoraphobiaAPI.HttpClients;
using static AgoraphobiaGUI.UserControls.ItemListUC;
using static AgoraphobiaLibrary.Room;

namespace AgoraphobiaGUI
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Player _player;
        private Account _account;
        private MainWindow _window;
        public List<Room> Exits;
        private Enemy _enemy;
        Dictionary<string, string> infoTxt = new Dictionary<string, string>()
        {
            { "Defense", "Surprisingly if you hit an enemy, they're going to fight you back. Defense reduces the damage you suffer from the hit, so you should keep that number high enough." },
            { "Attack", "The higher attack you have, the harder you hit your target. Building on that stat is fun, believe me!" },
            { "DreamCoins", "This is the official currency of the game. Merchants give you stuff for DreamCoins, it's that simple." },
            { "Loot", "Some tasty loot!"  },
            { "Door0", "" },
            { "Door1", "" },
            { "Door2", "" },
            { "Merchant", ""  },
            { "Enemy", ""  }
        };

        public GameWindow(Account account, Player player, MainWindow window)
        {
            InitializeComponent();
            _account = account;
            _window = window;
            _player = player;
            PlayerHttpClient.Save(_player);
            InitializeRoom();
        }

        private async void InitializeRoom()
        {
            Enemy.Visibility = Visibility.Visible;
            Exits = new List<Room>();
            _player = await PlayerHttpClient.LoadPlayer(_account.Id, _player.SlotId);
            _player.DeathOccured += PlayerDeath;
            _player.SanityOver += EndOfGame;
            _enemy = _player.Room!.Enemy!;

            DataContext = new
            {
                player = _player,
                enemy = _enemy
            };

            Exits.Add(await RandomRoomOfOrientation(RoomOrientation.Good));
            Exits.Add(await RandomRoomOfOrientation(RoomOrientation.Neutral));
            Exits.Add(await RandomRoomOfOrientation(RoomOrientation.Evil));
            Exits = RandomizeExits(Exits);

            for(int i = 0; i < Exits.Count; i++)
            {
                infoTxt[$"Door{i}"] = $"{Exits[i].Name}\n{Exits[i].Description}";
            }
            infoTxt["Merchant"] =  $"{_player.Room.Merchant.Name}\n{_player.Room.Merchant.Description}";
            infoTxt["Enemy"] = $"{_enemy.Name}\n{_player.Room.Enemy.Description}";

            LoadData();
        }

        private async Task LoadData()
        {

            var roomEnemyStatus = await RoomEnemyStatusHttpClient.GetEnemyStatus(_player.Id, _player.RoomId);
            if (roomEnemyStatus != null)
                _enemy.Hp = roomEnemyStatus.EnemyHp;
            await RoomWeaponLootStatusHttpClient.CopyWeapons(_player.Id, _player.RoomId);
            await ConsumableLootStatusHttpClient.CopyConsumables(_player.Id, _player.RoomId);
            await ArmorLootStatusHttpClient.CopyArmors(_player.Id, _player.RoomId);
            await WeaponSaleStatusHttpClient.CopyWeapons(_player.Id, _player.RoomId, _player.Room!.MerchantId);
            await ConsumableSaleStatusHttpClient.CopyConsumables(_player.Id, _player.RoomId, _player.Room!.MerchantId);
            await ArmorSaleStatusHttpClient.CopyArmors(_player.Id, _player.RoomId, _player.Room!.MerchantId);
        }

        private async Task<Room> RandomRoomOfOrientation(RoomOrientation orientation)
        {
            List<Room> rooms = await RoomHttpClient.RoomsByOrientation(orientation);
            if (_player.Room.Orientation == orientation)
            {
                rooms.Remove(rooms.Where(x=>x.Id == _player.Room.Id).First());
            }
            return rooms[Random.Shared.Next(0, rooms.Count)];
        } 

        private List<Room> RandomizeExits(List<Room> exits)
        {
            List<Room> temp = new();
            while(exits.Count>0)
            {
                Room randomRoom = exits[Random.Shared.Next(0, exits.Count)];
                temp.Add(randomRoom);
                exits.Remove(randomRoom);
            }
            return temp;
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
                weapons.Add(new WeaponUC(weapon.Weapon, ref _player, ref _enemy, ListType.Inventory, weapon.Quantity));
            }
            ItemListUC weaponList = new ItemListUC(weapons, new List<string>() { "Name", "Min Atk", "Max Atk", "Energy", "Price", "Qty" });

            List<UserControl> armors = new List<UserControl>();
            foreach (var armor in _player.ArmorInventories)
            {
                armors.Add(new ArmorUC(armor.Armor, ref _player, ListType.Inventory, armor.Quantity));
            }
            ItemListUC armorList = new ItemListUC(armors, new List<string>() { "Name", "Hp", "Defense", "Type", "Price", "Qty" });

            List<UserControl> consumables = new List<UserControl>();
            foreach (var consumable in _player.ConsumableInventories)
            {
                consumables.Add(new ConsumableUC(consumable.Consumable, ref _player, ListType.Inventory, consumable.Quantity));
            }
            ItemListUC consumableList = new ItemListUC(consumables, new List<string>() { "Name", "Energy", "Hp", "Defense", "Atk", "Sanity", "Duration", "Price", "Qty" });

            ItemNestedListUC nested = new ItemNestedListUC(new List<ItemListUC>() { weaponList, armorList, consumableList}, Main);

            Main.Children.Add(nested);
            nested.UpdateLayout();

            PlaceUCToMouse(nested);
        }

        public async void TradeWindow(object sender, MouseButtonEventArgs e)
        {
            Main.Children.Remove(Main.Children.OfType<ItemNestedListUC>().FirstOrDefault());
            List<UserControl> weapons = new List<UserControl>();
            var weaponSaleStatusList = await WeaponSaleStatusHttpClient
                .GetWeapons(_player.Id, _player.RoomId, _player.Room.MerchantId);
            foreach (var weapon in weaponSaleStatusList.Where(x => x.Quantity > 0))
            {
                weapons.Add(new WeaponUC(weapon.Weapon, ref _player, ref _enemy, ListType.Merchant, weapon.Quantity));
            }
            ItemListUC weaponList = new ItemListUC(weapons, new List<string>() { "Name", "Min Atk", "Max Atk", "Energy", "Price", "Qty" });

            List<UserControl> armors = new List<UserControl>();
            var armorSaleStatusList =
                await ArmorSaleStatusHttpClient.GetArmors(_player.Id, _player.RoomId, _player.Room!.MerchantId);
            foreach (var armor in armorSaleStatusList.Where(x => x.Quantity > 0))
            {
                armors.Add(new ArmorUC(armor.Armor, ref _player, ListType.Merchant, armor.Quantity));
            }
            ItemListUC armorList = new ItemListUC(armors, new List<string>() { "Name", "Hp", "Defense", "Type", "Price", "Qty" });

            List<UserControl> consumables = new List<UserControl>();
            var consumableSaleStatusList = await ConsumableSaleStatusHttpClient
                .GetConsumables(_player.Id, _player.RoomId, _player.Room.MerchantId);
            foreach (var consumable in consumableSaleStatusList.Where(x => x.Quantity > 0))
            {
                consumables.Add(new ConsumableUC(consumable.Consumable, ref _player, ListType.Merchant, consumable.Quantity));
            }
            ItemListUC consumableList = new ItemListUC(consumables, new List<string>() { "Name", "Energy", "Hp", "Defense", "Atk", "Sanity", "Duration", "Price", "Qty" });

            ItemNestedListUC nested = new ItemNestedListUC(new List<ItemListUC>() { weaponList, armorList, consumableList }, Main);

            Main.Children.Add(nested);
            nested.UpdateLayout();

            PlaceUCToMouse(nested);
        }

        public void FightWindow(object sender, MouseButtonEventArgs e)
        {
            Main.Children.Remove(Main.Children.OfType<ItemNestedListUC>().FirstOrDefault());
            List<UserControl> weapons = new List<UserControl>();
            foreach (var weapon in _player.WeaponInventories)
            {
                WeaponUC weaponUC = new WeaponUC(weapon.Weapon, ref _player, ref _enemy, ListType.Enemy, 0);
                weapons.Add(weaponUC);
            }

            ItemListUC items = new ItemListUC(weapons, new List<string>() { "Name", "Min Atk", "Max Atk", "Energy", "Price"});
            ItemNestedListUC nested = new ItemNestedListUC(new List<ItemListUC>(){ items}, Main);
            
            Main.Children.Add(nested);
            nested.UpdateLayout();

            PlaceUCToMouse(nested);
        }

        public async void LootWindow(object sender, MouseButtonEventArgs e)
        {
            Main.Children.Remove(Main.Children.OfType<ItemNestedListUC>().FirstOrDefault());
            List<UserControl> weapons = new List<UserControl>();
            var weaponLootStatuslList = await RoomWeaponLootStatusHttpClient.GetWeapons(_player.Id, _player.RoomId);
            foreach (var weapon in weaponLootStatuslList.Where(x => x.Quantity > 0))
            {
                weapons.Add(new WeaponUC(weapon.Weapon, ref _player, ref _enemy, ListType.Loot, weapon.Quantity));
            }
            ItemListUC weaponList = new ItemListUC(weapons, new List<string>() { "Name", "Min Atk", "Max Atk", "Energy", "Price", "Qty" });

            List<UserControl> armors = new List<UserControl>();
            var armorLootStatusList = await ArmorLootStatusHttpClient.GetArmors(_player.Id, _player.RoomId);
            foreach (var armor in armorLootStatusList.Where(x => x.Quantity > 0))
            {
                armors.Add(new ArmorUC(armor.Armor, ref _player, ListType.Loot, armor.Quantity));
            }
            ItemListUC armorList = new ItemListUC(armors, new List<string>() { "Name", "Hp", "Defense", "Type", "Price", "Qty" });

            List<UserControl> consumables = new List<UserControl>();
            var consumableLootStatusList = await ConsumableLootStatusHttpClient
                .GetConsumables(_player.Id, _player.RoomId);
            foreach (var consumable in consumableLootStatusList.Where(x => x.Quantity > 0))
            {
                consumables.Add(new ConsumableUC(consumable.Consumable, ref _player, ListType.Loot, consumable.Quantity));
            }
            ItemListUC consumableList = new ItemListUC(consumables, new List<string>() { "Name", "Energy", "Hp", "Defense", "Atk", "Sanity", "Duration", "Price", "Qty" });

            ItemNestedListUC nested = new ItemNestedListUC(new List<ItemListUC>() { weaponList, armorList, consumableList }, Main);

            Main.Children.Add(nested);
            nested.UpdateLayout();

            PlaceUCToMouse(nested);
        }

        public void PlayerDeath(object sender, EventArgs e)
        {
            _player.Death();
            ConsumableInventoryHttpClient.RemoveAllEffects(_player.Id);
            PlayerHttpClient.Save(_player);
            RoomEnemyStatusHttpClient.UpdateEnemyHealth(_player.Id, _player.RoomId, _enemy.Hp);
            InitializeRoom();

            PlayCutscene(new List<string>() { "Unfortunately you have deceased, but don't you worry." +
                " As you're in a dream you can't die permanently." +
                " However, don't do this too frequently because you recieve penalties and you'll go insane eventually." });
        }

        public async void OpenADoor(object sender, MouseEventArgs e)
        {
            if (_enemy.Hp <= 0)
            {
                if (MessageBox.Show("Do you really want to leave this place behind and go to the next room?", "Leave room",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int idx = int.Parse(((FrameworkElement)sender).Name.Replace("Door", ""));
                    _player.RoomId = Exits[idx].Id;
                    await PlayerHttpClient.Save(_player);
                    InitializeRoom();

                    await PlayCutscene(new List<string>() { "As you open the door, you sense chaos emerging from the room." });
                }
            }
            else
            {
                MessageBox.Show("The enemy is standing in your way, so you cannot proceed to the next room" +
                    " until you end its life.", "Enemy's still alive", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void EndOfGame(object sender, EventArgs e)
        {
            List<string> endText;
            if (_player.Sanity >= 100)
            {
                endText = new List<string>() {"This journey of yours is something to remember as it showed you the way of getting over different obstacles and mainly your insanity.",
                "You had some ups and downs along the way, but eventually you woke up as a sane man with new motivations towards life.",
                "Probably that's all what really matters.", $"As you check the date and time you see that you spent {"<time>"} unconscious.",
                $"With your wise actions and self-control you've finished this game with {"<points>"} points. Well played!"};
            }
            else
            {
                endText = new List<string>() {"You've fought hard with your own sub-conscious, but unfortunately you lost this battle.",
                "However, you can still win the war, so chin up and try it again!"};
            }

            await PlayCutscene(endText);
            //PlayerHttpClient.DeletePlayer(_player);
            Back(this, new RoutedEventArgs());
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

        //Cutscene
        int cutsceneWait; // in millisec
        CancellationTokenSource skipText = new();

        public async Task PlayCutscene(List<string> texts)
        {
            await Task.Delay(10); //Give a bit of delay, so SkipText token isnt cancelled with the click on enemy
            skipText.Dispose();
            skipText = new();
            CutsceneBox.Visibility = Visibility.Visible;
            Main.Visibility = Visibility.Hidden;
            foreach (string row in texts)
            {
                cutsceneWait = 50;
                CutsceneText.Text = "";
                foreach (char letter in row)
                {
                    CutsceneText.Text += letter;
                    try
                    {
                        await Task.Delay(cutsceneWait, skipText.Token);
                    }catch 
                    {
                        CutsceneText.Text = row;
                        break;
                    }
                }
                skipText.Dispose();
                skipText = new();

                cutsceneWait = 10000;
                try
                {
                    await Task.Delay(cutsceneWait, skipText.Token);
                }
                catch
                {
                    skipText.Dispose();
                    skipText = new();
                }
            }
            CutsceneBox.Visibility = Visibility.Hidden;
            Main.Visibility = Visibility.Visible;
        }

        public void CutsceneClicked(object sender, MouseButtonEventArgs e)
        {
            if (CutsceneBox.Visibility == Visibility.Visible && Main.Visibility == Visibility.Hidden)
            {
                skipText.Cancel();
            }
        }

        public void PlaceUCToMouse(FrameworkElement uc)
        {
            System.Windows.Point senderPos = Mouse.GetPosition(Main);
            double screenWidthError = (uc.ActualWidth + senderPos.X) - Main.ActualWidth;
            if (screenWidthError<=0)
            {
                Canvas.SetLeft(uc, senderPos.X);
            }
            else
            {
                Canvas.SetLeft(uc, senderPos.X - screenWidthError);//(uc, Main.ActualWidth - uc.ActualWidth) >> simplified math
            }

            double screenHeightError = (uc.ActualHeight + senderPos.Y) - Main.ActualHeight;
            if (screenHeightError<=0)
            {
                Canvas.SetTop(uc, senderPos.Y);
            }
            else
            {
                Canvas.SetTop(uc, senderPos.Y - screenHeightError);
            }
        }
    }
}
