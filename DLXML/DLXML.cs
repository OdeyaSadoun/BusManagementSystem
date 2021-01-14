using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DL
{
    sealed class DLXML:IDL
    {
        #region singelton
        /// <summary>
        /// singleton pattern is a software design pattern that restricts the instantiation of a class to one "single" instance.
        /// This is useful when exactly one object is needed to coordinate actions across the system.
        /// </summary>
        static readonly DLXML instance = new DLXML();
        static DLXML() { }// static ctor to ensure instance init is done just before first usage
        DLXML() { } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use
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
            List<DO.Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<DO.Bus>(BusPath);
            DO.Bus b = ListBuses.Find(p => ((p.LicenseNumber == id) && (p.IsDeleted == false)));
            if (b != null)
                return b;
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
            List<DO.Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<DO.Bus>(BusPath);
            return from bus in ListBuses
                   where bus.IsDeleted == false
                   select bus;
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
            List<DO.Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<DO.Bus>(BusPath);
            return from bus in ListBuses
                   where predicate(bus)
                   select bus;
        }
        #endregion

        #region AddBus
        /// <summary>
        /// A function that add a bus to the list
        /// </summary>
        /// <param name="bus"></param>
        public void AddBus(DO.Bus bus)
        {
            List<DO.Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<DO.Bus>(BusPath);
            if (ListBuses.FirstOrDefault(p => p.LicenseNumber == bus.LicenseNumber && !p.IsDeleted) != null)
                throw new DO.IncorrectLicenseNumberException(bus.LicenseNumber, $"The bus {bus.LicenseNumber} is exsit in the system, could not add it again");           
            ListBuses.Add(bus); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListBuses, BusPath);
        }
        #endregion

        #region DeleteBus
        /// <summary>
        /// A function that delete bus (mark the flag IsDeleted = true) 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBus(int id)
        {
            List<DO.Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<DO.Bus>(BusPath);
            DO.Bus b = ListBuses.Find(p => p.LicenseNumber == id && !p.IsDeleted);
            if (b != null)
            {
                b.IsDeleted = true;
            }
            else
                throw new DO.IncorrectLicenseNumberException(id, $"Incorrect license number: {id}. could not found this bus, try enter again");
            XMLTools.SaveListToXMLSerializer(ListBuses, BusPath);
        }
        #endregion

        #region UpdateBus
        /// <summary>
        /// A function that update the bus
        /// </summary>
        /// <param name="bus"></param>

        public void UpdateBus(DO.Bus bus)
        {
            List<DO.Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<DO.Bus>(BusPath);
            DO.Bus b = ListBuses.Find(p => p.LicenseNumber == bus.LicenseNumber && !p.IsDeleted);

            if (b != null)
            {
                ListBuses.Remove(b);
                ListBuses.Add(bus);
            }
            else
                throw new DO.IncorrectLicenseNumberException(bus.LicenseNumber, $"The bus {bus.LicenseNumber} is not exsit in the system, could not update it");
            XMLTools.SaveListToXMLSerializer(ListBuses, BusPath);
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
            List<DO.Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<DO.Bus>(BusPath);
            DO.Bus b = ListBuses.Find(p => p.LicenseNumber == licenseNumber && p.IsDeleted == false);
            if (b == null)
                throw new DO.IncorrectLicenseNumberException(licenseNumber, $"The bus {licenseNumber} is not exsit in the system, could not update it");
            update(b);
            XMLTools.SaveListToXMLSerializer(ListBuses, BusPath);
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
            List<DO.BusOnTrip> ListBusesOnTrip = XMLTools.LoadListFromXMLSerializer<DO.BusOnTrip>(BusOnTripPath);
            DO.BusOnTrip b = ListBusesOnTrip.Find(p => p.Id == id && p.LicenseNumber == licenseNumber && !p.IsDeleted);

            if (b != null)
                return b;
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
            List<DO.BusOnTrip> ListBusesOnTrip = XMLTools.LoadListFromXMLSerializer<DO.BusOnTrip>(BusOnTripPath);
            return from bus in ListBusesOnTrip
                   where bus.IsDeleted == false
                   select bus;
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
            List<DO.BusOnTrip> ListBusesOnTrip = XMLTools.LoadListFromXMLSerializer<DO.BusOnTrip>(BusOnTripPath);
            return from bus in ListBusesOnTrip
                   where predicate(bus)
                   select bus;
        }
        #endregion

        #region AddBusOnTrip
        /// <summary>
        /// A function that add a bus on trip to the list
        /// </summary>
        /// <param name="bus"></param>
        public void AddBusOnTrip(DO.BusOnTrip bus)
        {
            List<DO.BusOnTrip> ListBusesOnTrip = XMLTools.LoadListFromXMLSerializer<DO.BusOnTrip>(BusOnTripPath);
            if (ListBusesOnTrip.FirstOrDefault(p => p.Id == bus.Id && p.LicenseNumber == bus.LicenseNumber && !p.IsDeleted) != null)
                throw new IncorrectInputException($"The bus on trip {bus.LicenseNumber} with the line ID:{bus.Id} is exsit in the system, could not add it again");
            bus.Id = DO.Configuration.BusOnTripID++;//המספר הרץ
            ListBusesOnTrip.Add(bus);
            XMLTools.SaveListToXMLSerializer(ListBusesOnTrip, BusOnTripPath);
        }
        #endregion

        #region UpdateBusOnTrip
        /// <summary>
        /// A function that update the bus on trip
        /// </summary>
        /// <param name="bus"></param>
        public void UpdateBusOnTrip(DO.BusOnTrip bus)
        {
            List<DO.BusOnTrip> ListBusesOnTrip = XMLTools.LoadListFromXMLSerializer<DO.BusOnTrip>(BusOnTripPath);
            DO.BusOnTrip b = ListBusesOnTrip.Find(p => p.Id == bus.Id && p.LicenseNumber == bus.LicenseNumber && !p.IsDeleted);
            if (b != null)
            {
                ListBusesOnTrip.Remove(b);
                ListBusesOnTrip.Add(bus);
            }
            else
                throw new DO.IncorrectInputException($"The bus on trip {bus.LicenseNumber} with the line ID:{bus.Id} is not exsit in the system, could not update it");
            XMLTools.SaveListToXMLSerializer(ListBusesOnTrip, BusOnTripPath);
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
            List<DO.BusOnTrip> ListBusesOnTrip = XMLTools.LoadListFromXMLSerializer<DO.BusOnTrip>(BusOnTripPath);
            DO.BusOnTrip b = ListBusesOnTrip.Find(p => p.Id == id && p.LicenseNumber == licenseNumber && p.IsDeleted == false);
            if (b == null)
                throw new DO.IncorrectInputException($"The bus on trip {licenseNumber} with the line ID:{id} is not exsit in the system, could not update it"); ;
            update(b);
            XMLTools.SaveListToXMLSerializer(ListBusesOnTrip, BusOnTripPath);
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
            List<DO.BusOnTrip> ListBusesOnTrip = XMLTools.LoadListFromXMLSerializer<DO.BusOnTrip>(BusOnTripPath);
            DO.BusOnTrip b = ListBusesOnTrip.Find(p => p.Id == id && p.LicenseNumber == licenseNumber && !p.IsDeleted);


            if (b != null)
            {
                //DataSource.ListBusesOnTrip.Remove(b);
                b.IsDeleted = true;
            }
            else
                throw new DO.IncorrectInputException($"Incorrect license number: {licenseNumber} OR line ID: {id} could not found this bus, try enter again");
            XMLTools.SaveListToXMLSerializer(ListBusesOnTrip, BusOnTripPath);
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
            List<DO.LineTrip> ListLinesTrip = XMLTools.LoadListFromXMLSerializer<DO.LineTrip>(LineTripPath);
            return from lt in ListLinesTrip
                   where lt.IsDeleted == false
                   select lt;
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
            List<DO.LineTrip> ListLinesTrip = XMLTools.LoadListFromXMLSerializer<DO.LineTrip>(LineTripPath);
            return from trip in ListLinesTrip
                   where predicate(trip)
                   select trip;
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
            List<DO.LineTrip> ListLinesTrip = XMLTools.LoadListFromXMLSerializer<DO.LineTrip>(LineTripPath);
            DO.LineTrip lt = ListLinesTrip.Find(p => p.Id == id && p.LineId == lineId && !p.IsDeleted);

            if (lt != null)
                return lt;
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
            List<DO.LineTrip> ListLinesTrip = XMLTools.LoadListFromXMLSerializer<DO.LineTrip>(LineTripPath);
            if (ListLinesTrip.FirstOrDefault(p => p.Id == lt.Id && p.LineId == lt.LineId && !p.IsDeleted) != null)
                throw new IncorrectInputException($"The trip {lt.Id} with the line ID:{lt.LineId} is exsit in the system, could not add it again");
            ListLinesTrip.Add(lt);
            XMLTools.SaveListToXMLSerializer(ListLinesTrip, LineTripPath);
        }
        #endregion

        #region UpdateLineTrip
        /// <summary>
        /// A function that update the line trip
        /// </summary>
        /// <param name="bus"></param>
        public void UpdateLineTrip(DO.LineTrip lt)
        {
            List<DO.LineTrip> ListLinesTrip = XMLTools.LoadListFromXMLSerializer<DO.LineTrip>(LineTripPath);
            DO.LineTrip l = ListLinesTrip.Find(p => p.Id == lt.Id && p.LineId == lt.LineId && !p.IsDeleted);

            if (l != null)
            {
                ListLinesTrip.Remove(l);
                ListLinesTrip.Add(lt);
            }
            else
                throw new DO.IncorrectInputException($"The trip: {lt.Id} with the line ID: {lt.LineId} is not exsit in the system, could not update it");
            XMLTools.SaveListToXMLSerializer(ListLinesTrip, LineTripPath);
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
            List<DO.LineTrip> ListLinesTrip = XMLTools.LoadListFromXMLSerializer<DO.LineTrip>(LineTripPath);
            DO.LineTrip l = ListLinesTrip.Find(p => p.Id == id && p.LineId == lineId && p.IsDeleted == false);
            if (l == null)
                throw new DO.IncorrectInputException($"The trip: {id} with the line ID: {lineId} is not exsit in the system, could not update it");
            update(l);
            XMLTools.SaveListToXMLSerializer(ListLinesTrip, LineTripPath);
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
            List<DO.LineTrip> ListLinesTrip = XMLTools.LoadListFromXMLSerializer<DO.LineTrip>(LineTripPath);
            DO.LineTrip l = ListLinesTrip.Find(p => p.Id == id && p.LineId == lineId && !p.IsDeleted);

            if (l != null)
            {
                //DataSource.ListLinesTrip.Remove(b);
                l.IsDeleted = true;
            }
            else
                throw new DO.IncorrectInputException($"The trip: {id} with the line ID: {lineId} is not exsit in the system. could not be deleted, try enter again");
            XMLTools.SaveListToXMLSerializer(ListLinesTrip, LineTripPath);
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
            List<DO.Station> ListStations = XMLTools.LoadListFromXMLSerializer<DO.Station>(StationPath);
            return from station in ListStations
                   where station.IsDeleted == false
                   select station;
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
            List<DO.Station> ListStations = XMLTools.LoadListFromXMLSerializer<DO.Station>(StationPath);
            return from station in ListStations
                   where predicate(station)
                   select station;
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
            List<DO.Station> ListStations = XMLTools.LoadListFromXMLSerializer<DO.Station>(StationPath);
            DO.Station station = ListStations.Find(p => p.Code == code && !p.IsDeleted);
            if (station != null)
                return station;
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
            List<DO.Station> ListStations = XMLTools.LoadListFromXMLSerializer<DO.Station>(StationPath);
            if (ListStations.FirstOrDefault(p => p.Code == station.Code && !p.IsDeleted);
            throw new IncorrectCodeStationException(station.Code, $"The station  {station.Code}  is exsit in the system, could not add it again");
            ListStations.Add(station);
            XMLTools.SaveListToXMLSerializer(ListStations, StationPath);
        }
        #endregion

        #region UpdateStation
        /// <summary>
        /// A function that update the station
        /// </summary>
        /// <param name="station"></param>
        public void UpdateStation(DO.Station station)
        {
            List<DO.Station> ListStations = XMLTools.LoadListFromXMLSerializer<DO.Station>(StationPath);
            DO.Station s = ListStations.Find(p => p.Code == station.Code && !p.IsDeleted);
            if (s != null)
            {
                ListStations.Remove(s);
                ListStations.Add(station);
            }
            else
                throw new DO.IncorrectCodeStationException(station.Code, $"The station  {station.Code}  is not exsit in the system, could not update it");
            XMLTools.SaveListToXMLSerializer(ListStations, StationPath);
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
            List<DO.Station> ListStations = XMLTools.LoadListFromXMLSerializer<DO.Station>(StationPath);
            DO.Station station = ListStations.Find(p => p.Code == id && p.IsDeleted == false);
            if (station == null)
                throw new DO.IncorrectCodeStationException(id, $"The station {id}  is not exsit in the system, could not update it");
            update(station);
            XMLTools.SaveListToXMLSerializer(ListStations, StationPath);
        }
        #endregion

        #region DeleteStation
        /// <summary>
        /// A function that delete station (mark the flag IsDeleted = true) 
        /// </summary>
        /// <param name="code"></param>
        public void DeleteStation(int code)
        {
            List<DO.Station> ListStations = XMLTools.LoadListFromXMLSerializer<DO.Station>(StationPath);
            DO.Station s = ListStations.Find(p => p.Code == code && !p.IsDeleted);
            if (s != null)
            {
                //DataSource.ListStations.Remove(b);
                s.IsDeleted = true;
            }
            else
                throw new DO.IncorrectCodeStationException(code, $"Incorrect station code: {code}. could not found this station, try enter again");
            XMLTools.SaveListToXMLSerializer(ListStations, StationPath);
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
            List<DO.Line> ListLines = XMLTools.LoadListFromXMLSerializer<DO.Line>(LinePath);
            return from line in ListLines
                   where line.IsDeleted == false
                   select line;

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
            List<DO.Line> ListLines = XMLTools.LoadListFromXMLSerializer<DO.Line>(LinePath);
            return from line in ListLines
                   where predicate(line)
                   select line;

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
            List<DO.Line> ListLines = XMLTools.LoadListFromXMLSerializer<DO.Line>(LinePath);
            DO.Line l = ListLines.Find(p => p.Id == id && !p.IsDeleted);
            if (l != null)
                return l;
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
            List<DO.Line> ListLines = XMLTools.LoadListFromXMLSerializer<DO.Line>(LineNumberPath);
            DO.Line l = ListLines.Find(p => p.LineNumber == num && !p.IsDeleted);
            if (l != null)
                return l;
            else
                throw new DO.IncorrectInputException($"Incorrect line number: {num}. could not found this line, try enter again");
        }
        #endregion

        #region AddLine
        /// <summary>
        /// A function that add a line to the list
        /// </summary>
        /// <param name="line"></param>
        public void AddLine(DO.Line line)
        {
            List<DO.Line> ListLines = XMLTools.LoadListFromXMLSerializer<DO.Line>(LinePath);
            if (ListLines.FirstOrDefault(p => p.Id == line.Id && p.IsDeleted) != null)
                throw new IncorrectLineIDException(line.Id, $"The line  {line.Id}  is exsit in the system, could not add it again");
            line.Id = DO.Configuration.LineID++; //המספר הרץ
            ListLines.Add(line);
            XMLTools.SaveListToXMLSerializer(ListLines, LinePath);
        }
        #endregion

        #region UpdateLine
        /// <summary>
        /// A function that update the line 
        /// </summary>
        /// <param name="line"></param>
        public void UpdateLine(DO.Line line)
        {
            List<DO.Line> ListLines = XMLTools.LoadListFromXMLSerializer<DO.Line>(LinePath);
            DO.Line l = ListLines.Find(p => p.Id == line.Id && !p.IsDeleted);
            if (l != null)
            {
                ListLines.Remove(l);
                ListLines.Add(line);
            }
            else
                throw new DO.IncorrectLineIDException(line.Id, $"The line  {line.Id}  is not exsit in the system, could not update it");
            ListLines.Add(line);
            XMLTools.SaveListToXMLSerializer(ListLines, LinePath);
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
            List<DO.Line> ListLines = XMLTools.LoadListFromXMLSerializer<DO.Line>(LinePath);
            DO.Line line = ListLines.Find(p => p.Id == id && p.IsDeleted == false);
            if (line == null)
                throw new DO.IncorrectLineIDException(id, $"The line  {id}  is not exsit in the system, could not update it");
            update(line);
            XMLTools.SaveListToXMLSerializer(ListLines, LinePath);
        }
        #endregion

        #region DeleteLine
        /// <summary>
        /// A function that delete line (mark the flag IsDeleted = true) 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteLine(int id)
        {
            List<DO.Line> ListLines = XMLTools.LoadListFromXMLSerializer<DO.Line>(LinePath);
            DO.Line l = ListLines.Find(p => p.Id == id && !p.IsDeleted);

            if (l != null)
            {
                //DataSource.ListLines.Remove(l);
                l.IsDeleted = true;
            }
            else
                throw new DO.IncorrectLineIDException(id, $"Incorrect line: {id}. could not found this line, try enter again");
            XMLTools.SaveListToXMLSerializer(ListLines, LinePath);
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
            List<DO.LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(LinePath);
            return from linestation in ListLineStations
                   where linestation.IsDeleted == false
                   select linestation;
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
            List<DO.LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(LinePath);
            return from linestation in ListLineStations
                   where predicate(linestation)
                   select linestation;

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
            List<DO.LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(LinePath);
            DO.LineStation linestation = ListLineStations.Find(p => p.LineId == lineId && p.StationCode == stationCode && !p.IsDeleted);
            if (linestation != null)
                return linestation;
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
            List<DO.LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(LineStationPath);
            if (ListLineStations.FirstOrDefault(p => p.LineId == lineStation.LineId && p.StationCode == lineStation.StationCode && !p.IsDeleted) != null)
                throw new IncorrectInputException($"The line {lineStation.LineId} with the station code:{lineStation.StationCode} is exsit in the system, could not add it again");
            ListLineStations.Add(lineStation);
            XMLTools.SaveListToXMLSerializer(ListLineStations, LineStationPath);
        }
        #endregion

        #region UpdateLineStation
        /// <summary>
        /// A function that update the line station
        /// </summary>
        /// <param name="lineStation"></param>
        public void UpdateLineStation(DO.LineStation lineStation)
        {
            List<DO.LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(LineStationPath);
            DO.LineStation linestation = ListLineStations.Find(p => p.LineId == lineStation.LineId && p.StationCode == lineStation.StationCode && !p.IsDeleted);

            if (linestation != null)
            {
                ListLineStations.Remove(linestation);
                ListLineStations.Add(lineStation);
            }
            else
                throw new DO.IncorrectInputException($"The line id: {lineStation.LineId} OR station code: {lineStation.StationCode} are not exsit in the system, could not update it");
            XMLTools.SaveListToXMLSerializer(ListLineStations, LineStationPath);
        }
        #endregion

        #region UpdateLineStation
        /// <summary>
        /// method that knows to updt specific fields in LineStation
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="stationCode"></param>
        /// <param name="update"></param>
        public void UpdateLineStation(int lineId, int stationCode, Action<DO.LineStation> update)
        {
            List<DO.LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(LineStationPath);
            DO.LineStation linestation = ListLineStations.Find(p => (p.LineId == lineId && p.StationCode == stationCode && p.IsDeleted == false));
            if (linestation == null)
                throw new DO.IncorrectInputException($"The line id: {lineId} OR station code: {stationCode} are not exsit in the system, could not update it");
            update(linestation);
            XMLTools.SaveListToXMLSerializer(ListLineStations, LineStationPath);
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
            List<DO.LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(LineStationPath);
            DO.LineStation linestation = ListLineStations.Find(p => p.LineId == lineId && p.StationCode == stationCode && !p.IsDeleted);

            if (linestation != null)
            {
                //DataSource.ListLineStations.Remove(b);
                linestation.IsDeleted = true;
            }
            else
                throw new DO.IncorrectInputException($"Incorrect line id: {lineId} OR station code: {stationCode}. could not found this lineStation, try enter again");
            XMLTools.SaveListToXMLSerializer(ListLineStations, LineStationPath);
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
            XElement userRootElem = XMLTools.LoadListFromXMLElement(userPath);

            return from p in userRootElem.Elements()
                   select new DO.User()
                   {
                       UserName = p.Element("UserName").Value,
                       Password = p.Element("Password").Value,
                       Admin = bool.Parse(p.Element("Admin").Value),
                       IsDeleted = bool.Parse(p.Element("IsDeleted").Value),
                       UserProfile = (DO.Profile)Enum.Parse(typeof(DO.Profile), p.Element("UserProfile").Value),
                       Birthday = DateTime.Parse(p.Element("Birthday").Value),
                       Balance = double.Parse(p.Element("Balance").Value),
                       FirstName = p.Element("FirstName").Value,
                       LastName = p.Element("LastName").Value,
                   };
                   
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
            XElement userRootElem = XMLTools.LoadListFromXMLElement(userPath);

            return from p in userRootElem.Elements()
                   let p1 = new DO.User()
                   {
                       UserName = p.Element("UserName").Value,
                       Password = p.Element("Password").Value,
                       Admin = bool.Parse(p.Element("Admin").Value),
                       IsDeleted = bool.Parse(p.Element("IsDeleted").Value),
                       UserProfile = (DO.Profile)Enum.Parse(typeof(DO.Profile), p.Element("UserProfile").Value),
                       Birthday = DateTime.Parse(p.Element("Birthday").Value),
                       Balance = double.Parse(p.Element("Balance").Value),
                       FirstName = p.Element("FirstName").Value,
                       LastName = p.Element("LastName").Value,
                   }
                   where predicate(p1)
                   select p1;
        }
        #endregion

        #region GetUser
        /// <summary>
        /// A function that return a user
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DO.User GetUser(string userName)
        {
            XElement userRootElem = XMLTools.LoadListFromXMLElement(userPath);

            DO.User p = (from per in userRootElem.Elements()
                        where per.Element("UserName").Value == userName
                         select new DO.User()
                         {
                             UserName = per.Element("UserName").Value,
                             Password = per.Element("Password").Value,
                             Admin = bool.Parse(per.Element("Admin").Value),
                             IsDeleted = bool.Parse(per.Element("IsDeleted").Value),
                             UserProfile = (DO.Profile)Enum.Parse(typeof(DO.Profile), per.Element("UserProfile").Value),
                             Birthday = DateTime.Parse(per.Element("Birthday").Value),
                             Balance = double.Parse(per.Element("Balance").Value),
                             FirstName = per.Element("FirstName").Value,
                             LastName = per.Element("LastName").Value,                       
                         }
                        ).FirstOrDefault();

            if (p == null)
                throw new DO.IncorrectUserNameException(userName, $"bad person id: {userName}");

            return p;
        }
        #endregion

        #region AddUser
        /// <summary>
        /// A function that add a user to the list
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(DO.User user)
        {
            XElement userRootElem = XMLTools.LoadListFromXMLElement(userPath);
            XElement per1 = (from p in userRootElem.Elements()
                             where p.Element("UserName").Value == user.UserName
                             select p).FirstOrDefault();
            if (per1 != null)
                throw new DO.IncorrectUserNameException(user.UserName, "Duplicate user UserName");
            XElement userElem = new XElement("User",
                                   new XElement("UserName", user.UserName),
                                   new XElement("Password", user.Password),
                                   new XElement("Admin", user.Admin),
                                   new XElement("IsDeleted", user.IsDeleted),
                                   new XElement("UserProfile", user.UserProfile.ToString()),
                                   new XElement("Birthday", user.Birthday),
                                   new XElement("Balance", user.Balance),
                                   new XElement("FirstName", user.FirstName),
                                   new XElement("LastName", user.LastName);
            userRootElem.Add(userElem);
            XMLTools.SaveListToXMLElement(userRootElem, userPath);
        }
        #endregion
       
        #region UpdateUser
        /// <summary>
        /// A function that update the user
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUser(DO.User user)
        {
            XElement userRootElem = XMLTools.LoadListFromXMLElement(userPath);

            XElement per = (from p in userElem.Elements()
                            where p.Element("UserName").Value == user.UserName
                            select p).FirstOrDefault();

            if (per != null)
            {
                per.Element("UserName").Value = user.UserName;
                per.Element("Password").Value = user.Password;
                per.Element("Admin").Value = user.Admin.ToString();
                per.Element("IsDeleted").Value = user.IsDeleted.ToString();
                per.Element("UserProfile").Value = user.UserProfile.ToString();
                per.Element("Birthday").Value = user.Birthday.ToString();
                per.Element("Balance").Value = user.Balance.ToString();
                per.Element("FirstName").Value = user.FirstName;
                per.Element("LastName").Value = user.LastName;
                XMLTools.SaveListToXMLElement(userRootElem, userPath);
            }
            else
                throw new DO.IncorrectUserNameException(user.UserName, $"bad user user name: {user.UserName}");
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
            //DO.User user = DataSource.ListUsers.Find(p => p.UserName == userName && p.IsDeleted == false);
            //if (user == null)
            //    throw new DO.IncorrectUserNameException(userName, $"The user name:  {userName}  is not exsit in the system, could not update it");
            //update(user);
            throw new NotImplementedException();
        }
        #endregion

        #region DeleteUser
        /// <summary>
        /// A function that delete user (mark the flag IsDeleted = true)
        /// </summary>
        /// <param name="userName"></param>
        public void DeleteUser(string userName)
        {
            XElement userRootElem = XMLTools.LoadListFromXMLElement(userPath);
            XElement per = (from p in userRootElem.Elements()
                            where p.Element("UserName").Value == userName
                            select p).FirstOrDefault();
            if (per != null)
            {
                per.Remove();
                XMLTools.SaveListToXMLElement(userRootElem, userPath);
            }
            else
                throw new DO.IncorrectUserNameException(id, $"bad person id: {id}");
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
            List<DO.AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<DO.AdjacentStations>(AdjacentStationsPath);
            return from adjacentStations in ListAdjacentStations
                   where adjacentStations.IsDeleted == false
                   select adjacentStations;
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
            List<DO.AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<DO.AdjacentStations>(AdjacentStationsPath);
            return from adjacentStations in ListAdjacentStations
                   where predicate(adjacentStations)
                   select adjacentStations;
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
            List<DO.AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<DO.AdjacentStations>(AdjacentStationsPath);
            DO.AdjacentStations a = ListAdjacentStations.Find(p => (p.CodeStation1 == stationCode1) && (p.CodeStation2 == stationCode2) && !p.IsDeleted);
            if (a != null)
                return a;
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
            List<DO.AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<DO.AdjacentStations>(AdjacentStationsPath);
            if (ListAdjacentStations.FirstOrDefault(p => (p.CodeStation1 == adjacentStations.CodeStation1) && (p.CodeStation2 == adjacentStations.CodeStation2) && !p.IsDeleted) != null)
                throw new DO.IncorrectCodeStationException(adjacentStations.CodeStation1, adjacentStations.CodeStation2, $"Incorrect station code: {adjacentStations.CodeStation1} or station code {adjacentStations.CodeStation2}. could not found thier stations, try enter again");
            else
                ListAdjacentStations.Add(adjacentStations);
            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, AdjacentStationsPath);
        }
        #endregion

        #region UpdateAdjacentStations
        /// <summary>
        /// A function that update the AdjacentStations
        /// </summary>
        /// <param name="adjacentStations"></param>
        public void UpdateAdjacentStations(DO.AdjacentStations adjacentStations)
        {
            List<DO.AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<DO.AdjacentStations>(AdjacentStationsPath);
            DO.AdjacentStations u = ListAdjacentStations.Find(p => ((p.CodeStation1 == adjacentStations.CodeStation1) && (p.CodeStation2 == adjacentStations.CodeStation2)) && !p.IsDeleted);
            if (u != null)
            {
                ListAdjacentStations.Remove(u);
                ListAdjacentStations.Add(adjacentStations);
            }
            else
                throw new DO.IncorrectCodeStationException(adjacentStations.CodeStation1, adjacentStations.CodeStation2, $"Incorrect station code: {adjacentStations.CodeStation1} or station code {adjacentStations.CodeStation2}. could not update, try enter again");
            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, AdjacentStationsPath);
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
            List<DO.AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<DO.AdjacentStations>(AdjacentStationsPath);
            DO.AdjacentStations adjacentStations = ListAdjacentStations.Find(p => ((p.CodeStation1 == stationCode1 && p.CodeStation2 == stationCode2 && p.IsDeleted == false || p.CodeStation1 == stationCode2 && p.CodeStation2 == stationCode1) && p.IsDeleted == false));
            if (adjacentStations == null)
                throw new DO.IncorrectCodeStationException(stationCode1, stationCode2, $"Incorrect station code: {stationCode1} or station code {stationCode2}. could not update, try enter again");
            update(adjacentStations);
            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, AdjacentStationsPath);
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
            List<DO.AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<DO.AdjacentStations>(AdjacentStationsPath);
            DO.AdjacentStations a = ListAdjacentStations.Find(p => (p.CodeStation1 == stationCode1) && (p.CodeStation2 == stationCode1) && !p.IsDeleted);
            {
                //DataSource.ListAdjacentStations.Remove(u);
                a.IsDeleted = true;
            }
            else
                throw new DO.IncorrectCodeStationException(stationCode1, stationCode2, $"Incorrect station code: {stationCode1} or station code {stationCode2}. could not delete the AdjacentStations, try enter again");
            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, AdjacentStationsPath);
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
            List<DO.Trip> ListTrips = XMLTools.LoadListFromXMLSerializer<DO.Trip>(TripPath);
            return from trip in ListTrips
                   where trip.IsDeleted == false
                   select trip;
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
            List<DO.Trip> ListTrips = XMLTools.LoadListFromXMLSerializer<DO.Trip>(TripPath);
            return from trip in ListTrips
                   where predicate(trip)
                   select trip;
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
            List<DO.Trip> ListTrips = XMLTools.LoadListFromXMLSerializer<DO.Trip>(TripPath);
            DO.Trip trip = ListTrips.Find(p => p.Id == id && !p.IsDeleted);
            if (trip != null)
                return trip;
            else
                throw new DO.IncorrectTripIDException(id, $"Incorrect trip user id: {id}. could not found this trip, try enter again");
        }
        #endregion

        #region AddTrip
        /// <summary>
        /// A function that add trip to the list
        /// </summary>
        /// <param name="trip"></param>
        public void AddTrip(DO.Trip trip)
        {
            List<DO.Trip> ListTrips = XMLTools.LoadListFromXMLSerializer<DO.Trip>(TripPath);
            if (ListTrips.FirstOrDefault(p => p.Id == trip.Id && !p.IsDeleted) != null)
                throw new DO.IncorrectTripIDException(trip.Id, $"The trip user id: {trip.Id} is exist in the system. could not add it again");
            trip.Id = DO.Configuration.TripID;
            ListTrips.Add(trip);
            XMLTools.SaveListToXMLSerializer(ListTrips, ListTrips);
        }
        #endregion

        #region UpdateTrip
        /// <summary>
        ///  A function that update the trip
        /// </summary>
        /// <param name="trip"></param>
        public void UpdateTrip(DO.Trip trip)
        {
            List<DO.Trip> ListTrips = XMLTools.LoadListFromXMLSerializer<DO.Trip>(TripPath);
            DO.Trip t = ListTrips.Find(p => p.Id == trip.Id && !p.IsDeleted);
            if (t != null)
            {
                ListTrips.Remove(t);
                ListTrips.Add(trip);
            }
            else
                throw new DO.IncorrectTripIDException(trip.Id, $"The trip user id: {trip.Id} is not exist in the system. could not update it");
            XMLTools.SaveListToXMLSerializer(ListTrips, ListTrips);
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
            List<DO.Trip> ListTrips = XMLTools.LoadListFromXMLSerializer<DO.Trip>(TripPath);
            DO.Trip trip = ListTrips.Find(p => p.Id == id && p.IsDeleted == false);
            if (trip == null)
                throw new DO.IncorrectTripIDException(id, $"The trip user id: {id} is not exist in the system. could not update it");
            update(trip);
            XMLTools.SaveListToXMLSerializer(ListTrips, ListTrips);
        }
        #endregion

        #region DeleteTrip
        /// <summary>
        /// A function that delete trip (mark the flag IsDeleted = true)
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTrip(int id)
        {
            List<DO.Trip> ListTrips = XMLTools.LoadListFromXMLSerializer<DO.Trip>(ListTrips);
            DO.Trip trip = ListTrips.Find(p => p.Id == id && !p.IsDeleted);
            if (trip != null)
            {
                //DataSource.ListTrips.Remove(u);
                trip.IsDeleted = true;
            }
            else
                throw new DO.IncorrectTripIDException(id, $"The trip user id: {id} is not exist in the system. could not detete it");
            XMLTools.SaveListToXMLSerializer(ListTrips, ListTrips);
        }
        #endregion
        #endregion
    }
}//tamargavrieli18@gmail.com
