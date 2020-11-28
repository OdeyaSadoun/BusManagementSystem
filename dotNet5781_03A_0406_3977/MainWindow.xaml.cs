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
using dotNet5781_02_0406_3977;

namespace dotNet5781_03A_0406_3977
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Random rnd = new Random(DateTime.Now.Millisecond);
        BusLines BusCollection = new BusLines();
        private BusLine currentDisplayBusLine;


        /// <summary>
        /// constructor
        /// </summary>
        #region constructor
        public MainWindow()
        {

            InitializeComponent();

            #region Create stock of stations
            string[] streets = { "Rashbam ", "HaChida ", "Haatzmaut ", "Hertzel ", "Jerusalem ", "Halevi ", "Haneviim ", "Hatamar " , "Trumpeldor ", "Menachem Begin ", "Rotschild " , "Yahalom ", "Hayam ", "Ein Gedi ", "Hashikma " };
            BusStation[] bs = new BusStation[50];
            for (int i = 0; i < bs.Length; i += 5)
            {
                BusStation.BusLineStation bls = new BusStation.BusLineStation();
                bs[i] = new BusStation(streets[rnd.Next(0, streets.Length)] + rnd.Next(1, (i + 4) * 10));
                bs[i + 1] = new BusStation(streets[rnd.Next(0, streets.Length)] + rnd.Next(1, (i + 1) * 10));
                bs[i + 2] = new BusStation(streets[rnd.Next(0, streets.Length)] + rnd.Next(1, (i + 2) * 10),bls);
                bs[i + 3] = new BusStation(streets[rnd.Next(0, streets.Length)] + rnd.Next(1, (i + 3) * 10), bls);
                bs[i + 4] = new BusStation(streets[rnd.Next(0, streets.Length)] + rnd.Next(1, (i + 5) * 10));
            }
            #endregion

            List<BusLine> busLineLst = new List<BusLine>();
            for(int i = 1; i <= 10; i++)//10 buses
            {
                BusStation[] tempBs = new BusStation[bs.Length]; //ניצור מערך תחנות חדש בכל פעם לצורך אפשרות מחיקת התחנות
                bs.CopyTo(tempBs,0);
                BusLine bl = new BusLine();
                int tempNumber = rnd.Next(1, 1000); //the number of the bus

                List<BusStation> stat = new List<BusStation>(); // list of stations
                int tempStation = rnd.Next(2, 20); //number of the stations in the bus
                for (int j = 1; j <= tempStation; j++)
                {
                    int temp = rnd.Next(1, 50); // take station from array
                    while(tempBs[temp] == default)//לפני ההגרלה נוודא כי התחנה שהוגרלה באמת קיימת, ואם לא, נגריל שנית 
                        temp = rnd.Next(1, 50);
                    stat.Add(tempBs[temp]);
                    tempBs[temp] = default;// במערך התחנות החדש נמחק כל פעם את התחנה שכבר השתמשנו בה על מנת שלא תהיינה תחנות כפולות ברשימת התחנות
                }
                bl.BusNumber = tempNumber; 
                bl.stations = stat;
                bl.FirstStation = stat[0];
                bl.LastStation = stat[stat.Count-1];
                int tempArea = rnd.Next(1, 7);
                bl.BusLineArea = (Area)tempArea;
                //בדיקה האם האוטובוס הנוכחי שהוגרל קיים כבר במערכת על כל חלקיו (כלומר, לא רק מספר הקו זהה, שהרי קיימים בארץ אוטובוסים שונים בעלי מספר זהה)
                bool isExist = false;
                foreach (BusLine tempBl in busLineLst)
                {
                    if (tempBl == bl)
                        isExist = true;
                }
                if (isExist)
                    i--; //if the bus is exsist do a raffle again
                else
                    busLineLst.Add(bl); // if the bus isn't exsist add him to the list
            }

            BusCollection.Busess = busLineLst;

            cbBusLines.ItemsSource = BusCollection;
            cbBusLines.DisplayMemberPath = "BusNumber";
            cbBusLines.SelectedIndex = 0;
            

        }
        #endregion

        #region  ShowBusLine
        /// <summary>
        /// A function that displays the information about the bus line
        /// </summary>
        /// <param name="index"></param>
        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = BusCollection[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.Stations;
        }
        #endregion

        #region tbArea_TextChanged
        /// <summary>
        /// A function that displays the bus area inside the textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbArea_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        #endregion

        #region bBusLines_SelectionChanged
        /// <summary>
        /// A function that displays the bus number inside the combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).BusNumber);
        }

        #endregion

        #region lbBusLineStations_SelectionChanged
        /// <summary>
        /// A function that displays all the details for stations of the bus that selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbBusLineStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion
    }
}
