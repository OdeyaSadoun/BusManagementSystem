using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    class Line
    {
        #region Id - uniqe number
        /// <summary>
        /// Line ID
        /// </summary>
        public int Id { get; set; }//uniqe number
        #endregion

        #region LineNumber
        /// <summary>
        /// Line number
        /// </summary>
        public int LineNumber { get; set; }
        #endregion

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
        public int FirstStation { get; set; }
        #endregion

        #region LastStation
        /// <summary>
        /// The destination station of this line
        /// </summary>
        public int LastStation { get; set; }
        #endregion

        #region Fare
        /// <summary>
        /// How much do you have to pay for the trip
        /// </summary>
        public double Fare { get; set; }
        #endregion

        #region TravelTimeInThisLine
        /// <summary>
        /// Estimated travel time of the route
        /// </summary>
        public TimeSpan TravelTimeInThisLine { get; set; }
        #endregion

        #region IsDeleted
        /// <summary>
        /// Does this bus exist in the system or is it deleted from it
        /// </summary>
        public bool IsDeleted { get; set; }
        #endregion
    }
}
