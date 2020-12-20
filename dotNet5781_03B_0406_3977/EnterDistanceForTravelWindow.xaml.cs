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
        public double KmToTravel { get; set; }
        public Bus Bus { get; set; }
        public ProgressBar psBar { get; set; }
        public EnterDistanceForTravelWindow(Bus b)
        {
            InitializeComponent();
            Bus = b;

        }

        //private void enter_km(object sender, KeyEventArgs e)
        //{
        //    TextBox txt = sender as TextBox;
        //    if (txt == null)
        //        return;
        //    if (e == null)
        //        return;
        //    if (e.Key == Key.Enter || e.Key == Key.Return)
        //    {
        //        KmToTravel = double.Parse(txt.Text);
        //        Bus.goToTravel(KmToTravel);
        //    }
        //    this.Close();
        //}

        private void Enter_km_button_Click(object sender, RoutedEventArgs e)
        {
            Bus.currentMileage = double.Parse(TextBoxKm.Text);
            Bus.goToTravel(KmToTravel);

            //ProgressBar p = progressBar;
            //Label l = label;
            //Button bCare = sender as Button;
            //Button bRefule = brefuel;
            //bCare.IsEnabled = false;
            //bRefule.IsEnabled = false;

            //p.Foreground = Brushes.Yellow;
            //p.Value = 0;
            //Random rnd = new Random();
            //int temp = rnd.Next(20, 51);
            //MyBackground background = new MyBackground() { bus = Bus, Length = temp * int.Parse(TextBoxKm.Text)  /*, progressBar = p, result_Label = l, Care = bCare*/ };
            //background.start();
           this.Close();
        }
    }
}
