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
using BLApi;
namespace PlGui
{
    /// <summary>
    /// Interaction logic for UpdateBus.xaml
    /// </summary>
    public partial class UpdateBus : Window
    {
        IBL bl = BLFactory.GetBL();
        public BO.Bus b { get; set; }
        public UpdateBus(BO.Bus bus)
        {
            InitializeComponent();
            b = bus;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
        }



        private void updating_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((fuelRemainTextBox.Text != null)&&(b!=null))
                    b.FuelRemain = double.Parse(fuelRemainTextBox.Text);
                if ((totalMileageTextBox.Text != null)&& (b != null))
                    b.TotalMileage = double.Parse(totalMileageTextBox.Text);
                if ((isAccessibleCheckBox.IsChecked != null) && (b != null))
                    b.IsAccessible = isAccessibleCheckBox.IsChecked.Value;
                bl.UpdateBus(b);
                Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void totalMileageTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void fuelRemainTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
