using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineStation
    {
        #region LineId
        /// <summary>
        /// The ID of the line
        /// </summary>
        public int LineId { get; set; }
        #endregion

        #region StationCode
        /// <summary>
        /// The ID of the station
        /// </summary>
        public int StationCode { get; set; }
        #endregion

        #region LineStationIndex
        /// <summary>
        /// The current station number within the route of the line
        /// </summary>
        public int LineStationIndex { get; set; }
        #endregion

        #region PrevStation
        /// <summary>
        /// The number of the previous station within the route of the line
        /// </summary>
        public int PrevStation { get; set; }
        #endregion

        #region NextStation
        /// <summary>
        /// The next stop number within the line route
        /// </summary>
        public int NextStation { get; set; }
        #endregion

        #region IsDeleted
        /// <summary>
        /// Does this bus exist in the system or is it deleted from it
        /// </summary>
        public bool IsDeleted { get; set; }
        #endregion

    }
}
