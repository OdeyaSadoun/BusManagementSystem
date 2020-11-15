using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace dotNet5781_02_0406_3977
{
    public class BusLine// : IComparable<BusLine>

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
                Stations = value;
                if ((stations[0] != FirstStation) || (stations[stations.Count()] != LastStation))
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
            this.Stations = stations;
            this.FirstStation = firstStation;
            this.LastStation = lastStation;
            this.BusStationArea = area;
            stations[0].bls.Distance = 0;
            stations[0].bls.TravelTime = TimeSpan.Zero;

            for (int i=1; i<stations.Count; i++)//Updating the fields of travel times and the distance between the stations.
            {
                stations[i].bls.Distance = distanceBetweenStations(stations[i - 1], stations[i]);
                stations[i].bls.TravelTime = travelTimeBetweenStations(stations[i - 1], stations[i]);

            }
        }
        #endregion

        #region empty constructor
        public BusLine()
        {
            this.BusNumber = 0;
            this.Stations = null;
            this.FirstStation = null;
            this.LastStation = null;
            this.BusStationArea = Area.General;
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            return string.Format($"{0}, {1}", BusNumber, BusStationArea) + Stations.ToString();
        }
        #endregion

        #region isExsist
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
        public void removeStation(BusStation bs)
        {
            bool flag = isExsist(bs);
            if (!flag)
                throw new BusStationExceptions("The station isn't exsist in the station's list");
            if (stations[0] == bs)//If it's the first station
            {
                FirstStation = stations[1];
                FirstStation.bls.Distance = 0;
                FirstStation.bls.TravelTime = TimeSpan.Zero;
                stations.Remove(stations[0]);
                return;
            }
            if (stations[stations.Count-1] == bs)//If it's the last station
            {
                LastStation = stations[stations.Count - 2];               
                stations.Remove(stations[stations.Count - 1]);
                return;
            }
            for (int i = 1; i < stations.Count-1; i++)
            {               
                if (stations[i] == bs)
                { 
                    stations.Remove(stations[i]);
                    stations[i + 1].bls.Distance = distanceBetweenStations(stations[i - 1], stations[i]);
                    stations[i + 1].bls.TravelTime = travelTimeBetweenStations(stations[i - 1], stations[i]);
                    return;
                }
            }
        }
        #endregion

        #region addStation
        /// <summary>
        /// The function recives 2 BusStation.
        /// The first is the new station and after the second station we insert to the list
        /// </summary>
        /// <param name="newbls"></param>
        /// <param name="bls"></param>
        /// 

        public void addStation(BusStation newbs)
        {
            FirstStation = newbs;
            FirstStation.bls.Distance = 0;
            FirstStation.bls.TravelTime = TimeSpan.Zero;
            stations.Insert(-1, FirstStation);
            stations[1].bls.Distance = distanceBetweenStations(FirstStation, stations[1]);
            stations[1].bls.TravelTime = travelTimeBetweenStations(FirstStation, stations[1]);
        }

        public void addStation(BusStation newbs, BusStation bs)
        {
            bool flag = isExsist(bs);
            int index = -1;
            if (!flag)
                throw new BusStationExceptions("The station isn't exsist in the station's list");
            if(LastStation==bs)
            {
                bs = LastStation;
                stations.Insert(stations.Count-1, bs);
                stations[stations.Count - 1].bls.Distance = distanceBetweenStations(stations[stations.Count - 1], LastStation);
                stations[stations.Count - 1].bls.TravelTime = travelTimeBetweenStations(stations[stations.Count - 1], LastStation);
            }
            for (int i = 0; i < stations.Count; i++)
            {
                if (stations[i] == bs)
                {
                    index = i;                    
                    break;
                }
            }            
            stations.Insert(index + 1, newbs);
            //Updating the fields Distance and TravelTime of the new station. and the station after it
            stations[index+ 1].bls.Distance = distanceBetweenStations(stations[index], stations[index+1]);
            stations[index+ 1].bls.TravelTime = travelTimeBetweenStations(stations[index], stations[index+1]);
            //and the station after it.
            stations[index + 2].bls.Distance = distanceBetweenStations(stations[index+1], stations[index + 2]);
            stations[index + 2].bls.TravelTime = travelTimeBetweenStations(stations[index+1], stations[index + 2]);
        }
        #endregion

        #region distanceBetweenStations
        public double distanceBetweenStations(BusStation bs1, BusStation bs2)
        {
            bool flag1 = isExsist(bs1);
            bool flag2 = isExsist(bs2);
            if ((!flag1) || (!flag2))
                throw new BusStationExceptions("One or more of the station aren't exsist");
            GeoCoordinate c1 = new GeoCoordinate(bs1.BusStationLocation.Latitude, bs1.BusStationLocation.Longitude);
            GeoCoordinate c2 = new GeoCoordinate(bs2.BusStationLocation.Latitude, bs2.BusStationLocation.Longitude);
            double distanceInKm = c1.GetDistanceTo(c2) / 1000;
            return distanceInKm;
        }

        #endregion

        #region travelTimeBetweenStations

        public TimeSpan travelTimeBetweenStations(BusStation bs1, BusStation bs2)// TravelTimeלברר מזה 
        {
            TimeSpan Duration = bs2.bls.TravelTime - bs1.bls.TravelTime;
            return Duration;
        }
        #endregion

        #region subRoute

        public BusLine subRoute(BusStation bs1, BusStation bs2)
        {
            bool flag1 = isExsist(bs1);
            bool flag2 = isExsist(bs2);
            if ((!flag1) || (!flag2))
                throw new BusStationExceptions("One or more of the station aren't exsist");
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
            for (int i=index1; i<=index2; i++)
            {
                newbl.stations.Insert(j, this.stations[i]);
                j++;
            }
            return newbl;//לברר מה לעדכן במספר קו ובאזור
        }
        #endregion

    }
}
