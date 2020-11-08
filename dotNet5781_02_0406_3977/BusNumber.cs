using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0406_3977
{
    public class BusNumber
    {

        #region BusLine
        public int BusLine { get; set; }
        #endregion

        #region Stations
        public List<BusLineStation> Stations { get; set; }
        #endregion

        #region FirstStation
        public BusStation FirstStation { get; set; }
        #endregion

        #region LastStation
        public BusStation LastStation { get; set; }
        #endregion

        #region Area
        public string Area { get; set; }
        #endregion


        //Functions:

  //      public BusNumber()

       

      
    }
}
