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
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using BL;
using BLApi;
using BO;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        IBL bl = BLFactory.GetBL("2");
        
        public Login()
        {
            InitializeComponent();

        }
        
        
        


        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Enter an email.";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Enter a valid email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {
                string email = textBoxEmail.Text;
                string password = passwordBox1.Password;
                BO.User u = new BO.User();
                u = bl.FindUser(email);
                if (u!=null)
                {
                    if (u.Password == password)
                    {
                        if (u.Admin)
                        {
                            AdminShow adminShow = new AdminShow();
                            adminShow.ShowDialog();
                        }
                        else
                        {
                            UserShow userShow = new UserShow();
                            userShow.ShowDialog();
                        }
                                
                    }
                }
                else
                {
                    errormessage.Text = "Sorry! Please enter existing emailid/password.";
                }
                //con.Close();
            }
        }
        private void UserRegister_Click(object sender, RoutedEventArgs e)
        {
            UserWindow user = new UserWindow();
            user.ShowDialog();
            Close();
        }

        private void AdinRegister_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            admin.ShowDialog();
            Close();
        }

        private void textBoxEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
