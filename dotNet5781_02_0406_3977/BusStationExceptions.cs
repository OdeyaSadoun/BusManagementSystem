using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0406_3977
{
    class BusStationExceptions:Exception
    {
        public BusStationExceptions():base() { }
        public BusStationExceptions(string exp ) : base(exp) { }
    }
}
