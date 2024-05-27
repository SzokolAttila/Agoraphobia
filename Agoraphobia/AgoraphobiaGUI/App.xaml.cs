using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using AgoraphobiaLibrary;

namespace AgoraphobiaGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static MediaPlayer backgroundMusic = new MediaPlayer();
        public static double Volume
        {
            get
            {
                return backgroundMusic.Volume;
            }

            set
            {
                if (value>1)
                {
                    backgroundMusic.Volume = 1.0;
                }else if (value<0)
                {
                    backgroundMusic.Volume = 0.0;
                }
                else
                {
                    backgroundMusic.Volume = value;
                }
            }
        }

        public static Account? Account;
        public App()
        {
            InitializeComponent();
            StartBackgroundMusic();
        }

        public void StartBackgroundMusic()
        {
            backgroundMusic.Open(new Uri(@"Sounds/AgoraphobiaTheme.wav", UriKind.Relative));
            backgroundMusic.Volume = 0.0;
            backgroundMusic.MediaEnded += new EventHandler(EndOfBackgroundMusic);
            backgroundMusic.Play();

        }

        public void EndOfBackgroundMusic(object sender, EventArgs e)
        {
            backgroundMusic.Position = TimeSpan.Zero;
            backgroundMusic.Play();
        }
    }
}
