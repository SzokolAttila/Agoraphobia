using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Media;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using AgoraphobiaAPI.HttpClients;
using AgoraphobiaLibrary;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;

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

        public App()
        {
            InitializeComponent();
            StartBackgroundMusic();
            InitConnection();
        }

        private async void InitConnection()
        {
            try
            {
                var response = await AccountHttpClient.GetAccounts();
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show(e.Message, "Could not initiate connection with the database",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
