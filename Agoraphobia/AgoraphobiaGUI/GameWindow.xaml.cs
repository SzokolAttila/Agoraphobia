using AgoraphobiaGUI.UserControls;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
using System.Linq;
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

namespace AgoraphobiaGUI
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
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
        public GameWindow()
        {
            InitializeComponent();

            PlayIntro();
        }
        public void Back(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        public void ShowTutorial(object sender, RoutedEventArgs e)
        {
            System.Windows.Point senderPos = Mouse.GetPosition(Main);
            TutorialBox tb = new TutorialBox(tutorialTxt[((FrameworkElement)sender).Name]);
            Main.Children.Add(tb);
            tb.UpdateLayout();

            if (tb.ActualWidth + senderPos.X < Main.ActualWidth)
            {
                Canvas.SetLeft(tb, senderPos.X);
            }
            else
            {
                Canvas.SetLeft(tb, senderPos.X - tb.ActualWidth);
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
    }
}
