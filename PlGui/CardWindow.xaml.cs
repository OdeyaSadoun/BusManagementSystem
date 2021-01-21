using BLApi;
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

namespace PlGui
{
    /// <summary>
    /// Interaction logic for CardWindow.xaml
    /// </summary>
    public partial class CardWindow : Window
    {
        public BO.User user { get; set; }
        IBL bl = BLFactory.GetBL();

        public CardWindow(BO.User u)
        {
            InitializeComponent();
            user = u;
        }

      
        private void Balance_Click(object sender, RoutedEventArgs e)
        {
            string b = "Your balance is: ";
            MessageBox.Show(b + user.Balance + "₪", "your balance", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void Charging1_Click(object sender, RoutedEventArgs e)//30
        {        
            
            bl.Charge(30, user);
            MessageBox.Show("Charging 30₪ was performed successfuly", "Charging", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void Charging2_Click(object sender, RoutedEventArgs e)//50
        {
            bl.Charge(50, user);
            MessageBox.Show("Charging 50₪ was performed successfuly", "Charging", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void Charging3_Click(object sender, RoutedEventArgs e)//100
        {
            bl.Charge(100, user);
            MessageBox.Show("Charging 100₪ was performed successfuly", "Charging", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Charging4_Click(object sender, RoutedEventArgs e)//200
        {
            bl.Charge(200, user);
            MessageBox.Show("Charging 200₪ was performed successfuly", "Charging", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //private void Back_Click(object sender, RoutedEventArgs e)
        //{
        //    MainWindow main = new MainWindow();
        //    main.ShowDialog();
        //    Close();
        //}



    }
}
