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
            return stationInLine;
        }

        public static BO.ShortLine CopyToLineInStation(this DO.Line l, DO.LineStation s)
        {
            BO.ShortLine lineInStation = new BO.ShortLine();
            lineInStation = l.CopyToLine();
            lineInStation.Id = s.LineStationIndex;
            return lineInStation;
        }
        public static BO.Line CopyToLine(this DO.Line s)
        {
            BO.Line line = new BO.Line();

            line.Area = (BO.Area)s.Area;

            //DO.Station sDO = dl.GetStation(s.FirstStation);
            //line.FirstStation.CopyPropertiesTo(sDO);
            //sDO = dl.GetStation(s.LastStation);
            //line.LastStation.CopyPropertiesTo(sDO);


            line.Id = s.Id;
            line.Fare = s.Fare;
            line.TravelTimeInThisLine = s.TravelTimeInThisLine;
            line.LineNumber = s.LineNumber;
            line.IsDeleted = s.IsDeleted;
            

            return line;
        }

        public static BO.Station CopyToStation(this DO.Station s)
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



            return station;
        }
    }
}
