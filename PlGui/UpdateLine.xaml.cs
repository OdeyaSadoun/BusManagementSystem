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
                if (RemoveBox.Text != "")
                {
                    stationToDelete = bl.GetStationInLine(int.Parse(RemoveBox.Text), line.Id);

                    var v = (from item in listOfStations
                             where stationToDelete.LineId == line.Id
                             select item).FirstOrDefault();

                    listOfStations.Remove(v);
                }
                else
                {
                    line.ListOfStationsInLine = listOfStations;
                }

                //הוספת תחנה לרשימת התחנות
                if (AddBox.Text != "")
                {
                    stationToAdd = bl.GetStationInLine(int.Parse(AddBox.Text), line.Id);
                    if (stationToAdd != null)
                        throw new BO.IncorrectCodeStationException(int.Parse(AddBox.Text), "this station is exsist in this line");
                    station = bl.GetStation(int.Parse(AddBox.Text));
                    if(station == null)
                        throw new BO.IncorrectCodeStationException(int.Parse(AddBox.Text), "this station is not exsist in the system");
                    listOfStations.Add(stationToAdd);
                }
                else
                {
                    line.ListOfStationsInLine = listOfStations;
                }

                //עדכון מחיר
                if(CostBox.Text !="")
                {
                    line.Fare = double.Parse(CostBox.Text);
                }
                else
                {
                    line.Fare = line.Fare;
                }
                //הוספת זמן יציאה:
                if(TimeTravel.Text != "")
                {
                    BO.LineTrip lt = new BO.LineTrip() { IsDeleted = false, LineId = line.Id, StartAt = TimeSpan.Parse(TimeTravel.Text) };
                    List<BO.LineTrip> listTimes = (List<BO.LineTrip>)line.ListOfTripTime;
                    listTimes.Add(lt);
                    listTimes.OrderBy(s => s.StartAt).ToList();
                    line.ListOfTripTime = listTimes;

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
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}













        
