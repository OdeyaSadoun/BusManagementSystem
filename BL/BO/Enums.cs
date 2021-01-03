using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    //Enums:

    #region Area
    public enum Area //for the buses
    {
        North = 1, NorthSouth, South, Center, NorthCenter, SouthCenter, Jerusalem, JerusalemNorth, JerusalemCenter, JerusalemSouth
    }
    #endregion

    #region BusStatus
    public enum BusStatus
    {
        Ready = 1, Driving, Refueling, InTreatment
    }
    #endregion

    #region Profile
    public enum Profile
    {
        Youth = 1, Normal, Veteran, OrdinaryStudent, ExtendedStudent
    }
    #endregion
}
