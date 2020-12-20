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


namespace dotNet5781_03B_0406_3977
{
    /// <summary>
    /// Interaction logic for Double_Click.xaml
    /// </summary>
    public partial class Double_Click : Window
    {
        public Double_Click()
        {
            InitializeComponent();
        }

        private void care_Click(object sender, RoutedEventArgs e)
        {
            Bus b = (sender as Button).DataContext as Bus;
            b.care();
            MessageBox.Show("The bus " + b.LicenseNumber + " sent for caring");
        }

        private void refuel_Click(object sender, RoutedEventArgs e)
        {
            Bus b = (sender as Button).DataContext as Bus;
            b.refueling();
        }
    }
}
