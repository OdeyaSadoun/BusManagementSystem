using BLApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ElectronicScreen.xaml
    /// </summary>
    public partial class ElectronicScreen : Window
    {
        IBL bl = BLFactory.GetBL();
        public BO.Station station { get; set; }
        public Stopwatch stopWatch { get; set; }
        public BackgroundWorker timerWorker { get; set; }
        public TimeSpan timeStart { get; set; }
        public bool isTimerRun { get; set; }


        public ElectronicScreen(BO.Station currntStation)
        {
            InitializeComponent();
            
            station = currntStation;
            gridStation.DataContext = station;
            stopWatch = new Stopwatch();

            timerWorker = new BackgroundWorker();
            timerWorker.DoWork += Worker_DoWork;
            timerWorker.ProgressChanged += Worker_ProgressChanged;

            timerWorker.WorkerReportsProgress = true;

            timeStart = DateTime.Now.TimeOfDay;

            stopWatch.Restart();
            isTimerRun = true;

            timerWorker.RunWorkerAsync();
            

        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
            TimeSpan tsCurentTime = timeStart + stopWatch.Elapsed;
            string timerText = tsCurentTime.ToString().Substring(0, 8);
            this.timerTextBlock.Text = timerText;

            lv.ItemsSource = bl.GetLineTimingsPerStation(station, tsCurentTime).ToList();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //throw new NotImplementedException();

            while (isTimerRun)
            {
                timerWorker.ReportProgress(231);
                Thread.Sleep(1000);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            stopWatch.Stop();
            isTimerRun = false;
        }
    }
}
