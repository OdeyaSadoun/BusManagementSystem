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
    /// Interaction logic for WindowAddBus.xaml
    /// </summary>
    public partial class WindowAddBus : Window
    {
        public WindowAddBus()
        {
            InitializeComponent(); 
            statusComboBox.ItemsSource = Enum.GetValues(typeof(BusStatus)).Cast<BusStatus>();
            statusComboBox.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
        }

        private void sumMileageTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void statusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            statusComboBox.ItemsSource = Enum.GetValues(typeof(BusStatus)).Cast<BusStatus>();
            

        }
    }
}
