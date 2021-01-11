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
using System.Collections.ObjectModel;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public ObservableCollection<BO.Line> listOfLines { get; set; } = new ObservableCollection<BO.Line>();
        IBL bl = BLFactory.GetBL("2");
        public MainWindow()
        {
            InitializeComponent();
            foreach (BO.Line line in bl.GetAllLines())
            {
                listOfLines.Add(line);
            }
            lv.ItemsSource = listOfLines;

        }
    
      

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            UserWindow user = new UserWindow();
            user.ShowDialog();
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            admin.ShowDialog();
        }

       
      

        private void ButtonAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow about = new AboutWindow();
            about.ShowDialog();
           
        }

        private void ButtonContact_Click(object sender, RoutedEventArgs e)
        {
            ContactWindow contact = new ContactWindow();
            contact.ShowDialog();
            
        }

        
    }
}
