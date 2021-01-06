using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IDL
    {
        #region Bus
        IEnumerable<DO.Bus> GetAllBuses(); //V
        IEnumerable<DO.Bus> GetAllBusesBy(Predicate<DO.Bus> predicate);
        DO.Bus GetBus(int id);//V
        void AddBus(DO.Bus bus);//V
        void UpdateBus(DO.Bus bus);//V

        /// <summary>
        /// method that knows to updt specific fields in Bus
        /// </summary>
        /// <param name="id"></param>
        /// <param name="update"></param>
        void UpdateBus(int id, Action<DO.Bus> update); 
        void DeleteBus(int id);//V
        #endregion

        #region BusOnTrip
        IEnumerable<DO.BusOnTrip> GetAllBusesOnTrip();
        IEnumerable<DO.BusOnTrip> GetAllBusesOnTripBy(Predicate<DO.BusOnTrip> predicate);
        DO.BusOnTrip GetBusOnTrip(int id, int licenseNumber);
        void AddBusOnTrip(DO.BusOnTrip bus);
        void UpdateBusOnTrip(DO.BusOnTrip bus);
        void UpdateBusOnTrip(int id, int licenseNumber, Action<DO.BusOnTrip> update); //method that knows to updt specific fields in BusOnTrip
        void DeleteBusOnTrip(int id, int licenseNumber);
        #endregion

        #region LineTrip
        IEnumerable<DO.LineTrip> GetAllLinesTrip();
        IEnumerable<DO.LineTrip> GetAllLinesTripBy(Predicate<DO.LineTrip> predicate);
        DO.LineTrip GetLineTrip(int id, int lineId);
        void AddLineTrip(DO.LineTrip bus);
        void UpdateLineTrip(DO.LineTrip bus);
        void UpdateLineTrip(int id, int lineId, Action<DO.LineTrip> update); //method that knows to updt specific fields in LineTrip
        void DeleteLineTrip(int id, int lineId);
        #endregion

        #region Station
        IEnumerable<DO.Station> GetAllStationes();
        IEnumerable<DO.Station> GetAllStationesBy(Predicate<DO.Station> predicate);
        DO.Station GetStation(int id);
        void AddStation(DO.Station station);
        void UpdateStation(DO.Station station);
        void UpdateStation(int id, Action<DO.Station> update); //method that knows to updt specific fields in station
        void DeleteStation(int id);
        #endregion

        #region Line
        IEnumerable<DO.Line> GetAllLines();
        IEnumerable<DO.Line> GetAllLinesBy(Predicate<DO.Line> predicate);
        DO.Line GetLine(int id);
        void AddLine(DO.Line line);
        void UpdateLine(DO.Line line);
        void UpdateLine(int id, Action<DO.Line> update); //method that knows to updt specific fields in Line
        void DeleteLine(int id);
        #endregion

        #region LineStation
        IEnumerable<DO.LineStation> GetAllLinesStation();
        IEnumerable<DO.LineStation> GetAllLinesStationBy(Predicate<DO.LineStation> predicate);
        DO.LineStation GetLineStation(int id, int stationCode);
        void AddLineStation(DO.LineStation lineStation);
        void UpdateLineStation(DO.LineStation lineStation);
        void UpdateLineStation(int id, int stationCode, Action<DO.LineStation> update); //method that knows to updt specific fields in LineStation
        void DeleteLineStation(int id, int stationCode);
        #endregion

        #region User
        IEnumerable<DO.User> GetAllUsers();
        IEnumerable<DO.User> GetAllUsersBy(Predicate<DO.User> predicate);
        DO.User GetUser(string name);
        void AddUser(DO.User user);
        void UpdateUser(DO.User user);
        void UpdateUser(string name, Action<DO.User> update); //method that knows to updt specific fields in User
        void DeleteUser(string name);
        #endregion

        #region AdjacentStations
        IEnumerable<DO.AdjacentStations> GetAllAdjacentStations();
        IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsBy(Predicate<DO.AdjacentStations> predicate);
        DO.AdjacentStations GetAdjacentStations
            
            
            
            
            
            
            
            
            
            
            (int stationCode1, int stationCode2);
        void AddAdjacentStations(DO.AdjacentStations adjacentStations);
        void UpdateAdjacentStations(DO.AdjacentStations adjacentStations);
        void UpdateAdjacentStations(int stationCode1, int stationCode2, Action<DO.AdjacentStations> update); //method that knows to updt specific fields in AdjacentStations
        void DeleteAdjacentStations(int stationCode1, int stationCode2);
        #endregion

        #region Trip
        IEnumerable<DO.Trip> GetAllTrips();
        IEnumerable<DO.Trip> GetAllTripsBy(Predicate<DO.Trip> predicate);
        DO.Trip GetTrip(int id);
        void AddTrip(DO.Trip trip);
        void UpdateTrip(DO.Trip trip);
        void UpdateTrip(int id, Action<DO.Trip> update); //method that knows to updt specific fields in Trip
        void DeleteTrip(int id);
        #endregion
    }
}
