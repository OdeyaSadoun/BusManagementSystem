using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BL;
using BO;
using BLApi;
namespace PlGui
{
    /// <summary>
    /// Interaction logic for LineShow.xaml
    /// </summary>
    public partial class LineShow : Window
    {
        IBL bl = BLFactory.GetBL("2");
        
        public ObservableCollection<BO.Line> listOfLines { get; set; } = new ObservableCollection<BO.Line>();

        public LineShow()
        {
            InitializeComponent();            
            foreach(BO.Line line in bl.GetAllLines())
            {
                listOfLines.Add(line);
            }
            lvLines.ItemsSource = listOfLines;
        }
        
    }
}
