using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BLApi;
using BO;
using DO;
using DalApi;
using System.Threading;


namespace BL
{
    sealed class BLImp : IBL
    {
        IDL dl = DLFactory.GetDL();

        #region singelton
        /// <summary>
        /// singleton pattern is a software design pattern that restricts the instantiation of a class to one "single" instance.
        /// This is useful when exactly one object is needed to coordinate actions across the system.
        /// </summary>
        static readonly BLImp instance = new BLImp();
        static BLImp() { }// static ctor to ensure instance init is done just before first usage
        BLImp() { } // default => private
        public static BLImp Instance { get => instance; }// The public Instance property to use
        #endregion

        #region Bus

        #region busDoBoAdapter
        /// <summary>
        /// A function that copy details of bus from DO to BO
        /// </summary>
        /// <param name="busDO"></param>
        /// <returns></returns>
        BO.Bus busDoBoAdapter(DO.Bus busDO)
        {
            BO.Bus busBO = new BO.Bus();
            busDO.CopyPropertiesTo(busBO);
            return busBO;
        }
        #endregion

        #region GetBus
        /// <summary>
        /// A BO function that return a BO bus
        /// </summary>
        /// <param name = "id" ></ param >
        /// < returns ></ returns >
        public BO.Bus GetBus(int id)
        {
            DO.Bus busDO;
            try
            {
                busDO = dl.GetBus(id);
            }
            catch (DO.IncorrectLicenseNumberException ex)
            {
                throw new BO.IncorrectLicenseNumberException(ex.licenseNumber, ex.Message);
            }
            return busDoBoAdapter(busDO);
        }
        #endregion

        #region GetAllBuses
        /// <summary>
        /// A BO function that return all the buses 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.Bus> GetAllBuses()//פונקצית הרחבה
        {
            return from bus in dl.GetAllBuses()
                   select busDoBoAdapter(bus);
        }
        #endregion

        #region CountDigit - help function
        /// <summary>
        /// A function that count how many digits in the number
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private int CountDigit(int num)
        {
            int counter = 0;
            while (num != 0)
            {
                num = num / 10;
                counter++;
            }
            return counter;
        }
        #endregion

        #region AddBus
        /// <summary>
        /// A BO function that add a bus to the list
        /// </summary>
        /// <param name="bus"></param>
        public void AddBus(BO.Bus bus)
        {
            try
            {
                DO.Bus busDOtemp = new DO.Bus();
                bus.CopyPropertiesTo(busDOtemp);
                bus.IsDeleted = false;//אם מוסיפיםם זה וודאי לא מחוק
                bus.Status = BO.BusStatus.Ready;
                if (bus.DateBegin > DateTime.Now) //נבדוק האם התאריך תקין
                    throw new BO.IncorrectDateException(bus.DateBegin, "The date begin not valid");
                if (bus.TotalMileage < 0) // אם הקילומטראז פחות מ-0 אזי נשים 0 כברירת מחדל
                    bus.TotalMileage = 0;
                if (bus.FuelRemain < 0 || bus.FuelRemain > 1200) //אם הדלק לא תקין נמלא את המיכל עד 1200
                    bus.FuelRemain = 1200;

                int lengthLicenceNumber = CountDigit(bus.LicenseNumber); //נבדוק האם מספר הספרות בלוחית רישוי תואמת את שנת הייצור
                if (lengthLicenceNumber != 7 && lengthLicenceNumber != 8)
                    throw new BO.IncorrectLicenseNumberException(bus.LicenseNumber, "The license number isn't correct");
                if (((lengthLicenceNumber == 7 && bus.DateBegin.Year >= 2018) || (lengthLicenceNumber == 8 && bus.DateBegin.Year < 2018)))
                    throw new BO.IncorrectLicenseNumberOrDateException(bus.LicenseNumber, bus.DateBegin, "The license number or the date begin isn't correct");
                if (bus.LastTreatment > DateTime.Now || bus.LastTreatment < bus.DateBegin)
                    throw new BO.IncorrectDateException(bus.LastTreatment, "The date of last treatment is not valid");
                if (bus.KmBeforTreatment < 0 || bus.KmBeforTreatment > bus.TotalMileage)
                    throw new BO.IncorrectInputException("The kilometrage of last treatment is not valid");
                if(bus.KmBeforeFuel < 0 || bus.KmBeforeFuel > 1200)
                    throw new BO.IncorrectInputException("The kilometrage of last fuel is not valid");

                dl.AddBus(busDOtemp);
            }
            catch (DO.IncorrectLicenseNumberException ex)
            {
                throw new BO.IncorrectLicenseNumberException(ex.licenseNumber, ex.Message);
            }

        }
        #endregion

        #region UpdateBus
        /// <summary>
        /// A BO function that update the bus
        /// </summary>
        /// <param name="bus"></param>

        public void UpdateBus(BO.Bus bus)
        {
            DO.Bus busDO;
            try
            {
                busDO = dl.GetBus(bus.LicenseNumber);
                bus.CopyPropertiesTo(busDO);
                dl.DeleteBus(bus.LicenseNumber);
                dl.AddBus(busDO);
            }

            catch (DO.IncorrectLicenseNumberException ex)
            {
                throw new BO.IncorrectLicenseNumberException(ex.licenseNumber, ex.Message);
            }

        }
        #endregion

        #region DeleteBus
        /// <summary>
        /// A BO function that delete bus 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBus(BO.Bus busBO)
        {
            try
            {
                dl.DeleteBus(busBO.LicenseNumber);
            }
            catch (DO.IncorrectLicenseNumberException ex)
            {
                throw new BO.IncorrectLicenseNumberException(ex.licenseNumber, ex.Message);
            }
        }
        #endregion

        #region Refuel
        public void Refuel(BO.Bus bus)
        {
            DO.Bus busDo = dl.GetBus(bus.LicenseNumber);
            bus.FuelRemain = 1200;
            bus.Status = BO.BusStatus.Refueling;
            bus.KmBeforeFuel = 1200;
            busDo.FuelRemain = 1200;
            busDo.Status = DO.BusStatus.Refueling;
            busDo.KmBeforeFuel = 1200;
        }
        #endregion

        #region Threatment

        public void Threatment(BO.Bus bus)
        {
            DateTime currentDate = DateTime.Now;
            bus.LastTreatment = currentDate;
            bus.Status = BO.BusStatus.InTreatment;
            bus.KmBeforTreatment = 20000;
            bus.FuelRemain = 1200;
            bus.KmBeforeFuel = 1200;
        }

        #endregion
        #endregion

        #region lineTripDoBoAdapter
        /// <summary>
        /// A function that copy details of bus from DO to BO
        /// </summary>
        /// <param name="lineTripDO"></param>
        /// <returns></returns>
        BO.LineTrip lineTripDoBoAdapter(DO.LineTrip lineTripDO)
        {
            BO.LineTrip lineTripBO = new BO.LineTrip();
            lineTripDO.CopyPropertiesTo(lineTripBO);
            return lineTripBO;
        }
        #endregion

        #region GetAllLinesTrip
        /// <summary>
        /// A BO function that return all the lines trip 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.LineTrip> GetAllLinesTrip()
        {
            return from lt in dl.GetAllLinesTrip()
                   select lineTripDoBoAdapter(lt);
        }
        #endregion

        #region Line
        #region lineDoBoAdapter
        /// <summary>
        /// A function that copy details from DO to BO
        /// </summary>
        /// <param name="lineDO"></param>
        /// <returns></returns>
        BO.Line lineDoBoAdapter(DO.Line lineDO)
        {
            BO.Line lineBO = new BO.Line();
            int lineId = lineDO.Id;

            List<BO.StationInLine> stations = new List<BO.StationInLine>();
            List<BO.StationInLine> stationsTmp = new List<BO.StationInLine>();
            stations = (from stat in dl.GetAllLinesStationBy(stat => stat.LineId == lineId && stat.IsDeleted == false)
                                               let station = dl.GetStation(stat.StationCode)
                                               select station.CopyToStationInLine(stat)).ToList();
            //stations = (stations.OrderBy(s => s.LineStationIndex)).ToList();
            TimeSpan count = TimeSpan.Zero;
            foreach (BO.StationInLine s in stations)
            {
                if (s.LineStationIndex != stations[stations.Count - 1].LineStationIndex) // if this is not the end
                {
                    int sc1 = s.StationCode;//station code 1
                    int sc2 = stations[s.LineStationIndex+1].StationCode;//station code 2
                    DO.AdjacentStations adjStat = dl.GetAdjacentStations(sc1, sc2);
                    s.DistanceTo = adjStat.Distance;
                    s.TimeTo = adjStat.TravelTime;
                    count = count + s.TimeTo;
                }
            }
            lineBO = lineDO.CopyToLineDOToBO();
            stationsTmp = stations;
            //for (int i = 0; i < stationsTmp.Count; i++)
            //{
            //    if (stationsTmp[i].IsDeleted == true)
            //        stations.Remove(stations[i]);
            //}
            var temp = from stat in stationsTmp
                       where stat.IsDeleted == false
                       select stat;
            stations = temp.ToList();
            lineBO.FirstStation = GetStation( stations[0].StationCode);


            lineBO.ListOfStationsInLine = stations;
            lineBO.TravelTimeInThisLine = count;

            List<BO.LineTrip> tempTimes = GetAllLinesTrip().ToList();
            List<BO.LineTrip> times = new List<BO.LineTrip>();

            foreach (BO.LineTrip lt in tempTimes)
            {
                if (lt.LineId == lineId && lt.IsDeleted==false)
                    times.Add(lt);
            }
            lineBO.ListOfTripTime = times;
            //BO.Station sBO;
            //DO.Station sDO = dl.GetStation(lineDO.FirstStation);
            //lineBO.FirstStation = stationDoBoAdapter(sDO);
            //sDO = dl.GetStation(lineDO.LastStation);
            //lineBO.LastStation = stationDoBoAdapter(sDO);

            return lineBO;
        }
        #endregion

        #region GetLine
        /// <summary>
        ///  A function that return a line
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BO.Line GetLine(int id)
        {

            DO.Line lineDO;
            try
            {
                lineDO = dl.GetLine(id);
            }

            catch (DO.IncorrectLineIDException ex)
            {
                throw new BO.IncorrectLineIDException(ex.ID, ex.Message);
            }

            return lineDoBoAdapter(lineDO);
        }
        #endregion

        #region GetLineNumber
        /// <summary>
        ///  A function that return a line
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public BO.Line GetLineNumber(int num)
        {

            DO.Line lineDO;
            try
            {
                lineDO = dl.GetLineNumber(num);
            }

            catch (DO.IncorrectLineIDException ex)
            {
                throw new BO.IncorrectLineIDException(ex.ID, ex.Message);
            }

            return lineDoBoAdapter(lineDO);
        }
        #endregion

        #region GetAllLines
        /// <summary>
        /// A BO function that return all the lines
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.Line> GetAllLines()
        {
            return from line in dl.GetAllLines()
                   select lineDoBoAdapter(line);

        }
        #endregion

        #region AddLine
        /// <summary>
        /// A function that add a line to the list
        /// </summary>
        /// <param name="line"></param>
        public void AddLine(BO.Line line)
        {
            try
            {
                DO.Line lineDOtemp = new DO.Line();
                lineDOtemp = DeepCopyUtilities.CopyToLineBOToDO(line);

                if (line.LineNumber < 0 || line.Fare < 0 || line.TravelTimeInThisLine == TimeSpan.Zero) //מספר הקו שלילי ולא תקין או מחיר הנסיעה שלילי או זמן הנסיעה 0
                    throw new BO.IncorrectInputException("The line number or fare of trabel time not valid");

                //DO.Station sDO1 = dl.GetStation(line.FirstStation.Code);
                //DO.Station sDO2 = dl.GetStation(line.LastStation.Code);

                //if ((sDO1 == null) || (sDO2 == null))
                //    throw new BO.IncorrectCodeStationException(line.FirstStation.Code, "This station code could not found");
                //line.FirstStation = sDO1.CopyToStationDOToBO();
                //line.LastStation = sDO2.CopyToStationDOToBO();
                lineDOtemp.FirstStation = line.FirstStation.Code;
                lineDOtemp.LastStation = line.LastStation.Code;

                dl.AddLine(lineDOtemp);


                //עידכונים של תחנות עוקבות ותחנות קו
                lineDOtemp.Id = dl.GetAllLinesBy(s => s.LineNumber == lineDOtemp.LineNumber && s.IsDeleted==false /*&& s.Area == lineDOtemp.Area*/).FirstOrDefault().Id;
                //int sc1 = line.Stations[0].StationCode;//stationCode of the first station
                //int sc2 = line.Stations[1].StationCode;//station Code of the last station
                //lineDo.FirstStation = sc1;
                //lineDo.LastStation = sc2;
                //try
                //{
                    if (dl.GetAdjacentStations(lineDOtemp.FirstStation, lineDOtemp.LastStation) == null)//add to adjcenct stations if its not exists
                    {
                        DO.AdjacentStations adj = new DO.AdjacentStations() { CodeStation1 = lineDOtemp.FirstStation, CodeStation2 = lineDOtemp.LastStation,  TravelTime = (line.ListOfStationsInLine.FirstOrDefault()).TimeTo, Distance = (line.ListOfStationsInLine.FirstOrDefault()).DistanceTo };
                        dl.AddAdjacentStations(adj);
                    }
                    DO.LineStation first = new DO.LineStation() { LineId = lineDOtemp.Id, StationCode = lineDOtemp.FirstStation, LineStationIndex = 0, IsDeleted = false };
                    DO.LineStation last = new DO.LineStation() { LineId = lineDOtemp.Id, StationCode = lineDOtemp.LastStation, LineStationIndex = 1, IsDeleted = false};
                    dl.AddLineStation(first);//add first line station
                    dl.AddLineStation(last);//add last line ststion
               // }


                ////לעבור על רשימת התחנות ולשאול האם אני בתחנה מסוימת ובאותה תחנה להוסיף את הקו לרשימת הקווים
                //List<BO.StationInLine> tempListStations = new List<StationInLine>();
                //if (line.ListOfStationsInLine.Count() == 0)
                //    throw new ArgumentNullException("The list of the line is empty");


                //foreach (BO.StationInLine station in line.ListOfStationsInLine)
                //    if (station.LineId == line.Id)
                //        tempListStations.Add(station);
                //BO.Station bs;
                //BO.ShortLine sline = new ShortLine();
                //for (int i = 0; i < tempListStations.Count(); i++) //נעבור על רשימת התחנות קו שיש בקו הזה
                //{
                //    foreach (DO.Station station in dl.GetAllStationes())//נעבור על כל רשימת התחנות הקיימות
                //        if (station.Code == tempListStations[i].StationCode)
                //        {
                //            bs = GetStation(station.Code);
                //            sline.Id = line.Id;
                //            sline.IsDeleted = line.IsDeleted;
                //            sline.LastStation = line.LastStation;
                //            sline.LineNumber = line.LineNumber;

                //            bs.ListOfLines.ToList().Add(sline); //עדכון הקו הרצוי ברשימת התחנות שהוא עובר בה
                //        }
                //}
            }
            catch (DO.IncorrectLineIDException ex)
            {
                throw new BO.IncorrectLineIDException(ex.ID, ex.Message);
            }



        }
        #endregion
      
        #region UpdateLine
        /// <summary>
        /// A function that update the line 
        /// </summary>
        /// <param name="line"></param>
        public void UpdateLine(BO.Line line)
        {
            DO.Line lineDO = new DO.Line();
            //line.CopyPropertiesTo(lineDO);
            lineDO = line.CopyToLineBOToDO();
            try
            {
                dl.UpdateLine(lineDO);
            }
            catch (DO.IncorrectLineIDException ex)
            {
                throw new BO.IncorrectLineIDException(ex.ID, ex.Message);
            }
        }


        #endregion

        #region DeleteLine
        /// <summary>
        /// A function that delete line(mark the flag IsDeleted = true)
        /// </summary>
        /// <param name = "id" ></ param >
        public void DeleteLine(BO.Line line)
        {
            try
            {
                
                //delete fron the line station list
                List<DO.LineStation> listLineStations = dl.GetAllLinesStationBy(s => s.LineId == line.Id && line.IsDeleted == false).ToList();
                foreach (DO.LineStation s in listLineStations)
                {
                    dl.DeleteLineStation(s.LineId, s.StationCode);
                }
                dl.DeleteLine(line.Id);
            }
            catch (DO.IncorrectLineIDException ex)
            {
                throw new BO.IncorrectLineIDException(ex.ID, ex.Message);
            }

        }
        #endregion

        #endregion

        #region StationInLine

        #region stationInLineDoBoAdapter
        /// <summary>
        /// A function that copy details of bus from DO to BO
        /// </summary>
        /// <param name="lineTripDO"></param>
        /// <returns></returns>
        BO.StationInLine stationInLineDoBoAdapter(DO.LineStation lineStationDO)
        {
            BO.StationInLine stationInLineBO = new BO.StationInLine();
            DO.Station s = dl.GetStation(lineStationDO.StationCode);
            stationInLineBO= DeepCopyUtilities.CopyToStationInLine(s,lineStationDO);
            return stationInLineBO;
        }
        #endregion 

        #region DeleteStationInLine
        /// <summary>
        /// A function that delete DeleteStationInLine BO - LineStation DO
        /// </summary>
        /// <param name="sInL"></param>
        public void DeleteStationInLine(BO.StationInLine sInL)
        {
            try
            {
                //DO.LineStation ls = dl.GetLineStation(sInL.LineId, sInL.StationCode);
                BO.Station stat = GetStation(sInL.StationCode);
                BO.Line line = GetLine(sInL.LineId);
                DO.Line lineDO = dl.GetLine(sInL.LineId);

                if (line.FirstStation == stat)
                {
                    line.FirstStation = GetStation(line.ListOfStationsInLine.ToList()[1].StationCode);
                    lineDO.FirstStation = line.FirstStation.Code;
                }
                dl.DeleteLineStation(sInL.LineId, sInL.StationCode);

            }
            catch(DO.IncorrectInputException ex)
            {
                throw new BO.IncorrectInputException( ex.Message);
            }
        }
        #endregion

        
        #region GetAllStationsInLine
        /// <summary>
        /// A BO function that return all the lines trip 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.StationInLine> GetAllStationsInLine()
        {
            return from s in dl.GetAllLinesStation()
                   select stationInLineDoBoAdapter(s);
        }
        #endregion

        #region GetAllStationesInLineBy
        /// <summary>
        /// A function that returns the stations that have the special thing that the predicat BO
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<BO.StationInLine> GetAllStationesInLineBy(int id)
        {
            return from station in dl.GetAllLinesStationBy(s=> s.LineId == id)
                   select stationInLineDoBoAdapter(station);
        }
        #endregion

        //#region GetStationInLine
        ///// <summary>
        /////  A function that return a BO station
        ///// </summary>
        ///// <param name="code"></param>
        ///// <returns></returns>
        //public BO.StationInLine GetStationInLine(int code, int id)
        //{
        //    DO.Station stationDO;
        //    DO.LineStation lDO;

        //    try
        //    {
        //        stationDO = dl.GetStation(code);
        //        lDO = dl.GetLineStation(code, id);
        //    }

        //    catch (DO.IncorrectCodeStationException ex)
        //    {
        //        throw new BO.IncorrectLineIDException(ex.stationCode, ex.Message);
        //    }

        //    return stationInLineDoBoAdapter(lDO);
        //}
        #region GetStationInLine
        /// <summary>
        /// A BO function that return a BO bus
        /// </summary>
        /// <param name = "id" ></ param >
        /// < returns ></ returns >
        public BO.StationInLine GetStationInLine(int stationCode, int lineId)
        {
            DO.LineStation lineStationDO;
            DO.Station station;
            BO.Line l = GetLine(lineId);
            BO.StationInLine stationinlineBO;
            try
            {
                foreach (BO.StationInLine s in l.ListOfStationsInLine)
                {
                    if (s.StationCode == stationCode)
                        return s;
                }
                throw new BO.IncorrectInputException($"The line {lineId} or the station {stationCode} are not correct");
            }
            catch (DO.IncorrectInputException ex)
            {
                throw new BO.IncorrectInputException(ex.Message);
            }

        }
       // #endregion
        #endregion

        #endregion

        #region Station

        #region stationDoBoAdapter
        /// <summary>
        /// A function that copy details from DO to BO
        /// </summary>
        /// <param name="busDO"></param>
        /// <returns></returns>
        BO.Station stationDoBoAdapter(DO.Station stationDO)
        {
            BO.Station stationBO = new BO.Station();
            int stationCode = stationDO.Code;
            stationBO = stationDO.CopyToStationDOToBO();


            stationBO.ListOfLines = (from stat in dl.GetAllLinesStationBy(stat => stat.StationCode == stationCode && stat.IsDeleted == false)
                                     let line = dl.GetLine(stat.LineId)
                                     select line.CopyToLineInStation(stat)).ToList();

            //stationBO.ListOfStationsInLines = (from stat in dl.GetAllLinesStationBy(stat => stat.StationCode == stationCode && stat.IsDeleted == false)
            //                                   let station = dl.GetStation(stat.StationCode)
            //                                   select station.CopyToStationInLine(stat)).ToList();
            return stationBO;
        }
        #endregion

        #region GetStation
        /// <summary>
        ///  A function that return a BO station
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public BO.Station GetStation(int code)
        {
            DO.Station stationDO;
            try
            {
                stationDO = dl.GetStation(code);
            }

            catch (DO.IncorrectCodeStationException ex)
            {
                throw new BO.IncorrectLineIDException(ex.stationCode, ex.Message);
            }

            return stationDoBoAdapter(stationDO);
        }
        #endregion

        #region GetAllStations
        /// <summary>
        /// A function that return all the stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.Station> GetAllStations()
        {
            return from station in dl.GetAllStationes()
                   select stationDoBoAdapter(station);
        }
        #endregion
      
        #region AddStation
        /// <summary>
        /// A function that add a station to 
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(BO.Station station)
        {
            DO.Station statDO = new DO.Station();
            station.CopyPropertiesTo(statDO);//סטיישן מועתק ל DO
            statDO.IsDeleted = false;
            try
            {
                dl.AddStation(statDO);
            }
            catch (DO.IncorrectCodeStationException ex)
            {
                throw new BO.IncorrectCodeStationException(ex.stationCode, ex.Message);
            }
        }
        #endregion

        #region UpdateStation
        /// <summary>
        /// A function that update the station
        /// </summary>
        /// <param name="station"></param>
        public void UpdateStation(BO.Station station)
        {
            try
            {
                DO.Station stationDO = new DO.Station();
                station.CopyPropertiesTo(stationDO);
                stationDO.IsDeleted = false;              
                dl.UpdateStation(stationDO);
            }
            catch (BO.IncorrectCodeStationException ex)
            {
                throw new BO.IncorrectCodeStationException(ex.stationCode, ex.Message);
            }
        }
        #endregion

        #region DeleteStation
        /// <summary>
        /// A function that delete station (mark the flag IsDeleted = true) 
        /// </summary>
        /// <param name="code"></param>
        public void DeleteStation(BO.Station station)
        {
            try
            {
                //BO.Station statBO = stationDoBoAdapter(statDO);
                //if (statBO.ListOfLines.Count() != 0)//if there are lines that stop in the station
                //    throw new BO.IncorrectCodeStationException(station.Code, "Station cant be deleted because other buses stop there");
                ////נעבור על רשימת הקווים, בכל קו נבדוק האם התחנה היתה קיימת, ופשוט נמחק אותה, (ןנעדכן את הזמן והמרחק לתחנה הבאה)
                List<BO.Line> listLines = GetAllLines().ToList();
                bool stationdelete = false;
                foreach (BO.Line l in listLines)
                {
                    foreach (BO.StationInLine sl in l.ListOfStationsInLine)
                    {
                        if (sl.StationCode == station.Code)
                        {
                            DeleteStationInLine(sl);
                            stationdelete = true;
                            break;
                        }
                    }
                    //DO.Station statDO = dl.GetStation(station.Code);
                    //if (stationdelete)
                    //    break;
                }
                List<DO.AdjacentStations> listAdj = dl.GetAllAdjacentStations().ToList();
                int i = 0;
                DO.AdjacentStations tempAdj = listAdj[i];
                DO.AdjacentStations adjToAdd = new AdjacentStations() ;
                //מה קורה שזו תחנה ראשונה???
                if(tempAdj.CodeStation1 == station.Code)
                {
                    dl.DeleteAdjacentStations(tempAdj.CodeStation1, tempAdj.CodeStation2);
                    i++;
                    tempAdj = listAdj[i];
                }
                //לראות מה הולך עם הזמנים
                bool isDeleteAdj = false;
                for(int j = i; j < listAdj.Count(); j ++)
                {
                    if (listAdj[j].CodeStation1 == station.Code)//התחנה הראשונה בזוג תחנות עוקבות היא התחנה שנמחקה
                    {
                        dl.DeleteAdjacentStations(listAdj[j].CodeStation1, listAdj[j].CodeStation2);
                        adjToAdd.CodeStation1 = tempAdj.CodeStation1;
                        adjToAdd.CodeStation2 = listAdj[j].CodeStation2;
                        adjToAdd.Distance = tempAdj.Distance + listAdj[j].Distance;
                        adjToAdd.TravelTime = tempAdj.TravelTime + listAdj[j].TravelTime;
                        dl.AddAdjacentStations(adjToAdd);
                        isDeleteAdj = true;

                    }
                    if(!isDeleteAdj)
                        tempAdj = listAdj[j];
                    else
                        tempAdj = listAdj[j-1];

                    //else if (listAdj[i].CodeStation2 == statBO.Code)
                    //{
                    //}
                }
  
                dl.DeleteStation(station.Code);
                //foreach (DO.AdjacentStations s in listAdj)//delete from adjacent Station list
                //{

                //    //if (s.CodeStation1 == station.Code || s.CodeStation2 == station.Code)
                //    //    dl.DeleteAdjacentStations(s.CodeStation1, s.CodeStation2);
                //    if(s.CodeStation1 == statBO.Code)//התחנה הראשונה בזוג תחנות עוקבות היא התחנה שנמחקה

                // }

            }
            catch (DO.IncorrectCodeStationException ex)
            {
                throw new BO.IncorrectCodeStationException(ex.stationCode, ex.Message);
            }
        }
        #endregion
        #endregion

        #region User


        #region userDoBoAdapter
        /// <summary>
        /// A function that copy user's details from DO to BO
        /// </summary>
        /// <param name="busDO"></param>
        /// <returns></returns>
        BO.User userDoBoAdapter(DO.User userDO)
        {
            BO.User userBO = new BO.User();
            userDO.CopyPropertiesTo(userBO);
            return userBO;
        }
        #endregion

        #region GetUser
        /// <summary>
        /// A BO function that return a user
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public BO.User GetUser(string name)
        {
            DO.User userDO;
            try
            {
                userDO = dl.GetUser(name);
            }

            catch (DO.IncorrectUserNameException ex)
            {
                throw new BO.IncorrectUserNameException(ex.UserName, ex.Message);
            }

            return userDoBoAdapter(userDO);
        }
        #endregion

        #region GetAllUsers
        /// <summary>
        /// A BO function that return all the users
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.User> GetAllUsers()
        {
            {
                return from user in dl.GetAllUsers()
                       select userDoBoAdapter(user);
            }
        }
        #endregion

        #region AddUser
        /// <summary>
        /// A BO function that add a user to the list
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(BO.User userBO)

        {
            try
            {
                DO.User userDO = new DO.User();
                userBO.CopyPropertiesTo(userDO);
                userDO.IsDeleted = false;
                dl.AddUser(userDO);
            }
            catch (DO.IncorrectUserNameException ex)
            {
                throw new BO.IncorrectUserNameException(ex.UserName, ex.Message);
            }

        }
        #endregion

        #region Charge
        /// <summary>
        /// A function that charge the balance of the user
        /// </summary>
        /// <param name="balance"></param>
        /// <param name="user"></param>
        public void Charge(int balance, BO.User user)
        {
            if (user == null)
                throw new ArgumentNullException("User was null");
            else if ((user.UserProfile == BO.Profile.Youth) || (user.UserProfile == BO.Profile.Veteran) || (user.UserProfile == BO.Profile.ExtendedStudent))
            {
                user.Balance = user.Balance + balance * 1.5;
            }
            else if (user.UserProfile == BO.Profile.Normal)
            {
                user.Balance = user.Balance + balance * 1.25;
            }
            else if (user.UserProfile == BO.Profile.OrdinaryStudent)
            {
                user.Balance = user.Balance + balance * 1.333;
            }
        }
        #endregion

        #region UpdateUser
        /// <summary>
        /// A BO function that update the user
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUser(BO.User user)
        {
            DO.User userDO;
            try
            {
                userDO = dl.GetUser(user.UserName);
                user.CopyPropertiesTo(userDO);
                dl.DeleteUser(user.UserName);
                dl.AddUser(userDO);
            }

            catch (DO.IncorrectUserNameException ex)
            {
                throw new BO.IncorrectUserNameException(ex.UserName, ex.Message);
            }
        }
        #endregion

        #region DeleteUser
        /// <summary>
        /// A Bo function that delete user 
        /// </summary>
        /// <param name="userName"></param>
        public void DeleteUser(BO.User userBO)
        {
            try
            {
                dl.DeleteUser(userBO.UserName);
            }
            catch (DO.IncorrectUserNameException ex)
            {
                throw new BO.IncorrectUserNameException(ex.UserName, ex.Message);
            }
        }
        #endregion

        #endregion

        #region Trip

        //#region tripDoBoAdapter
        ///// <summary>
        ///// A function that copy details from DO to BO
        ///// </summary>
        ///// <param name="tripDO"></param>
        ///// <returns></returns>
        //BO.Trip tripDoBoAdapter(DO.Trip tripDO)
        //{
        //    BO.Trip tripBO = new BO.Trip();
        //    tripDO.CopyPropertiesTo(tripBO);
        //    return tripBO;
        //}
        //#endregion

        //#region GetTrip
        ///// <summary>
        ///// A function that return trip
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public BO.Trip GetTrip(int id)
        //{
        //    DO.Trip tripDO;
        //    try
        //    {
        //        tripDO = dl.GetTrip(id);
        //    }

        //    catch (DO.IncorrectTripIDException ex)
        //    {
        //        throw new BO.IncorrectTripIDException(ex.ID, ex.Message);
        //    }

        //    return tripDoBoAdapter(tripDO);
        //}
        //#endregion
        //#region GetAllTrips
        ///// <summary>
        ///// A function that return all the trips
        ///// </summary>
        ///// <returns></returns>
        //public IEnumerable<DO.Trip> GetAllTrips()
        //{
        //    return from user in DataSource.ListTrips
        //           select user.Clone();
        //}
        //#endregion

        //#region GetAllTripsBy
        ///// <summary>
        ///// A function that returns the trip that have the special thing that the predicat do
        ///// </summary>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        //public IEnumerable<DO.Trip> GetAllTripsBy(Predicate<DO.Trip> predicate)
        //{
        //    return from trip in DataSource.ListTrips
        //           where predicate(trip)
        //           select trip.Clone();
        //}
        //#endregion


        //#region AddTrip
        ///// <summary>
        ///// A function that add trip to the list
        ///// </summary>
        ///// <param name="trip"></param>
        //public void AddTrip(DO.Trip trip)
        //{
        //    if (DataSource.ListTrips.FirstOrDefault(p => p.Id == trip.Id && p.IsDeleted) != null)
        //        throw new Exception();
        //    trip.Id = DO.Configuration.TripID;
        //    DataSource.ListTrips.Add(trip.Clone());
        //}
        //#endregion

        //#region UpdateTrip
        ///// <summary>
        /////  A function that update the trip
        ///// </summary>
        ///// <param name="trip"></param>
        //public void UpdateTrip(DO.Trip trip)
        //{
        //    DO.Trip u = DataSource.ListTrips.Find(p => p.Id == trip.Id && p.IsDeleted);

        //    if (u != null)
        //    {
        //        DataSource.ListTrips.Remove(u);
        //        DataSource.ListTrips.Add(trip.Clone());
        //    }
        //    else
        //        throw new Exception();
        //}
        //#endregion

        //#region UpdateTrip
        ///// <summary>
        ///// method that knows to updt specific fields in Trip
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="update"></param>
        //public void UpdateTrip(int id, Action<DO.Trip> update) //method that knows to updt specific fields in Trip
        //{
        //    DO.Trip trip = DataSource.ListTrips.Find(p => p.Id == id && p.IsDeleted == false);
        //    if (trip == null)
        //        throw new Exception();
        //    update(trip);
        //}
        //#endregion

        //#region DeleteTrip
        ///// <summary>
        ///// A function that delete trip (mark the flag IsDeleted = true)
        ///// </summary>
        ///// <param name="id"></param>
        //public void DeleteTrip(int id)
        //{
        //    DO.Trip u = DataSource.ListTrips.Find(p => p.Id == id && p.IsDeleted);

        //    if (u != null)
        //    {
        //        //DataSource.ListTrips.Remove(u);
        //        u.IsDeleted = true;
        //    }
        //    else
        //        throw new Exception();
        //}
        //#endregion
        //#endregion

        #region BusOnTrip

        //#region BusOnTripDoBoAdapter
        ///// <summary>
        ///// A function that copy details from DO to BO
        ///// </summary>
        ///// <param name="busDO"></param>
        ///// <returns></returns>
        //BO.BusOnTrip BusOnTripDoBoAdapter(DO.BusOnTrip BusOnTripDO)
        //{
        //    BO.BusOnTrip BusOnTripBO = new BO.BusOnTrip();
        //    BusOnTripDO.CopyPropertiesTo(BusOnTripBO);
        //    return BusOnTripBO;
        //}
        //#endregion 

        //#region GetBusOnTrip
        ///// <summary>
        /////  A function that return a bus on trip
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="licenseNumber"></param>
        ///// <returns></returns>
        //public DO.BusOnTrip GetBusOnTrip(int id, int licenseNumber)
        //{
        //    DO.BusOnTrip b = DataSource.ListBusesOnTrip.Find(p => p.Id == id && p.LicenseNumber == licenseNumber && p.IsDeleted);

        //    if (b != null)
        //        return b.Clone();
        //    else

        //        throw new Exception();
        //}
        //#endregion

        //#region GetAllBusesOnTrip
        ///// <summary>
        ///// A function that return all the buses on trip
        ///// </summary>
        ///// <returns></returns>
        //public IEnumerable<DO.BusOnTrip> GetAllBusesOnTrip()
        //{
        //    return from bus in DataSource.ListBusesOnTrip
        //           select bus.Clone();
        //}
        //#endregion

        //#region GetAllBusesOnTripBy
        ///// <summary>
        /////  A function that returns the buses on trip that have the special thing that the predicat do
        ///// </summary>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        //public IEnumerable<DO.BusOnTrip> GetAllBusesOnTripBy(Predicate<DO.BusOnTrip> predicate)
        //{
        //    return from bus in DataSource.ListBusesOnTrip
        //           where predicate(bus)
        //           select bus.Clone();
        //}
        //#endregion

        //#region AddBusOnTrip
        ///// <summary>
        ///// A function that add a bus on trip to the list
        ///// </summary>
        ///// <param name="bus"></param>
        //public void AddBusOnTrip(DO.BusOnTrip bus)
        //{

        //    if (DataSource.ListBusesOnTrip.FirstOrDefault(p => p.Id == bus.Id && p.LicenseNumber == bus.LicenseNumber && p.IsDeleted) != null)
        //        throw new Exception();
        //    bus.Id = DO.Configuration.BusOnTripID++;//המספר הרץ
        //    DataSource.ListBusesOnTrip.Add(bus.Clone());
        //}
        //#endregion

        //#region UpdateBusOnTrip
        ///// <summary>
        ///// A function that update the bus on trip
        ///// </summary>
        ///// <param name="bus"></param>
        //public void UpdateBusOnTrip(DO.BusOnTrip bus)
        //{
        //    DO.BusOnTrip b = DataSource.ListBusesOnTrip.Find(p => p.Id == bus.Id && p.LicenseNumber == bus.LicenseNumber && p.IsDeleted);

        //    if (b != null)
        //    {
        //        DataSource.ListBusesOnTrip.Remove(b);
        //        DataSource.ListBusesOnTrip.Add(bus.Clone());
        //    }
        //    else
        //        throw new Exception();
        //}
        //#endregion UpdateBusOnTrip

        //#region UpdateBusOnTrip

        ///// <summary>
        ///// method that knows to updt specific fields in BusOnTrip
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="update"></param>
        //public void UpdateBusOnTrip(int id, int licenseNumber, Action<DO.BusOnTrip> update)
        //{
        //    DO.BusOnTrip b = DataSource.ListBusesOnTrip.Find(p => p.Id == id && p.LicenseNumber == licenseNumber && p.IsDeleted == false);
        //    if (b == null)
        //        throw new Exception();
        //    update(b);
        //}
        //#endregion

        //#region DeleteBusOnTrip
        ///// <summary>
        ///// A function that delete bus on trip (mark the flag IsDeleted = true) 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="licenseNumber"></param>
        //public void DeleteBusOnTrip(int id, int licenseNumber)
        //{
        //    DO.BusOnTrip b = DataSource.ListBusesOnTrip.Find(p => p.Id == id && p.LicenseNumber == licenseNumber && p.IsDeleted);

        //    if (b != null)
        //    {
        //        //DataSource.ListBusesOnTrip.Remove(b);
        //        b.IsDeleted = true;
        //    }
        //    else
        //        throw new Exception();
        //}
        //#endregion
        #endregion





        //#region StationInLine
        //public void UpdateTimeAndDistance(BO.StationInLine first, BO.StationInLine second)
        //{
        //    try
        //    {
        //        DO.AdjacentStations adj = new DO.AdjacentStations() { StationCode1 = first.StationCode, StationCode2 = second.StationCode, Distance = first.Distance, Time = first.Time, IsDeleted = false };
        //        dl.UpdateAdjacentStations(adj);
        //    }
        //    catch (DO.BadAdjacentStationsException ex)
        //    {
        //        throw new BO.BadAdjacentStationsException(ex.stationCode1, ex.stationCode2, ex.Message);
        //    }
        //    {
        //        throw new Exception("Error, it cannot be update");
        //    }
        //}
        //#endregion





        //#region LineStation
        //public bool IsExistLineStation(DO.LineStation s)
        //{
        //    try
        //    {
        //        DO.LineStation linestation = dl.GetLineStation(s.LineId, s.StationCode);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }

        //}
        //public void AddLineStation(BO.LineStation s)
        //{
        //    DO.LineStation sDO = (DO.LineStation)s.CopyPropertiesToNew(typeof(DO.LineStation));
        //    try
        //    {
        //        if (IsExistLineStation(sDO))
        //            throw new BO.BadLineStationException(sDO.LineId, sDO.StationCode, "The station is already exist in the line");
        //        //עידכון של כל האינדקסים של התחנות הבאות בקו
        //        List<DO.LineStation> lSList = ((dl.GetAllLineStationsBy(stat => stat.LineId == sDO.LineId && stat.IsDeleted == false)).OrderBy(stat => stat.LineStationIndex)).ToList();
        //        int indexlast = lSList[lSList.Count - 1].LineStationIndex;
        //        if (sDO.LineStationIndex != indexlast + 1)//if we didnt add a last station
        //        {
        //            for (int i = sDO.LineStationIndex; i < indexlast + 1; i++)
        //            {
        //                lSList[i - 1].LineStationIndex++;
        //            }
        //        }
        //        //עידכון תחנה קודמת והבאה וגם את התחנה הראשונה והאחרונה של היישות קו
        //        DO.LineStation prev;
        //        DO.LineStation next;
        //        if (sDO.LineStationIndex > 1)//its not the first station
        //        {
        //            prev = lSList[sDO.LineStationIndex - 2];
        //            prev.NextStationCode = sDO.StationCode;
        //            sDO.PrevStationCode = prev.StationCode;
        //        }
        //        else//if its the first station-we need to update the first ans last station in the DO.Line
        //        {
        //            DO.Line line = dl.GetLine(sDO.LineId);
        //            line.FirstStation = sDO.StationCode;
        //            dl.UpdateLine(line);
        //        }
        //        if (sDO.LineStationIndex != indexlast + 1)//if its not the last station
        //        {
        //            next = lSList[sDO.LineStationIndex];
        //            next.PrevStationCode = sDO.StationCode;
        //            sDO.NextStationCode = next.StationCode;
        //        }
        //        else//if its the last station we need to update the last station in the DO.Line
        //        {
        //            DO.Line line = dl.GetLine(sDO.LineId);
        //            line.LastStation = sDO.StationCode;
        //            dl.UpdateLine(line);
        //        }
        //        foreach (DO.LineStation item in lSList)
        //        {
        //            dl.UpdateLineStation(item);
        //        }

        //        dl.AddLineStation(sDO);

        //        //טיפול בתחנות עוקבות לאחר ההוספה
        //        List<DO.LineStation> lst = ((dl.GetAllLineStationsBy(stat => stat.LineId == sDO.LineId && stat.IsDeleted == false)).OrderBy(stat => stat.LineStationIndex)).ToList();
        //        if (s.LineStationIndex != 1)//if its the first station- it doesnt have prev
        //        {
        //            prev = lst[s.LineStationIndex - 2];
        //            if (!IsExistAdjacentStations(prev.StationCode, s.StationCode))
        //            {
        //                DO.AdjacentStations adjPrev = new DO.AdjacentStations() { StationCode1 = prev.StationCode, StationCode2 = s.StationCode };
        //                dl.AddAdjacentStations(adjPrev);
        //            }
        //        }
        //        if (s.LineStationIndex != lst[lst.Count - 1].LineStationIndex)//if its the last station- it doesnt have next
        //        {
        //            next = lst[s.LineStationIndex];
        //            if (!IsExistAdjacentStations(s.StationCode, next.StationCode))
        //            {
        //                DO.AdjacentStations adjNext = new DO.AdjacentStations() { StationCode1 = s.StationCode, StationCode2 = next.StationCode };
        //                dl.AddAdjacentStations(adjNext);
        //            }
        //        }

        //    }
        //    catch (DO.BadLineStationException ex)
        //    {
        //        throw new BO.BadLineStationException(ex.lineId, ex.stationCode, ex.Message);
        //    }
        //    catch (DO.BadAdjacentStationsException ex)
        //    {
        //        throw new BO.BadAdjacentStationsException(ex.stationCode1, ex.stationCode2, ex.Message);
        //    }
        //}
        //public void DeleteLineStation(int lineId, int stationCode)
        //{
        //    try
        //    {
        //        //AdjacentStation
        //        DO.LineStation statDel = dl.GetLineStation(lineId, stationCode);//the station that we want to delete
        //        BO.Line line = GetLine(lineId);
        //        if (line.Stations.Count <= 2)
        //            throw new BO.BadInputException("The Station cannot be deleted, there is only 2 stations in the line route");
        //        if (line.Stations[0].StationCode != stationCode && line.Stations[line.Stations.Count - 1].StationCode != stationCode)//if its not the first or the last station
        //        {
        //            BO.StationInLine prev = line.Stations[statDel.LineStationIndex - 2];
        //            BO.StationInLine next = line.Stations[statDel.LineStationIndex];
        //            if (!dl.IsExistAdjacentStations(prev.StationCode, next.StationCode))
        //            {
        //                DO.AdjacentStations adj = new DO.AdjacentStations() { StationCode1 = prev.StationCode, StationCode2 = next.StationCode, IsDeleted = false };
        //                dl.AddAdjacentStations(adj);
        //            }
        //        }
        //        //delete the line station
        //        List<DO.LineStation> lSList = ((dl.GetAllLineStationsBy(stat => stat.LineId == statDel.LineId && stat.IsDeleted == false)).OrderBy(stat => stat.LineStationIndex)).ToList();
        //        DO.LineStation NextFind;
        //        if (statDel.LineStationIndex > 1)//if its not the first station
        //        {
        //            DO.LineStation PrevFind = lSList[statDel.LineStationIndex - 2];
        //            if (statDel.LineStationIndex != lSList[lSList.Count - 1].LineStationIndex)//if its not the last station
        //            {
        //                NextFind = lSList[statDel.LineStationIndex];
        //                PrevFind.NextStationCode = NextFind.StationCode;
        //                NextFind.PrevStationCode = PrevFind.StationCode;
        //            }
        //            else
        //            {
        //                PrevFind.NextStationCode = 0;
        //            }
        //        }
        //        else//if its the first station
        //        {
        //            if (statDel.LineStationIndex != lSList[lSList.Count - 1].LineStationIndex)
        //            {
        //                NextFind = lSList[statDel.LineStationIndex];
        //                NextFind.PrevStationCode = 0;
        //            }
        //        }
        //        //update index;
        //        if (statDel.LineStationIndex != lSList[lSList.Count - 1].LineStationIndex)//update all the indexes of all the next stations if its not the last station
        //        {
        //            for (int i = statDel.LineStationIndex; i < lSList.Count; i++)
        //            {
        //                lSList[i].LineStationIndex--;
        //            }
        //        }
        //        foreach (DO.LineStation item in lSList)
        //        {
        //            dl.UpdateLineStation(item);
        //        }

        //        dl.DeleteLineStation(lineId, stationCode);
        //    }
        //    catch (DO.BadLineStationException ex)
        //    {
        //        throw new BO.BadLineStationException(ex.lineId, ex.stationCode, ex.Message);
        //    }
        //    catch (BO.BadLineIdException ex)
        //    {
        //        throw new BO.BadLineIdException(ex.ID, ex.Message);
        //    }
        //    catch (DO.BadAdjacentStationsException ex)
        //    {
        //        throw new BO.BadAdjacentStationsException(ex.stationCode1, ex.stationCode2);
        //    }
        //}
        #endregion



        #region GetLineTimingsPerStation
        public IEnumerable<BO.LineTiming> GetLineTimingsPerStation(BO.Station station, TimeSpan tsCurentTime)//פונקצית הרחבה
        {

            IEnumerable<BO.LineTiming> lst=(IEnumerable<BO.LineTiming>)new List<BO.LineTiming>();
            List<BO.LineTiming> temp = new List<LineTiming>();
            List<BO.LineTrip> times;
            DO.Line lineDO = new DO.Line();
            BO.Line lineBO = new BO.Line();

            BO.Station curStation = station;
            List<BO.ShortLine> ListOfLines = curStation.ListOfLines.ToList();

            foreach (BO.Line l in ListOfLines)
            {
                lineDO = dl.GetLine(l.Id);
                lineBO = lineDoBoAdapter(lineDO);
                BO.LineTiming lt = new BO.LineTiming();
                lt.LineId = l.Id;
                lt.LineNumber = l.LineNumber;
                List<BO.StationInLine> listOfStationsInLine = lineBO.ListOfStationsInLine.ToList();
                times = lineBO.ListOfTripTime.ToList();
                lt.SourceStation = GetStation(listOfStationsInLine[0].StationCode);
                lt.TargetStation = GetStation(listOfStationsInLine[listOfStationsInLine.Count() - 1].StationCode);
                BO.StationInLine sil = GetStationInLine(station.Code, l.Id);
                //foreach (BO.LineTrip ltrip in lineBO.ListOfTripTime)
                //{

                for (int i = 0; i < times.Count; i++)
                {
                    TimeSpan sum = times[i].StartAt;
                    if (sum < tsCurentTime)
                        continue;

                    int count = -1;

                    //foreach (BO.StationInLine s in listOfStationsInLine)
                    //{
                    //    if (s.StationCode == station.Code)
                    //    {
                    //        sil = s;
                    //        break;
                    //    }
                    //}
                    while (sil.LineStationIndex != count)
                    {
                        sum +=  listOfStationsInLine[count + 1].TimeTo;
                        count++;
                    }
                    lt.Timing = sum;
                    temp.Add(new BO.LineTiming() { LineId = lt.LineId, LineNumber = lt.LineNumber, SourceStation = lt.SourceStation, TargetStation = lt.TargetStation, Timing = lt.Timing } );
                }
                //}
                //lst.ToList().Add(lt);                
            }
            temp = (temp.OrderBy(s => s.Timing)).ToList();
            lst = temp.ToList();
            
            return lst;
        }
        #endregion
    }
}//tamargavrieli18@gmail.com