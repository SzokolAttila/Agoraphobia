using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AgoraphobiaGUI.UserControls
{
    public partial class SettingsUC : UserControl, INotifyPropertyChanged
    {
        Panel container;
        Color? Bg;

        public double MainVolume
        {
            get
            {
                return App.Volume;
            }
            set
            {
                App.Volume = value;
                OnPropertyChanged("MainVolume");
            }
        }
        public SettingsUC(Panel container, Color? bg = null, Color? border = null)
        {
            InitializeComponent();
            DataContext = this;
            this.container = container;
            Main.Background = new SolidColorBrush(bg ?? Colors.Transparent);
            Main.BorderBrush = new SolidColorBrush(border ?? Colors.Transparent);
            Bg = bg;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Back(object sender, RoutedEventArgs e)
        {
            if (Bg==null)
            {
                container.Children.Add(new MainMenuUC((Grid)container));
            }
            container.Children.Remove(this);
        }

        public void VolumeChange(object sender, RoutedEventArgs e)
        {
            MainVolume = ((Slider)sender).Value;
        }
    }
}
