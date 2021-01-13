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
using BLApi;


namespace PlGui
{
    /// <summary>
    /// Interaction logic for LineWindow.xaml
    /// </summary>
    public partial class LineWindow : Window
    {
        public ObservableCollection<BO.Line> listOfLines { get; set; } = new ObservableCollection<BO.Line>();

        IBL bl = BLFactory.GetBL("2");

        #region constructor
        public LineWindow()
        {
            InitializeComponent();
            foreach (BO.Line line in bl.GetAllLines())
            {
                listOfLines.Add(line);
            }
            listViewLine.ItemsSource= listOfLines;
        }
        #endregion

        #region update_click_button
        private void update_click_button(object sender, RoutedEventArgs e)
        {
           
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
                BO.Line line = (sender as Button).DataContext as BO.Line;

                bl.DeleteLine(line);
            }
            catch(BO.IncorrectLineIDException ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region details_click_button
        private void details_click_button(object sender, RoutedEventArgs e)
        {

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
                    BO.Line line = bl.GetLine(numberLine);
                    LineShow win = new LineShow(line);
                    win.ShowDialog();
                    this.Close();
                }
            }
            catch (BO.IncorrectLineIDException ex)
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

        #region addLine_Click
        /// <summary>
        /// A function that add line when click on button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addLine_Click(object sender, RoutedEventArgs e)
        {
            AddLine window = new AddLine();
            window.ShowDialog();
        }
        #endregion
    }
}
