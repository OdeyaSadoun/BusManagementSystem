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
    /// Interaction logic for AddLine.xaml
    /// </summary>
    public partial class AddLine : Window
    {
        IBL bl = BLFactory.GetBL("2");

       // List<BO.Area> areaList = new List<BO.Area> { BO.Area.Center, BO.Area.Jerusalem, BO.Area.JerusalemCenter, BO.Area.JerusalemNorth, BO.Area.JerusalemSouth, BO.Area.North, BO.Area.NorthCenter, BO.Area.NorthSouth, BO.Area.South, BO.Area.SouthCenter };
        public AddLine()
        {
            InitializeComponent();
            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Area));
            //areaComboBox.ItemsSource = areaList;
            areaComboBox.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource lineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("lineViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // lineViewSource.Source = [generic data source]
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Line line = new BO.Line() { LineNumber = int.Parse(lineNumberTextBox.Text), Area = (BO.Area)areaComboBox.SelectedItem, IsDeleted = false, Fare = int.Parse(fareTextBox.Text), TravelTimeInThisLine = TimeSpan.Parse(travelTimeInThisLineTextBox.Text)  };
                line.FirstStation = bl.GetStation(int.Parse(firstStationTextBox.Text));
                line.LastStation = bl.GetStation(int.Parse(lastStationTextBox.Text));
                bl.AddLine(line);
                MessageBox.Show("The line added successfuly");
                Close();

            }
            catch (BO.IncorrectInputException ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
