
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

        #region lineTrip

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
            stationsTmp = stations;

            var temp = from stat in stationsTmp
                       where stat.IsDeleted == false
                       select stat;
            stations = temp.ToList();
            lineBO = lineDO.CopyToLineDOToBO();
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
                BO.Station stat = GetStation(sInL.StationCode);
                DO.Line lineDO = dl.GetLine(sInL.LineId);

                dl.DeleteLineStation(sInL.LineId, sInL.StationCode);
                lineDO.TravelTimeInThisLine -= sInL.TimeTo;


            }
            catch(DO.IncorrectInputException ex)
            {
                throw new BO.IncorrectInputException( ex.Message);
            }
        }
        #endregion

        #region AddStationInLine

        public void AddStationInLine(BO.Station s, BO.Line l)
        {
            try
            {
                BO.StationInLine sil = new StationInLine() { LineId = l.Id, LineStationIndex = l.ListOfStationsInLine.Count(), StationCode = s.Code, StationName = s.Name };
                DO.LineStation ls = new DO.LineStation() { StationCode = s.Code, LineId = l.Id, LineStationIndex = l.ListOfStationsInLine.Count() };
                dl.AddLineStation(ls);
                Random rnd = new Random();
                DO.AdjacentStations aj = new AdjacentStations() { CodeStation1 = l.ListOfStationsInLine.ToList()[l.ListOfStationsInLine.Count() - 1].StationCode, CodeStation2 = sil.StationCode , Distance = rnd.Next(6,50)};
                aj.TravelTime = new TimeSpan( rnd.Next(0, 2), rnd.Next(0,60), rnd.Next(0,60));
                dl.AddAdjacentStations(aj);
                l.ListOfStationsInLine.ToList()[l.ListOfStationsInLine.Count() - 1].DistanceTo = aj.Distance;
                l.ListOfStationsInLine.ToList()[l.ListOfStationsInLine.Count() - 1].TimeTo = aj.TravelTime;
               
                l.TravelTimeInThisLine += l.ListOfStationsInLine.ToList()[l.ListOfStationsInLine.Count() - 1].TimeTo;
                l.ListOfStationsInLine.ToList().Add(sil);
                l.LastStation = GetStation(l.ListOfStationsInLine.ToList()[l.ListOfStationsInLine.Count() - 1].StationCode);
       

            }
            catch (DO.IncorrectInputException ex)
            {
                throw new BO.IncorrectInputException(ex.Message);
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
                DO.Station statDO = dl.GetStation(station.Code);
                if (station.ListOfLines.Count() != 0)//if there are lines that stop in the station
                    throw new BO.IncorrectCodeStationException(station.Code, "Station cant be deleted because other buses stop there");
                //////נעבור על רשימת הקווים, בכל קו נבדוק האם התחנה היתה קיימת, ופשוט נמחק אותה, (ןנעדכן את הזמן והמרחק לתחנה הבאה)
                //List<BO.Line> listLines = GetAllLines().ToList();
                //bool stationdelete = false;
                //foreach (BO.Line l in listLines)
                //{
                //    foreach (BO.StationInLine sl in l.ListOfStationsInLine)
                //    {
                //        if (sl.StationCode == station.Code)
                //        {
                //            DeleteStationInLine(sl);
                //            stationdelete = true;
                //            break;
                //        }
                //    }
                //    //DO.Station statDO = dl.GetStation(station.Code);
                //    //if (stationdelete)
                //    //    break;
                //}
                //List<DO.AdjacentStations> listAdj = dl.GetAllAdjacentStations().ToList();
                //int i = 0;
                //DO.AdjacentStations tempAdj = listAdj[i];
                //DO.AdjacentStations adjToAdd = new AdjacentStations() ;
                ////מה קורה שזו תחנה ראשונה???
                //if(tempAdj.CodeStation1 == station.Code)
                //{
                //    dl.DeleteAdjacentStations(tempAdj.CodeStation1, tempAdj.CodeStation2);
                //    i++;
                //    tempAdj = listAdj[i];
                //}
                ////לראות מה הולך עם הזמנים
                //bool isDeleteAdj = false;
                //for(int j = i; j < listAdj.Count(); j ++)
                //{
                //    if (listAdj[j].CodeStation1 == station.Code)//התחנה הראשונה בזוג תחנות עוקבות היא התחנה שנמחקה
                //    {
                //        dl.DeleteAdjacentStations(listAdj[j].CodeStation1, listAdj[j].CodeStation2);
                //        adjToAdd.CodeStation1 = tempAdj.CodeStation1;
                //        adjToAdd.CodeStation2 = listAdj[j].CodeStation2;
                //        adjToAdd.Distance = tempAdj.Distance + listAdj[j].Distance;
                //        adjToAdd.TravelTime = tempAdj.TravelTime + listAdj[j].TravelTime;
                //        dl.AddAdjacentStations(adjToAdd);
                //        isDeleteAdj = true;

                //    }
                //    if(!isDeleteAdj)
                //        tempAdj = listAdj[j];
                //    else
                //        tempAdj = listAdj[j-1];

                //    //else if (listAdj[i].CodeStation2 == statBO.Code)
                //    //{
                //    //}
                //}
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

        #region lineTrip
        #region AddLineTrip
        /// <summary>
        /// A function that add a station to 
        /// </summary>
        /// <param name="station"></param>
        public void AddLineTrip(BO.LineTrip ltrip)
        {
            DO.LineTrip ltDO = new DO.LineTrip();            
            ltrip.CopyPropertiesTo(ltDO);// מועתק ל DO
            ltDO.IsDeleted = false;
            try
            {
                dl.AddLineTrip(ltDO);
            }
            catch (DO.IncorrectCodeStationException ex)
            {
                throw new BO.IncorrectCodeStationException(ex.stationCode, ex.Message);
            }
        }
        #endregion
        #endregion

        #region GetLineTimingsPerStation
        /// <summary>
        /// A function that return the time per station
        /// </summary>
        /// <param name="station"></param>
        /// <param name="tsCurentTime"></param>
        /// <returns></returns>
        public IEnumerable<BO.LineTiming> GetLineTimingsPerStation(BO.Station station, TimeSpan tsCurentTime)
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

                for (int i = 0; i < times.Count; i++)
                {
                    TimeSpan sum = times[i].StartAt;
                    if (sum < tsCurentTime)
                        continue;

                    int count = -1;

                    while (sil.LineStationIndex != count)
                    {
                        sum +=  listOfStationsInLine[count + 1].TimeTo;
                        count++;
                    }
                    lt.Timing = sum;
                    temp.Add(new BO.LineTiming() { LineId = lt.LineId, LineNumber = lt.LineNumber, SourceStation = lt.SourceStation, TargetStation = lt.TargetStation, Timing = lt.Timing } );
                }
             
            }
            temp = (temp.OrderBy(s => s.Timing)).ToList();
            lst = temp.ToList();
            
            return lst;
        }
        #endregion

       
    }
}