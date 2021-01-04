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
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace dotNet5781_03B_0406_3977
{
    /// <summary>
    /// Interaction logic for EnterDistanceForTravelWindow.xaml
    /// </summary>


    public partial class EnterDistanceForTravelWindow : Window
    {
        //proprties:

        public double KmToTravel { get; set; }
        public Bus Bus { get; set; }
        public ProgressBar psBar { get; set; }

        //functions:

        #region constructor
        public EnterDistanceForTravelWindow(Bus b)
        {
            InitializeComponent();
            Bus = b;
        }
        #endregion

    
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var temp = TextBoxKm.Text;

                Bus.currentMileage = float.Parse(TextBoxKm.Text);

                Bus.goToTravel(Bus.currentMileage);
                this.Close();
            }
        }

        private void TextBoxKm_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
