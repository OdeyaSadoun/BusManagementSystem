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

        #region Time
        /// <summary>
        /// Boarding time
        /// </summary>
        public TimeSpan Time { get; set; }
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
