using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doteNet5781_01_0406_3977
{
    struct Date
    {
        int day;
        int month;
        int year;
    }
   
    class Bus
    {
        private int year;

        public int Year
        {
            get { return year; }
            set
            {//year cant be a negative num
                if (year < 0)
                {
                    year = 0;
                }
                else
                {
                    year = value;
                }          
            }
        }

        private string licenseNumber;

        public string LicenseNumber
        {
            get { return licenseNumber; }
            set {
                string temp = value;
                if ((year < 2018) && (temp.Length == 7))
                {
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if ((i == 2) || (i == 5))//format xx-xxx-xx
                            licenseNumber += "-";

                        licenseNumber += temp[i];
                    }

                }
                else if ((year > 2018) && (temp.Length == 8))
                {
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if ((i == 3) || (i == 5))//format xxx-xx-xx
                            licenseNumber += "-";

                        licenseNumber += temp[i];
                    }
                }
                else
                {//default licenseNumber
                    if (year < 2018)
                        licenseNumber = "00-000-00";
                    if (year > 2018)
                        licenseNumber = "000-00-000";

                }
            }
        }
        public Date DateBegin { get; set; }
    }
}
