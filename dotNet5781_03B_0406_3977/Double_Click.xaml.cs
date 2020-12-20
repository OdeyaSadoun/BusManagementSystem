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
        public Bus currentBus { get; set; }
        public ProgressBar progressBar { get; set; }
        public Label label { get; set; }
        public Button bcare { get; set; }
        public Button brefuel { get; set; }

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


        private void care_Click(object sender, RoutedEventArgs e)
        {
            Bus b = currentBus;
            b.care();
            ProgressBar p = progressBar;
            Label l = label;
            Button bCare = sender as Button;
            Button bRefule = brefuel;
            bCare.IsEnabled = false;
            bRefule.IsEnabled = false;

            p.Foreground = Brushes.Yellow;
            p.Value = 0;
            MyBackground background = new MyBackground() { bus = b, Length = 144, progressBar = p, result_Label = l, Care = bCare };
            background.start();
           
        }

        private void refuel_Click(object sender, RoutedEventArgs e)
        {
            Bus b = currentBus;
            b.refueling();
            ProgressBar p = progressBar;
            Label l = label;
            Button bRefule = sender as Button;
            Button bCare = bcare;
            bCare.IsEnabled = false;
            bRefule.IsEnabled = false;
            p.Foreground = Brushes.Yellow;
            p.Value = 0;
            MyBackground background = new MyBackground() { bus = b, Length = 144, progressBar = p, result_Label = l, Care = bCare };
            background.start();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
        }
    }
}
