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
using BLApi;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for UpdateStation.xaml
    /// </summary>
    public partial class UpdateStation : Window
    {
        IBL bl = BLFactory.GetBL();
        public BO.Station s { get; set; }
        public UpdateStation(BO.Station station)
        {
            InitializeComponent();
            s = station;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource stationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("stationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // stationViewSource.Source = [generic data source]
        }

        private void addressTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void latitudeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void longitudeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void updating_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                if ((addressTextBox.Text != "") && (s != null))
                    s.Address = addressTextBox.Text;
                if ((latitudeTextBox.Text != "") && (s != null))
                    s.Latitude = double.Parse(latitudeTextBox.Text);
                if ((longitudeTextBox.Text != "") && (s != null))
                    s.Longitude = double.Parse(longitudeTextBox.Text);
                if ((isAccessibleCheckBox.IsChecked != null) && (s != null))
                    s.IsAccessible = isAccessibleCheckBox.IsChecked.Value;
                if ((isBenchCheckBox.IsChecked != null) && (s != null))
                    s.IsAccessible = isBenchCheckBox.IsChecked.Value;
                if ((isDigitalPanelCheckBox.IsChecked != null) && (s != null))
                    s.IsAccessible = isDigitalPanelCheckBox.IsChecked.Value;
                bl.UpdateStation(s);
                Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
