using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;
using DalApi;
namespace DalObject
{
    sealed class DalObject : IDL
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
        #region GetAllBusesBy
        public IEnumerable<DO.Bus> GetAllBusesBy(Predicate<DO.Bus> predicate)
        {
            throw new NotImplementedException();
        }
        #endregion

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

        #region UpdateBus

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

        #region UpdateBus
        public void UpdateBus(int id, Action<DO.Bus> update)
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion

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
        #endregion

        #region GetAllBusesOnTrip
        IEnumerable<DO.BusOnTrip> GetAllBusesOnTrip()
        {
            return from bus in DataSource.ListBusesOnTrip
                   select bus.Clone();
        }
        #endregion

        #region GetAllBusesOnTripBy
        IEnumerable<DO.BusOnTrip> GetAllBusesOnTripBy(Predicate<DO.BusOnTrip> predicate)
        {

        }
        #endregion

        #region AddBusOnTrip
        void AddBusOnTrip(DO.BusOnTrip bus)
        {
            /****************************************************************************************/
            if (DataSource.ListBusesOnTrip.FirstOrDefault(p => p.Id == bus.Id) != null)
                throw new DO.BadPersonIdException(bus.Id, "Duplicate person ID");
            DataSource.ListBusesOnTrip.Add(bus.Clone());
        }
        #endregion

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

        #region UpdateBusOnTrip

        /// <summary>
        /// method that knows to updt specific fields in BusOnTrip
        /// </summary>
        /// <param name="id"></param>
        /// <param name="update"></param>
        void UpdateBusOnTrip(int id, Action<DO.BusOnTrip> update)
        {

        }
        #endregion

        #region DeleteBusOnTrip
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
        #endregion
        #endregion

        #region LineTrip
        #region GetAllLinesTrip
        IEnumerable<DO.LineTrip> GetAllLinesTrip()
        {
            return from lt in DataSource.ListLinesTrip
                   select lt.Clone();
        }
        #endregion

        #region GetAllLinesTripBy
        IEnumerable<DO.BusOnTrip> GetAllLinesTripBy(Predicate<DO.LineTrip> predicate)
        {

        }
        #endregion

        #region GetLineTrip
        DO.LineTrip GetLineTrip(int id)
        {
            DO.LineTrip lt = DataSource.ListLinesTrip.Find(p => p.Id == id);

            if (lt != null)
                return lt.Clone();
            else
                throw new DO.BadPersonIdException(id, $"bad person id: {id}");
        }
        #endregion

        #region AddLineTrip
        void AddLineTrip(DO.LineTrip lt)
        {
            if (DataSource.ListLinesTrip.FirstOrDefault(p => p.Id == lt.Id) != null)
                throw new DO.BadPersonIdException(lt.Id, "Duplicate person ID");
            DataSource.ListLinesTrip.Add(lt.Clone());
        }
        #endregion

        #region UpdateLineTrip
        void UpdateLineTrip(DO.LineTrip bus)
        {

            DO.LineTrip b = DataSource.ListLinesTrip.Find(p => p.Id == bus.Id);

            if (b != null)
            {
                DataSource.ListLinesTrip.Remove(b);
                DataSource.ListLinesTrip.Add(bus.Clone());
            }
            else
                throw new DO.BadPersonIdException(bus.Id, $"bad person id: {bus.Id}");
        }
        #endregion

        #region UpdateLineTrip
        void UpdateLineTrip(int id, Action<DO.LineTrip> update) //method that knows to updt specific fields in LineTrip
        {
            throw new NotImplementedException();
        }
        #endregion

        #region DeleteLineTrip
        void DeleteLineTrip(int id)
        {
            DO.LineTrip b = DataSource.ListLinesTrip.Find(p => p.Id == id);

            if (b != null)
            {
                DataSource.ListLinesTrip.Remove(b);
            }
            else
                throw new DO.BadPersonIdException(id, $"bad person id: {id}");
        }
        #endregion
        #endregion

        #region Station

        #region GetAllStationes
        IEnumerable<DO.Station> GetAllStationes()
        {
            return from bus in DataSource.ListStations
                   select bus.Clone();
        }
        #endregion

        #region GetAllStationesBy
        IEnumerable<DO.Station> GetAllStationesBy(Predicate<DO.Station> predicate)
        {

        }
        #endregion

        #region GetStation
        DO.Station GetStation(int code)
        {
            DO.Bus b = DataSource.ListStations.Find(p => p.Code == Code);

            if (b != null)
                return b.Clone();
            else
                throw new DO.BadPersonIdException(code, $"bad person id: {code}");
        }
        #endregion

        #region AddStation
        void AddStation(DO.Station station)
        {
            if (DataSource.ListStations.FirstOrDefault(p => p.Code == station.Code) != null)
                throw new DO.BadPersonIdException(station.Code, "Duplicate person ID");
            DataSource.ListStations.Add(station.Clone());

        }
        #endregion

        #region UpdateStation
        void UpdateStation(DO.Station station)
        {
            DO.Station b = DataSource.ListStations.Find(p => p.Code == station.Code);

            if (b != null)
            {
                DataSource.ListStations.Remove(b);
                DataSource.ListStations.Add(station.Clone());
            }
            else
                throw new DO.BadPersonIdException(station.Code, $"bad person id: {station.Code}");
        }
        #endregion

        #region UpdateStation
        void UpdateStation(int id, Action<DO.Station> update) //method that knows to updt specific fields in station
        {

        }
        #endregion

        #region DeleteStation
        void DeleteStation(int code)
        {
            DO.Station b = DataSource.ListStations.Find(p => p.Code == code);

            if (b != null)
            {
                DataSource.ListStations.Remove(b);
            }
            else
                throw new DO.BadPersonIdException(code, $"bad person id: {code}");
        }
        #endregion
        #endregion

        #region Line
        #region GetAllLines
        IEnumerable<DO.Line> GetAllLines()
        {
            return from bus in DataSource.ListLines
                   select bus.Clone();

        }
        #endregion

        #region GetAllLinesBy
        IEnumerable<DO.Line> GetAllLinesBy(Predicate<DO.Line> predicate)
        {

        }
        #endregion

        #region GetLine
        DO.Line GetLine(int id)
        {
            DO.Line l = DataSource.ListLines.Find(p => p.Id == id);

            if (l != null)
                return l.Clone();
            else
                throw new DO.BadPersonIdException(id, $"bad person id: {id}");
        }
        #endregion

        #region AddLine
        void AddLine(DO.Line line)
        {
            if (DataSource.ListLines.FirstOrDefault(p => p.Id == line.Id) != null)
                throw new DO.BadPersonIdException(line.Id, "Duplicate person ID");
            DataSource.ListBuses.Add(line.Clone());
        }
        #endregion

        #region UpdateLine
        void UpdateLine(DO.Line line)
        {
            DO.Line l = DataSource.ListLines.Find(p => p.Id == line.Id);

            if (l != null)
            {
                DataSource.ListLines.Remove(l);
                DataSource.ListLines.Add(line.Clone());
            }
            else
                throw new DO.BadPersonIdException(line.Id, $"bad person id: {line.Id}");

        }
        #endregion

        #region UpdateLine
        void UpdateLine(int id, Action<DO.Line> update) //method that knows to updt specific fields in Line
        {
            throw new NotImplementedException();
        }
        #endregion

        #region DeleteLine
        void DeleteLine(int id)
        {
            DO.Line l = DataSource.ListLines.Find(p => p.Id == id);

            if (l != null)
            {
                DataSource.ListLines.Remove(l);
            }
            else
                throw new DO.BadPersonIdException(id, $"bad person id: {id}");
        }
        #endregion
        #endregion

        #region LineStation
        #region GetAllLinesStation
        IEnumerable<DO.LineStation> GetAllLinesStation()
        {
            return from bus in DataSource.ListLineStations
                   select bus.Clone();
        }
        #endregion

        #region GetAllLinesStationBy
        IEnumerable<DO.LineStation> GetAllLinesStationBy(Predicate<DO.LineStation> predicate)
        {

        }
        #endregion

        #region GetLineStation
        DO.LineStation GetLineStation(int id)
        {
            DO.LineStation b = DataSource.ListLineStations.Find(p => p.LineId == id);

            if (b != null)
                return b.Clone();
            else
                throw new DO.BadPersonIdException(id, $"bad person id: {id}");
        }
        #endregion

        #region AddLineStation
        void AddLineStation(DO.LineStation lineStation)
        {
            if (DataSource.ListLineStations.FirstOrDefault(p => p.LineId == lineStation.LineId) != null)
                throw new DO.BadPersonIdException(lineStation.LineId, "Duplicate person ID");
            DataSource.ListLineStations.Add(lineStation.Clone());
        }
        #endregion

        #region UpdateLineStation
        void UpdateLineStation(DO.LineStation lineStation)
        {
            DO.LineStation b = DataSource.ListLineStations.Find(p => p.LineId == lineStation.LineId);

            if (b != null)
            {
                DataSource.ListLineStations.Remove(b);
                DataSource.ListLineStations.Add(lineStation.Clone());
            }
            else
                throw new DO.BadPersonIdException(lineStation.LineId $"bad person id: {lineStation.LineId}");
        }
        #endregion

        #region UpdateLineStation
        void UpdateLineStation(int id, Action<DO.LineStation> update) //method that knows to updt specific fields in LineStation
        {
            throw new NotImplementedException();
        }
        #endregion

        #region DeleteLineStation
        void DeleteLineStation(int id)
        {
            DO.LineStation b = DataSource.ListLineStations.Find(p => p.LineId == id);

            if (b != null)
            {
                DataSource.ListLineStations.Remove(b);
            }
            else
                throw new DO.BadPersonIdException(id, $"bad person id: {id}");
        }
        #endregion
        #endregion

        #region User
        #region GetAllUsers
        IEnumerable<DO.User> GetAllUsers()
        {
            return from user in DataSource.ListUsers
                   select user.Clone();
        }
        #endregion

        #region GetAllUsersBy
        IEnumerable<DO.User> GetAllUsersBy(Predicate<DO.User> predicate)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region GetUser
        DO.User GetUser(string name)
        {
            DO.User u = DataSource.ListUsers.Find(p => p.UserName == name);

            if (u != null)
                return u.Clone();
            else
                throw new DO.BadPersonIdException(name, $"bad person id: {name}");
        }
        #endregion

        #region AddUser
        void AddUser(DO.User user)
        {
            if (DataSource.ListUsers.FirstOrDefault(p => p.UserName == user.UserName) != null)
                throw new DO.BadPersonIdException(user.UserName, "Duplicate person ID");
            DataSource.ListUsers.Add(user.Clone());
        }
        #endregion

        #region UpdateUser
        void UpdateUser(DO.User user)
        {
            DO.User u = DataSource.ListUsers.Find(p => p.UserName == user.UserName);

            if (u != null)
            {
                DataSource.ListUsers.Remove(u);
                DataSource.ListUsers.Add(user.Clone());
            }
            else
                throw new DO.BadPersonIdException(user.UserName, $"bad person id: {user.UserName}");
        }
        #endregion

        #region UpdateUser
        void UpdateUser(int id, Action<DO.User> update)//method that knows to updt specific fields in User
        {

        }
        #endregion

        #region DeleteUser
        void DeleteUser(string userName)
        {
            DO.User u = DataSource.ListUsers.Find(p => p.UserName == userName);

            if (u != null)
            {
                DataSource.ListUsers.Remove(u);
            }
            else
                throw new DO.BadPersonIdException(userName, $"bad person id: {userName}");
        }
        #endregion
        #endregion

        #region AdjacentStations

        #region GetAllAdjacentStations
        IEnumerable<DO.AdjacentStations> GetAllAdjacentStations()
        {
            return from adjacentStations in DataSource.ListAdjacentStations
                   select adjacentStations.Clone();
        }
        #endregion

        #region GetAllAdjacentStationsBy
        IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsBy(Predicate<DO.AdjacentStations> predicate)
        {
            throw new NotImplementedException();
        }
        #endregion

       #region GetAdjacentStations
        DO.AdjacentStations GetAdjacentStations(int id)
        {
                DO.AdjacentStations a = DataSource.ListAdjacentStations.Find(p =>( p.CodeStation1 == id)&&(p.CodeStation2 == id));

                if (a != null)
                    return a.Clone();
                else
                    throw new DO.BadPersonIdException(id,  $"bad person id: {id}");
        }
        #endregion

        #region AddAdjacentStations
        void AddAdjacentStations(DO.AdjacentStations adjacentStations)
        {
            if (DataSource.ListAdjacentStations.FirstOrDefault(p => (p.CodeStation1== adjacentStations.CodeStation1) && (p.CodeStation2 == adjacentStations.CodeStation2)) != null);
                    throw new DO.BadPersonIdException(adjacentStations.CodeStation1, adjacentStations.CodeStation2, "Duplicate person ID");
            else
                DataSource.ListAdjacentStations.Add(adjacentStations.Clone());
        }
        #endregion

        #region UpdateAdjacentStations
        void UpdateAdjacentStations(DO.AdjacentStations adjacentStations)
        {
            DO.AdjacentStations u = DataSource.ListAdjacentStations.Find(p => ((p.CodeStation1 == adjacentStations.CodeStation1) && (p.CodeStation2 == adjacentStations.CodeStation2)));
                if (u != null)
                {
                    DataSource.ListAdjacentStations.Remove(u);
                    DataSource.ListAdjacentStations.Add(adjacentStations.Clone());
                }
                else
                    throw new DO.BadPersonIdException(adjacentStations.CodeStation1, adjacentStations.CodeStation2, $"bad person id: {adjacentStations.CodeStation1, adjacentStations.CodeStation2}");
        }
        #endregion

        #region UpdateAdjacentStations
        void UpdateAdjacentStations(int id, Action<DO.AdjacentStations> update) //method that knows to updt specific fields in AdjacentStations
        {
            throw new NotImplementedException();
        }
        #endregion

        #region DeleteAdjacentStations
        void DeleteAdjacentStations(int id)
        {
                DO.AdjacentStations u = DataSource.ListAdjacentStations.Find(p => (p.CodeStation1 == id) && (p.CodeStation2 == id));

            if (u != null)
                {
                    DataSource.ListAdjacentStations.Remove(u);
                }
                else
                    throw new DO.BadPersonIdException(id, $"bad person id: {id}");
        }
        #endregion
        #endregion

        #region Trip
        #region GetAllTrips
        IEnumerable<DO.Trip> GetAllTrips()
        {
            return from user in DataSource.ListTrips
                   select user.Clone();
        }
        #endregion

        #region GetAllTripsBy
        IEnumerable<DO.Trip> GetAllTripsBy(Predicate<DO.Trip> predicate)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region GetTrip
        DO.Trip GetTrip(int id)
        {
            DO.Trip t = DataSource.ListTrips.Find(p => p.Id == id);

            if (t != null)
                return t.Clone();
            else
                throw new DO.BadPersonIdException(id, $"bad person id: {id}");
        }
        #endregion

        #region AddTrip
        void AddTrip(DO.Trip trip)
        {
            if (DataSource.ListTrips.FirstOrDefault(p => p.Id == trip.Id) != null)
                throw new DO.BadPersonIdException(trip.Id, "Duplicate person ID");
            DataSource.ListTrips.Add(trip.Clone());
        }
        #endregion

        #region UpdateTrip
        void UpdateTrip(DO.Trip trip)
        {
            DO.Trip u = DataSource.ListTrips.Find(p => p.Id == trip.Id);

            if (u != null)
            {
                DataSource.ListTrips.Remove(u);
                DataSource.ListTrips.Add(trip.Clone());
            }
            else
                throw new DO.BadPersonIdException(trip.Id, $"bad person id: {trip.Id}");
        }
        #endregion

        #region UpdateTrip
        void UpdateTrip(int id, Action<DO.Trip> update) //method that knows to updt specific fields in Trip
        {

        }
        #endregion

        #region DeleteTrip
        void DeleteTrip(int id)
        {
            DO.Trip u = DataSource.ListTrips.Find(p => p.Id == id);

            if (u != null)
            {
                DataSource.ListTrips.Remove(u);
            }
            else
                throw new DO.BadPersonIdException(id, $"bad person id: {id}");
        }
        #endregion
        #endregion
    }
}
