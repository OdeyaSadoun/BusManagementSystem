using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineTiming
    {
        public int LineId { get; set; }
        public int LineNumber { get; set; }
        public TimeSpan Timing { get; set; }
        public Station SourceStation { get; set; }
        public Station TargetStation{ get; set; }
    }
}
