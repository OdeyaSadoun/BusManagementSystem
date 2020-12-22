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
using System.Threading;




namespace dotNet5781_03B_0406_3977
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Random rnd = new Random(DateTime.Now.Millisecond);

        //ObservableCollection<Bus> busLst = new ObservableCollection<Bus>();
        public ObservableCollection<Bus> listOfBuses { get; } = new ObservableCollection<Bus>();

        BackgroundWorker worker;

        #region constructor
        public MainWindow()
        {
            InitializeComponent();
            //ObservableCollection<Bus> busLst = new ObservableCollection<Bus>();
            for(int i=0; i<10; i++)
            {
                Bus b = new Bus();
                string licenseNumber = (rnd.Next(999999, 100000000)).ToString();
                int tempYearBegin;
                if (int.Parse(licenseNumber) < 10000000) // 7 digits
                    tempYearBegin = rnd.Next(2000, 2018);
                else
                    tempYearBegin = rnd.Next(2018, 2021);
                DateTime timeBegin =new DateTime(/*years*/tempYearBegin, /*monthes*/rnd.Next(1, 13), /*days*/rnd.Next(1, 29));
                int mileage = rnd.Next(0, 200000);//החלטנו שזה הכולל
                int tempYearLastCare;
                if (tempYearBegin <= 2019)
                    tempYearLastCare = rnd.Next(2019, 2021);
                else
                    tempYearLastCare = rnd.Next(tempYearBegin, 2021);
                DateTime lastCare = new DateTime(/*years*/tempYearLastCare, /*monthes*/rnd.Next(1, 13), /*days*/rnd.Next(1, 29));
                float kmAfterCare = (float)rnd.NextDouble() * 20000;//מקסימום נסיעה אחרי הטיפול הוא 20000
                float kmAfterFuel = (float)rnd.NextDouble() * 1200;//1מקסימום נסיעה אחרי התדלוק הוא 200

                b.DateBegin = timeBegin;
                b.LicenseNumber = licenseNumber;
                b.SumMileage = mileage;
                b.KmBeforeFuel = 1200 - kmAfterFuel;
                b.KmBeforCare = 20000 - kmAfterCare;
                b.LastCare = lastCare;
                //busLst.Add(b);
                listOfBuses.Add(b);
            }
           // listOfBuses = busLst;
            lbBuses.ItemsSource = listOfBuses /*busLst*/;
            //lbBuses.DisplayMemberPath = "LicenseNumber";
        }

        #endregion

        #region B_AddBus_Click
        private void B_AddBus_Click(object sender, RoutedEventArgs e)
        {
            WindowAddBus win = new WindowAddBus(/*listOfBuses*/);
            win.Buses = listOfBuses;  
            win.ShowDialog();
            
        }
        #endregion

        #region drive_click_button
        private void drive_click_button(object sender, RoutedEventArgs e)
        {
            Bus b = (sender as Button).DataContext as Bus;

            if (b.Status != BusStatus.Ready)
                MessageBox.Show("The status of the bus is:" + b.Status + "\nThe bus can't go to drive", "warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            else 
            {
                EnterDistanceForTravelWindow win = new EnterDistanceForTravelWindow(b);
                win.ShowDialog();
            }
            float km = b.currentMileage;
            if (b.YearPassed())
            MessageBox.Show("The bus cant perform this travel because a year passed from", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (b.KmBeforCare - km <= 0)
            MessageBox.Show("The bus cant perform this travel because the bus traveled 20000 km without care - need to care", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (b.KmBeforeFuel - km <= 0)
                MessageBox.Show("The fuel isnt enough for travel, you should fuel", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                ListBoxItem myListBoxItem = (ListBoxItem)(lbBuses.ItemContainerGenerator.ContainerFromItem(b));
                ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
                DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                ProgressBar p = (ProgressBar)myDataTemplate.FindName("Time_Before_Ready", myContentPresenter);
                Label lresult = (Label)myDataTemplate.FindName("result_Label", myContentPresenter);
                Label lseconds = (Label)myDataTemplate.FindName("seconds_Label", myContentPresenter);
                Button bRefuel = (Button)myDataTemplate.FindName("Refuel_Button", myContentPresenter);
                Button bCare = (Button)myDataTemplate.FindName("Care_Button", myContentPresenter);
                Button bDrive = sender as Button;

                bRefuel.IsEnabled = false;
                bCare.IsEnabled = false;
                bDrive.IsEnabled = false;


                p.Foreground = Brushes.LimeGreen;
                p.Value = 0;
                b.Status = BusStatus.Driving;
                Random rnd = new Random();
                int temp = rnd.Next(20, 51);
                MyBackground background = new MyBackground() { bus = b, Length = temp * (int)km, progressBar = p, seconds_Label = lseconds, result_Label = lresult, Care = bCare, Reful = bRefuel, Drive = bDrive };
                background.start();
            }
        }
        #endregion

        #region FindVisualChild
        private childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                {
                    return (childItem)child;
                }
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
        #endregion

        #region refuel_click_button
        private void refuel_click_button(object sender, RoutedEventArgs e)
        {
            Bus b = (sender as Button).DataContext as Bus;
            if (b.Status != BusStatus.Ready)
                MessageBox.Show("the bus in " + b.Status + "\n The bus can't go to refuel now!", "warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                b.refueling();
                ListBoxItem myListBoxItem = (ListBoxItem)(lbBuses.ItemContainerGenerator.ContainerFromItem(b));
                ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
                DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                ProgressBar p = (ProgressBar)myDataTemplate.FindName("Time_Before_Ready", myContentPresenter);
                Label lresult = (Label)myDataTemplate.FindName("result_Label", myContentPresenter);
                Label lseconds = (Label)myDataTemplate.FindName("seconds_Label", myContentPresenter);


                Button bRefuel = sender as Button;
                Button bCare = (Button)myDataTemplate.FindName("Care_Button", myContentPresenter);
                Button bDrive = (Button)myDataTemplate.FindName("Drive_Button", myContentPresenter);

                bRefuel.IsEnabled = false;
                bCare.IsEnabled = false;
                bDrive.IsEnabled = false;


                p.Foreground = Brushes.LightBlue;
                p.Value = 0;
                MyBackground background = new MyBackground() { bus = b, Length = 12, progressBar = p, seconds_Label = lseconds, result_Label = lresult, Care = bCare, Reful = bRefuel, Drive = bDrive };
                background.start();
                //MessageBox.Show("The bus " + b.LicenseNumber + " sent for refueling");
            }
        }
        #endregion

        #region care_click_button
        private void care_click_button(object sender, RoutedEventArgs e)
        {
            Bus b = (sender as Button).DataContext as Bus;
            if (b.Status != BusStatus.Ready)
                MessageBox.Show("the bus in " + b.Status + "\n The bus can't go to care now!", "warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                b.care();
                ListBoxItem myListBoxItem = (ListBoxItem)(lbBuses.ItemContainerGenerator.ContainerFromItem(b));
                ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
                DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                ProgressBar p = (ProgressBar)myDataTemplate.FindName("Time_Before_Ready", myContentPresenter);
                Label lresult = (Label)myDataTemplate.FindName("result_Label", myContentPresenter);
                Label lseconds = (Label)myDataTemplate.FindName("seconds_Label", myContentPresenter);

                Button bCare = sender as Button;
                Button bRefuel = (Button)myDataTemplate.FindName("Refuel_Button", myContentPresenter);
                Button bDrive = (Button)myDataTemplate.FindName("Drive_Button", myContentPresenter);

                bRefuel.IsEnabled = false;
                bCare.IsEnabled = false;
                bDrive.IsEnabled = false;
                Button bcare = sender as Button;
                bcare.IsEnabled = false;
                p.Foreground = Brushes.Yellow;
                p.Value = 0;
                MyBackground background = new MyBackground() { bus = b, Length = 144, progressBar = p, seconds_Label = lseconds, result_Label = lresult, Care = bCare, Reful = bRefuel, Drive = bDrive };
                background.start();
                // MessageBox.Show("The bus "+ b.LicenseNumber+" sent for caring");
            }
        }
        #endregion

        #region Preview_Double_Click
        private void Preview_Double_Click(object sender, MouseButtonEventArgs e)
        {

            Bus b = (sender as ListBox).SelectedItem as Bus;
            ListBoxItem myListBoxItem = (ListBoxItem)(lbBuses.ItemContainerGenerator.ContainerFromItem(b));
            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            ProgressBar p = (ProgressBar)myDataTemplate.FindName("Time_Before_Ready", myContentPresenter);
            Label l1 = (Label)myDataTemplate.FindName("result_Label", myContentPresenter);
            Label l2 = (Label)myDataTemplate.FindName("seconds_Label", myContentPresenter);

            Button bRefuel = (Button)myDataTemplate.FindName("Refuel_Button", myContentPresenter);
            Button bCare = (Button)myDataTemplate.FindName("Care_Button", myContentPresenter);
            Double_Click win = new Double_Click(b) { progressBar = p,lseconds=l2, lresult = l1, bcare = bCare, brefuel = bRefuel };
            win.ShowDialog();
        }


        #endregion

        #region remove_click_button
        private void remove_click_button(object sender, RoutedEventArgs e)
        {

            Bus b = (sender as Button).DataContext as Bus;

            listOfBuses.Remove(b);
           
        }
        #endregion

       
    }

}
