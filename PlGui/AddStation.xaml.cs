using BLApi;
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

namespace PlGui
{
    /// <summary>
    /// Interaction logic for AddStation.xaml
    /// </summary>
    public partial class AddStation : Window
    {
        IBL bl = BLFactory.GetBL("2");
        public AddStation()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource stationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("stationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // stationViewSource.Source = [generic data source]
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Station station = new BO.Station() { Address = addressTextBox.Text, Code = int.Parse(codeTextBox.Text), IsAccessible = isAccessibleCheckBox.IsEnabled, IsBench = isBenchCheckBox.IsEnabled, IsDigitalPanel = isDigitalPanelCheckBox.IsEnabled, Latitude = double.Parse(latitudeTextBox.Text), Longitude = double.Parse(longitudeTextBox.Text), Name = nameTextBox.Text };
                bl.AddStation(station);
                
                Close();

            }
            catch (BO.IncorrectCodeStationException ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
