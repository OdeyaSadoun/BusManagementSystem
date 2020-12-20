using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace dotNet5781_03B_0406_3977
{
    
    class MyBackground
    {
        BackgroundWorker worker;
        public int Length { get; set; }
        public ProgressBar progressBar { get; set; }
        public Bus bus { get; set; }
        public Label result_Label { get; set; }
        public Label seconds_Label { get; set; }
        public Button Reful { get; set; }
        public Button Care { get; set; }
        public Button Drive { get; set; }


        public MyBackground()
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;

        }
        public void start()
        {
            progressBar.Visibility = Visibility.Visible;
            result_Label.Visibility = Visibility.Visible;
            seconds_Label.Visibility = Visibility.Visible;

            worker.RunWorkerAsync();
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
            progressBar.Visibility = Visibility.Hidden;
            result_Label.Visibility = Visibility.Hidden;
            seconds_Label.Visibility = Visibility.Hidden;
            string message = ""; ////////////////////
            if (bus.Status == BusStatus.Careing)
                message = "The caring of bus number: " + bus.LicenseNumber + " ended successfully!";
            else if (bus.Status == BusStatus.Driving)
                message = "The driving of bus number: " + bus.LicenseNumber + " ended successfully!";
            else if (bus.Status == BusStatus.Refueling)
                message = "The refueling of bus number: " + bus.LicenseNumber + " ended successfully!";
            MessageBox.Show(message);
            bus.Status = BusStatus.Ready;
            Reful.IsEnabled = true;
            Care.IsEnabled = true;
            Drive.IsEnabled = true;




        }
        //void Countdown(int count, TimeSpan interval, Action<int> ts)
        //{
        //    var dt = new System.Windows.Threading.DispatcherTimer();
        //    dt.Interval = interval;
        //    dt.Tick += (_, a) =>
        //    {
        //        if (count-- == 0)
        //            dt.Stop();
        //        else
        //            ts(count);
        //    };
        //    ts(count);
        //    dt.Start();
        //}

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            progressBar.Value = progress * 100 / Length;
            result_Label.Content = progress*100/Length + "%";
            seconds_Label.Content = Length - progress;//the seconde that need to wait


        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i < Length; i++)
            {
                Thread.Sleep(1000);
                worker.ReportProgress(i);
            }

        }

    }
}
