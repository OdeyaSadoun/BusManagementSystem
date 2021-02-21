using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BO;

namespace BLApi
{
    public interface IBL
    {
        #region Bus
        IEnumerable<BO.Bus> GetAllBuses();
        BO.Bus GetBus(int id);
        void AddBus(BO.Bus bus);
        void UpdateBus(BO.Bus bus);
        void DeleteBus(BO.Bus busBO);
        void Threatment(BO.Bus bus);
        void Refuel(BO.Bus bus);
        #endregion

        #region LineTrip
        IEnumerable<BO.LineTrip> GetAllLinesTrip();
        void AddLineTrip(BO.LineTrip ltrip);

        #endregion

        #region Station
        IEnumerable<BO.Station> GetAllStations();
        BO.Station GetStation(int id);
        void AddStation(BO.Station station);
        void UpdateStation(BO.Station station);
        void DeleteStation(BO.Station station);

        #endregion

        #region Line
        IEnumerable<BO.Line> GetAllLines();
        BO.Line GetLine(int id);
        BO.Line GetLineNumber(int num);
        void AddLine(BO.Line line);
        void UpdateLine(BO.Line line);
        void DeleteLine(BO.Line line);
        #endregion

        #region StationInLine
        void DeleteStationInLine(BO.StationInLine sInL);
        IEnumerable<BO.StationInLine> GetAllStationsInLine();
        IEnumerable<BO.StationInLine> GetAllStationesInLineBy(int id);
        BO.StationInLine GetStationInLine(int stationCode, int lineId);
        void AddStationInLine(BO.Station s, BO.Line l);
        #endregion

        #region User
        IEnumerable<BO.User> GetAllUsers();
        BO.User GetUser(string name);
        void AddUser(BO.User user);
        void UpdateUser(BO.User user);
        void DeleteUser(BO.User userBO);
        void Charge(int balance, BO.User user);

        #endregion

        IEnumerable<BO.LineTiming> GetLineTimingsPerStation(BO.Station station, TimeSpan tsCurentTime);

        
    }
}