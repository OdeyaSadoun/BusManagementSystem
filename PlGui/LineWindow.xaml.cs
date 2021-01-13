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
        public LineWindow()
        {
            InitializeComponent();
            foreach (BO.Line line in bl.GetAllLines())
            {
                listOfLines.Add(line);
            }
            listViewLine.ItemsSource= listOfLines;
        }

        private void update_click_button(object sender, RoutedEventArgs e)
        {
           
        }

        private void delete_click_button(object sender, RoutedEventArgs e)
        {

            BO.Line line = (sender as Button).DataContext as BO.Line;

           
        }

        private void details_click_button(object sender, RoutedEventArgs e)
        {

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

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
        private void searchLine_TextChanged(object sender, TextChangedEventArgs e)
        {


        }

        private void addLine_Click(object sender, RoutedEventArgs e)
        {
            AddLine window = new AddLine();
            window.ShowDialog();
        }
    }
}
