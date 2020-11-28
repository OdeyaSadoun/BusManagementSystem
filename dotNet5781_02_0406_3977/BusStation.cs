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
        public static Random rnd = new Random();

        public static int key = 0;

        #region struct Location
        public struct Location
        {
            public double Latitude;
            public double Longitude;
        }
        #endregion

        //proprties:

        #region BusLineStation bls
        public class BusLineStation
        {
            //proprties:

            #region Distance
            public double Distance { get; set; }
            #endregion

            #region TravelTime
            public TimeSpan TravelTime { get; set; }
            #endregion

            //functions:
            #region empty constructor
            public BusLineStation() 
            {
                this.Distance = rnd.NextDouble() * (33.3 - 31) + 31;
                this.TravelTime = new TimeSpan(/*hours*/rnd.Next(0, 3), /*minutes*/rnd.Next(0, 60), /*seconds*/rnd.Next(0, 60));
            }
            #endregion

            #region ToString
            public override string ToString()
            {
                return string.Format($"Distance:{0}, TravelTime:{1}", Distance, TravelTime);
            }
            #endregion

        }
        public BusLineStation bls{ get; set; }
        #endregion

        #region  BusStationKey
        public int BusStationKey { get; private set; } //לבדוק מה זה מספר סטטי
        #endregion

        #region BusStationLocation
        private Location busStationLocation;

        public Location BusStationLocation
        {
            get { return busStationLocation; }
            set
            {
                busStationLocation.Latitude = rnd.NextDouble() * (33.3 - 31) + 31;
                busStationLocation.Longitude = rnd.NextDouble() * (35.5 - 34.3) + 34.3;
            }
        }
        #endregion

        # region BusStationAdress
        public string BusStationAdress { get; set; }
        #endregion


        //Functions:

        #region constructor with option to enter only adress and this constractor is adition a empty constructor
        /// <summary>
        ///constructor with option to enter only adress and this constractor is adition a empty constructor 
        /// </summary>
        /// <param name="busStationAdress"></param>
        public BusStation(/*Location busStationLocation,*/ string busStationAdress="") 
        {
            this.BusStationKey = key++;
            if (BusStationKey >= 1000000)
                throw new ArgumentOutOfRangeException("The number has more than 6 digits"); 

            //this.BusStationLocation = busStationLocation;
            this.busStationLocation.Latitude = rnd.NextDouble() * (33.3 - 31) + 31;
            this.busStationLocation.Longitude = rnd.NextDouble() * (35.5 - 34.3) + 34.3;
            this.BusStationAdress = busStationAdress;
            this.bls = new BusLineStation();

        }
        #endregion

        #region constructor with adress and travel time and distanc- bls
        /// <summary>
        ///constructor with adress and travel time and distanc- bls  

        /// </summary>
        /// <param name="busStationAdress"></param>
        /// <param name="bls"></param>

        public BusStation(/*Location busStationLocation,*/string busStationAdress, BusLineStation bls)
        {
            this.BusStationKey = key++;
            if (BusStationKey >= 1000000)
                throw new ArgumentOutOfRangeException("The number has more than 6 digits");

            //this.BusStationLocation = busStationLocation;
            this.busStationLocation.Latitude = rnd.NextDouble() * (33.3 - 31) + 31;
            this.busStationLocation.Longitude = rnd.NextDouble() * (35.5 - 34.3) + 34.3;
            this.BusStationAdress = busStationAdress;
            this.bls = new BusLineStation();
            this.bls.Distance = bls.Distance;
            this.bls.TravelTime = bls.TravelTime;
        }
        #endregion

        #region constructor with travel time and distance - bls
        /// <summary>
        /// constructor with travel time and distance - bls
        /// </summary>
        /// <param name="bls"></param>
        public BusStation(/*Location busStationLocation,*/ BusLineStation bls)
        {
            this.BusStationKey = key++;
            if (BusStationKey >= 1000000)
                throw new ArgumentOutOfRangeException("The number has more than 6 digits");

            //this.BusStationLocation = busStationLocation;
            this.busStationLocation.Latitude = rnd.NextDouble() * (33.3 - 31) + 31;
            this.busStationLocation.Longitude = rnd.NextDouble() * (35.5 - 34.3) + 34.3;
            this.BusStationAdress = "";
            this.bls = new BusLineStation();
            this.bls.Distance = bls.Distance;
            this.bls.TravelTime = bls.TravelTime;
        }

        //#region empty constructor
        //public BusStation() { }
        //#endregion
        #endregion

        #region ToString
        /// <summary>
        /// An override function to print the object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return (string.Format("Bus Station Code: " + BusStationKey.ToString("000000") + "\t\t" + BusStationLocation.Latitude + "°N \t"+ BusStationLocation.Longitude + "°E \t" + BusStationAdress));
        }
        #endregion

    }
}
