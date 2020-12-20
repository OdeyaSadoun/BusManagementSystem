﻿using System;
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

        ObservableCollection<Bus> busLst = new ObservableCollection<Bus>();
        public ObservableCollection<Bus> listOfBuses { get; set; }
        public double Km { get; set; }

        BackgroundWorker worker;

        #region constructor
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
                if (tempYearBegin <= 2019)
                    tempYearLastCare = rnd.Next(2019, 2021);
                else
                    tempYearLastCare = rnd.Next(tempYearBegin, 2021);
                DateTime lastCare = new DateTime(/*years*/tempYearLastCare, /*monthes*/rnd.Next(1, 13), /*days*/rnd.Next(1, 29));
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
            listOfBuses = busLst;
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
            EnterDistanceForTravelWindow win = new EnterDistanceForTravelWindow(b);
            win.ShowDialog();

            ListBoxItem myListBoxItem = (ListBoxItem)(lbBuses.ItemContainerGenerator.ContainerFromItem(b));
            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            ProgressBar p = (ProgressBar)myDataTemplate.FindName("Time_Before_Ready", myContentPresenter);
            Label l = (Label)myDataTemplate.FindName("result_Label", myContentPresenter);
            Button bRefuel = (Button)myDataTemplate.FindName("Drive_Button", myContentPresenter);
            Button bCare = (Button)myDataTemplate.FindName("Care_Button", myContentPresenter);
            Button bDrive = sender as Button; 

            bRefuel.IsEnabled = false;
            bCare.IsEnabled = false;
            bDrive.IsEnabled = false;


            p.Foreground = Brushes.Green;
            p.Value = 0;
            Random rnd = new Random();
            int temp = rnd.Next(20, 51);
          
            MyBackground background = new MyBackground() { bus = b, Length = temp * int.Parse(TextBoxKm.Text), progressBar = p, result_Label = l, Care = bCare, Reful = bRefuel, Drive = bDrive };
            background.start();

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
            b.refueling();
            ListBoxItem myListBoxItem = (ListBoxItem)(lbBuses.ItemContainerGenerator.ContainerFromItem(b));
            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            ProgressBar p = (ProgressBar)myDataTemplate.FindName("Time_Before_Ready", myContentPresenter);
            Label l = (Label)myDataTemplate.FindName("result_Label", myContentPresenter);
            Button bRefuel = sender as Button;
            Button bCare = (Button)myDataTemplate.FindName("Care_Button", myContentPresenter);
            Button bDrive = (Button)myDataTemplate.FindName("Drive_Button", myContentPresenter);

            bRefuel.IsEnabled = false;
            bCare.IsEnabled = false;
            bDrive.IsEnabled = false;


            p.Foreground = Brushes.Red;
            p.Value = 0;
            MyBackground background = new MyBackground() { bus = b, Length = 12, progressBar = p, result_Label = l, Care = bCare, Reful = bRefuel, Drive = bDrive };
            background.start();
            //MessageBox.Show("The bus " + b.LicenseNumber + " sent for refueling");
        }
        #endregion

        #region care_click_button
        private void care_click_button(object sender, RoutedEventArgs e)
        {
            Bus b = (sender as Button).DataContext as Bus;
            b.care();
            ListBoxItem myListBoxItem = (ListBoxItem)(lbBuses.ItemContainerGenerator.ContainerFromItem(b));
            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            ProgressBar p = (ProgressBar)myDataTemplate.FindName("Time_Before_Ready", myContentPresenter);
            Label l = (Label)myDataTemplate.FindName("result_Label", myContentPresenter);
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
            MyBackground background = new MyBackground() { bus = b, Length = 144, progressBar = p, result_Label = l , Care = bCare, Reful = bRefuel, Drive = bDrive};
            background.start();
           // MessageBox.Show("The bus "+ b.LicenseNumber+" sent for caring");
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
            Label l = (Label)myDataTemplate.FindName("result_Label", myContentPresenter);
            Button bRefuel = (Button)myDataTemplate.FindName("Refuel_Button", myContentPresenter);
            Button bCare = (Button)myDataTemplate.FindName("Care_Button", myContentPresenter);
            Double_Click win = new Double_Click(b) { progressBar = p, label = l, bcare = bCare, brefuel = bRefuel };
            win.ShowDialog();
        }
        #endregion
    }

}
