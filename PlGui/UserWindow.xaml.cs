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
using System.Windows.Navigation;
using System.Windows.Shapes;
//using DalApi;
//using DO;
using BO;
using BLApi;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        IBL bl = BLFactory.GetBL();
        public UserWindow()
        {
            InitializeComponent();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();

            Close();
        }
        public void ResetDetails()
        {
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxEmail.Text = "";
            dataPickerBirthday.DisplayDate = DateTime.Today;
            passwordBox1.Password = "";
            passwordBoxConfirm.Password = "";
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            ResetDetails();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmail.Text.Length == 0)
            {// לבדוק את התנאי
             //   לשלוח חריגה ומהחריגה להקפיץ את ההודעה
                errormessage.Text = "Enter an email.";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {//תנאי זה בודק את תקינות הדוא"ל
                errormessage.Text = "Enter a valid email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {
                string firstname = textBoxFirstName.Text;
                string lastname = textBoxLastName.Text;
                string email = textBoxEmail.Text;
                string password = passwordBox1.Password;
                if (passwordBox1.Password.Length == 0)//אם לא הוכנס סיסמא
                {
                    errormessage.Text = "Enter password.";
                    passwordBox1.Focus();
                }
                else if (passwordBoxConfirm.Password.Length == 0)//אימות סיסמאת במידה ולא הוכנס
                {
                    errormessage.Text = "Enter Confirm password.";
                    passwordBoxConfirm.Focus();
                }
                else if (passwordBox1.Password != passwordBoxConfirm.Password)//אם הסיסמאות לא זהות
                {
                    errormessage.Text = "Confirm password must be same as password.";
                    passwordBoxConfirm.Focus();
                }
                else//הקלט תקין
                {
                    errormessage.Text = "";
                    BO.User u1 = new BO.User() { UserName = email, Password = password, FirstName = firstname, LastName = lastname, IsDeleted = false, Birthday = dataPickerBirthday.DisplayDate, Admin = false, Balance = 0 };
                    if (YearsPassed18())
                    {
                        u1.UserProfile = Profile.Normal;
                    }
                    else
                        u1.UserProfile = Profile.Youth;
                    bl.AddUser(u1);
                    //string address = textBoxAddress.Text;                  
                    errormessage.Text = "You have Registered successfully.";
                    ResetDetails();
                }
            }
        }
        public bool YearsPassed18()
        //A function that cheak if passed year from the date begin
        {
            DateTime zeroTime = new DateTime(1, 1, 1);

            var diff = DateTime.Now - dataPickerBirthday.DisplayDate;
            int years = (zeroTime + diff).Year - 1;
            if (years >= 18)
            {
                return true;

            }
            return false;

        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{

        //    System.Windows.Data.CollectionViewSource userViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("userViewSource")));
        //    // Load data by setting the CollectionViewSource.Source property:
        //    // userViewSource.Source = [generic data source]
        //    System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
        //    // Load data by setting the CollectionViewSource.Source property:
        //    // busViewSource.Source = [generic data source]
        //}

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{

        //    System.Windows.Data.CollectionViewSource userViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("userViewSource")));
        //    // Load data by setting the CollectionViewSource.Source property:
        //    // userViewSource.Source = [generic data source]
        //    System.Windows.Data.CollectionViewSource userViewSource1 = ((System.Windows.Data.CollectionViewSource)(this.FindResource("userViewSource1")));
        //    // Load data by setting the CollectionViewSource.Source property:
        //    // userViewSource1.Source = [generic data source]
        //}
    }
}
