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
        //property
        #region ObservableCollection<Bus> Buses
        //private ObservableCollection<Bus> buses;
        public ObservableCollection<Bus> Buses{ get; set; }

        #endregion 

        //functions
        #region WindowAddBus
        //constructor
        public WindowAddBus(/*ObservableCollection<Bus> b*/)
        {
            InitializeComponent();
            //Buses = b;
        }
        #endregion

        #region  Window_Loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
        }
        #endregion

        #region umMileageTextBox_TextChanged
        private void sumMileageTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        #endregion

        #region submit_bus_button_Click
        /// <summary>
        /// Abutton for sumbit the bus that we add,check the input and after that we add it to buse's list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submit_bus_button_Click(object sender, RoutedEventArgs e)
        {
            Bus b = new Bus() { LicenseNumber = licenseNumberTextBox.Text, DateBegin = dateBeginDatePicker.DisplayDate, LastCare = lastCareDatePicker.DisplayDate, SumMileage = int.Parse(sumMileageTextBox.Text) };
            if ((b.LicenseNumber.Length == 8) && (b.DateBegin.Year < 2018))
                //throw new ArgumentException("error input licenseNumber");
                MessageBox.Show("error input licenseNumber or date");

            if ((b.LicenseNumber.Length == 7) && (b.DateBegin.Year >= 2018))
                throw new ArgumentException("error input licenseNumber");

            Buses.Add(b);
            this.Close();
        }
        #endregion

    }
}
