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
using BLApi;
namespace PlGui
{
    /// <summary>
    /// Interaction logic for SearchIdWindow.xaml
    /// </summary>
    public partial class SearchIdWindow : Window
    {
        IBL bl = BLFactory.GetBL("2");
        public ObservableCollection<BO.Station> Stations { get; set; } = new ObservableCollection<BO.Station>();

        public BO.Line l { get; set; }
        public SearchIdWindow(BO.Line line)
        {
            InitializeComponent();
            l = line;
            foreach (BO.Station station in bl.GetAllStations())
            {
                    foreach (BO.Line lines in station.ListOfLines)
                    {
                        if (lines == l)
                            Stations.Add(station);
                    }
                
            }
            lvStation.ItemsSource = Stations;
        }

    }
}
