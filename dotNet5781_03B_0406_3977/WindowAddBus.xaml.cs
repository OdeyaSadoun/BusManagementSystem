using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for WindowAddBus.xaml
    /// </summary>
    public partial class WindowAddBus : Window
    {
        //private ObservableCollection<Bus> buses;
        public ObservableCollection<Bus> Buses{ get; set; }

        public WindowAddBus(/*ObservableCollection<Bus> b*/)
        {
            InitializeComponent();
            //Buses = b;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
        }

        private void sumMileageTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void submit_bus_button_Click(object sender, RoutedEventArgs e)
        {
            Buses.Add(new Bus() { LicenseNumber = licenseNumberTextBox.Text, DateBegin = dateBeginDatePicker.DisplayDate, Fuel = int.Parse(fuelTextBox.Text), LastCare = lastCareDatePicker.DisplayDate, SumMileage = int.Parse(sumMileageTextBox.Text)});
            this.Close();
        }

    }
}
