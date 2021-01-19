using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BLApi
{
    public static class BLFactory
    {
        public static IBL GetBL(string type)
        {
            return BLImp.Instance;
            //switch (type)
            //{
            //    case "1":
            //        return BLImp.Instance()
            //    //case "2":
            //    //return new BLImp2();
            //    default:
            //        return new BLImp();
            //}
        }
    }
}
