﻿using BLApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for BusWindowPassnger.xaml
    /// </summary>
    public partial class BusWindowPassnger : Window
    {
        public ObservableCollection<BO.Bus> listOfBuses { get; set; } = new ObservableCollection<BO.Bus>();
        IBL bl = BLFactory.GetBL();

        #region constructor
        public BusWindowPassnger()
        {
            InitializeComponent();


            listViewBus.ItemsSource = bl.GetAllBuses().ToList();
        }
        #endregion

        #region details_click_button
        private void details_click_button(object sender, RoutedEventArgs e)
        {
            BO.Bus bus = (sender as Button).DataContext as BO.Bus;
            BusShow win = new BusShow(bus);
            win.ShowDialog();
        }
        #endregion

        #region NumberValidationTextBox
        /// <summary>
        /// A function that dont anable anter chars other numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion

        #region Grid_KeyUp
        /// <summary>
        /// A function that when do enter the input in to the system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.Key == Key.Enter)
                {
                    var temp = searchBus.Text;

                    var licenseNumber = int.Parse(searchBus.Text);
                    BO.Bus bus = bl.GetBus(licenseNumber);
                    BusShow win = new BusShow(bus);
                    win.ShowDialog();
                    this.Close();
                }
            }
            catch (BO.IncorrectLicenseNumberException ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region searchBus_TextChanged
        private void searchBus_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        #endregion

     
        #region listViewBus_SelectionChanged
        private void listViewBus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion

        #region listViewBus_PreviewMouseDoubleClick
        private void listViewBus_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.Bus bus = (sender as ListBox).SelectedItem as BO.Bus;
            BusShow win = new BusShow(bus);
            win.ShowDialog();
        }
        #endregion
    }
}
