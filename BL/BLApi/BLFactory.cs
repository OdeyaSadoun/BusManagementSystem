﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BLApi
{
    public static class BLFactory
    {
        public static IBL GetBL()
        {
            
            return BLImp.Instance;

        }
    }
}
