
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Remoting.Metadata.W3cXsd2001;
    using System.Text;
    using System.Threading.Tasks;

    namespace dotNet5781_01_0406_3977
    {
        struct Date
        {
            int day;
            int month;
            int year;
        };

        class Bus1
        {
            private int year;

            public int Year
            {
                get { return year; }
                set
                {
                    if (year < 0) //year can't be a negative number
                        year = 0;
                    else
                        year = value;
                }
            }

            private string licenseNumber;

            public string LicenseNumber
            {
                get { return licenseNumber; }
                set
                {
                    string temp = value;
                    if ((year < 2018) && (temp.Length == 7))
                    {
                        for (int i = 0; i < temp.Length; i++)
                        {
                            if ((i == 2) || (i == 5)) // for the format XX-XXX-XX
                                licenseNumber += "-";
                            licenseNumber += temp[i];
                        }
                    }
                    else if ((year >= 2018) && (temp.Length == 8))
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
                        if (year < 2018)
                            licenseNumber = "00-000-00";
                        else
                            licenseNumber = "000-00-000";
                    }
                }
            }

            public Date DateBegin { get; set; }

        }
    }



