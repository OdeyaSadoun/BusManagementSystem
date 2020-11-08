using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0406_3977
{
    public class BusStation
    {
        public static int BusStationKey { get; set; } //לבדוק מה זה מספר סטטי

        #region struct BusStationLocation
        public struct Location
        {
            public double Latitude;
            public double Longitude;
        }
        #endregion

        private Location busStationLocation;

        public Location BusStationLocation
        {
            get { return busStationLocation; }
            set
            {
                Random rnd = new Random();
                busStationLocation.Latitude = rnd.NextDouble() * (33.3 - 31) + 31;
                busStationLocation.Latitude = rnd.NextDouble() * (35.5 - 34.3) + 34.3;
            }
        }


        public string BusstationAddress { get; set; }

        //Functions

        public override string ToString()
        {
            return string.Format($"Bus Station Code:{0}, {1}°N, {2}°E , {3}", BusStationKey,BusStationLocation.Latitude, BusStationLocation.Longitude, BusstationAddress) ;
        }
    }
}
