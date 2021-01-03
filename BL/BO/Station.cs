using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    class Station
    {
        #region Code - uniqe code
        /// <summary>
        /// the uniqe code of ststion
        /// </summary>
        public int Code { get; set; }//uniqe code
        #endregion

        #region Name
        /// <summary>
        /// thhe station name
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region Longitude
        /// <summary>
        /// the longitude of the station
        /// </summary>
        public double Longitude { get; set; }
        #endregion

        #region Latitude
        /// <summary>
        ///  the latitude of the station
        /// </summary>
        public double Latitude { get; set; }
        #endregion

        #region Address
        /// <summary>
        /// the address of the station 
        /// </summary>
        public string Address { get; set; }
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
