using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;
using DalApi;
namespace DalObject
{
    sealed class DalObject:IDL
    {
        #region singelton
        /// <summary>
        /// singleton pattern is a software design pattern that restricts the instantiation of a class to one "single" instance.
        /// This is useful when exactly one object is needed to coordinate actions across the system.
        /// </summary>
        static readonly DalObject instance = new DalObject();
        static DalObject() { }// static ctor to ensure instance init is done just before first usage
        DalObject() { } // default => private
        public static DalObject Instance { get => instance; }// The public Instance property to use
        #endregion

        //Implement IDL methods, CRUD

        #region Bus

        #region GetBus
        public DO.Bus GetBus(int id)
        {
            DO.Bus b = DataSource.ListBuses.Find(p => p.LicenseNum == id);

            if (b != null)
                return b.Clone();
            else
                throw new DO.BadPersonIdException(id, $"bad person id: {id}");
        }
        #endregion

        #region GetAllBuses
        public IEnumerable<DO.Bus> GetAllBuses()//פונקצית הרחבה
        {
            return from bus in DataSource.ListBuses
                   select bus.Clone();
        }
        #endregion
        //**************************************************************************//
        public IEnumerable<DO.Bus> GetAllBusesBy(Predicate<DO.Bus> predicate)
        {
            throw new NotImplementedException();
        }

        #region AddBus
        public void AddBus(DO.Bus bus)
        {
            if (DataSource.ListBuses.FirstOrDefault(p => p.LicenseNum == bus.LicenseNum) != null)
                throw new DO.BadPersonIdException(bus.LicenseNum, "Duplicate person ID");
            DataSource.ListBuses.Add(bus.Clone());
        }
        #endregion

        #region DeleteBus
        public void DeleteBus(int id)
        {
            DO.Bus b = DataSource.ListBuses.Find(p => p.LicenseNum == id);

            if (b != null)
            {
                DataSource.ListBuses.Remove(b);
            }
            else
                throw new DO.BadPersonIdException(id, $"bad person id: {id}");
        }
        #endregion

        #region UpdateBus(DO.Bus bus)

        public void UpdateBus(DO.Bus bus)
        {
            DO.Bus b = DataSource.ListBuses.Find(p => p.LicenseNum == bus.LicenseNum);

            if (b != null)
            {
                DataSource.ListBuses.Remove(b);
                DataSource.ListBuses.Add(bus.Clone());
            }
            else
                throw new DO.BadPersonIdException(bus.LicenseNum, $"bad person id: {bus.LicenseNum}");
        }
        #endregion

        public void UpdateBus(int id, Action<DO.Bus> update)
        {
            throw new NotImplementedException();
        }
        #endregion Bus

        #region BusOnTrip

        #region GetBusOnTrip
        public DO.BusOnTrip GetBusOnTrip(int id)
        {
            DO.BusOnTrip b = DataSource.ListBusesOnTrip.Find(p => p.Id == id);

            if (b != null)
                return b.Clone();
            else
                throw new DO.BadPersonIdException(id, $"bad person id: {id}");
        }
        #endregion GetBusOnTrip

        #region GetAllBusesOnTrip
        IEnumerable<DO.BusOnTrip> GetAllBusesOnTrip()
        {
            return from bus in DataSource.ListBusesOnTrip
                   select bus.Clone();
        }
        #endregion GetAllBusesOnTrip
        IEnumerable<DO.BusOnTrip> GetAllBusesOnTripBy(Predicate<DO.BusOnTrip> predicate)
        {

        }

        void AddBusOnTrip(DO.BusOnTrip bus)
        {
            /****************************************************************************************/
            if (DataSource.ListBusesOnTrip.FirstOrDefault(p => p.Id == bus.Id) != null)
                throw new DO.BadPersonIdException(bus.Id, "Duplicate person ID");
            DataSource.ListBusesOnTrip.Add(bus.Clone());
        }
        #region UpdateBusOnTrip
        void UpdateBusOnTrip(DO.BusOnTrip bus)
        {
            DO.BusOnTrip b = DataSource.ListBusesOnTrip.Find(p => p.Id == bus.Id);

            if (b != null)
            {
                DataSource.ListBusesOnTrip.Remove(b);
                DataSource.ListBusesOnTrip.Add(bus.Clone());
            }
            else
                throw new DO.BadPersonIdException(bus.Id, $"bad person id: {bus.Id}");
        }
        #endregion UpdateBusOnTrip

        /// <summary>
        /// method that knows to updt specific fields in BusOnTrip
        /// </summary>
        /// <param name="id"></param>
        /// <param name="update"></param>
        void UpdateBusOnTrip(int id, Action<DO.BusOnTrip> update)
        {

        }
        void DeleteBusOnTrip(int id)
        {
            DO.BusOnTrip b = DataSource.ListBusesOnTrip.Find(p => p.Id == id);

            if (b != null)
            {
                DataSource.ListBusesOnTrip.Remove(b);
            }
            else
                throw new DO.BadPersonIdException(id, $"bad person id: {id}");
        }
        #endregion BusOnTrip


        #region LineTrip
        IEnumerable<DO.LineTrip> GetAllLinesTrip();
        IEnumerable<DO.BusOnTrip> GetAllLinesTripBy(Predicate<DO.LineTrip> predicate);
        DO.LineTrip GetLineTrip(int id);
        void AddLineTrip(DO.LineTrip bus);
        void UpdateLineTrip(DO.LineTrip bus);
        void UpdateLineTrip(int id, Action<DO.LineTrip> update); //method that knows to updt specific fields in LineTrip
        void DeleteLineTrip(int id);
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
        DO.LineStation GetLineStation(int id);
        void AddLineStation(DO.LineStation lineStation);
        void UpdateLineStation(DO.LineStation lineStation);
        void UpdateLineStation(int id, Action<DO.LineStation> update); //method that knows to updt specific fields in LineStation
        void DeleteLineStation(int id);
        #endregion

        #region User
        IEnumerable<DO.User> GetAllUsers();
        IEnumerable<DO.User> GetAllUsersBy(Predicate<DO.User> predicate);
        DO.User GetUser(int id);
        void AddUser(DO.User user);
        void UpdateUser(DO.User user);
        void UpdateUser(int id, Action<DO.User> update); //method that knows to updt specific fields in User
        void DeleteUser(int id);
        #endregion

        #region AdjacentStations
        IEnumerable<DO.AdjacentStations> GetAllAdjacentStations();
        IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsBy(Predicate<DO.AdjacentStations> predicate);
        DO.AdjacentStations GetAdjacentStationsr(int id);
        void AddAdjacentStations(DO.AdjacentStations adjacentStations);
        void UpdateAdjacentStations(DO.AdjacentStations adjacentStations);
        void UpdateAdjacentStations(int id, Action<DO.AdjacentStations> update); //method that knows to updt specific fields in AdjacentStations
        void DeleteAdjacentStations(int id);
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
