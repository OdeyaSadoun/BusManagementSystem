using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    class Bus
    {
        #region LicenseNumber - uniqe number
        /// <summary>
        /// license number of bus
        /// </summary>
        public int LicenseNumber { get; set; }//uniqe number
        #endregion

        #region DateBegin
        /// <summary>
        /// The time that the bus was produce
        /// </summary>
        public DateTime DateBegin { get; set; }
        #endregion

        #region LastTreatment
        /// <summary>
        /// Date of last treatment of the bus
        /// </summary>
        public DateTime LastTreatment { get; set; }
        #endregion

        #region TotalMileage
        /// <summary>
        /// The amount of mileage of the bus
        /// </summary>
        public double TotalMileage { get; set; }
        #endregion

        #region FuelRemain
        /// <summary>
        /// The amount of fuel on the bus
        /// </summary>
        public double FuelRemain { get; set; }
        #endregion

        #region CurrentMileage
        /// <summary>
        /// The amount of mileage needed for the current trip
        /// </summary>
        public float CurrentMileage { get; set; }
        #endregion

        #region Status
        /// <summary>
        /// What is the status of the bus
        /// </summary>
        public BusStatus Status { get; set; }
        #endregion

        #region KmBeforTreatment
        /// <summary>
        /// The amount of miles left for travel before the bus needs to be serviced
        /// </summary>
        public float KmBeforTreatment { get; set; }
        #endregion

        #region KmBeforeFuel
        /// <summary>
        /// The amount of miles left for travel before the bus needs to be refueled
        /// </summary>
        public float KmBeforeFuel { get; set; }
        #endregion

        #region IsDeleted
        /// <summary>
        /// Does this bus exist in the system or is it deleted from it
        /// </summary>
        public bool IsDeleted { get; set; }
        #endregion

        #region IsAccessible
        /// <summary>
        /// Is the bus accessible to the disabled or not
        /// </summary>
        public bool IsAccessible { get; set; }
        #endregion
    }
}
