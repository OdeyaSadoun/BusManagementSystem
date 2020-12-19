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
using System.ComponentModel;


namespace dotNet5781_03B_0406_3977
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Random rnd = new Random(DateTime.Now.Millisecond);
        ObservableCollection<Bus> busLst = new ObservableCollection<Bus>();
        public ObservableCollection<Bus> listOfBuses { get; set; }

        BackgroundWorker worker;
        public MainWindow()
        {
            InitializeComponent();
            ObservableCollection<Bus> busLst = new ObservableCollection<Bus>();
            for(int i=0; i<10; i++)
            {
                Bus b = new Bus();               
                string licenseNumber = (rnd.Next(999999, 100000000)).ToString();
                int tempYearBegin = rnd.Next(1900, 2022);
                DateTime timeBegin =new DateTime(/*years*/tempYearBegin, /*monthes*/rnd.Next(1, 13), /*days*/rnd.Next(1, 29));
                int mileage = rnd.Next(0, 200000);//החלטנו שזה הכולל
                int tempYearLastCare;
                if (tempYearBegin <= 2015)
                    tempYearLastCare = rnd.Next(2015, 2022);
                else
                    tempYearLastCare = rnd.Next(tempYearBegin, 2022);
                DateTime lastCare = new DateTime(/*years*/tempYearLastCare, /*monthes*/rnd.Next(1, 13), /*days*/rnd.Next(1, 29));
                double kmAfterCare = rnd.NextDouble() * 20000;//מקסימום נסיעה אחרי הטיפול הוא 20000
                double kmAfterFuel = rnd.NextDouble() * 1200;//1מקסימום נסיעה אחרי התדלוק הוא 200
                int tempStatus = rnd.Next(1, 5);
                b.Status = (BusStatus)tempStatus;
                b.LicenseNumber = licenseNumber;
                b.SumMileage = mileage;
                b.DateBegin = timeBegin;
                b.KmBeforeFuel = 1200 - kmAfterFuel;
                b.KmBeforCare = 20000 - kmAfterCare;
                b.LastCare = lastCare;
                busLst.Add(b);

            }
            listOfBuses = busLst;
            lbBuses.ItemsSource = listOfBuses /*busLst*/;
            //lbBuses.DisplayMemberPath = "LicenseNumber";

            

        }

      

        private void B_AddBus_Click(object sender, RoutedEventArgs e)
        {
            WindowAddBus win = new WindowAddBus(/*listOfBuses*/);
            win.Buses = listOfBuses;  
            win.ShowDialog();
            
        }

        private void drive_click_button(object sender, RoutedEventArgs e)
        {
            Bus b = (sender as Button).DataContext as Bus;
            EnterDistanceForTravelWindow win = new EnterDistanceForTravelWindow(b);
            win.ShowDialog();
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync(b);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            
            throw new NotImplementedException();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //throw new NotImplementedException();
            res
        }
    }
    
}
