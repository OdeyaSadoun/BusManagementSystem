using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Tamar Gavrieli 322533977 && Odeya Sadoun 212380406

namespace dotNet5781_02_0406_3977
{
    class Program
    {
        #region EnterBusStation
        /// <summary>
        /// A function that enter bus station to the list
        /// </summary>
        /// <returns>the new station</returns>
        public static BusStation EnterBusStation()
        {
            Console.WriteLine("Do you want to enter busLine's adress, if you want enter 1, else enter 0");
            int temp = int.Parse(Console.ReadLine());
            string adress;
            if (1 == temp)
            {
                Console.WriteLine("Enter the adress");
                adress = Console.ReadLine();
            }
            else
                adress = "";
            BusStation bs = new BusStation(adress);

            return bs;

        }
        #endregion

        #region EnterBusLine
        /// <summary>
        /// A function that enter bus line to the list
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="bs"></param>
        /// <returns>the new bus line</returns>
        public static BusLine EnterBusLine(BusLines bl, BusStation[]bs)
        {
            Console.WriteLine("Enter busLine's number");
            int busNumber = int.Parse(Console.ReadLine());
            List<BusLine> lst = bl.Busess;
            int intd = lst.FindIndex(item => item.BusNumber == busNumber); // check if the bus with this index exist in the list
            if (intd != -1) // if it's exist
                throw new BusStationExceptions("The bus is exsist");

            BusLine tempBus = new BusLine();//create new bus

             tempBus.BusNumber = busNumber;
            Console.WriteLine("Enter 1 to General, Enter 2 to North, Enter 3 to South, Enter 4 to Center, Enter 5 to Jerusalem");
            int tempArea = int.Parse(Console.ReadLine());
            if((tempArea == 1) || (tempArea == 2) || (tempArea == 3) || (tempArea == 4) || (tempArea == 5))
                tempBus.BusLineArea = (Area)tempArea;
            else
                tempBus.BusLineArea = Area.Other;

            //לצורך רשימת תחנות האוטובוס נגריל מספר רנדומלי
            // בין 1-20 עבור רשימת התחנות שתהיה בקו
            //לאחר מכן נעבור בלולאת פור כמות הפעמים שהוגרלה ובכל פעם נגריל מספר בין 1-50
            //והמספר שיצא המיקום שלו במאגר התחנות יכנס לרשימת התחנות
            List<BusStation> stat = new List<BusStation>();
            Random rnd = new Random();
            int tempRnd = rnd.Next(2, 20);
            for(int i=1; i<=tempRnd; i++)
            {
                int temp = rnd.Next(1, 50);
                stat.Add(bs[temp]);
            }
            tempBus.stations = stat;
            tempBus.FirstStation = stat[0];
            tempBus.LastStation = stat[stat.Count-1];
            return tempBus;
        }
        #endregion

        #region FindStationByKey
        /// <summary>
        /// A function that recives busStation key
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="key"></param>
        /// <returns>the bus station that the key is</returns>
        public static BusStation FindStationByKey(BusLine bl , int key)
        {
            BusStation bs = default;
            foreach (BusStation b in bl.stations)
            {
                if (key == b.BusStationKey)
                {
                    bs = b;
                    break;
                }
            }
            //if (!isExsist)
            //    throw new BusStationExceptions("The station does not exist");
            return bs;
        }
        #endregion

        public static int FindKeyByStation(BusStation bs,BusLine bl)
        {
            int index = -1;
            for (int i = 0; i < bl.stations.Count; i++)
            {
                if (bl.stations[i] == bs)
                    index = i;
            }
            return index;
        }
        static void Main(string[] args)
        {

            Random r = BusStation.rnd;

            #region Create stock of stations
            string[] streets = { "Rashbam ", "HaChida ", "Haatzmaut ", "Hertzel ", "Jerusalem ", "Halevi ", "Haneviim " };
            BusStation[] bs = new BusStation[50];
            for (int i = 0; i < bs.Length; i += 5)
            {
                BusStation.BusLineStation bls = new BusStation.BusLineStation();
                bs[i] = new BusStation();
                bs[i + 1] = new BusStation(streets[r.Next(0, streets.Length)] + r.Next(1, (i + 1) * 10));
                bs[i + 2] = new BusStation(bls);
                bs[i + 3] = new BusStation(streets[r.Next(0, streets.Length)] + r.Next(1, (i + 3) * 10), bls);
                bs[i + 4] = new BusStation(streets[r.Next(0, streets.Length)] + r.Next(1, (i + 1) * 10));
            }
            #endregion

            #region Create lists of stations
            List<BusStation> lst1 = new List<BusStation> { bs[5], bs[6], bs[8], bs[7], bs[17], bs[16], bs[15], bs[21], bs[10] };
            List<BusStation> lst2 = new List<BusStation> { bs[11], bs[9], bs[10], bs[12], bs[5], bs[13], bs[14], bs[18], bs[19], bs[20], bs[22], bs[23], bs[24] };
            List<BusStation> lst3 = new List<BusStation> { bs[25], bs[27], bs[29], bs[26], bs[18], bs[30], bs[31] };
            List<BusStation> lst4 = new List<BusStation> { bs[32], bs[37], bs[33], bs[34], bs[39], bs[35], bs[36] };
            List<BusStation> lst5 = new List<BusStation> { bs[38], bs[42], bs[43], bs[46], bs[48], bs[49], bs[40], bs[41], bs[44], bs[45], bs[47], bs[2] };
            List<BusStation> lst6 = new List<BusStation> { bs[1], bs[2], bs[3], bs[4], bs[5], bs[6], bs[7] };
            List<BusStation> lst7 = new List<BusStation> { bs[8], bs[9], bs[10], bs[4], bs[5], bs[11] };
            List<BusStation> lst8 = new List<BusStation> { bs[5], bs[7], bs[9], bs[12], bs[13], bs[14], bs[15], bs[2], bs[3], bs[6], bs[8], bs[10] };
            List<BusStation> lst9 = new List<BusStation> { bs[1], bs[7], bs[3], bs[2], bs[9], bs[12],bs[28], bs[13], bs[45], bs[47]};
            List<BusStation> lst10 = new List<BusStation> { bs[27], bs[29], bs[26], bs[17], bs[16], bs[15], bs[21], bs[10], bs[40], bs[41], bs[44], bs[45] };
            #endregion

            #region Create stock of buses
            BusLine bl1 = new BusLine(401, lst1, bs[5], bs[10], Area.Center);
            BusLine bl2 = new BusLine(39, lst2, bs[11], bs[24], Area.Jerusalem);
            BusLine bl3 = new BusLine(947, lst3, bs[25], bs[31], Area.North);
            BusLine bl4 = new BusLine(426, lst4, bs[32], bs[36], Area.Center);
            BusLine bl5 = new BusLine(402, lst5, bs[38], bs[2], Area.Center);
            BusLine bl6 = new BusLine(277, lst6, bs[1], bs[7], Area.Center);
            BusLine bl7 = new BusLine(400, lst7, bs[8], bs[11], Area.Jerusalem);
            BusLine bl8 = new BusLine(179, lst8, bs[5], bs[10], Area.North);
            BusLine bl9 = new BusLine(3, lst9, bs[1], bs[47], Area.Center);
            BusLine bl10 = new BusLine(12, lst10, bs[27], bs[45], Area.Center);
            #endregion

            List<BusLine> tempLstBusLine = new List<BusLine> { bl1, bl2, bl3, bl4, bl5, bl6, bl7, bl8, bl9, bl10 }; // creat list with all the busess 

            BusLines allBusLines = new BusLines(tempLstBusLine); //the collection of the busess


            Menu choice;
            int busNumber = -1;
            int stationKey = -1;
            BusLine bus = default;
            BusStation station = default;

            do
            {
                Console.WriteLine("\nEnter 1 to add new busLine\n" +
                                    "Enter 2 to add station to busLine\n" +
                                    "Enter 3 to remove busLine\n" +
                                    "Enter 4 to remove station from the busLine roat\n" +
                                    "Enter 5 to search busLine in spesific station by key\n" +
                                    "Enter 6 to find the options to travel between two stations\n" +
                                    "Enter 7 to print all the busLine\n" +
                                    "Enter 8 to print all the bus stations and all the busLines that go from their\n" +
                                    "Enter 9 to exit\n");
                choice = (Menu)int.Parse(Console.ReadLine());
                try
                {
                    switch (choice)
                    {

                        case Menu.addBusLine:
                            bus = EnterBusLine(allBusLines, bs);
                            allBusLines.AddBus(bus);
                            break;

                        case Menu.addBusStationToBusLine:
                            Console.WriteLine("Enter the line number to which you want to enter the requested station ");
                            busNumber = int.Parse(Console.ReadLine());
                            bus = allBusLines[busNumber]; // if the bus isn't exist the indexer throw exption
                            station = EnterBusStation();
                            allBusLines[busNumber].AddStation(station);
                            //create new array to bus stations:
                            BusStation [] newbs = new BusStation[bs.Length + 1];
                            for (int i = 0; i < newbs.Length-1; i++)
                                newbs[i] = bs[i];
                            newbs[newbs.Length - 1] = station;

                            break;

                        case Menu.removeBusLine:
                            Console.WriteLine("Enter the bus number that you want to remove ");
                            busNumber = int.Parse(Console.ReadLine());
                            bus = allBusLines[busNumber];
                            if (!allBusLines.IsExsist(bus))
                                throw new BusStationExceptions("The bus isn't exsist");
                            allBusLines.RemoveBus(bus);
                            break;

                        case Menu.removeBusStationFromBusLine:
                            Console.WriteLine("Enter the bus number that you want to remove station from him");
                            busNumber = int.Parse(Console.ReadLine());
                            bus = allBusLines[busNumber];
                            Console.WriteLine("Enter the bus statiom key that you want to remove");
                            stationKey = int.Parse(Console.ReadLine());
                            station = FindStationByKey(bus, stationKey);

                            bus.RemoveStation(station);
                            break;

                        case Menu.searchBusLinesInStation://search buses in specific bus station
                            Console.WriteLine("Enter the bus station key");
                            stationKey = int.Parse(Console.ReadLine());
                            List<BusLine> busesLst = allBusLines.BusesInBusStation(stationKey);
                            BusLines tempBuses = new BusLines(busesLst);
                            Console.WriteLine("The buses that go through this station are:");
                            Console.WriteLine(tempBuses);//לאחר שמצאנו את רשימת הקווים העוברים בתחנה הרצויה נדפיס אותם
                            break;

                        case Menu.printOptionToTravelBetweenBusStation:
                            Console.WriteLine("Enter your key of source bus station");//קליטת המקור
                            int intsource = int.Parse(Console.ReadLine());
                            BusStation source = default;
                            foreach (BusLine bl in allBusLines)
                            {
                                source = FindStationByKey(bl, intsource);
                                if (source != default)
                                    break;
                            }
                            if(source == default)
                                throw new BusStationExceptions("The station does not exist");


                            Console.WriteLine("Enter your destination bus station");//קליטת היעד
                            int intdestination = int.Parse(Console.ReadLine());
                            BusStation destination = default;
                            foreach (BusLine bl in allBusLines)
                            {
                                destination = FindStationByKey(bl, intdestination);
                                if (destination != default)
                                    break;
                            }
                            if (destination == default)
                                throw new BusStationExceptions("The station does not exist");

                            List<BusLine> optionToTravel = new List<BusLine>();
                            foreach (BusLine bl in allBusLines)
                            {
                                if (bl.IsExsist(source) && bl.IsExsist(destination))
                                    if (FindKeyByStation(source,bl)<= FindKeyByStation(destination,bl))
                                        optionToTravel.Add(bl);
                            }
                            //הגענו כעת לרשימה שמכילה את כל הקווים שבהם קיימות את תחנת המוצא והיעד

                            if (optionToTravel.Count == 0) // if the list is empty
                                throw new BusStationExceptions("you can't travel with the station that do you want");

                            List<BusLine> optionToTravelBetweenStations = new List<BusLine>();
                            foreach (BusLine bl in optionToTravel)
                            {
                                optionToTravelBetweenStations.Add(bl.SubRoute(source, destination));
                            }//הגענו לרשימה שמכילה את תתי המסלולים מתחנת המוצא ותחנת היעד
                            optionToTravelBetweenStations.Reverse(); //הופך את סדר הרשימה על מנת שנעשה את המיון כך שהכי למעלה יהיה את המהיר ביותר
                            BusLines newBusLines = new BusLines(optionToTravelBetweenStations);//freate new bus lines object
                            newBusLines.SortByTravelTime();

                            Console.WriteLine("The options are:");
                            Console.WriteLine(newBusLines);
                            break;

                        case Menu.printAllTheBusses:
                            foreach (BusLine bl in allBusLines)
                            {
                                Console.WriteLine(bl);
                            }
                            break;

                        case Menu.printAllBusStations://הפונקציה עוברת על כל מאגר התחנות
                            //שבמאגר זה קיימות התחנות של כל האוטובוסים
                            // לכל תחנה תודפס רשימת הקווים העוברים בה
                            List<BusLine> tempLst = new List<BusLine>();
                            for (int i = 1; i <= bs.Length-1; i++)
                            {
                                Console.WriteLine("station number:" + i);
                                Console.WriteLine("the buses that go through the stations are:");
                               // if(!ifNewArray)
                                    tempLst = allBusLines.BusesInBusStation(bs[i].BusStationKey);
                               // else
                                //    tempLst = allBusLines.BusesInBusStation(newbs[i].BusStationKey);

                                BusLines tempBusLines = new BusLines(tempLst);
                                Console.WriteLine(tempBusLines);
                            }
                            break;

                        case Menu.exit:
                            Console.WriteLine("Bye");
                            break;

                        default:
                            throw new ArgumentException("Invalid number, press again");

                    }

                }
                catch (BusStationExceptions bsEx) // catch to our class to special busStation exeptions
                {
                    Console.WriteLine(bsEx.Message);
                }
                catch (Exception ex)//catch for other exeptions
                {
                    Console.WriteLine(ex.Message);
                }


            } while (choice != Menu.exit);

        }
    }
}


