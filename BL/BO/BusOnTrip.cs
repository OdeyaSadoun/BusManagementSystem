using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusOnTrip
    {
        #region Id - uniqe number
        /// <summary>
        /// Bus ID on the travel
        /// </summary>
        public int Id { get; set; } // uniqe number
        #endregion

        #region LicenseNumber - uniqe number
        /// <summary>
        /// License number of this bus
        /// </summary>
        public int LicenseNumber { get; set; }//uniqe number
        #endregion

        #region LineNumber
        /// <summary>
        /// Identifies the line in the current trip
        /// </summary>
        public int LineNumber { get; set; }
        #endregion

        #region PlannedTakeOff
        /// <summary>
        /// The planned time that the line should leave for the trip - according to the schedule
        /// </summary>
        public TimeSpan PlannedTakeOff { get; set; }
        #endregion

        #region ActualTakeOff
        /// <summary>
        /// The time the line left for actual travel
        /// </summary>
        public TimeSpan ActualTakeOff { get; set; }
        #endregion

        #region PrevStation
        /// <summary>
        /// Last stop number on the line that the bus passed
        /// </summary>
        public int PrevStation { get; set; }
        #endregion

        #region PrevStationAt
        /// <summary>
        /// Transit time at the last stop on the line the bus passed
        /// </summary>
        public TimeSpan PrevStationAt { get; set; }
        #endregion

        #region NextStationAt
        /// <summary>
        /// Time to get to the next station
        /// </summary>
        public TimeSpan NextStationAt { get; set; }
        #endregion

        #region IsAccessible
        /// <summary>
        /// Is the bus accessible to the disabled or not
        /// </summary>
        public bool IsAccessible { get; set; }
        #endregion

        #region DriverNumber
        /// <summary>
        /// The employee number of the driver currently driving on this line
        /// </summary>
        public int DriverNumber { get; set; }
        #endregion

        #region IsDeleted
        /// <summary>
        /// Does this bus exist in the system or is it deleted from it
        /// </summary>
        public bool IsDeleted { get; set; }
        #endregion
    }
}
