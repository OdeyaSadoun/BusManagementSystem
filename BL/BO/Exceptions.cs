using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{

    [Serializable]

    public class IncorrectLicenseNumberException : Exception
    {
        public int licenseNumber;
        public IncorrectLicenseNumberException(int ln) : base() => licenseNumber = ln;
        public IncorrectLicenseNumberException(int ln, string message) :
            base(message) => licenseNumber = ln;
        public IncorrectLicenseNumberException(int ln, string message, Exception innerException) :
            base(message, innerException) => licenseNumber = ln;

        public override string ToString() => base.ToString() + $", Incorrect license number: {licenseNumber}";
    }


    [Serializable]

    public class IncorrectLicenseNumberOrDateException : Exception
    {
        public int licenseNumber;
        public DateTime day;
        public IncorrectLicenseNumberOrDateException(int ln, DateTime dt) : base()
        {
            licenseNumber = ln;
            day = dt;
        }
        public IncorrectLicenseNumberOrDateException(int ln, DateTime dt, string message) :
            base(message)
        {
            licenseNumber = ln;
            day = dt;
        }
        public IncorrectLicenseNumberOrDateException(int ln, DateTime dt, string message, Exception innerException) :
            base(message, innerException)
        {
            licenseNumber = ln;
            day = dt;
        }

        public override string ToString() => base.ToString() + $", Incorrect license number: {licenseNumber} OR date begine: {day}";
    }

    [Serializable]
    public class IncorrectInputException : Exception
    {
        public IncorrectInputException(string message) : base(message) { }
        public override string ToString() => base.ToString();
    }

    [Serializable]
    public class IncorrectLineIDException : Exception
    {
        public int ID;
        public IncorrectLineIDException(int id) : base() => ID = id;
        public IncorrectLineIDException(int id, string message) :
            base(message) => ID = id;
        public IncorrectLineIDException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $", Incorrect Line ID number: {ID}";
    }

    [Serializable]
    public class IncorrectCodeStationException : Exception
    {
        public int stationCode;
        public IncorrectCodeStationException(int code) : base() => stationCode = code;
        public IncorrectCodeStationException(int code, string message) :
            base(message) => stationCode = code;
        public IncorrectCodeStationException(int code, string message, Exception innerException) :
            base(message, innerException) => stationCode = code;

        public override string ToString() => base.ToString() + $", Incorrect station code number: {stationCode}";
    }

    [Serializable]
    public class IncorrectUserNameException : Exception
    {
        public string UserName;
        public IncorrectUserNameException(string code) : base() => UserName = code;
        public IncorrectUserNameException(string code, string message) :
            base(message) => UserName = code;
        public IncorrectUserNameException(string message, Exception innerException) :
            base(message, innerException) => UserName = ((DO.IncorrectUserNameException)innerException).UserName;

        public override string ToString() => base.ToString() + $", Incorrect station code number: {UserName}";
    }

    [Serializable]
    public class IncorrectTripIDException : Exception
    {
        public int ID;
        public IncorrectTripIDException(int id) : base() => ID = id;
        public IncorrectTripIDException(int id, string message) :
            base(message) => ID = id;
        public IncorrectTripIDException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $", Incorrect Line ID number: {ID}";
    }

    [Serializable]
    public class IncorrectDateException : Exception
    {
        public DateTime dadetime;
        public IncorrectDateException(DateTime dt) : base() => dadetime = dt;
        public IncorrectDateException(DateTime dt, string message) :
            base(message) => dadetime = dt;
        public IncorrectDateException(DateTime dt, string message, Exception innerException) :
            base(message, innerException) => dadetime = dt;

        public override string ToString() => base.ToString() + $", Incorrect license number: {dadetime}";
    }


}
