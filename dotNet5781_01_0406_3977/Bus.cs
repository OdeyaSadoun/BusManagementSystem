
    using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
    using System.Runtime.Remoting.Metadata.W3cXsd2001;
    using System.Text;
    using System.Threading.Tasks;

    namespace dotNet5781_01_0406_3977
    {
        struct Date
        {
            public int day;
            public int month;
            public int year;
        };
        class Bus
        {
        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;

        }
        private string licenseNumber;

            public string LicenseNumber
            {
                get { return licenseNumber; }
                set
                {
                    string temp = value;
                    if(!IsDigitsOnly(temp))
                        throw new ArgumentException("error input licenseNumber");

                    if (temp.Length == 7)
                    {
                        for (int i = 0; i < temp.Length; i++)
                        {
                            if ((i == 2) || (i == 5)) // for the format XX-XXX-XX
                                licenseNumber += "-";
                            licenseNumber += temp[i];
                        }
                    }
                    else if (temp.Length == 8)
                    {
                        for (int i = 0; i < temp.Length; i++)
                        {
                            if ((i == 3) || (i == 5)) // for the format XXX-XX-XXX
                                licenseNumber += "-";
                            licenseNumber += temp[i];
                        }
                    }
                    else
                    {
                         //input defalt value
                         throw new ArgumentException("error input licenseNumber");
                    }
                }
            }

        private Date dateBegin;

        public Date DateBegin
        {
            get { return dateBegin; }
            set
            {

                if ((dateBegin.day > 31) || (dateBegin.day < 1))
                {
                    throw new ArgumentException("day must be btween 1 to 31");
                    //dateBegin.day = 1; //defalt value
                }
                else
                    dateBegin.day = value.day;

                if ((dateBegin.month > 12) || (dateBegin.month < 1))
                {
                    throw new ArgumentException("month must be between 1 to 12");
                    //dateBegin.day = 1; //defalt value
                }
                else
                    dateBegin.month = value.month;

                if (dateBegin.year < 1900) //befor 1900 there weren't buses.
                {
                    throw new ArgumentException("year must be more 1900 years");
                    //dateBegin.year = 1900;
                }
                else
                    dateBegin.year = value.year;
            }
        }

        private double mileage;

        public double Mileage
        {
            get { return mileage; }
            set 
            { 
                if(mileage < 0)
                {
                    throw new ArgumentException("mileage can't be negative");
                    //mileage = 0; //defalt value
                }
                else
                    mileage = value;
            }
        }
        private double fuelAfterKm;

        public double FuelAfterKm
        {
            get { return fuelAfterKm; }
            set { fuelAfterKm -= value; }
        }

        private double fuel;

        public double Fuel
        {
            get { return fuel; }
            set
            {
                if (fuel < 0)
                {
                    throw new ArgumentException("fuel can't be negative");
                    //fuel = 0; //defalt value
                }
                else
                    fuel = value;
            }
        }
        private Date lastCare;

        public Date LastCare
        {
            get { return lastCare; }
            set {
                if ((lastCare.day > 31) || (lastCare.day < 1))
                {
                    throw new ArgumentException("day must be between 1 to 31");
                    //lastCare.day = 1; //defalt value
                }
                else
                    lastCare.day = value.day;

                if ((lastCare.month > 12) || (lastCare.month < 1))
                {
                    throw new ArgumentException("month must be between 1 to 12");
                    //lastCare.day = 1; //defalt value
                }
                else
                    lastCare.month = value.month;

                if (lastCare.year < 2015) //before 2015 there weren't buses.
                {
                    throw new ArgumentException("last year must be more than 2015 years");
                    //lastCare.year = 1900;
                }
                else
                    lastCare.year = value.year;
            }
        }



        public Bus(string licenseNumber, Date dateBegin, double mileage = 0, double fuel = 0, double fuelAfterKm = 1200)
            //constructor
        {
            this.licenseNumber = licenseNumber;
            this.dateBegin = dateBegin;
            this.mileage = mileage;
            this.fuel = fuel;
            this.fuelAfterKm = fuelAfterKm;
            this.lastCare.day = 1;
            this.lastCare.month = 1;
            this.lastCare.year = 2015;

        }

        public Bus()
            //defalt constructor
        {
            this.licenseNumber = "000-00-000";
            this.dateBegin.day = 1;
            this.dateBegin.month = 1;
            this.dateBegin.year = 1900;
            this.mileage = 0;
            this.fuel = 0;
            this.fuelAfterKm = 1200;
            this.lastCare.day = 1;
            this.lastCare.month = 1;
            this.lastCare.year = 2015;
        }
    }
    }



