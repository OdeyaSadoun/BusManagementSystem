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
using System.Windows.Shapes;
using System.ComponentModel;


namespace dotNet5781_03B_0406_3977
{
    /// <summary>
    /// Interaction logic for EnterDistanceForTravelWindow.xaml
    /// </summary>


    public partial class EnterDistanceForTravelWindow : Window
    {
        public EnterDistanceForTravelWindow()
        {
            InitializeComponent();

        }

        private void enter_km(object sender, KeyEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null)
                return;
            if (e == null)
                return;
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txt.Text.Length > 0)
                {
                    int amount = int.Parse(txt.Text);
                    txt.Text = "";
                    if (sender == txtBox_km_to_travel)
                        




                }
            }
        }
    }
}
