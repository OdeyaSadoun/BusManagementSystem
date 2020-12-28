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
            if ((b.LicenseNumber.Length == 10) && (b.DateBegin.Year < 2018))//אם המספר רישוי 8 ספרות אך השנה קטנה מ 2018
                MessageBox.Show("Error input licenseNumber or date\nPlease try again", "warning", MessageBoxButton.OK, MessageBoxImage.Warning);

            else if ((b.LicenseNumber.Length == 9) && (b.DateBegin.Year >= 2018))//אם המספר רישוי 7 ספרות אך השנה גדולה או שווה ל 2018
                MessageBox.Show("Error input licenseNumber or date\nPlease try again", "warning", MessageBoxButton.OK, MessageBoxImage.Warning);

            else if (b.LicenseNumber == "")
                MessageBox.Show("Error input licenseNumber\nPlease try again", "warning", MessageBoxButton.OK, MessageBoxImage.Warning);


            else
            {
                bool flag = false;
                foreach (Bus bus in Buses)
                {
                    if (bus.LicenseNumber == b.LicenseNumber)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                    MessageBox.Show("The bus with this license number is exist!\nPlease try again", "warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    Buses.Add(b);
                    this.Close();
                }
            }
        }
        #endregion

    }
}
