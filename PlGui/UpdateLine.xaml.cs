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

                BO.StationInLine stationToDelete = new BO.StationInLine();
                BO.StationInLine stationToAdd = new BO.StationInLine();
                BO.Station station = new BO.Station();

                //BO.Line line = (sender as Button).DataContext as BO.Line;
                List<BO.StationInLine> listOfStations = bl.GetAllStationesInLineBy(line.Id).ToList();


                //מחיקת תחנה מרשימת התחנות
                if (RemoveBox.Text != null)
                {
                    stationToDelete = bl.GetStationInLine(int.Parse(RemoveBox.Text), line.Id);

                    var v = (from item in listOfStations
                             where stationToDelete.LineId == line.Id
                             select item).FirstOrDefault();

                    listOfStations.Remove(v);
                }

                //הוספת תחנה לרשימת התחנות
                if (AddBox.Text != null)
                {
                    stationToAdd = bl.GetStationInLine(int.Parse(AddBox.Text), line.Id);
                    if (stationToAdd != null)
                        throw new BO.IncorrectCodeStationException(int.Parse(AddBox.Text), "this station is exsist in this line");
                    station = bl.GetStation(int.Parse(AddBox.Text));
                    if(station == null)
                        throw new BO.IncorrectCodeStationException(int.Parse(AddBox.Text), "this station is not exsist in the system");
                    listOfStations.Add(stationToAdd);
                }

                //עדכון מחיר
                if(CostBox.Text !=null)
                {
                    line.Fare = double.Parse(CostBox.Text);
                }
                    //bl.DeleteStationInLine()
                    //listOfStations = temp;// כרגע זוהי רשימת תחנות הקו בקו הנוכחי
                    //temp.Clear();//נאפס את הרשימה לצורך שימוש חוזר
                    //if (RemoveBox.Text != null)
                    //{
                    //    foreach(BO.StationInLine s in listOfStations)
                    //    {
                    //        if (s.StationCode != int.Parse(RemoveBox.Text))
                    //            temp.Add(s);
                    //    }
                    //    line.ListOfStationsInLine = temp;//עדכון רשימת התחנות של הקו

                    //}
                    ////הוספת תחנה לקו 
                    //temp.Clear();//נאפס את הרשימה לצורך שימוש חוזר
                    //if (AddBox.Text!=null)
                    //{
                    //    foreach (BO.StationInLine s in line.ListOfStationsInLine)
                    //    {
                    //        if (s.StationCode == int.Parse(AddBox.Text))
                    //            throw new BO.IncorrectCodeStationException(int.Parse(AddBox.Text), "The station is exsist in this line. you can not add it again");              
                    //    }
                    //    //אם התחנה לא קיימת במסלול
                    //    temp = line.ListOfStationsInLine.ToList();
                    //    temp.Add(int.Parse(AddBox.Text))
                    //    line.ListOfStationsInLine = temp;
                    //}
                    bl.UpdateLine(line);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}













        
