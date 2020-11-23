using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace dotNet5781_02_0406_3977
{
    class BusLines : IEnumerable
    {
        public List<BusLine> Buses { get; set; }

        #region enptyConstructor
        public BusLines() { }

        #endregion;

        #region variableConstructor

        public BusLines(List<BusLine> lstBuses)
        {
            this.Buses = lstBuses;
            for (int i = 0; i < lstBuses.Count; i++)
            {
                for (int j = 0; j < lstBuses.Count; j++)
                {
                    if (i != j)
                        if (Buses[i] == lstBuses[j])
                            throw new BusStationExceptions("There are the same buses in the list");
                }
            }
        }
        #endregion

        #region IEnumerator

        public IEnumerator GetEnumerator()
        {
            return Buses.GetEnumerator();
        }
        #endregion

        #region  isExsist
        public bool isExsist(BusLine bs)
        {
            foreach (BusLine b in Buses)
            {
                if (b == bs)
                    return true;
            }
            return false;
        }
        #endregion

        #region addBus
        void addBus(BusLine newbl)
        {
            if (newbl.stations.Count < 2)
                throw new BusStationExceptions("The bus must has at least 2 stations");
            //באישור רכז הקורס דן זילברשטיין פתרנו את התרגיל ע"פ הבנתינו הבסיסית.
            //שכל קו יכול להופיע כמה פעמים  ולא רק הלוך וחזור.
            //לדוגמה מספר קו כלשהו בין זוג ערים ובין זוג ערים נוסף קיים מספר קו זהה,
            //אך זהו קו אחר ולכן קיימים  לנו יותר מ 2 קווים בעלי מספר זהה
            //אך לא כל הקו זהה אלא יש שינוי קל אז הכנסנו למערכת 
            //ורק כאשר כל הקו זהה נזרוק חריגה שהרי לא יכול להיות אותו קו בדיוק פעמיים במערכת.  
            foreach(BusLine bl in Buses)
            {
                if(bl==newbl)
                {
                    throw new BusStationExceptions("The bus is exsist");
                }
            }
      
            Buses.Add(newbl);
        }
        #endregion

        #region removeBus

        void removeBus(BusLine newbl)
        {
            if (!isExsist(newbl))
                throw new BusStationExceptions("The bus isn't exsist in the list");
            Buses.Remove(newbl);
        }
        #endregion

        #region busesInBusStation

        public List<BusLine> busesInBusStation(int busStationKey)
        {
            List<BusLine> returnLst = new List<BusLine>();
            foreach(BusLine bl in Buses)
            {
                foreach(BusStation s in bl.stations)
                {
                    if (s.BusStationKey == busStationKey)
                        returnLst.Add(bl);
                }

            }
            if (returnLst.Count == 0)
                throw new BusStationExceptions("There is no bus in this station");
            return returnLst;
        }
        #endregion

        #region sortByTravelTime

        public List<BusLine> sortByTravelTime()
        {//Copy to an array to create a copy of a list
            BusLine[] sortArrayBls = new BusLine[Buses.Count];          
            Buses.CopyTo(sortArrayBls);
            List<BusLine> sortListBls = sortArrayBls.ToList();
            sortListBls.Sort();
            return sortListBls;
        }
        #endregion

        #region indexer
        public List<BusLine> this[int busNumber]
        {
            get
            {
                List<BusLine> bs = Buses.FindAll(item => item.BusNumber == busNumber);
                if (bs.Count != 0)
                    return bs;
                else
                    throw new BusStationExceptions("The line does not exist in the list");
            }
        }
        #endregion
    }



}
