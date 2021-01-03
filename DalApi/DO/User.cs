using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class User
    {
        #region UserName - uniqe user name
        /// <summary>
        /// Username - the unique ID of the user
        /// </summary>
        public string UserName{ get; set; }//uniqe user name
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
    }
}
