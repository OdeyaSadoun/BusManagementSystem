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
    /// Interaction logic for AdminShow.xaml
    /// </summary>
    public partial class AdminShow : Window
    {
        public BO.User user { get; set; }
        public AdminShow(BO.User u)
        {
            InitializeComponent();
            user = u;
            grid1.DataContext = u;
        }

        private void Card_Click(object sender, RoutedEventArgs e)
        {
            CardWindow window = new CardWindow(user);
            window.ShowDialog();
        }

        private void lvBus_Click(object sender, RoutedEventArgs e)
        {
            if(user.Admin)
            {
                BusWindow bus = new BusWindow();
                bus.ShowDialog();
            }
            else
            {
                BusWindowPassnger bus = new BusWindowPassnger();
                bus.ShowDialog();
            }
        }

        private void lvLine_Click(object sender, RoutedEventArgs e)
        {
            if (user.Admin)
            {
                LineWindow line = new LineWindow();
                line.ShowDialog();
            }
            else
            {
                LineWindowPassenger line = new LineWindowPassenger();
                line.ShowDialog();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (user.Admin)
            {
                StationWindow station = new StationWindow(user);
                station.ShowDialog();
            }
            else
            {
                StationWindowPassnger station = new StationWindowPassnger(user);

                station.ShowDialog();

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource userViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("userViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // userViewSource.Source = [generic data source]
        }
    }
}
