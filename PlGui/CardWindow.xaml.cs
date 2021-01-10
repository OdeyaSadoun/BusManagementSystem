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
        public CardWindow(BO.User u)
        {
            InitializeComponent();
            user = u;
        }

      
        private void Balance_Click(object sender, RoutedEventArgs e)
        {
            string b = "Your balance is:";
            MessageBox.Show(b+user.Balance+ "₪");

        }

        private void Charging1_Click(object sender, RoutedEventArgs e)//30 שקל
        {
            user = sender as BO.User;
            if ((user.Profile == BO.Enums.Profile.Youth)|| (user.Profile == BO.Enums.Veteran)|| (user.Profile == BO.Enums.ExtendedStudent))
                user.Balance += 60;
            else if(user.Profile == BO.Enums.Normal)
                 user.Balance += 37.5;
            else if (user.Profile == BO.Enums.OrdinaryStudent)
                 user.Balance += 45;

        }

        private void Charging2_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Charging3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Charging4_Click(object sender, RoutedEventArgs e)
        {

        }

       
    }
}
