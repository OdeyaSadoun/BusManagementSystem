using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineStation
    {
        #region LineId - uniqe number
        /// <summary>
        /// The ID of the line
        /// </summary>
        public int LineId { get; set; }//uniqe number
        #endregion

        #region StationCode - uniqe number
        /// <summary>
        /// The ID of the station
        /// </summary>
        public int StationCode { get; set; }//uniqe number
        #endregion

        #region LineStationIndex
        /// <summary>
        /// The current station number within the route of the line
        /// </summary>
        public int LineStationIndex { get; set; }
        #endregion

        #region IsDeleted
        /// <summary>
        /// Does this bus exist in the system or is it deleted from it
        /// </summary>
        public bool IsDeleted { get; set; }
        #endregion

        /*************************/

        #region ToString
        /// <summary>
        /// A to string function
        /// </summary>
        /// <returns></returns>
        public override string ToString() => this.ToStringProperty();
        #endregion

    }
}
