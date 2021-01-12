using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StationInLine
    {
        #region StationCode
        /// <summary>
        /// the code of the station
        /// </summary>
        public int StationCode { get; set; }
        #endregion

        #region LineId
        /// <summary>
        /// the id of the line that exist in the station
        /// </summary>
        public int LineId { get; set; }
        #endregion

        #region StationName
        /// <summary>
        /// the name of the station
        /// </summary>
        public string StationName { get; set; }
        #endregion

        #region LineStationIndex
        /// <summary>
        /// the index of this station in the line
        /// </summary>
        public int LineStationIndex { get; set; }
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

        //#region ToString
        //public override string ToString()
        //{
        //    return "Station code: " + StationCode + "  Line station index: " + LineStationIndex + "  Name: " + StationName;
        //}
        //#endregion
    }
}
