
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


            private string licenseNumber;

            public string LicenseNumber
            {
                get { return licenseNumber; }
                set
                {
                    string temp = value;
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
                       Console.WriteLine("error input licenseNumber");
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
                    Console.WriteLine("day must be between 1 to 31");
                    dateBegin.day = 1; //defalt value
                }
                else
                    dateBegin.day = value.day;

                if ((dateBegin.month > 12) || (dateBegin.month < 1))
                {
                    Console.WriteLine("month must be between 1 to 12");
                    dateBegin.day = 1; //defalt value
                }
                else
                    dateBegin.month = value.month;

                if (dateBegin.year < 1900) //befor 1900 there weren't buses.
                {
                    dateBegin.year = 1900;
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
                    Console.WriteLine("mileage can't be negative");
                    mileage = 0; //defalt value
                }
                else
                    mileage = value;
            }
        }

        private double fuel;

        public double Fuel
        {
            get { return fuel; }
            set
            {
                if (fuel < 0)
                {
                    Console.WriteLine("fuel can't be negative");
                    fuel = 0; //defalt value
                }
                else
                    fuel = value;
            }
        }

        public Bus(string licenseNumber, Date dateBegin, double mileage, double fuel)
            //constructor
        {
            this.licenseNumber = licenseNumber;
            this.dateBegin = dateBegin;
            this.mileage = mileage;
            this.fuel = fuel;
        }

        public Bus()
            //defalt constructor
        {
            this.licenseNumber = "000-00-000";
            this.dateBegin.day = 0;
            this.dateBegin.month = 0;
            this.dateBegin.year = 0;
            this.mileage = 0;
            this.fuel = 0;
        }
    }
    }



