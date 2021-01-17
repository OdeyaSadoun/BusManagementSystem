using BLApi;
using BO;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        IBL bl = BLFactory.GetBL("2");
        public BO.User User1 { get; set; }
        #region constructor
        public StationWindow(BO.User u)
        {
            InitializeComponent();
            User1 = u;
            //listViewStations.ItemsSource = bl.GetAllStations().ToList();
            //if (User1.Admin)
            //{
            //    addStation.Visibility = Visibility.Visible;
                

            //}
           
            //    IsEnabled = true;
            listViewStations.ItemsSource = bl.GetAllStations().ToList();
            //if (u.Admin)
            //{
            //   listViewStations.FindName("Delete_Button")= Visibility.Visible;
            //}
            //listViewStations.ItemsSource = bl.GetAllStations().ToList();
        }
        #endregion

  

   
        #region update_click_button
        private void update_click_button(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Station station = (sender as Button).DataContext as BO.Station;

                bl.UpdateStation(station);
                listViewStations.ItemsSource = bl.GetAllStations().ToList(); //reftesh
            }
            catch (BO.IncorrectCodeStationException ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region update_click_button
        private void lines_click_button(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Station station = (sender as Button).DataContext as BO.Station;
                //bl.GetStation(station.Code);
                List<BO.ShortLine> linesInStation = station.ListOfLines.ToList();
                //listViewStations.ItemsSource = bl.GetAllStations().ToList(); //reftesh
                LineShowInStation win = new LineShowInStation(linesInStation);
                win.ShowDialog();
                
            }
            catch (BO.IncorrectCodeStationException ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region delete_click_button
        /// <summary>
        /// A function that delete line when the administor click on the button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delete_click_button(object sender, RoutedEventArgs e)
        {
            try
            {
                
                BO.Station station = (sender as Button).DataContext as BO.Station;
                //if (User.Admin)
                //{
                //    (sender as Button).Visibility = Visibility.Visible;
                //    //bDelete.Visibility = Visibility.Visible;
                //}
                bl.DeleteStation(station);
                listViewStations.ItemsSource = bl.GetAllStations().ToList(); //reftesh
            }
            catch (BO.IncorrectCodeStationException ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region details_click_button
        private void details_click_button(object sender, RoutedEventArgs e)
        {
            BO.Station station = (sender as Button).DataContext as BO.Station;
            StationShow win = new StationShow(station);
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
                    var temp = searchStation.Text;

                    var code = int.Parse(searchStation.Text);
                    BO.Station station = bl.GetStation(code);
                    StationShow win = new StationShow(station);
                    win.ShowDialog();
                    this.Close();
                }
            }
            catch (BO.IncorrectCodeStationException ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region searchStation_TextChanged
        private void searchStation_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Button b = (sender as Button);


        }
        #endregion

        #region addStation_Click
        /// <summary>
        /// A function that add line when click on button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addStation_Click(object sender, RoutedEventArgs e)
        {
            AddStation window = new AddStation();
            window.ShowDialog();
            listViewStations.ItemsSource = bl.GetAllStations().ToList();//refresh
        }
        #endregion

        #region listViewStations_PreviewMouseDoubleClick
        private void listViewStations_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.Station station = (sender as ListBox).SelectedItem as BO.Station;
            StationShow win = new StationShow(station);
            win.ShowDialog();
        }
        #endregion
    }
}
