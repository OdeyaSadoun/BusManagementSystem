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
    /// Interaction logic for BusWindow.xaml
    /// </summary>
    public partial class BusWindow : Window
    {
        public ObservableCollection<BO.Bus> listOfBuses { get; set; } = new ObservableCollection<BO.Bus>();
        IBL bl = BLFactory.GetBL("2");
        public BusWindow()
        {
            InitializeComponent();
            foreach (BO.Bus bus in bl.GetAllBuses())
            {
                listOfBuses.Add(bus);
            }
            lv.ItemsSource = listOfBuses;
        }
    }
}
