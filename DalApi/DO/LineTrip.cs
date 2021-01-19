﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineTrip
    {
        #region Id - uniqe number
        /// <summary>
        /// The ID of this line trip
        /// </summary>
        public int Id { get; set; }//uniqe number
        #endregion

        #region LineId - uniqe number
        /// <summary>
        /// The ID of the line for this travel
        /// </summary>
        public int LineId { get; set; }//uniqe number
        #endregion

        #region StartAt
        /// <summary>
        /// The time of the begining of this travel line
        /// </summary>
        public TimeSpan StartAt { get; set; }
        #endregion

        //#region Frequency
        ///// <summary>
        ///// The frequency of this travel line
        ///// </summary>
        //public TimeSpan Frequency { get; set; }
        //#endregion

        //#region FinishAt
        ///// <summary>
        ///// The time of the end of this travel line
        ///// </summary>
        //public TimeSpan FinishAt { get; set; }
        //#endregion

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
