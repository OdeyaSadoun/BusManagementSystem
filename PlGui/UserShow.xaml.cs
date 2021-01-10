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
    /// Interaction logic for UserShow.xaml
    /// </summary>
    public partial class UserShow : Window
    {
        public BO.User user{ get; set; }
        public UserShow(BO.User u)
        {
            InitializeComponent();
            user = u;
        }

        private void Card_Click(object sender, RoutedEventArgs e)
        {

            CardWindow window = new CardWindow(user);
            window.ShowDialog();
        }
    }
}
