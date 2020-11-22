using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0406_3977
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = BusStation.rnd;
            try
            {
                //Random rnd = new Random();
                //List<BusStation>lst = new List<BusStation>()
                //{
                //   new BusStation(){BusStationAdress= "aaa", BusStationLocation = Area.North }
                //}
                string[] streets = { "Rashbam ", "HaChida ", "Haatzmaut ", "Hertzel ", "Jerusalem ", "Halevi ", "Haneviim " };
                BusStation[] bs = new BusStation[100];
                for (int i = 0; i < bs.Length; i += 4)
                {
                    BusStation.BusLineStation bls = new BusStation.BusLineStation();
                    bs[i] = new BusStation();
                    bs[i + 1] = new BusStation(streets[r.Next(0, streets.Length)] + r.Next(1, (i + 1) * 10));
                    bs[i + 2] = new BusStation(bls);
                    bs[i + 3] = new BusStation(streets[r.Next(0, streets.Length)] + r.Next(1, (i + 3) * 10), bls);
                }
                for (int i = 0; i < bs.Length; i++)
                {
                    Console.WriteLine(bs[i]);
                }

                List<BusStation> lst1 = new List<BusStation> { bs[1], bs[2], bs[3], bs[4], bs[5], bs[6], bs[7] };
                List<BusStation> lst2 = new List<BusStation> { bs[8], bs[9], bs[10], bs[4], bs[5], bs[8], bs[11] };
                List<BusStation> lst3 = new List<BusStation> { bs[5], bs[7], bs[9], bs[12], bs[13], bs[14], bs[15] };
                List<BusStation> lst4 = new List<BusStation> { bs[1], bs[7], bs[3], bs[2], bs[9], bs[12], bs[13] };
                List<BusStation> lst5 = new List<BusStation> { bs[1], bs[2], bs[3], bs[6], bs[8], bs[9], bs[10] };

                BusLine bl1 = new BusLine(56, lst1, bs[1], bs[7], Area.Center);
                BusLine bl2 = new BusLine(31, lst2, bs[8], bs[11], Area.Jerusalem);
                BusLine bl3 = new BusLine(75, lst3, bs[5], bs[15], Area.North);
                BusLine bl4 = new BusLine(52, lst4, bs[1], bs[13], Area.Center);
                BusLine bl5 = new BusLine(12, lst5, bs[1], bs[10], Area.Center);

                Console.WriteLine(bl1);
                Console.WriteLine(bl2);
             Console.WriteLine(bl3);
                Console.WriteLine(bl4);
               Console.WriteLine(bl5);
                //  Console.WriteLine(bl1.subRoute(bs[3], bs[6]));
                // bl1.removeStation(bs[1]);
             //   bl1.subRoute(bs[2], bs[4]);
                BusLine tmp = bl1.ChoiceOfBuses(bs[1], bs[7], bl4);
                Console.WriteLine(tmp);
                for (int i = 0; i < bl1.stations.Count; i++)
                    Console.WriteLine(bl1.stations[i].bls.TravelTime);
       //        Console.WriteLine(bl1.TotalTimeBetweenSomeStations(bl1.FirstStation, bl1.LastStation));
                //   Console.WriteLine(bl1);
                Console.WriteLine(bl1.TotalTimeBetweenSomeStations(bs[1], bs[7]));
                Console.WriteLine(bl4.TotalTimeBetweenSomeStations(bs[1], bs[7]));

                Console.WriteLine(tmp);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
