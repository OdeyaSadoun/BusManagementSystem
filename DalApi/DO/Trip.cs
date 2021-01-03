using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Trip
    {
        #region Id - uniq number
        /// <summary>
        /// The trip's id
        /// </summary>
        public int Id { get; set; }
        #endregion

        #region UserName
        /// <summary>
        /// Passenger username
        /// </summary>
        public string UserName { get; set; }
        #endregion

        #region LineId
        /// <summary>
        /// Line ID
        /// </summary>
        public int LineId { get; set; }
        #endregion

        #region StartStation
        /// <summary>
        /// Passenger boarding station
        /// </summary>
        public int StartStation { get; set; }
        #endregion

        #region InAt
        /// <summary>
        /// Boarding time
        /// </summary>
        public TimeSpan InAt { get; set; }
        #endregion

        #region DestinationStation
        /// <summary>
        /// Passenger drop-off station
        /// </summary>
        public int DestinationStation { get; set; }
        #endregion

        #region OutAt
        /// <summary>
        /// Time to get off the bus
        /// </summary>
        public TimeSpan OutAt { get; set; }
        #endregion

        #region IsDeleted
        /// <summary>
        /// Does this bus exist in the system or is it deleted from it
        /// </summary>
        public bool IsDeleted { get; set; }
        #endregion

    }
}
