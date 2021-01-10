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
            user = (sender as Button).DataContext as BO.User;
            if ((user.UserProfile == BO.Profile.Youth)|| (user.UserProfile == BO.Profile.Veteran)|| (user.UserProfile == BO.Profile.ExtendedStudent))
                user.Balance += 60;
            else if(user.UserProfile == BO.Profile.Normal)
                 user.Balance += 37.5;
            else if (user.UserProfile == BO.Profile.OrdinaryStudent)
                 user.Balance += 45;
            
        }

        private void Charging2_Click(object sender, RoutedEventArgs e)//50
        {
            user = (sender as Button).DataContext as BO.User;
            if ((user.UserProfile == BO.Profile.Youth) || (user.UserProfile == BO.Profile.Veteran) || (user.UserProfile == BO.Profile.ExtendedStudent))
                user.Balance += 100;
            else if (user.UserProfile == BO.Profile.Normal)
                user.Balance += 62.5;
            else if (user.UserProfile == BO.Profile.OrdinaryStudent)
                user.Balance += 75;
        }
        private void Charging3_Click(object sender, RoutedEventArgs e)//100
        {
            user = (sender as Button).DataContext as BO.User;
            if ((user.UserProfile == BO.Profile.Youth) || (user.UserProfile == BO.Profile.Veteran) || (user.UserProfile == BO.Profile.ExtendedStudent))
                user.Balance += 200;
            else if (user.UserProfile == BO.Profile.Normal)
                user.Balance += 125;
            else if (user.UserProfile == BO.Profile.OrdinaryStudent)
                user.Balance += 150;
        }

        private void Charging4_Click(object sender, RoutedEventArgs e)//200
        {
            user = (sender as Button).DataContext as BO.User;
            if ((user.UserProfile == BO.Profile.Youth) || (user.UserProfile == BO.Profile.Veteran) || (user.UserProfile == BO.Profile.ExtendedStudent))
                user.Balance += 400;
            else if (user.UserProfile == BO.Profile.Normal)
                user.Balance += 250;
            else if (user.UserProfile == BO.Profile.OrdinaryStudent)
                user.Balance += 300;
        }

       
    }
}
