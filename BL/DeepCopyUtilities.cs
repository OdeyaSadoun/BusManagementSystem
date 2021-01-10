using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class DeepCopyUtilities
    {
        public static void CopyPropertiesTo<T, S>(this S from, T to)
        {
            foreach (PropertyInfo propTo in to.GetType().GetProperties())
            {
                PropertyInfo propFrom = typeof(S).GetProperty(propTo.Name);
                if (propFrom == null)
                    continue;
                var value = propFrom.GetValue(from, null);
                if (value is ValueType || value is string || value is BO.ShortLine)
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
            l.CopyPropertiesTo(lineInStation);
            lineInStation.Id = s.LineStationIndex;
            return lineInStation;
        }
    }
}
