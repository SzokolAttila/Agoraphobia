using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AgoraphobiaGUI.UserControls
{
    public partial class TutorialUC : UserControl
    {
        readonly Grid container;
        
        Dictionary<string, string> tutorialTxt = new Dictionary<string, string>()
        {
            { "Health", "Health bar represents your current health points to your maximum. When it goes down to zero you die, but not permanently. It lowers your sanity, but you don't necessarily lose the game."},
            { "Sanity", "Sanity bar represents your current sanity on a scale of 0 to 100. When you hit zero, you go insane and lose the game. If you can reach 100 you successfully stay sane throughout your dreams and you win. Doesn't sound that difficult, right?" },
            { "Energy", "Energy works as your stamina. For using your weapons you spend your energy. However, as you take a rest or use some consumables you can regain it." },
            { "Defense", "Surprisingly if you hit an enemy, they're going to fight you back. Defense reduces the damage you suffer from the hit, so you should keep that number high enough." },
            { "Attack", "The higher attack you have, the harder you hit your target. Building on that stat is fun, believe me!" },
            { "DreamCoins", "This is the official currency of the game. Merchants give you stuff for DreamCoins, it's that simple." },
            { "Effects", "Consumables can give you some effects, for example buff your attacks. You can open this panel to see what effects do you have and how long will they last." },
            { "Inventory", "Here you can see all the handy stuff you gathered on this road of yours." },
            { "BadDoor", "One of the three holds bigger challenges. (Obviously you don't know which one.)" },
            { "NeutralDoor", "The other one offers you a fair trial. (Still no clue which.)" },
            { "GoodDoor", "And the third one gives you the blessing. (Well, not consequently the third.)" },
            { "Merchant", "Give me some DreamCoins so I'll give you some useful stuff! Fair deal, isn't it?" },
            { "Enemy", "Arrhh gaaawrr I'm going to be a real pain in the a$$" },
            { "Loot", "Besides all these tough enemies, you can always find some time for a good ol' looting." }
        };

        public TutorialUC(Grid container)
        {
            InitializeComponent();
            this.container = container;
        }

        public void Back(object sender, RoutedEventArgs e)
        {
            container.Children.Add(new MainMenuUC(container));
            container.Children.Remove(this);
        }

        public void ShowTutorial(object sender, RoutedEventArgs e)
        {
            Point senderPos = Mouse.GetPosition(Main);
            TutorialBox tb = new TutorialBox(tutorialTxt[((FrameworkElement)sender).Name]);
            Main.Children.Add(tb);
            tb.UpdateLayout();
            
            if (tb.ActualWidth+senderPos.X<Main.ActualWidth)
            {
                Canvas.SetLeft(tb, senderPos.X);
            }
            else
            {
                Canvas.SetLeft(tb, senderPos.X-tb.ActualWidth);
            }

            if (tb.ActualHeight + senderPos.Y < Main.ActualHeight)
            {
                Canvas.SetTop(tb, senderPos.Y);
            }
            else
            {
                Canvas.SetTop(tb, senderPos.Y - tb.ActualHeight);
            }
        }

        public void HideTutorial(object sender, RoutedEventArgs e)
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

        public void TradeWindow(object sender, RoutedEventArgs e)
        {

        }

        public void FightWindow(object sender, RoutedEventArgs e)
        {

        }

        public void LootWindow(object sender, RoutedEventArgs e)
        {

        }
    }
}
