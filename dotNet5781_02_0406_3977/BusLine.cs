using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace dotNet5781_02_0406_3977
{
    public class BusLine : IComparable

    {

        #region BusNumber
        public int BusNumber { get; set; }
        #endregion

        #region Stations
        public List<BusStation> stations;

        public List<BusStation> Stations
        {
            get { return stations; }
            set
            {
                stations = value;
                if ((stations[0] != FirstStation) || (stations[stations.Count()-1] != LastStation))
                    throw new BusStationExceptions("The departure or destination station does not match the list of stations");

            }
        }

        #endregion

        #region FirstStation
        public BusStation FirstStation { get; set; }
        #endregion

        #region LastStation
        public BusStation LastStation { get; set; }
        #endregion

        #region BusStationArea
        public Area BusStationArea { get; set; }
        #endregion


        //Functions:

        #region variables constructor
        public BusLine(int busNumber, List<BusStation> stations, BusStation firstStation, BusStation lastStation, Area area)
        {
            this.BusNumber = busNumber;
            this.FirstStation = firstStation;
            this.LastStation = lastStation;
            this.Stations = stations;
            this.BusStationArea = area;
            stations[0].bls.Distance = 0;
            stations[0].bls.TravelTime = TimeSpan.Zero;

            for (int i = 1; i < stations.Count; i++)//Updating the fields of travel times and the distance between the stations.
            {
                stations[i].bls.Distance = DistanceBetweenStations(stations[i - 1], stations[i]);
                stations[i].bls.TravelTime = TravelTimeBetweenStations(stations[i - 1], stations[i]);

            }
        }
        #endregion

        #region empty constructor
        public BusLine()
        {
            this.BusNumber = 0;
            this.stations = new List<BusStation> (); //
       //     this.FirstStation = default;
       //     this.LastStation = default;
            this.BusStationArea = Area.General;
        }
        #endregion

        #region AllStations
        /// <summary>
        /// A function that collect all the bus stations in the list 
        /// </summary>
        /// <returns>the busstations in the print shape</returns>
        public string AllStations()
        {
            string allStationStr = "";
            foreach (BusStation bs in stations)
            {
                allStationStr = allStationStr+bs.ToString()+"\n";
            }
            return allStationStr;
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            return string.Format("\nThe bus number is: " + BusNumber + "\t The area is: " + BusStationArea + "\nThe stations are: \n"  + AllStations());
        }
        #endregion

        #region isExsist
        /// <summary>
        /// A Boolean method that checks whether a particular station is on the route of the line
        /// </summary>
        /// <param name="bls"></param>
        /// <returns> true if the bus station exist and false if not</returns>
        public bool isExsist(BusStation bls)
        {
            foreach (BusStation b in stations)
            {
                if (b == bls)
                    return true;
            }
            return false;
        }
        #endregion

        #region removeStation
        /// <summary>
        /// A function that remove station from the list
        /// </summary>
        /// <param name="bs"></param>
        public void removeStation(BusStation bs)
        {
            ExceptionsNonExistence(bs);
            if (stations[0] == bs)//If it's the first station
            {
                FirstStation = stations[1];
                FirstStation.bls.Distance = 0;
                FirstStation.bls.TravelTime = TimeSpan.Zero;
                stations.Remove(stations[0]);
                return;
            }
            if (stations[stations.Count - 1] == bs)//If it's the last station
            {
                LastStation = stations[stations.Count - 2];
                stations.Remove(stations[stations.Count - 1]);
                return;
            }
            for (int i = 1; i < stations.Count - 1; i++)
            {
                if (stations[i] == bs)
                {
                    stations.Remove(stations[i]);
                    stations[i + 1].bls.Distance = DistanceBetweenStations(stations[i - 1], stations[i]);
                    stations[i + 1].bls.TravelTime = TravelTimeBetweenStations(stations[i - 1], stations[i]);
                    return;
                }
            }
        }
        #endregion

        #region addStation

        /// <summary>
        /// The function recives station and put it in the begining of the list
        /// </summary>
        /// <param name="newbs"></param>
        public void addStation(BusStation newbs)
        {
            FirstStation = newbs;
            FirstStation.bls.Distance = 0;
            FirstStation.bls.TravelTime = TimeSpan.Zero;
            stations.Insert(0, FirstStation);
            stations[1].bls.Distance = DistanceBetweenStations(FirstStation, stations[1]);
            stations[1].bls.TravelTime = TravelTimeBetweenStations(FirstStation, stations[1]);
        }

        /// <summary>
        /// The function recives 2 BusStation.
        /// The first is the new station and after the second station we insert to the list
        /// </summary>
        /// <param name="newbls"></param>
        /// <param name="bls"></param>
        /// 
        public void addStation(BusStation newbs, BusStation bs)
        {
            ExceptionsNonExistence(bs);
            if (LastStation == bs) //if the station that we want to add after is the last station
            {
                LastStation = newbs;
                stations.Insert(stations.Count, newbs);
                stations[stations.Count - 1].bls.Distance = DistanceBetweenStations(stations[stations.Count - 2], LastStation);
                stations[stations.Count - 1].bls.TravelTime = TravelTimeBetweenStations(stations[stations.Count - 2], LastStation);
            }
            else
            {
                int index = stations.IndexOf(bs);
                //for (int i = 0; i < stations.Count; i++)
                //{
                //    if (stations[i] == bs)
                //    {
                //        index = i;                    
                //        break;
                //    }
                //}            
                stations.Insert(index + 1, newbs);
                //Updating the fields Distance and TravelTime of the new station. and the station after it
                stations[index + 1].bls.Distance = DistanceBetweenStations(stations[index], stations[index + 1]);
                stations[index + 1].bls.TravelTime = TravelTimeBetweenStations(stations[index], stations[index + 1]);
                //and the station after it.
                stations[index + 2].bls.Distance = DistanceBetweenStations(stations[index + 1], stations[index + 2]);
                stations[index + 2].bls.TravelTime = TravelTimeBetweenStations(stations[index + 1], stations[index + 2]);
            }
        }
        #endregion

        #region DistanceBetweenStations
        /// <summary>
        /// A function that calculate the distance between two stations
        /// </summary>
        /// <param name="bs1"></param>
        /// <param name="bs2"></param>
        /// <returns></returns>
        public double DistanceBetweenStations(BusStation bs1, BusStation bs2)
        {
            ExceptionsNonExistence(bs1, bs2);
            GeoCoordinate c1 = new GeoCoordinate(bs1.BusStationLocation.Latitude, bs1.BusStationLocation.Longitude);
            GeoCoordinate c2 = new GeoCoordinate(bs2.BusStationLocation.Latitude, bs2.BusStationLocation.Longitude);
            double distanceInKm = c1.GetDistanceTo(c2) / 1000;
            return distanceInKm;
        }

        #endregion

        #region travelTimeBetweenStations
        /// <summary>
        ///  A function that calculate the travel time between two stations
        /// </summary>
        /// <param name="bs1"></param>
        /// <param name="bs2"></param>
        /// <returns></returns>
        public TimeSpan TravelTimeBetweenStations(BusStation bs1, BusStation bs2)
        {
            TimeSpan Duration;
            if (bs1.bls.TravelTime.Ticks<bs2.bls.TravelTime.Ticks)
            {
                Duration = new TimeSpan(bs2.bls.TravelTime.Hours - bs1.bls.TravelTime.Hours,
                bs2.bls.TravelTime.Minutes - bs1.bls.TravelTime.Minutes,
                bs2.bls.TravelTime.Seconds - bs1.bls.TravelTime.Seconds);
            }
            else
            {
                Duration = new TimeSpan(bs1.bls.TravelTime.Hours - bs2.bls.TravelTime.Hours,
                bs1.bls.TravelTime.Minutes - bs2.bls.TravelTime.Minutes,
                bs1.bls.TravelTime.Seconds - bs2.bls.TravelTime.Seconds);
            }
            return Duration;
        }
        #endregion

        #region subRoute
        /// <summary>
        /// The method receives 2 stations
        /// and returns A bus line object that actually represented the section of the line between the two stations(including)
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="bs1"></param>
        /// <param name="bs2"></param>
        /// <returns>A function that returns a sub-trajectory of the line</returns>
        public BusLine subRoute(BusStation bs1, BusStation bs2)
        {
            ExceptionsNonExistence(bs1, bs2);
            BusLine newbl = new BusLine();
            newbl.FirstStation = bs1;
            newbl.LastStation = bs2;
            int index1 = -1;
            int index2 = -1;
            for (int i = 0; i < stations.Count; i++)
            {
                if (stations[i] == bs1)
                    index1 = i;
                if (stations[i] == bs2)
                    index2 = i;
                if ((index1 != -1) && (index2 != -1))
                    break;
            }
            int j = 0;
            for (int i = index1; i <= index2; i++)
            {
                newbl.stations.Insert(j, this.stations[i]);
                j++;
            }
            newbl.BusNumber = this.BusNumber;
            newbl.BusStationArea = this.BusStationArea;
            return newbl;
        }
        #endregion

        #region ExceptionsNonExistence
        /// <summary>
        /// A function that check if the stations are exsist in the list
        /// if one from them don't exist - the functioun throw exption.
        /// </summary>
        /// <param name="bs1"></param>
        /// <param name="bs2"></param>
        public void ExceptionsNonExistence(BusStation bs1, BusStation bs2)
        {
            bool flag1 = isExsist(bs1);
            bool flag2 = isExsist(bs2);
            if ((!flag1) || (!flag2))
                throw new BusStationExceptions("One or more of the station aren't exsist in the list");
        }

        /// <summary>
        /// A ExistenceNonExistence loaded function
        /// </summary>
        /// <param name="bs"></param>
        public void ExceptionsNonExistence(BusStation bs)
        {
            bool flag = isExsist(bs);
            if (!flag)
                throw new BusStationExceptions("The bus station dosn't exist in the list");
        }
        #endregion

        #region TotalTimeBetweenSomeStations
        /// <summary>
        /// teh function recives 2 bus stations and check the total travel time between their.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns>the total time</returns>
        public TimeSpan TotalTimeBetweenSomeStations(BusStation source, BusStation destination)
        {
            ExceptionsNonExistence(source, destination);
            if(stations.IndexOf(destination)<stations.IndexOf(source))
            { //change the location between the buses, if they was sent in the inversion order.
                BusStation tempBusStation = source;
                source = destination;
                destination = tempBusStation;
            }
            TimeSpan tempTimeSpan = TimeSpan.Zero;
            for (int i = stations.IndexOf(source); i <= stations.IndexOf(destination); i++)
            {
                tempTimeSpan = tempTimeSpan + stations[i].bls.TravelTime;
            }
            return tempTimeSpan;
        }
        #endregion

        #region CompareTo
/// <summary>
/// A function that comper between 2 total travel time of busline
/// </summary>
/// <param name="obj"></param>
/// <returns>1 if the first is more time, 0 if there are equals and -1 if the seconds is more time</returns>
        public int CompareTo(object obj)
        {
            BusLine bl = (BusLine)obj;
            TimeSpan myTotalTime = TotalTimeBetweenSomeStations(this.FirstStation, this.LastStation);
            TimeSpan objTotalTime = TotalTimeBetweenSomeStations(bl.FirstStation, bl.LastStation);
            if (myTotalTime < objTotalTime)
                return -1;
            if (myTotalTime > objTotalTime)
                return 1;
            return 0;
        }
        #endregion

        #region ChoiceOfBuses
        public BusLine ChoiceOfBuses(BusStation source, BusStation destination, BusLine otherBl)
        {
            ExceptionsNonExistence(source, destination);
            BusLine thistemp = this;
            BusLine othertemp = otherBl;
            //update temp1:
            thistemp.FirstStation = source;
            thistemp.LastStation = destination;
            //update temp2:
            othertemp.FirstStation = source;
            othertemp.LastStation = destination;
            //int i = 0; // delete the stations befor
            //while (temp1.stations[i] != bs)
            //{
            //    temp1.removeStation(temp1.stations[i]);
            //}
            //while (temp1.stations[i] != bs)
            //{
            //    temp1.removeStation(temp1.stations[i]);
            //}
            int priority = thistemp.CompareTo(othertemp);
            if (priority < 0) //temp2 shorter
                return otherBl;
            return this; //temp1 shorter or they are equals

        }
        #endregion

   
    }
}
