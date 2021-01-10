using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Line: ShortLine
    {
        #region Area
        /// <summary>
        /// The area where the line travels
        /// </summary>
        public Area Area { get; set; }
        #endregion

        #region FirstStation
        /// <summary>
        /// The starting point of this line
        /// </summary>
        public BO.Station FirstStation { get; set; }
        #endregion

        #region Fare
        /// <summary>
        /// How much do you have to pay for the trip
        /// </summary>
        public double Fare { get; set; }
        #endregion

        #region DistanceTo

        /// <summary>
        /// the distance from the current station to the next station 
        /// </summary>
        public float DistanceTo { get; set; }
        #endregion

        #region TimeTo
        /// <summary>
        /// the time travel from the current station to the next station
        /// </summary>
        public TimeSpan TimeTo { get; set; }
        #endregion

        #region TravelTimeInThisLine
        /// <summary>
        /// Estimated travel time of the route
        /// </summary>
        public TimeSpan TravelTimeInThisLine { get; set; }
        #endregion

        #region ListOfStationsInLine
        /// <summary>
        /// the list of all the stations in line in this bus on trip
        /// </summary>
        public IEnumerable<StationInLine> ListOfStationsInLine { get; set; }
        #endregion
    }
}
