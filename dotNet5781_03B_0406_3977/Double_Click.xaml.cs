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


namespace dotNet5781_03B_0406_3977
{
    /// <summary>
    /// Interaction logic for Double_Click.xaml
    /// </summary>
    public partial class Double_Click : Window
    {
        //proprties:

        public Bus currentBus { get; set; }
        public ProgressBar progressBar { get; set; }
        public Label lresult { get; set; }
        public Label lseconds { get; set; }
        public Button bcare { get; set; }
        public Button brefuel { get; set; }

        //functions:

        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="b"></param>
        public Double_Click(Bus b)
        {
            InitializeComponent();
            currentBus = b;
            licenseNumberTextBlock.Text = currentBus.LicenseNumber.ToString();
            dateBeginTextBlock.Text = currentBus.DateBegin.Day + "/" + currentBus.DateBegin.Month + "/" + currentBus.DateBegin.Year;
            lastCareTextBlock.Text = currentBus.LastCare.Day + "/" + currentBus.LastCare.Month + "/" + currentBus.LastCare.Year;
            kmBeforCareTextBlock.Text = currentBus.KmBeforCare.ToString();
            kmBeforeFuelTextBlock.Text = currentBus.KmBeforeFuel.ToString();
            statusTextBlock.Text = currentBus.Status.ToString();
            sumMileageTextBlock.Text = currentBus.SumMileage.ToString();

        }
        #endregion

        #region care_Click
        /// <summary>
        /// Checking the status of the bus, if it is ready the bus is sent for refueling the progressBar is activated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void care_Click(object sender, RoutedEventArgs e)
        {
            Bus b = currentBus;
            if (b.Status != BusStatus.Ready)
                MessageBox.Show("The bus in " + b.Status + "\n The bus can't go to care now!", "warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                b.care();
                ProgressBar p = progressBar;
                Label l1 = lresult;
                Label l2 = lseconds;

                Button bCare = sender as Button;
                Button bRefule = brefuel;
                p.Foreground = Brushes.Yellow;
                p.Value = 0;
                MyBackground background = new MyBackground() { bus = b, Length = 144, progressBar = p, seconds_Label= l2, result_Label = l1, Care = bCare, Reful = bRefule};
                background.start();
                this.Close();

            }

        }
        #endregion

        #region refuel_Click
        /// <summary>
        /// Checking the status of the bus, if it is ready the bus is sent for caring theprogressBar is activated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refuel_Click(object sender, RoutedEventArgs e)
        {
            Bus b = currentBus;
            if (b.Status != BusStatus.Ready)
                MessageBox.Show("The bus in " + b.Status + "\n The bus can't go to refuel now!", "warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                b.refueling();
                ProgressBar p = progressBar;
                Label l1 = lresult;
                Label l2 = lseconds;
                Button bCare = bcare;
                Button bRefule = sender as Button;
                p.Foreground = Brushes.LightBlue;
                p.Value = 0;
                MyBackground background = new MyBackground() { bus = b, Length = 12, progressBar = p, seconds_Label= lseconds, result_Label = lresult, Care = bCare, Reful = bRefule};
                background.start();
                this.Close();
            }

        }
        #endregion

        #region Window_Loaded_1

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));         
        }
        #endregion
    }
}
