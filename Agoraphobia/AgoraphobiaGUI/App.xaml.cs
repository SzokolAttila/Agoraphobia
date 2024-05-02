using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace AgoraphobiaGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MediaPlayer backgroundMusic = new MediaPlayer();

        public App()
        {
            InitializeComponent();
            StartBackgroundMusic();
        }

        public void StartBackgroundMusic()
        {
            backgroundMusic.Open(new Uri(@"Sounds/AgoraphobiaTheme.wav", UriKind.Relative));
            backgroundMusic.Volume = 80;
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
