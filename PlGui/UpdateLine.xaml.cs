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
using BLApi;
namespace PlGui
{
    /// <summary>
    /// Interaction logic for UpdateLine.xaml
    /// </summary>
    public partial class UpdateLine : Window
    {
        public BO.Line line { get; set; }

        IBL bl = BLFactory.GetBL();
        public UpdateLine(BO.Line l)
        {
            InitializeComponent();
            line = l;
        }


        private void AddTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RemoveTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CostTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Updating_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<BO.StationInLine> listOfStations = bl.GetAllStationsInLine().ToList();
                List<BO.StationInLine> temp = new List<BO.StationInLine>();
                foreach (BO.StationInLine l in listOfStations)
                    if (l.LineId == line.Id)
                        temp.Add(l);
                listOfStations = temp;
                temp.Clear();
                if (RemoveBox.Text != null)
                {
                    foreach(BO.StationInLine s in listOfStations)
                    {
                        if (s.StationCode != int.Parse(RemoveBox.Text))
                            temp.Add(s);
                    }
                    line.ListOfStationsInLine = temp;
                    
                }
                List<BO.StationInLine> lst = new List<BO.StationInLine>();
                if (AddBox.Text!=null)
                {
                    foreach (BO.StationInLine s in listOfStations)
                    {
                        if (s.StationCode != int.Parse(AddBox.Text))
                            temp.Add(s);
                    }
                    line.ListOfStationsInLine = temp;
                }
                bl.UpdateLine(line);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}













        
