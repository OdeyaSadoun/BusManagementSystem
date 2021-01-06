using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Station
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

        #region ListOfLines
        /// <summary>
        /// the list of all the stations in this bus on trip
        /// </summary>
        public IEnumerable<ShortLine> ListOfLines { get; set; }
        #endregion

        #region ListOfStationsInLines
        public IEnumerable<StationInLine> ListOfStationsInLines { get; set; }
        #endregion
    }
}
