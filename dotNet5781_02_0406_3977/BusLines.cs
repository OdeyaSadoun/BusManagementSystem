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
        //property:

        #region Busess
        public List<BusLine> Busess { get; set; }
        #endregion

        //Functions:

        #region emptyConstructor
        /// <summary>
        /// empty constructor
        /// </summary>
        public BusLines() { }

        #endregion;

        #region variableConstructor
        /// <summary>
        /// A variable constructor
        /// </summary>
        /// <param name="lstBuses"></param>
        public BusLines(List<BusLine> lstBuses)
        {
            this.Busess = lstBuses;
            for (int i = 0; i < lstBuses.Count; i++)
            {
                for (int j = 0; j < lstBuses.Count; j++)
                {
                    if (i != j)
                        if (Busess[i] == lstBuses[j])
                            throw new BusStationExceptions("There are the same buses in the list");
                }
            }
        }
        #endregion

        #region IEnumerator
        /// <summary>
        /// A function that returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>enumerator that iterates through a collection</returns>

        public IEnumerator GetEnumerator()
        {
            return Busess.GetEnumerator();
        }
        #endregion

        #region  isExsist
        /// <summary>
        /// A function that check if the bus exist in the collection of the busess
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        public bool isExsist(BusLine bs)
        {
            foreach (BusLine b in Busess)
            {
                if (b == bs)
                    return true;
            }
            return false;
        }
        #endregion

        #region addBus
        /// <summary>
        /// A function that add bus to the collection
        /// </summary>
        /// <param name="newbl"></param>
        public void addBus(BusLine newbl)
        {
            if (newbl.stations.Count < 2)
                throw new BusStationExceptions("The bus must has at least 2 stations");
            //באישור רכז הקורס דן זילברשטיין פתרנו את התרגיל ע"פ הבנתינו הבסיסית.
            //שכל קו יכול להופיע כמה פעמים  ולא רק הלוך וחזור.
            //לדוגמה מספר קו כלשהו בין זוג ערים ובין זוג ערים נוסף קיים מספר קו זהה,
            //אך זהו קו אחר ולכן קיימים  לנו יותר מ 2 קווים בעלי מספר זהה
            //אך לא כל הקו זהה אלא יש שינוי קל אז הכנסנו למערכת 
            //ורק כאשר כל הקו זהה נזרוק חריגה שהרי לא יכול להיות אותו קו בדיוק פעמיים במערכת.  
            foreach(BusLine bl in Busess)
            {
                if(bl==newbl)
                {
                    throw new BusStationExceptions("The bus is exsist");
                }
            }
      
            Busess.Add(newbl);
        }
        #endregion

        #region removeBus
        /// <summary>
        /// A function that remove bus from the collection
        /// </summary>
        /// <param name="newbl"></param>
        public void removeBus(BusLine newbl)
        {
            if (!isExsist(newbl))
                throw new BusStationExceptions("The bus isn't exsist in the list");
            Busess.Remove(newbl);
        }
        #endregion

        #region busessInBusStation
        /// <summary>
        /// A function that recives a bus station
        /// </summary>
        /// <param name="busStationKey"></param>
        /// <returns>a list with all the busess that go through ther</returns>
        public List<BusLine> busessInBusStation(int busStationKey)
        {
            List<BusLine> returnLst = new List<BusLine>();
            foreach(BusLine bl in Busess)
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
        /// <summary>
        /// A function that sort the list by the travel time
        /// </summary>
        /// <returns></returns>
        public List<BusLine> sortByTravelTime()
        {//Copy to an array to create a copy of a list
            BusLine[] sortArrayBls = new BusLine[Busess.Count];          
            Busess.CopyTo(sortArrayBls);
            List<BusLine> sortListBls = sortArrayBls.ToList();
            sortListBls.Sort();
            return sortListBls;
        }
        #endregion

        #region indexer
        /// <summary>
        /// A function that receives a line number and returns the instance. If there is no such line an anomaly will be thrown.
        /// </summary>
        /// <param name="busNumber"></param>
        /// <returns></returns>
        public BusLine this[int busNumber]
        {
            get
            {
                BusLine bs= Busess.Find(item => item.BusNumber == busNumber);
                if (bs != default)
                    return bs;
                else
                    throw new BusStationExceptions("The line does not exist in the list");
            }
            set
            {
                int intd = Busess.FindIndex(item => item.BusNumber == busNumber);
                if(intd==-1)
                    throw new BusStationExceptions("The bus isn't exsist");
                Busess[intd] = value;
                

                //bool flag = false;
                //BusLine temp;
                //foreach (BusLine bl in Busess)
                //{
                //    if (bl.BusNumber == busNumber)
                //    {
                //        temp = bl;
                //        flag = true;
                //        break;
                //    }
                //}
                //if (!flag)
                //    throw new BusStationExceptions("The bus isn't exsist");
                //Busess[temp] = value;

            }
        }
        #endregion
    }



}
