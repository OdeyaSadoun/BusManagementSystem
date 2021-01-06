using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StationsInLine
    {
        public int StationCode { get; set; }

        public string StationName { get; set; }

        public int LineStationIndex { get; set; }

        public float DistanceFrom { get; set; }

        public TimeSpan Timefrom { get; set; }

        public override string ToString()
        {
            return "Station code: " + StationCode + "  Line station index: " + LineStationIndex + "  Name: " + StationName;
        }
    }
}
