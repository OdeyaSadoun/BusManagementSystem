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
            DO.Bus b = DataSource.ListBuses.Find(p => ((p.LicenseNumber == id)&&(p.IsDeleted==false)));

            if (b != null)
                return b.Clone();
            else
                throw new Exception();
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
            return from bus in DataSource.ListBuses
                   where predicate(bus)
                   select bus.Clone();
        }
        #endregion

        #region AddBus
        public void AddBus(DO.Bus bus)
        {
            if (DataSource.ListBuses.FirstOrDefault(p => p.LicenseNumber == bus.LicenseNumber && p.IsDeleted) != null)
                throw new Exception();
            DataSource.ListBuses.Add(bus.Clone());
        }
        #endregion

        #region DeleteBus
        public void DeleteBus(int id)
        {
            DO.Bus b = DataSource.ListBuses.Find(p => p.LicenseNumber == id && p.IsDeleted);

            if (b != null)
            {
                //DataSource.ListBuses.Remove(b);
                b.IsDeleted = true;
            }
            else
                throw new Exception();
        }
        #endregion

        #region UpdateBus

        public void UpdateBus(DO.Bus bus)
        {
            DO.Bus b = DataSource.ListBuses.Find(p => p.LicenseNumber == bus.LicenseNumber && p.IsDeleted);

            if (b != null)
            {
                DataSource.ListBuses.Remove(b);
                DataSource.ListBuses.Add(bus.Clone());
            }
            else
                throw new Exception();
        }
        #endregion

        #region UpdateBus
        public void UpdateBus(int licenseNumber, Action<DO.Bus> update)
        {
            DO.Bus b = DataSource.ListBuses.Find(p => p.LicenseNumber == licenseNumber && p.IsDeleted == false);
            if (b == null)
                throw new Exception();
            update(b);
        }
        #endregion
        #endregion

        #region BusOnTrip
        #region GetBusOnTrip
        public DO.BusOnTrip GetBusOnTrip(int id, int licenseNumber)
        {
            DO.BusOnTrip b = DataSource.ListBusesOnTrip.Find(p => p.Id == id && p.LicenseNumber == licenseNumber && p.IsDeleted);

            if (b != null)
                return b.Clone();
            else
                throw new Exception();
        }
        #endregion

        #region GetAllBusesOnTrip
        public IEnumerable<DO.BusOnTrip> GetAllBusesOnTrip()
        {
            return from bus in DataSource.ListBusesOnTrip
                   select bus.Clone();
        }
        #endregion

        #region GetAllBusesOnTripBy
        public IEnumerable<DO.BusOnTrip> GetAllBusesOnTripBy(Predicate<DO.BusOnTrip> predicate)
        {
            return from bus in DataSource.ListBusesOnTrip
                   where predicate(bus)
                   select bus.Clone();
        }
        #endregion

        #region AddBusOnTrip
        public void AddBusOnTrip(DO.BusOnTrip bus)
        {
            /****************************************************************************************/
            if (DataSource.ListBusesOnTrip.FirstOrDefault(p => p.Id == bus.Id && p.LicenseNumber == bus.LicenseNumber && p.IsDeleted) != null)
                throw new Exception();
            DataSource.ListBusesOnTrip.Add(bus.Clone());
        }
        #endregion

        #region UpdateBusOnTrip
        public void UpdateBusOnTrip(DO.BusOnTrip bus)
        {
            DO.BusOnTrip b = DataSource.ListBusesOnTrip.Find(p => p.Id == bus.Id && p.LicenseNumber == bus.LicenseNumber && p.IsDeleted);

            if (b != null)
            {
                DataSource.ListBusesOnTrip.Remove(b);
                DataSource.ListBusesOnTrip.Add(bus.Clone());
            }
            else
                throw new Exception();
        }
        #endregion UpdateBusOnTrip

        #region UpdateBusOnTrip

        /// <summary>
        /// method that knows to updt specific fields in BusOnTrip
        /// </summary>
        /// <param name="id"></param>
        /// <param name="update"></param>
        public void UpdateBusOnTrip(int id, int licenseNumber, Action<DO.BusOnTrip> update)
        {
            DO.BusOnTrip b = DataSource.ListBusesOnTrip.Find(p => p.Id == id && p.LicenseNumber == licenseNumber  && p.IsDeleted == false);
            if (b == null)
                throw new Exception();
            update(b);
        }
        #endregion

        #region DeleteBusOnTrip
        public void DeleteBusOnTrip(int id, int licenseNumber)
        {
            DO.BusOnTrip b = DataSource.ListBusesOnTrip.Find(p => p.Id == id && p.LicenseNumber == licenseNumber && p.IsDeleted);

            if (b != null)
            {
                DataSource.ListBusesOnTrip.Remove(b);
            }
            else
                throw new Exception();
        }
        #endregion
        #endregion

        #region LineTrip
        #region GetAllLinesTrip
        public IEnumerable<DO.LineTrip> GetAllLinesTrip()
        {
            return from lt in DataSource.ListLinesTrip
                   select lt.Clone();
        }
        #endregion

        #region GetAllLinesTripBy
        public IEnumerable<DO.LineTrip> GetAllLinesTripBy(Predicate<DO.LineTrip> predicate)
        {
            return from trip in DataSource.ListLinesTrip
                   where predicate(trip)
                   select trip.Clone();
        }
        #endregion

        #region GetLineTrip
        public DO.LineTrip GetLineTrip(int id, int lineId)
        {
            DO.LineTrip lt = DataSource.ListLinesTrip.Find(p => p.Id == id && p.LineId== lineId && p.IsDeleted);

            if (lt != null)
                return lt.Clone();
            else
                throw new Exception();
        }
        #endregion

        #region AddLineTrip
        public void AddLineTrip(DO.LineTrip lt)
        {
            if (DataSource.ListLinesTrip.FirstOrDefault(p => p.Id == lt.Id && p.LineId == lt.LineId && p.IsDeleted) != null)
                throw new Exception();
            DataSource.ListLinesTrip.Add(lt.Clone());
        }
        #endregion

        #region UpdateLineTrip
        public void UpdateLineTrip(DO.LineTrip bus)
        {

            DO.LineTrip b = DataSource.ListLinesTrip.Find(p => p.Id == bus.Id && p.LineId == bus.LineId && p.IsDeleted);

            if (b != null)
            {
                DataSource.ListLinesTrip.Remove(b);
                DataSource.ListLinesTrip.Add(bus.Clone());
            }
            else
                throw new Exception();
        }
        #endregion

        #region UpdateLineTrip
        public void UpdateLineTrip(int id, int lineId, Action<DO.LineTrip> update) //method that knows to updt specific fields in LineTrip
        {

            DO.LineTrip b = DataSource.ListLinesTrip.Find(p => p.Id == id && p.LineId == lineId  && p.IsDeleted == false);
            if (b == null)
                throw new Exception();
            update(b);
        }
        #endregion

        #region DeleteLineTrip
        public void DeleteLineTrip(int id, int lineId)
        {
            DO.LineTrip b = DataSource.ListLinesTrip.Find(p => p.Id == id && p.LineId == lineId  && p.IsDeleted);

            if (b != null)
            {
                DataSource.ListLinesTrip.Remove(b);
            }
            else
                throw new Exception();
        }
        #endregion
        #endregion

        #region Station

        #region GetAllStationes
        public IEnumerable<DO.Station> GetAllStationes()
        {
            return from bus in DataSource.ListStations
                   select bus.Clone();
        }
        #endregion

        #region GetAllStationesBy
        public IEnumerable<DO.Station> GetAllStationesBy(Predicate<DO.Station> predicate)
        {
            return from station in DataSource.ListStations
                   where predicate(station)
                   select station.Clone();
        }
        #endregion

        #region GetStation
        public DO.Station GetStation(int code)
        {
            DO.Station b = DataSource.ListStations.Find(p => p.Code == code && p.IsDeleted);

            if (b != null)
                return b.Clone();
            else
                throw new Exception();
        }
        #endregion

        #region AddStation
        public void AddStation(DO.Station station)
        {
            if (DataSource.ListStations.FirstOrDefault(p => p.Code == station.Code && p.IsDeleted) != null)
                throw new Exception();
            DataSource.ListStations.Add(station.Clone());

        }
        #endregion

        #region UpdateStation
        public void UpdateStation(DO.Station station)
        {
            DO.Station b = DataSource.ListStations.Find(p => p.Code == station.Code && p.IsDeleted);

                if (b != null)
                {
                    DataSource.ListStations.Remove(b);
                    DataSource.ListStations.Add(station.Clone());
                }
                else
                throw new Exception();
        }
        #endregion

        #region UpdateStation
        public void UpdateStation(int id, Action<DO.Station> update) //method that knows to updt specific fields in station
            {
                DO.Station station = DataSource.ListStations.Find(p => p.Code == id && p.IsDeleted == false);
                if (station == null)
                    throw new Exception();
                update(station);
            }
        #endregion

        #region DeleteStation
        public void DeleteStation(int code)
        {
            DO.Station b = DataSource.ListStations.Find(p => p.Code == code && p.IsDeleted);

            if (b != null)
            {
                DataSource.ListStations.Remove(b);
            }
            else
                throw new Exception();
        }
        #endregion
        #endregion

        #region Line
        #region GetAllLines
        public IEnumerable<DO.Line> GetAllLines()
        {
            return from bus in DataSource.ListLines
                   select bus.Clone();

        }
        #endregion

        #region GetAllLinesBy
        public IEnumerable<DO.Line> GetAllLinesBy(Predicate<DO.Line> predicate)
        {
                return from line in DataSource.ListLines
                       where predicate(line)
                       select line.Clone();

        }
        #endregion

        #region GetLine
        public DO.Line GetLine(int id)
        {
            DO.Line l = DataSource.ListLines.Find(p => p.Id == id && p.IsDeleted);

            if (l != null)
                return l.Clone();
            else
                throw new Exception();
        }
        #endregion

        #region AddLine
        public void AddLine(DO.Line line)
        {
            if (DataSource.ListLines.FirstOrDefault(p => p.Id == line.Id && p.IsDeleted) != null)
                throw new Exception();
            DataSource.ListLines.Add(line.Clone());
        }
        #endregion

        #region UpdateLine
        public void UpdateLine(DO.Line line)
        {
            DO.Line l = DataSource.ListLines.Find(p => p.Id == line.Id && p.IsDeleted);

            if (l != null)
            {
                DataSource.ListLines.Remove(l);
                DataSource.ListLines.Add(line.Clone());
            }
            else
                throw new Exception();

        }
        #endregion

        #region UpdateLine
        public void UpdateLine(int id, Action<DO.Line> update) //method that knows to updt specific fields in Line
        {
                DO.Line line = DataSource.ListLines.Find(p => p.Id == id && p.IsDeleted == false);
                if (line == null)
                    throw new Exception();
                update(line);
        }
        #endregion

        #region DeleteLine
        public void DeleteLine(int id)
        {
            DO.Line l = DataSource.ListLines.Find(p => p.Id == id && p.IsDeleted);

            if (l != null)
            {
                DataSource.ListLines.Remove(l);
            }
            else
                throw new Exception();
        }
        #endregion
        #endregion

        #region LineStation
        #region GetAllLinesStation
        public IEnumerable<DO.LineStation> GetAllLinesStation()
        {
            return from bus in DataSource.ListLineStations
                   select bus.Clone();
        }
        #endregion

        #region GetAllLinesStationBy
        public IEnumerable<DO.LineStation> GetAllLinesStationBy(Predicate<DO.LineStation> predicate)
        {
                return from line in DataSource.ListLineStations
                       where predicate(line)
                       select line.Clone();

        }
        #endregion

        #region GetLineStation
        public DO.LineStation GetLineStation(int lineId, int stationCode)
        {
            DO.LineStation b = DataSource.ListLineStations.Find(p => p.LineId == lineId && p.StationCode== stationCode && p.IsDeleted);

            if (b != null)
                return b.Clone();
            else
                throw new Exception();
        }
        #endregion

        #region AddLineStation
        public void AddLineStation(DO.LineStation lineStation)
        {
            if (DataSource.ListLineStations.FirstOrDefault(p => p.LineId == lineStation.LineId && p.StationCode==lineStation.StationCode && p.IsDeleted) != null)
                throw new Exception();
            DataSource.ListLineStations.Add(lineStation.Clone());
        }
        #endregion

        #region UpdateLineStation
        public void UpdateLineStation(DO.LineStation lineStation)
        {
            DO.LineStation b = DataSource.ListLineStations.Find(p => p.LineId == lineStation.LineId && p.StationCode == lineStation.StationCode && p.IsDeleted);

            if (b != null)
            {
                DataSource.ListLineStations.Remove(b);
                DataSource.ListLineStations.Add(lineStation.Clone());
            }
            else
                throw new Exception();
        }
        #endregion

        #region UpdateLineStation
        public void UpdateLineStation(int lineId,int stationCode, Action<DO.LineStation> update) //method that knows to updt specific fields in LineStation
        {
                DO.LineStation line = DataSource.ListLineStations.Find(p => (p.LineId == lineId && p.StationCode == stationCode && p.IsDeleted == false));
                if (line == null)
                    throw new Exception();
                update(line);
        }
        #endregion

        #region DeleteLineStation
        public void DeleteLineStation(int lineId, int stationCode)
        {
            DO.LineStation b = DataSource.ListLineStations.Find(p => p.LineId == lineId && p.StationCode== stationCode && p.IsDeleted);

            if (b != null)
            {
                DataSource.ListLineStations.Remove(b);
            }
            else
                throw new Exception();
        }
        #endregion
        #endregion

        #region User
        #region GetAllUsers
        public IEnumerable<DO.User> GetAllUsers()
        {
            return from user in DataSource.ListUsers
                   select user.Clone();
        }
        #endregion

        #region GetAllUsersBy
        public IEnumerable<DO.User> GetAllUsersBy(Predicate<DO.User> predicate)
        {
                return from user in DataSource.ListUsers
                       where predicate(user)
                       select user.Clone();
        }
        #endregion

        #region GetUser
        public DO.User GetUser(string name)
        {
            DO.User u = DataSource.ListUsers.Find(p => p.UserName == name && p.IsDeleted);

            if (u != null)
                return u.Clone();
            else
                throw new Exception();
        }
        #endregion

        #region AddUser
        public void AddUser(DO.User user)
        {
            if (DataSource.ListUsers.FirstOrDefault(p => p.UserName == user.UserName && p.IsDeleted) != null)
                throw new Exception();
            DataSource.ListUsers.Add(user.Clone());
        }
        #endregion

        #region UpdateUser
        public void UpdateUser(DO.User user)
        {
            DO.User u = DataSource.ListUsers.Find(p => p.UserName == user.UserName && p.IsDeleted);

            if (u != null)
            {
                DataSource.ListUsers.Remove(u);
                DataSource.ListUsers.Add(user.Clone());
            }
            else
                throw new Exception();
        }
        #endregion

        #region UpdateUser
        public void UpdateUser(string userName, Action<DO.User> update)//method that knows to updt specific fields in User
        {
                DO.User user = DataSource.ListUsers.Find(p => p.UserName == userName && p.IsDeleted == false);
                if (user == null)
                    throw new Exception();
                update(user);
        }
        #endregion

        #region DeleteUser
        public void DeleteUser(string userName)
        {
            DO.User u = DataSource.ListUsers.Find(p => p.UserName == userName && p.IsDeleted);

            if (u != null)
            {
                DataSource.ListUsers.Remove(u);
            }
            else
                throw new Exception();
        }
        #endregion
        #endregion

        #region AdjacentStations

        #region GetAllAdjacentStations
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStations()
        {
            return from adjacentStations in DataSource.ListAdjacentStations
                   select adjacentStations.Clone();
        }
        #endregion

        #region GetAllAdjacentStationsBy
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsBy(Predicate<DO.AdjacentStations> predicate)
        {
                return from adjacentStations in DataSource.ListAdjacentStations
                       where predicate(adjacentStations)
                       select adjacentStations.Clone();
        }
        #endregion

        #region GetAdjacentStations
        public DO.AdjacentStations GetAdjacentStations(int stationCode1, int stationCode2)
        {
                DO.AdjacentStations a = DataSource.ListAdjacentStations.Find(p =>( p.CodeStation1 == stationCode1) &&(p.CodeStation2 == stationCode2) && p.IsDeleted);

                if (a != null)
                    return a.Clone();
                else
                throw new Exception();
        }
        #endregion

        #region AddAdjacentStations
        public void AddAdjacentStations(DO.AdjacentStations adjacentStations)
        {
            if (DataSource.ListAdjacentStations.FirstOrDefault(p => (p.CodeStation1== adjacentStations.CodeStation1) && (p.CodeStation2 == adjacentStations.CodeStation2)&& p.IsDeleted) != null)
                      throw new Exception();
            else
                DataSource.ListAdjacentStations.Add(adjacentStations.Clone());
        }
        #endregion

        #region UpdateAdjacentStations
        public void UpdateAdjacentStations(DO.AdjacentStations adjacentStations)
        {
            DO.AdjacentStations u = DataSource.ListAdjacentStations.Find(p => ((p.CodeStation1 == adjacentStations.CodeStation1) && (p.CodeStation2 == adjacentStations.CodeStation2))&& p.IsDeleted);
                if (u != null)
                {
                    DataSource.ListAdjacentStations.Remove(u);
                    DataSource.ListAdjacentStations.Add(adjacentStations.Clone());
                }
                else
                throw new Exception();
        }
        #endregion

        #region UpdateAdjacentStations
        public void UpdateAdjacentStations(int stationCode1, int stationCode2, Action<DO.AdjacentStations> update) //method that knows to updt specific fields in AdjacentStations
        {
                DO.AdjacentStations adjacentStations = DataSource.ListAdjacentStations.Find(p => ((p.CodeStation1 == stationCode1 && p.CodeStation2 == stationCode2 && p.IsDeleted == false || p.CodeStation1 == stationCode2 && p.CodeStation2 == stationCode1) && p.IsDeleted == false));
                if (adjacentStations == null)
                    throw new Exception();
                update(adjacentStations);
        }
        #endregion

        #region DeleteAdjacentStations
        public void DeleteAdjacentStations(int stationCode1, int stationCode2)
        {
                DO.AdjacentStations u = DataSource.ListAdjacentStations.Find(p => (p.CodeStation1 == stationCode1) && (p.CodeStation2 == stationCode1) && p.IsDeleted);

            if (u != null)
                {
                    DataSource.ListAdjacentStations.Remove(u);
                }
                else
                throw new Exception();
        }
        #endregion
        #endregion

        #region Trip
        #region GetAllTrips
        public IEnumerable<DO.Trip> GetAllTrips()
        {
            return from user in DataSource.ListTrips
                   select user.Clone();
        }
        #endregion

        #region GetAllTripsBy
        public IEnumerable<DO.Trip> GetAllTripsBy(Predicate<DO.Trip> predicate)
        {
                return from trip in DataSource.ListTrips
                       where predicate(trip)
                       select trip.Clone();
        }
        #endregion

        #region GetTrip
        public DO.Trip GetTrip(int id)
        {
            DO.Trip trip = DataSource.ListTrips.Find(p => p.Id == id && p.IsDeleted);

            if (trip != null)
                return trip.Clone();
            else
                throw new Exception();
        }
        #endregion

        #region AddTrip
        public void AddTrip(DO.Trip trip)
        {
            if (DataSource.ListTrips.FirstOrDefault(p => p.Id == trip.Id && p.IsDeleted) != null)
                throw new Exception();
            DataSource.ListTrips.Add(trip.Clone());
        }
        #endregion

        #region UpdateTrip
        public void UpdateTrip(DO.Trip trip)
        {
            DO.Trip u = DataSource.ListTrips.Find(p => p.Id == trip.Id && p.IsDeleted);

            if (u != null)
            {
                DataSource.ListTrips.Remove(u);
                DataSource.ListTrips.Add(trip.Clone());
            }
            else
                throw new Exception();
        }
        #endregion

        #region UpdateTrip
        public void UpdateTrip(int id, Action<DO.Trip> update) //method that knows to updt specific fields in Trip
        {
                DO.Trip trip = DataSource.ListTrips.Find(p => p.Id == id && p.IsDeleted == false);
                if (trip == null)
                    throw new Exception();
                update(trip);
        }
        #endregion

        #region DeleteTrip
        public void DeleteTrip(int id)
        {
            DO.Trip u = DataSource.ListTrips.Find(p => p.Id == id && p.IsDeleted);

            if (u != null)
            {
                DataSource.ListTrips.Remove(u);
            }
            else
                throw new Exception();
        }
        #endregion
        #endregion
    }
}
