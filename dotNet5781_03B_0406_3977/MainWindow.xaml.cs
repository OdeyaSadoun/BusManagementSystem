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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dotNet5781_03B_0406_3977
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Random rnd = new Random(DateTime.Now.Millisecond);

        public MainWindow()
        {
            InitializeComponent();
            ObservableCollection<Bus> busLst = new ObservableCollection<Bus>();
            for(int i=0; i<10; i++)
            {
                Bus b = new Bus();               
                string licenseNumber = (rnd.Next(999999, 100000000)).ToString();
                DateTime timeBegin =new DateTime(/*years*/rnd.Next(1900, 2022), /*monthes*/rnd.Next(1, 13), /*days*/rnd.Next(1, 31));
                int mileage = rnd.Next(0, 200000);//החלטנו שזה הכולל
                DateTime lastCare = new DateTime(/*years*/rnd.Next(2015, 2022), /*monthes*/rnd.Next(1, 13), /*days*/rnd.Next(1, 31));
                double kmAfterCare = rnd.NextDouble() * 20000;//מקסימום נסיעה אחרי הטיפול הוא 20000
                double kmAfterFuel = rnd.NextDouble() * 1200;//1מקסימום נסיעה אחרי התדלוק הוא 200
                b.LicenseNumber = licenseNumber;
                b.SumMileage = mileage;
                b.DateBegin = timeBegin;
                b.KmBeforeFuel = 1200 - kmAfterFuel;
                b.KmBeforCare = 20000 - kmAfterCare;
                b.LastCare = lastCare;
                busLst.Add(b);

            }

            lbBuses.ItemsSource = busLst;
            lbBuses.DisplayMemberPath = "LicenseNumber";



        }

        private void B_AddBus_Click(object sender, RoutedEventArgs e)
        {
            WindowAddBus win = new WindowAddBus();
            win.ShowDialog();
        }
    }
    
}
