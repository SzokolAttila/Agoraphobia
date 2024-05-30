using AgoraphobiaLibrary;
using System;
using System.Collections.Generic;
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

namespace AgoraphobiaGUI.UserControls.ItemUCs
{
    /// <summary>
    /// Interaction logic for EffectUC.xaml
    /// </summary>
    public partial class EffectUC : UserControl
    {
        public EffectUC(Effect effect)
        {
            InitializeComponent();
            Name.Text = effect.Consumable.Name;
            Energy.Text = effect.Consumable.Energy.ToString();
            Hp.Text = effect.Consumable.Hp.ToString();
            Defense.Text = effect.Consumable.Defense.ToString();
            Attack.Text = effect.Consumable.Attack.ToString();
            Sanity.Text = effect.Consumable.Sanity.ToString();
            Duration.Text = effect.CurrentDuration.ToString();
        }


        public void HoverStart(object sender, MouseEventArgs e)
        {
            Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        }

        public void HoverEnd(object sender, MouseEventArgs e)
        {
            Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

    }
}
