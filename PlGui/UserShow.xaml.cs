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
    /// Interaction logic for UserShow.xaml
    /// </summary>
    public partial class UserShow : Window
    {
        public ObservableCollection<BO.Bus> listOfBuses { get; set; } = new ObservableCollection<BO.Bus>();
        //public ObservableCollection<BO.Line> listOfLines { get; set; } = new ObservableCollection<BO.Line>();
        IBL bl = BLFactory.GetBL("2");
        public BO.User user{ get; set; }
       



        public UserShow(BO.User u)
        {
            InitializeComponent();
            user = u;
            foreach (BO.Bus bus in bl.GetAllBuses())
            {
                listOfBuses.Add(bus);
            }

        }

        private void Card_Click(object sender, RoutedEventArgs e)
        {

            CardWindow window = new CardWindow(user);
            window.ShowDialog();
        }

        

        private void lvBus_Click(object sender, RoutedEventArgs e)
        {
            BusWindow bus = new BusWindow();
            bus.ShowDialog();
        }

        private void lvLines_Click(object sender, RoutedEventArgs e)
        {
            LineWindow line = new LineWindow();
            line.ShowDialog();
        }

        

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            ////{
            ////    int temp = int.Parse(Searching.Text);
            ////    foreach (BO.Bus bus in listOfBuses)
            ////    {
            ////        if (temp == bus.LicenseNumber)
                        
            ////    }

            ////    this.Close();
            //}
        }

        

        private void PreviewTextInputSearch(object sender, TextCompositionEventArgs e)
        {

        }

        private void TextChangedSearching(object sender, TextChangedEventArgs e)
        {

        }
    }
}
