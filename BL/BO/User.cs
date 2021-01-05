using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class User
    {
        #region UserName - uniqe user name
        /// <summary>
        /// Username - the unique ID of the user
        /// </summary>
        public string UserName { get; set; }//uniqe user name
        #endregion

        #region Password
        /// <summary>
        /// User login password
        /// </summary>
        public string Password { get; set; }
        #endregion

        #region Admin
        /// <summary>
        /// Administrator permission
        /// </summary>
        public bool Admin { get; set; }
        #endregion

        #region IsDeleted
        /// <summary>
        /// Does this bus exist in the system or is it deleted from it
        /// </summary>
        public bool IsDeleted { get; set; }
        #endregion

        #region UserProfile
        /// <summary>
        /// This user's profile
        /// </summary>
        public Profile UserProfile { get; set; }
        #endregion

        #region Birthday
        /// <summary>
        /// Date of birth
        /// </summary>
        public DateTime Birthday { get; set; }
        #endregion

        #region Balance
        /// <summary>
        /// The balance of money left to the multi-line user
        /// </summary>
        public double Balance { get; set; }
        #endregion

        #region FirstName
        public string FirstName { get; set; }
        #endregion

        #region LastName
        public string LastName { get; set; }
        #endregion
    }
}
