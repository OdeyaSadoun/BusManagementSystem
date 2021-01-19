using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ShortLine
    {
        #region Id - uniqe number
        /// <summary>
        /// Line ID
        /// </summary>
        public int Id { get; set; }//uniqe number
        #endregion

        #region LineNumber
        /// <summary>
        /// Line number
        /// </summary>
        public int LineNumber { get; set; }
        #endregion

        #region LastStation
        /// <summary>
        /// The destination station of this line
        /// </summary>
        public BO.Station LastStation { get; set; }
        #endregion

        #region IsDeleted
        /// <summary>
        /// Does this bus exist in the system or is it deleted from it
        /// </summary>
        /// 
       
        public bool IsDeleted { get; set; }
        #endregion
        #region ListOfTripTime
        /// <summary>
        /// the list of all the 
        /// </summary>
        public IEnumerable<LineTrip> ListOfTripTime { get; set; }
        #endregion

        //    #region ToString
        //    /// <summary>
        //    /// A to string function
        //    /// </summary>
        //    /// <returns></returns>
        //    public override string ToString()
        //    {
        //        return string.Format("The line id is: " + Id + "\t line number: " + LineNumber + "\t to: " + LastStation.Name);

        //    }
        //    #endregion
        //}
    }
}
