using BLApi;
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
    /// Interaction logic for LineWindowPassenger.xaml
    /// </summary>
    public partial class LineWindowPassenger : Window
    {
        public ObservableCollection<BO.Line> listOfLines { get; set; } = new ObservableCollection<BO.Line>();

        IBL bl = BLFactory.GetBL("2");
        public Button Update { get; set; }
        public Button Delete { get; set; }
        public BO.User User { get; set; }


        #region constructor
        public LineWindowPassenger()
        {
            InitializeComponent();
            listViewLine.ItemsSource = bl.GetAllLines().ToList();

        }
        #endregion




        #region details_click_button
        private void details_click_button(object sender, RoutedEventArgs e)
        {
            BO.Line line = (sender as Button).DataContext as BO.Line;
            LineShow win = new LineShow(line);
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
                    var temp = searchLine.Text;

                    var numberLine = int.Parse(searchLine.Text);
                    BO.Line line = bl.GetLineNumber(numberLine);
                    LineShow win = new LineShow(line);
                    win.ShowDialog();
                    this.Close();
                }
            }
            catch (BO.IncorrectLineIDException ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region searchLine_TextChanged
        private void searchLine_TextChanged(object sender, TextChangedEventArgs e)
        {


        }
        #endregion

 

        #region listViewLine_SelectionChanged
        private void listViewLine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion

        #region listViewLine_PreviewMouseDoubleClick
        private void listViewLine_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.Line line = (sender as ListBox).SelectedItem as BO.Line;
            LineShow win = new LineShow(line);
            win.ShowDialog();
        }
        #endregion
    }
}
