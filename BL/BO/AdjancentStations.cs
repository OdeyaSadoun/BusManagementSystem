using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class AdjancentStations
    {
        #region CodeStation1 - uniqe number1
        /// <summary>
        /// Station ID of station number 1
        /// </summary>
        public int CodeStation1 { get; set; } //uniqe number1
        #endregion

        #region CodeStation2 - uniqe number2
        /// <summary>
        /// Station ID of station number 2
        /// </summary>
        public int CodeStation2 { get; set; }//uniqe number2
        #endregion

        #region Distance
        /// <summary>
        /// The distance between 2 stations
        /// </summary>
        public double Distance { get; set; }
        #endregion

        #region TravelTime
        /// <summary>
        /// Average travel time between the 2 stations
        /// </summary>
        public TimeSpan TravelTime { get; set; }
        #endregion

        #region IsDeleted
        /// <summary>
        /// Does this bus exist in the system or is it deleted from it
        /// </summary>
        public bool IsDeleted { get; set; }
        #endregion

    }
}
