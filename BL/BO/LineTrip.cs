using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    class LineTrip
    {
        #region Id
        /// <summary>
        /// The ID of this line trip
        /// </summary>
        public int Id { get; set; }
        #endregion

        #region LineId
        /// <summary>
        /// The ID of the line for this travel
        /// </summary>
        public int LineId { get; set; }
        #endregion

        #region StartAt
        /// <summary>
        /// The time of the begining of this travel line
        /// </summary>
        public TimeSpan StartAt { get; set; }
        #endregion

        #region Frequency
        /// <summary>
        /// The frequency of this travel line
        /// </summary>
        public TimeSpan Frequency { get; set; }
        #endregion

        #region FinishAt
        /// <summary>
        /// The time of the end of this travel line
        /// </summary>
        public TimeSpan FinishAt { get; set; }
        #endregion

        #region IsDeleted
        /// <summary>
        /// Does this bus exist in the system or is it deleted from it
        /// </summary>
        public bool IsDeleted { get; set; }
        #endregion

        #region IsDigitalPanel
        /// <summary>
        /// Information whether there is a digital panel at the same station or not
        /// </summary>
        public bool IsDigitalPanel { get; set; }
        #endregion

        #region IsBench
        /// <summary>
        /// Is there a bench at the bus stop for the benefit of the residents
        /// </summary>
        public bool IsBench { get; set; }
        #endregion
    }
}
