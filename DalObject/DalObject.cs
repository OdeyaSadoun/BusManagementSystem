using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;
using DO;
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
        /// <summary>
        /// A function that return a bus
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DO.Bus GetBus(int id)
        {
            DO.Bus b = DataSource.ListBuses.Find(p => ((p.LicenseNumber == id)&&(p.IsDeleted==false)));

            if (b != null)
                return b.Clone();
            else
                throw new DO.IncorrectLicenseNumberException(id, $"Incorrect license number: {id}. could not found this bus, try enter again");
        }
        #endregion

        #region GetAllBuses
        /// <summary>
        /// A function that return all the buses 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.Bus> GetAllBuses()//פונקצית הרחבה
        {
            return from bus in DataSource.ListBuses
                   where bus.IsDeleted == false
                   select bus.Clone();
        }
        #endregion
    
        #region GetAllBusesBy
        /// <summary>
        /// A function that returns the buses that have the special thing that the predicat do
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.Bus> GetAllBusesBy(Predicate<DO.Bus> predicate)
        {
            return from bus in DataSource.ListBuses
                   where predicate(bus)
                   select bus.Clone();
        }
        #endregion

        #region AddBus
        /// <summary>
        /// A function that add a bus to the list
        /// </summary>
        /// <param name="bus"></param>
        public void AddBus(DO.Bus bus)
        {
            if (DataSource.ListBuses.FirstOrDefault(p => p.LicenseNumber == bus.LicenseNumber && !p.IsDeleted) != null)
                throw new IncorrectLicenseNumberException(bus.LicenseNumber, $"The bus {bus.LicenseNumber} is exsit in the system, could not add it again");
            DataSource.ListBuses.Add(bus.Clone());
        }
        #endregion

        #region DeleteBus
        /// <summary>
        /// A function that delete bus (mark the flag IsDeleted = true) 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBus(int id)
        {
            DO.Bus b = DataSource.ListBuses.Find(p => p.LicenseNumber == id && !p.IsDeleted);

            if (b != null)
            {
                //DataSource.ListBuses.Remove(b);
                b.IsDeleted = true;
            }
            else
                throw new DO.IncorrectLicenseNumberException(id,$"Incorrect license number: {id}. could not found this bus, try enter again");
        }
        #endregion

        #region UpdateBus
        /// <summary>
        /// A function that update the bus
        /// </summary>
        /// <param name="bus"></param>

        public void UpdateBus(DO.Bus bus)
        {
            DO.Bus b = DataSource.ListBuses.Find(p => p.LicenseNumber == bus.LicenseNumber && !p.IsDeleted);

            if (b != null)
            {
                DataSource.ListBuses.Remove(b);
                DataSource.ListBuses.Add(bus.Clone());
            }
            else
                throw new DO.IncorrectLicenseNumberException(bus.LicenseNumber,$"The bus {bus.LicenseNumber} is not exsit in the system, could not update it");
        }
        #endregion

        #region UpdateBus
        /// <summary>
        /// method that knows to updt specific fields in bus
        /// </summary>
        /// <param name="licenseNumber"></param>
        /// <param name="update"></param>
        public void UpdateBus(int licenseNumber, Action<DO.Bus> update)
        {
            DO.Bus b = DataSource.ListBuses.Find(p => p.LicenseNumber == licenseNumber && p.IsDeleted == false);
            if (b == null)
                throw new DO.IncorrectLicenseNumberException(licenseNumber,$"The bus {licenseNumber} is not exsit in the system, could not update it");
            update(b);
        }
        #endregion
        #endregion

        #region BusOnTrip

        #region GetBusOnTrip
        /// <summary>
        ///  A function that return a bus on trip
        /// </summary>
        /// <param name="id"></param>
        /// <param name="licenseNumber"></param>
        /// <returns></returns>
        public DO.BusOnTrip GetBusOnTrip(int id, int licenseNumber)
        {
            DO.BusOnTrip b = DataSource.ListBusesOnTrip.Find(p => p.Id == id && p.LicenseNumber == licenseNumber && !p.IsDeleted);

            if (b != null)
                return b.Clone();
            else
                throw new DO.IncorrectInputException($"Incorrect license number: {licenseNumber} OR trip id: {id}. could not found this bus on trip, try enter again");
        }
        #endregion

        #region GetAllBusesOnTrip
        /// <summary>
        /// A function that return all the buses on trip
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.BusOnTrip> GetAllBusesOnTrip()
        {
            return from bus in DataSource.ListBusesOnTrip
                   where bus.IsDeleted == false
                   select bus.Clone();
        }
        #endregion

        #region GetAllBusesOnTripBy
        /// <summary>
        ///  A function that returns the buses on trip that have the special thing that the predicat do
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.BusOnTrip> GetAllBusesOnTripBy(Predicate<DO.BusOnTrip> predicate)
        {
            return from bus in DataSource.ListBusesOnTrip
                   where predicate(bus)
                   select bus.Clone();
        }
        #endregion

        #region AddBusOnTrip
        /// <summary>
        /// A function that add a bus on trip to the list
        /// </summary>
        /// <param name="bus"></param>
        public void AddBusOnTrip(DO.BusOnTrip bus)
        { 
            if (DataSource.ListBusesOnTrip.FirstOrDefault(p => p.Id == bus.Id && p.LicenseNumber == bus.LicenseNumber && !p.IsDeleted) != null)
                throw new IncorrectInputException($"The bus on trip {bus.LicenseNumber} with the line ID:{bus.Id} is exsit in the system, could not add it again");
            bus.Id = DO.Configuration.BusOnTripID++;//המספר הרץ
            DataSource.ListBusesOnTrip.Add(bus.Clone());
        }
        #endregion

        #region UpdateBusOnTrip
        /// <summary>
        /// A function that update the bus on trip
        /// </summary>
        /// <param name="bus"></param>
        public void UpdateBusOnTrip(DO.BusOnTrip bus)
        {
            DO.BusOnTrip b = DataSource.ListBusesOnTrip.Find(p => p.Id == bus.Id && p.LicenseNumber == bus.LicenseNumber && !p.IsDeleted);

            if (b != null)
            {
                DataSource.ListBusesOnTrip.Remove(b);
                DataSource.ListBusesOnTrip.Add(bus.Clone());
            }
            else
                throw new DO.IncorrectInputException($"The bus on trip {bus.LicenseNumber} with the line ID:{bus.Id} is not exsit in the system, could not update it");
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
                throw new DO.IncorrectInputException($"The bus on trip {licenseNumber} with the line ID:{id} is not exsit in the system, could not update it"); ;
            update(b);
        }
        #endregion

        #region DeleteBusOnTrip
        /// <summary>
        /// A function that delete bus on trip (mark the flag IsDeleted = true) 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="licenseNumber"></param>
        public void DeleteBusOnTrip(int id, int licenseNumber)
        {
            DO.BusOnTrip b = DataSource.ListBusesOnTrip.Find(p => p.Id == id && p.LicenseNumber == licenseNumber && !p.IsDeleted);

            if (b != null)
            {
                //DataSource.ListBusesOnTrip.Remove(b);
                b.IsDeleted = true;
            }
            else
                throw new DO.IncorrectInputException($"Incorrect license number: {licenseNumber} OR line ID: {id} could not found this bus, try enter again");
        }
        #endregion
        #endregion

        
        #region LineTrip
        #region GetAllLinesTrip
        /// <summary>
        /// A function that return all the lines trip
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.LineTrip> GetAllLinesTrip()
        {
            return from lt in DataSource.ListLinesTrip
                   where lt.IsDeleted == false
                   select lt.Clone();
        }
        #endregion

        #region GetAllLinesTripBy
        /// <summary>
        /// A function that returns the lines trip that have the special thing that the predicat do
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.LineTrip> GetAllLinesTripBy(Predicate<DO.LineTrip> predicate)
        {
            return from trip in DataSource.ListLinesTrip
                   where predicate(trip)
                   select trip.Clone();
        }
        #endregion

        #region GetLineTrip
        /// <summary>
        /// A function that return a line trip
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lineId"></param>
        /// <returns></returns>
        public DO.LineTrip GetLineTrip(int id, int lineId)
        {
            DO.LineTrip lt = DataSource.ListLinesTrip.Find(p => p.Id == id && p.LineId== lineId && !p.IsDeleted);

            if (lt != null)
                return lt.Clone();
            else
                throw new DO.IncorrectInputException($"Incorrect station code: {lineId} OR trip id: {id}. could not found this station, try enter again");
        }
        #endregion

        #region AddLineTrip
        /// <summary>
        /// A function that add a line trip to the list
        /// </summary>
        /// <param name="lt"></param>
        public void AddLineTrip(DO.LineTrip lt)
        {
            if (DataSource.ListLinesTrip.FirstOrDefault(p => p.Id == lt.Id && p.LineId == lt.LineId && !p.IsDeleted) != null)
                throw new IncorrectInputException($"The trip {lt.Id} with the line ID:{lt.LineId} is exsit in the system, could not add it again");
            
            DataSource.ListLinesTrip.Add(lt.Clone());
        }
        #endregion

        #region UpdateLineTrip
        /// <summary>
        /// A function that update the line trip
        /// </summary>
        /// <param name="bus"></param>
        public void UpdateLineTrip(DO.LineTrip lt)
        {

            DO.LineTrip l = DataSource.ListLinesTrip.Find(p => p.Id == lt.Id && p.LineId == lt.LineId && !p.IsDeleted);

            if (l != null)
            {
                DataSource.ListLinesTrip.Remove(l);
                DataSource.ListLinesTrip.Add(lt.Clone());
            }
            else
                throw new DO.IncorrectInputException($"The trip: {lt.Id} with the line ID: {lt.LineId} is not exsit in the system, could not update it"); 
        }
        #endregion

        #region UpdateLineTrip
        /// <summary>
        /// method that knows to updt specific fields in LineTrip
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lineId"></param>
        /// <param name="update"></param>
        public void UpdateLineTrip(int id, int lineId, Action<DO.LineTrip> update)
        {

            DO.LineTrip l = DataSource.ListLinesTrip.Find(p => p.Id == id && p.LineId == lineId  && p.IsDeleted == false);
            if (l == null)
                throw new DO.IncorrectInputException($"The trip: {id} with the line ID: {lineId} is not exsit in the system, could not update it");
            update(l);
        }
        #endregion

        #region DeleteLineTrip
        /// <summary>
        /// A function that delete line trip (mark the flag IsDeleted = true) 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lineId"></param>
        public void DeleteLineTrip(int id, int lineId)
        {
            DO.LineTrip l = DataSource.ListLinesTrip.Find(p => p.Id == id && p.LineId == lineId  && !p.IsDeleted);

            if (l != null)
            {
                //DataSource.ListLinesTrip.Remove(b);
                l.IsDeleted = true;
            }
            else
                throw new DO.IncorrectInputException($"The trip: {id} with the line ID: {lineId} is not exsit in the system. could not be deleted, try enter again");
        }
        #endregion
        #endregion

        #region Station

        #region GetAllStationes
        /// <summary>
        /// A function that return all the stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.Station> GetAllStationes()
        {
            return from station in DataSource.ListStations
                   where station.IsDeleted == false
                   select station.Clone();
        }
        #endregion

        #region GetAllStationesBy
        /// <summary>
        /// A function that returns the stations that have the special thing that the predicat do
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.Station> GetAllStationesBy(Predicate<DO.Station> predicate)
        {
            return from station in DataSource.ListStations
                   where predicate(station)
                   select station.Clone();
        }
        #endregion

        #region GetStation
        /// <summary>
        ///  A function that return a station
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DO.Station GetStation(int code)
        {
            DO.Station station = DataSource.ListStations.Find(p => p.Code == code && !p.IsDeleted);

            if (station != null)
                return station.Clone();
            else
                throw new DO.IncorrectCodeStationException(code, $"Incorrect station code: {code}. could not found this station, try enter again");
        }
        #endregion

        #region AddStation
        /// <summary>
        /// A function that add a station to the list
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(DO.Station station)
        {
            if (DataSource.ListStations.FirstOrDefault(p => p.Code == station.Code && !p.IsDeleted) != null)
                throw new IncorrectCodeStationException(station.Code, $"The station  {station.Code}  is exsit in the system, could not add it again");
            DataSource.ListStations.Add(station.Clone());

        }
        #endregion

        #region UpdateStation
        /// <summary>
        /// A function that update the station
        /// </summary>
        /// <param name="station"></param>
        public void UpdateStation(DO.Station station)
        {
            DO.Station s = DataSource.ListStations.Find(p => p.Code == station.Code && !p.IsDeleted);

                if (s != null)
                {
                    DataSource.ListStations.Remove(s);
                    DataSource.ListStations.Add(station.Clone());
                }
                else
                throw new DO.IncorrectCodeStationException(station.Code,$"The station  {station.Code}  is not exsit in the system, could not update it");
        }
        #endregion

        #region UpdateStation
        /// <summary>
        /// method that knows to updt specific fields in station
        /// </summary>
        /// <param name="id"></param>
        /// <param name="update"></param>
        public void UpdateStation(int id, Action<DO.Station> update)
        {
            DO.Station station = DataSource.ListStations.Find(p => p.Code == id && p.IsDeleted == false);
            if (station == null)
                throw new DO.IncorrectCodeStationException(id,$"The station {id}  is not exsit in the system, could not update it");
            update(station);
        }
        #endregion

        #region DeleteStation
        /// <summary>
        /// A function that delete station (mark the flag IsDeleted = true) 
        /// </summary>
        /// <param name="code"></param>
        public void DeleteStation(int code)
        {
            DO.Station s = DataSource.ListStations.Find(p => p.Code == code && !p.IsDeleted);

            if (s != null)
            {
                //DataSource.ListStations.Remove(b);
                s.IsDeleted = true;
            }
            else
                throw new DO.IncorrectCodeStationException(code, $"Incorrect station code: {code}. could not found this station, try enter again");
        }
        #endregion
        #endregion

        #region Line
        #region GetAllLines
        /// <summary>
        /// A function that return all the lines
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.Line> GetAllLines()
        {
            return from line in DataSource.ListLines
                   where line.IsDeleted == false
                   select line.Clone();

        }
        #endregion

        #region GetAllLinesBy
        /// <summary>
        ///  A function that returns the lines that have the special thing that the predicat do
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.Line> GetAllLinesBy(Predicate<DO.Line> predicate)
        {
                return from line in DataSource.ListLines
                       where predicate(line)
                       select line.Clone();

        }
        #endregion

        #region GetLine
        /// <summary>
        ///  A function that return a line
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DO.Line GetLine(int id)
        {
            DO.Line l = DataSource.ListLines.Find(p => p.Id == id && !p.IsDeleted);

            if (l != null)
                return l.Clone();
            else
                throw new DO.IncorrectLineIDException(id, $"Incorrect line: {id}. could not found this line, try enter again");
        }
        #endregion

        #region GetLineNumber
        /// <summary>
        ///  A function that return a line
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public DO.Line GetLineNumber(int num)
        {
            DO.Line l = DataSource.ListLines.Find(p => p.LineNumber == num && !p.IsDeleted);

            if (l != null)
                return l.Clone();
            else
                throw new DO.IncorrectInputException( $"Incorrect line number: {num}. could not found this line, try enter again");
        }
        #endregion

        #region AddLine
        /// <summary>
        /// A function that add a line to the list
        /// </summary>
        /// <param name="line"></param>
        public void AddLine(DO.Line line)
        {
            if (DataSource.ListLines.FirstOrDefault(p => p.Id == line.Id && p.IsDeleted) != null)
                throw new IncorrectLineIDException(line.Id,$"The line  {line.Id}  is exsit in the system, could not add it again");
            line.Id = DO.Configuration.LineID++; //המספר הרץ
            DataSource.ListLines.Add(line.Clone());
        }
        #endregion

        #region UpdateLine
        /// <summary>
        /// A function that update the line 
        /// </summary>
        /// <param name="line"></param>
        public void UpdateLine(DO.Line line)
        {
            DO.Line l = DataSource.ListLines.Find(p => p.Id == line.Id && !p.IsDeleted);

            if (l != null)
            {
                DataSource.ListLines.Remove(l);
                DataSource.ListLines.Add(line.Clone());
            }
            else
                throw new DO.IncorrectLineIDException(line.Id,$"The line  {line.Id}  is not exsit in the system, could not update it");

        }
        #endregion

        #region UpdateLine
        /// <summary>
        /// method that knows to updt specific fields in Line
        /// </summary>
        /// <param name="id"></param>
        /// <param name="update"></param>
        public void UpdateLine(int id, Action<DO.Line> update) 
        {
                DO.Line line = DataSource.ListLines.Find(p => p.Id == id && p.IsDeleted == false);
                if (line == null)
                throw new DO.IncorrectLineIDException(id,$"The line  {id}  is not exsit in the system, could not update it");
            update(line);
        }
        #endregion

        #region DeleteLine
        /// <summary>
        /// A function that delete line (mark the flag IsDeleted = true) 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteLine(int id)
        {
            DO.Line l = DataSource.ListLines.Find(p => p.Id == id && !p.IsDeleted);

            if (l != null)
            {
                //DataSource.ListLines.Remove(l);
                l.IsDeleted = true;
            }
            else
                throw new DO.IncorrectLineIDException(id, $"Incorrect line: {id}. could not found this line, try enter again");
        }
        #endregion
        #endregion

        #region LineStation
        #region GetAllLinesStation
        /// <summary>
        /// A function that return all the lines stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.LineStation> GetAllLinesStation()
        {
            return from linestation in DataSource.ListLineStations
                   where linestation.IsDeleted == false
                   select linestation.Clone();
        }
        #endregion

        #region GetAllLinesStationBy
        /// <summary>
        /// A function that returns the lines station that have the special thing that the predicat do
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.LineStation> GetAllLinesStationBy(Predicate<DO.LineStation> predicate)
        {
                return from linestation in DataSource.ListLineStations
                       where predicate(linestation)
                       select linestation.Clone();

        }
        #endregion

        #region GetLineStation
        /// <summary>
        ///  A function that return a line station
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="stationCode"></param>
        /// <returns></returns>
        public DO.LineStation GetLineStation(int lineId, int stationCode)
        {
            DO.LineStation linestation = DataSource.ListLineStations.Find(p => p.LineId == lineId && p.StationCode== stationCode && !p.IsDeleted);

            if (linestation != null)
                return linestation.Clone();
            else
                throw new DO.IncorrectInputException($"Incorrect line: {lineId} OR  station code: {stationCode}. could not found this lineStation, try enter again");
        }
        #endregion

        #region AddLineStation
        /// <summary>
        /// A function that add a line station to the list
        /// </summary>
        /// <param name="lineStation"></param>
        public void AddLineStation(DO.LineStation lineStation)
        {
            if (DataSource.ListLineStations.FirstOrDefault(p => p.LineId == lineStation.LineId && p.StationCode==lineStation.StationCode && !p.IsDeleted) != null)
                throw new IncorrectInputException($"The line {lineStation.LineId} with the station code:{lineStation.StationCode} is exsit in the system, could not add it again");
            DataSource.ListLineStations.Add(lineStation.Clone());
        }
        #endregion

        #region UpdateLineStation
        /// <summary>
        /// A function that update the line station
        /// </summary>
        /// <param name="lineStation"></param>
        public void UpdateLineStation(DO.LineStation lineStation)
        {
            DO.LineStation linestation = DataSource.ListLineStations.Find(p => p.LineId == lineStation.LineId && p.StationCode == lineStation.StationCode && !p.IsDeleted);

            if (linestation != null)
            {
                DataSource.ListLineStations.Remove(linestation);
                DataSource.ListLineStations.Add(lineStation.Clone());
            }
            else
                throw new DO.IncorrectInputException($"The line id: {lineStation.LineId} OR station code: {lineStation.StationCode} are not exsit in the system, could not update it");
        }
        #endregion

        #region UpdateLineStation
        /// <summary>
        /// method that knows to updt specific fields in LineStation
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="stationCode"></param>
        /// <param name="update"></param>
        public void UpdateLineStation(int lineId,int stationCode, Action<DO.LineStation> update) 
        {
                DO.LineStation linestation = DataSource.ListLineStations.Find(p => (p.LineId == lineId && p.StationCode == stationCode && p.IsDeleted == false));
                if (linestation == null)
                throw new DO.IncorrectInputException($"The line id: {lineId} OR station code: {stationCode} are not exsit in the system, could not update it");
            update(linestation);
        }
        #endregion

        #region DeleteLineStation
        /// <summary>
        ///  A function that delete line station (mark the flag IsDeleted = true)
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="stationCode"></param>
        public void DeleteLineStation(int lineId, int stationCode)
        {
            DO.LineStation linestation = DataSource.ListLineStations.Find(p => p.LineId == lineId && p.StationCode== stationCode && !p.IsDeleted);

            if (linestation != null)
            {
                //DataSource.ListLineStations.Remove(b);
                linestation.IsDeleted = true;
            }
            else
                throw new DO.IncorrectInputException($"Incorrect line id: {lineId} OR station code: {stationCode}. could not found this lineStation, try enter again");
        }
        #endregion
        #endregion

        #region User
        #region GetAllUsers
        /// <summary>
        /// A function that return all the users
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.User> GetAllUsers()
        {
            return from user in DataSource.ListUsers
                   where user.IsDeleted == false
                   select user.Clone();
        }
        #endregion

        #region GetAllUsersBy
        /// <summary>
        ///  A function that returns the users that have the special thing that the predicat do
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.User> GetAllUsersBy(Predicate<DO.User> predicate)
        {
                return from user in DataSource.ListUsers
                       where predicate(user)
                       select user.Clone();
        }
        #endregion

        #region GetUser
        /// <summary>
        /// A function that return a user
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DO.User GetUser(string name)
        {
            DO.User u = DataSource.ListUsers.Find(p => p.UserName == name && p.IsDeleted==false);

            if (u != null)
                return u.Clone();
            else
                throw new DO.IncorrectUserNameException(name, $"Incorrect user name: {name}. could not found this user name, try enter again");
        }
        #endregion

        #region AddUser
        /// <summary>
        /// A function that add a user to the list
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(DO.User user)
        {
            if (DataSource.ListUsers.FirstOrDefault(p => p.UserName == user.UserName && !p.IsDeleted) != null)
                throw new IncorrectUserNameException(user.UserName, $"The user name:  {user.UserName}  is exsit in the system, could not add it again");
            DataSource.ListUsers.Add(user.Clone());
        }
        #endregion

        #region UpdateUser
        /// <summary>
        /// A function that update the user
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUser(DO.User user)
        {
            DO.User u = DataSource.ListUsers.Find(p => p.UserName == user.UserName && !p.IsDeleted);

            if (u != null)
            {
                DataSource.ListUsers.Remove(u);
                DataSource.ListUsers.Add(user.Clone());
            }
            else
                throw new DO.IncorrectUserNameException(user.UserName, $"The user name:  {user.UserName}  is not exsit in the system, could not update it");
        }
        #endregion

        #region UpdateUser
        /// <summary>
        /// method that knows to updt specific fields in User
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="update"></param>
        public void UpdateUser(string userName, Action<DO.User> update)
        {
                DO.User user = DataSource.ListUsers.Find(p => p.UserName == userName && p.IsDeleted == false);
                if (user == null)
                throw new DO.IncorrectUserNameException(userName, $"The user name:  {userName}  is not exsit in the system, could not update it");
            update(user);
        }
        #endregion

        #region DeleteUser
        /// <summary>
        /// A function that delete user (mark the flag IsDeleted = true)
        /// </summary>
        /// <param name="userName"></param>
        public void DeleteUser(string userName)
        {
            DO.User u = DataSource.ListUsers.Find(p => p.UserName == userName && !p.IsDeleted);

            if (u != null)
            {
                //DataSource.ListUsers.Remove(u);
                u.IsDeleted = true;
            }
            else
                throw new DO.IncorrectUserNameException(userName, $"Incorrect user name: {userName}. could not delete this user, try enter again");
        }
        #endregion
        #endregion

        #region AdjacentStations

        #region GetAllAdjacentStations
        /// <summary>
        /// A function that return all the AdjacentStations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStations()
        {
            return from adjacentStations in DataSource.ListAdjacentStations
                   where adjacentStations.IsDeleted == false
                   select adjacentStations.Clone();
        }
        #endregion

        #region GetAllAdjacentStationsBy
        /// <summary>
        /// A function that returns the AdjacentStations that have the special thing that the predicat do
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsBy(Predicate<DO.AdjacentStations> predicate)
        {
                return from adjacentStations in DataSource.ListAdjacentStations
                       where predicate(adjacentStations)
                       select adjacentStations.Clone();
        }
        #endregion

        #region GetAdjacentStations
        /// <summary>
        /// A function that return AdjacentStations
        /// </summary>
        /// <param name="stationCode1"></param>
        /// <param name="stationCode2"></param>
        /// <returns></returns>
        public DO.AdjacentStations GetAdjacentStations(int stationCode1, int stationCode2)
        {
                DO.AdjacentStations a = DataSource.ListAdjacentStations.Find(p =>( p.CodeStation1 == stationCode1) &&(p.CodeStation2 == stationCode2) && !p.IsDeleted);

                if (a != null)
                    return a.Clone();
                else
                throw new DO.IncorrectCodeStationException(stationCode1, stationCode2, $"Incorrect station code: {stationCode1} or station code {stationCode2}. could not found thier stations, try enter again");
        }
        #endregion

        #region AddAdjacentStations
        /// <summary>
        /// A function that add AdjacentStations to the list
        /// </summary>
        /// <param name="adjacentStations"></param>
        public void AddAdjacentStations(DO.AdjacentStations adjacentStations)
        {
            if (DataSource.ListAdjacentStations.FirstOrDefault(p => (p.CodeStation1== adjacentStations.CodeStation1) && (p.CodeStation2 == adjacentStations.CodeStation2)&& !p.IsDeleted) != null)
                throw new DO.IncorrectCodeStationException(adjacentStations.CodeStation1, adjacentStations.CodeStation2, $"Incorrect station code: {adjacentStations.CodeStation1} or station code {adjacentStations.CodeStation2}. could not found thier stations, try enter again");
            else
                DataSource.ListAdjacentStations.Add(adjacentStations.Clone());
        }
        #endregion

        #region UpdateAdjacentStations
        /// <summary>
        /// A function that update the AdjacentStations
        /// </summary>
        /// <param name="adjacentStations"></param>
        public void UpdateAdjacentStations(DO.AdjacentStations adjacentStations)
        {
            DO.AdjacentStations u = DataSource.ListAdjacentStations.Find(p => ((p.CodeStation1 == adjacentStations.CodeStation1) && (p.CodeStation2 == adjacentStations.CodeStation2))&& !p.IsDeleted);
                if (u != null)
                {
                    DataSource.ListAdjacentStations.Remove(u);
                    DataSource.ListAdjacentStations.Add(adjacentStations.Clone());
                }
                else
                throw new DO.IncorrectCodeStationException(adjacentStations.CodeStation1, adjacentStations.CodeStation2, $"Incorrect station code: {adjacentStations.CodeStation1} or station code {adjacentStations.CodeStation2}. could not update, try enter again");
        }
        #endregion

        #region UpdateAdjacentStations
        /// <summary>
        /// method that knows to updt specific fields in AdjacentStations
        /// </summary>
        /// <param name="stationCode1"></param>
        /// <param name="stationCode2"></param>
        /// <param name="update"></param>
        public void UpdateAdjacentStations(int stationCode1, int stationCode2, Action<DO.AdjacentStations> update) 
        {
                DO.AdjacentStations adjacentStations = DataSource.ListAdjacentStations.Find(p => ((p.CodeStation1 == stationCode1 && p.CodeStation2 == stationCode2 && p.IsDeleted == false || p.CodeStation1 == stationCode2 && p.CodeStation2 == stationCode1) && p.IsDeleted == false));
                if (adjacentStations == null)
                throw new DO.IncorrectCodeStationException(stationCode1, stationCode2, $"Incorrect station code: {stationCode1} or station code {stationCode2}. could not update, try enter again");
            update(adjacentStations);
        }
        #endregion

        #region DeleteAdjacentStations
        /// <summary>
        ///  A function that delete AdjacentStations (mark the flag IsDeleted = true)
        /// </summary>
        /// <param name="stationCode1"></param>
        /// <param name="stationCode2"></param>
        public void DeleteAdjacentStations(int stationCode1, int stationCode2)
        {
            DO.AdjacentStations a = DataSource.ListAdjacentStations.Find(p => (p.CodeStation1 == stationCode1) && (p.CodeStation2 == stationCode2) && !p.IsDeleted);

            if (a != null)
            {
                //DataSource.ListAdjacentStations.Remove(u);
                a.IsDeleted = true;
            }
            else
                throw new DO.IncorrectCodeStationException(stationCode1, stationCode2, $"Incorrect station code: {stationCode1} or station code {stationCode2}. could not delete the AdjacentStations, try enter again");

        }
        #endregion
        #endregion

        #region Trip
        #region GetAllTrips
        /// <summary>
        /// A function that return all the trips
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.Trip> GetAllTrips()
        {
            return from trip in DataSource.ListTrips
                   where trip.IsDeleted == false
                   select trip.Clone();
        }
        #endregion

        #region GetAllTripsBy
        /// <summary>
        /// A function that returns the trip that have the special thing that the predicat do
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.Trip> GetAllTripsBy(Predicate<DO.Trip> predicate)
        {
                return from trip in DataSource.ListTrips
                       where predicate(trip)
                       select trip.Clone();
        }
        #endregion

        #region GetTrip
        /// <summary>
        /// A function that return trip
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DO.Trip GetTrip(int id)
        {
            DO.Trip trip = DataSource.ListTrips.Find(p => p.Id == id && !p.IsDeleted);

            if (trip != null)
                return trip.Clone();
            else
                throw new DO. IncorrectTripIDException(id, $"Incorrect trip user id: {id}. could not found this trip, try enter again");
        }
        #endregion

        #region AddTrip
        /// <summary>
        /// A function that add trip to the list
        /// </summary>
        /// <param name="trip"></param>
        public void AddTrip(DO.Trip trip)
        {
            if (DataSource.ListTrips.FirstOrDefault(p => p.Id == trip.Id && !p.IsDeleted) != null)
                throw new DO.IncorrectTripIDException(trip.Id, $"The trip user id: {trip.Id} is exist in the system. could not add it again");
            trip.Id = DO.Configuration.TripID;
            DataSource.ListTrips.Add(trip.Clone());
        }
        #endregion

        #region UpdateTrip
        /// <summary>
        ///  A function that update the trip
        /// </summary>
        /// <param name="trip"></param>
        public void UpdateTrip(DO.Trip trip)
        {
            DO.Trip t = DataSource.ListTrips.Find(p => p.Id == trip.Id && !p.IsDeleted);

            if (t != null)
            {
                DataSource.ListTrips.Remove(t);
                DataSource.ListTrips.Add(trip.Clone());
            }
            else
                throw new DO.IncorrectTripIDException(trip.Id, $"The trip user id: {trip.Id} is not exist in the system. could not update it");
        }
        #endregion

        #region UpdateTrip
        /// <summary>
        /// method that knows to updt specific fields in Trip
        /// </summary>
        /// <param name="id"></param>
        /// <param name="update"></param>
        public void UpdateTrip(int id, Action<DO.Trip> update) //method that knows to updt specific fields in Trip
        {
                DO.Trip trip = DataSource.ListTrips.Find(p => p.Id == id && p.IsDeleted == false);
                if (trip == null)
                throw new DO.IncorrectTripIDException(id, $"The trip user id: {id} is not exist in the system. could not update it");
            update(trip);
        }
        #endregion

        #region DeleteTrip
        /// <summary>
        /// A function that delete trip (mark the flag IsDeleted = true)
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTrip(int id)
        {
            DO.Trip trip = DataSource.ListTrips.Find(p => p.Id == id && !p.IsDeleted);

            if (trip != null)
            {
                //DataSource.ListTrips.Remove(u);
                trip.IsDeleted = true;
            }
            else
                throw new DO.IncorrectTripIDException(id, $"The trip user id: {id} is not exist in the system. could not detete it");
        }
        #endregion
        #endregion
    }
}
