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
            List<BO.StationInLine> lst = bl.GetAllStationsInLine().ToList();
            stationAdd.ItemsSource = lst;
            stationAdd.DisplayMemberPath = "StationCode";
            stationAdd.SelectedIndex = 0;
        }


        private void AddTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //tamargavrieli18@gmail.com.
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
                    List<BO.LineTrip> listTimes = (List<BO.LineTrip>)line.ListOfTripTime;
                    //ObservableCollection<BO.LineTrip> listTimes = new ObservableCollection<BO.LineTrip>( (List<BO.LineTrip>)line.ListOfTripTime);

                    int countOfTrip = listTimes.Count();
                    BO.LineTrip lt = new BO.LineTrip() { IsDeleted = false, LineId = line.Id, StartAt = TimeSpan.Parse(TimeTravel.Text), Id= countOfTrip+1 };                 
                    listTimes.Add(lt);
                    listTimes.OrderBy(s => s.StartAt).ToList();
                    bl.AddLineTrip(lt);
                    listTimes.OrderBy(s => s.StartAt).ToList();
                    line.ListOfTripTime = listTimes;
                }
                if(stationAdd.Text!="")
                {
                    List<BO.StationInLine> lstStationLine = bl.GetAllStationesInLineBy(line.Id).ToList();
                    int tempCode = int.Parse(stationAdd.Text);
                    foreach (BO.StationInLine b in lstStationLine)
                    {
                        if (b.StationCode == tempCode)
                        {
                            throw new ArgumentException("The station already exsist");
                        }
                    }
                    //List<BO.StationInLine> list = bl.GetAllStationsInLine().ToList();
                    //List<BO.StationInLine> inLine = line.ListOfStationsInLine.ToList();
                    //foreach (BO.StationInLine b in list)
                    //{
                    //    if (b.StationCode == tempCode)
                    //    {
                    //        inLine.Add(b);
                    //        break;
                    //    }

                    //}
                    //line.ListOfStationsInLine = inLine;
                    BO.Station tempStat = bl.GetStation(tempCode);
                    BO.Line tempLine = bl.GetLine(line.Id);
                    bl.AddStationInLine(tempStat, tempLine);
                }

                    bl.UpdateLine(line);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }
    }
}













        
