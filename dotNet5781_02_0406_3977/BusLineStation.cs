using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0406_3977
{
    public class BusLineStation:BusStation
    {
        public double Distance { get; set; }
        public DateTime TravelTime { get; set; }
    }
}
