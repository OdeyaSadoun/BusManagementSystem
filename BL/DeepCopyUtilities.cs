using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BLApi;

namespace BL
{
    public static class DeepCopyUtilities
    {
        static IDL dl = DLFactory.GetDL();
        static IBL bl = BLFactory.GetBL();

        public static void CopyPropertiesTo<T, S>(this S from, T to)
        {
            foreach (PropertyInfo propTo in to.GetType().GetProperties())
            {
                PropertyInfo propFrom = typeof(S).GetProperty(propTo.Name);
                if (propFrom == null)
                    continue;
                var value = propFrom.GetValue(from, null);
                if (value is ValueType || value is string || value is BO.Station || value is IEnumerable<BO.StationInLine>)
                        propTo.SetValue(to, value);
            }
        }
        public static object CopyPropertiesToNew<S>(this S from, Type type)
        {
            object to = Activator.CreateInstance(type); // new object of Type
            from.CopyPropertiesTo(to);
            return to;
        }
        public static BO.StationInLine CopyToStationInLine(this DO.Station st, DO.LineStation s)
        {
            BO.StationInLine stationInLine = new BO.StationInLine();
            st.CopyPropertiesTo(stationInLine);
            stationInLine.LineStationIndex = s.LineStationIndex;
            stationInLine.StationCode = s.StationCode;
            stationInLine.StationName = st.Name;
            return stationInLine;
        }

        public static BO.ShortLine CopyToLineInStation(this DO.Line l, DO.LineStation s)
        {
            BO.ShortLine lineInStation = new BO.ShortLine();
            lineInStation = l.CopyToLineDOToBO();
           // lineInStation.Id = s.LineStationIndex;
            return lineInStation;
        }
        public static BO.Line CopyToLineDOToBO(this DO.Line s)
        {
            BO.Line line = new BO.Line();

            line.Area = (BO.Area)s.Area;

            DO.Station sDO = dl.GetStation(s.FirstStation);
            line.FirstStation = CopyToStationDOToBO(sDO);
            //line.FirstStation = bl.GetStation(s.FirstStation);
            sDO = dl.GetStation(s.LastStation);
            line.LastStation = CopyToStationDOToBO(sDO);
            //line.LastStation = bl.GetStation(s.LastStation);


            line.Id = s.Id;
            line.Fare = s.Fare;
            line.TravelTimeInThisLine = s.TravelTimeInThisLine;
            line.LineNumber = s.LineNumber;
            line.IsDeleted = s.IsDeleted;

            //רשימה של זמני יציאה- לברר
            //List<TimeSpan> times = new List<TimeSpan>();
            //line.ListOfTripTime = times;

            //רשימה של תחנות קו

            //List<BO.StationInLine> stations = new List<BO.StationInLine>();
            //stations = (from stat in dl.GetAllLinesStationBy(stat => stat.LineId == line.Id && stat.IsDeleted == false)
            //            let station = dl.GetStation(stat.StationCode)
            //            select station.CopyToStationInLine(stat)).ToList();
            //line.ListOfStationsInLine = stations;

            //לבדוק מה קורה לגבי כל הרשימות
            return line;
        }

        public static DO.Line CopyToLineBOToDO(this BO.Line lineBO)
        {
            DO.Line lineDO = new DO.Line();

            lineDO.Area = (DO.Area)lineBO.Area;

            BO.Station sBO = bl.GetStation(lineBO.FirstStation.Code);
            lineDO.FirstStation = sBO.Code;
            sBO = bl.GetStation(lineBO.LastStation.Code);
            lineDO.LastStation = sBO.Code;


            lineDO.Id = lineBO.Id;
            lineDO.Fare = lineBO.Fare;
            lineDO.TravelTimeInThisLine = lineBO.TravelTimeInThisLine;
            lineDO.LineNumber = lineBO.LineNumber;
            lineDO.IsDeleted = lineBO.IsDeleted;


            return lineDO;
        }

        public static BO.Station CopyToStationDOToBO(this DO.Station s)
        {
            BO.Station station = new BO.Station();
            station.Address = s.Address;
            station.Code = s.Code;
            station.IsAccessible = s.IsAccessible;
            station.IsBench = s.IsBench;
            station.IsDeleted = s.IsDeleted;
            station.IsDigitalPanel = s.IsDigitalPanel;
            station.Latitude = s.Latitude;
            station.Longitude = s.Longitude;
            station.Name = s.Name;

            //List<BO.Line> line = new List<BO.Line>();
            //foreach(BO.Line l in bl.GetAllLines())
            //{
            //    foreach (BO.StationInLine stationInLine in l.ListOfStationsInLine.ToList())
            //    {
            //        if (stationInLine.StationCode == station.Code)
            //            line.Add(l);
            //    }
            //}







            //List<BO.StationInLine> line = new List<BO.StationInLine>();
            //line = (from l in dl.GetAllLinesStationBy(l => l.StationCode == station.Code && l.IsDeleted == false)
            //            let line = dl.GetStation(stat.StationCode)
            //            select station.CopyToStationInLine(stat)).ToList();
            //לבדוק מה לעשות לגבי רשימת התחנות

            return station;
        }

    }
}
