﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_0406_3977
{
    public enum Area //for the buses
    {
        General=1, North, South, Center, Jerusalem, Other

    }
    public enum Menu // for the main
    {
        addBusLine = 1, addBusStationToBusLine, removeBusLine, removeBusStationFromBusLine, searchBusLinesInStation, printOptionToTravelBetweenBusStation, printAllTheBusses, printAllBusStations, exit
    }



}
