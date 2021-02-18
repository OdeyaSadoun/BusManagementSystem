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
using BL;
using BO;
using BLApi;
namespace PlGui
{
    /// <summary>
    /// Interaction logic for LineShow.xaml
    /// </summary>
    public partial class LineShow : Window
    {
        IBL bl = BLFactory.GetBL();

        public BO.Line thisLine { get; set; }

        public LineShow(BO.Line line)
        {
            InitializeComponent();
            //stations.ItemsSource = bl.get
            grid1.DataContext = line;
            grid3.DataContext = line;
            stations.ItemsSource = line.ListOfStationsInLine.ToList();
            //stations.DataContext = line.ListOfStationsInLine.ToList();
            stations.Visibility = Visibility.Visible;
            times.ItemsSource = line.ListOfTripTime.ToList();
            times.SelectedItem = 0;
            thisLine = line;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource lineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("lineViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // lineViewSource.Source = [generic data source]
        }

        private void DeleteStation(object sender, RoutedEventArgs e)
        {
            BO.StationInLine b = (sender as Button).DataContext as BO.StationInLine;
           //bl.DeleteStationInLine(b);
             BO.StationInLine stationToDelete = bl.GetStationInLine(b.StationCode, thisLine.Id);

            var v = (from item in thisLine.ListOfStationsInLine
                     where stationToDelete.LineId == thisLine.Id
                     select item).FirstOrDefault();

            thisLine.ListOfStationsInLine.ToList().Remove(v);

        }
    }
}
