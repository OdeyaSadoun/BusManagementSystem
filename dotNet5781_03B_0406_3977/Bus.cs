
    using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
    using System.Runtime.Remoting.Metadata.W3cXsd2001;
    using System.Text;
    using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;




//Tamar Gavrieli 322533977 & Odeya Sadoun 212380406
//Exercize number 1


namespace dotNet5781_03B_0406_3977
{
    public class Bus
    {
        //FILDES:
        #region licenseNumber
        private string licenseNumber;

        public string LicenseNumber
        {
            get { return licenseNumber; }
            set
            {
                string temp = value;
                if (!IsDigitsOnly(temp))
                    throw new ArgumentException("error input licenseNumber");
                licenseNumber = VisualShapeOfLicenseNumber(temp);
            }
        }
        #endregion 

        #region DateBegin
        public DateTime DateBegin { get; set; }
        //private Date dateBegin;

        //public Date DateBegin
        //{
        //    get { return dateBegin; }
        //    set
        //    {

        //        if ((dateBegin.day > 31) || (dateBegin.day < 1))
        //        {
        //            throw new ArgumentException("day must be btween 1 to 31");
        //            //dateBegin.day = 1; //defalt value
        //        }
        //        else
        //            dateBegin.day = value.day;

        //        if ((dateBegin.month > 12) || (dateBegin.month < 1))
        //        {
        //            throw new ArgumentException("month must be between 1 to 12");
        //            //dateBegin.day = 1; //defalt value
        //        }
        //        else
        //            dateBegin.month = value.month;

        //        if (dateBegin.year < 1900) //befor 1900 there weren't buses.
        //        {
        //            throw new ArgumentException("year must be more 1900 years");
        //            //dateBegin.year = 1900;
        //        }
        //        else
        //            dateBegin.year = value.year;
        //    }
        //}
        #endregion

        #region sumMileage
        private double sumMileage;

        public double SumMileage
        {
            get { return sumMileage; }
            set
            {
                if (sumMileage < 0)
                {
                    throw new ArgumentException("mileage can't be negative");
                    //mileage = 0; //defalt value
                }
                else
                    sumMileage = value;
            }
        }
        #endregion

        #region currentMileage
        public double currentMileage { get; set; }
        #endregion

        #region kmBeforCare //The amount of mileages left for the bus to travel before needing treatment
        private double kmBeforCare;

        public double KmBeforCare
        {
            get { return kmBeforCare; }
            set { kmBeforCare = value; }
        }
        #endregion 

        #region fuel
        private double fuel;

        public double Fuel
        {
            get { return fuel; }
            set
            {
                if (fuel < 0)
                {
                    throw new ArgumentException("fuel can't be negative");
                }
                else
                    fuel = value;
            }
        }
        #endregion

        #region kmBeforeFuel //The amount of mileages left for the bus to travel before needing refueling

        private double kmBeforeFuel;

        public double KmBeforeFuel
        {
            get { return kmBeforeFuel; }
            set {kmBeforeFuel = value;}
        }
        #endregion

        #region lastCare
        public DateTime LastCare { get; set; }
        //private Date lastCare;

        //public Date LastCare
        //{
        //    get { return lastCare; }
        //    set {
        //        if ((lastCare.day > 31) || (lastCare.day < 1))
        //        {
        //            throw new ArgumentException("day must be between 1 to 31");
        //            //lastCare.day = 1; //defalt value
        //        }
        //        else
        //            lastCare.day = value.day;

        //        if ((lastCare.month > 12) || (lastCare.month < 1))
        //        {
        //            throw new ArgumentException("month must be between 1 to 12");
        //            //lastCare.day = 1; //defalt value
        //        }
        //        else
        //            lastCare.month = value.month;

        //        if (lastCare.year < 2015) //before 2015 there weren't buses.
        //        {
        //            throw new ArgumentException("last year must be more than 2015 years");
        //            //lastCare.year = 1900;
        //        }
        //        else
        //            lastCare.year = value.year;
        //    }
        //}

        #endregion

        #region BusStatus
        public BusStatus Status { get; set; }
        #endregion

        //FUNCTIONS:
        #region Constructor
        public Bus(string licenseNumber, DateTime dateBegin, double mileage = 0,  double fuel = 0)
        //constructor
        {
            this.LicenseNumber = licenseNumber;
            this.DateBegin = dateBegin;
            this.sumMileage = mileage;
            this.Fuel = fuel;
            //defult values
            this.Status = BusStatus.Ready;
            this.currentMileage = 0;
            this.kmBeforeFuel = 1200;
            this.KmBeforCare = 20000;
            this.LastCare = new DateTime(2015, 01, 01);
        }

        #endregion

        #region Defult Constructor
        public Bus()
        //defalt constructor
        {
            this.licenseNumber = "000-00-000";
            this.DateBegin = new DateTime(1900, 01, 01);
            this.sumMileage = 0;
            this.Status = BusStatus.Ready;
            this.fuel = 0;
            this.currentMileage = 0;
            this.kmBeforeFuel = 1200;
            this.KmBeforCare = 20000;
            this.LastCare = new DateTime(2015, 01, 01);
        }
        #endregion

        #region VisualShapeOfLicenseNumber
        public static string VisualShapeOfLicenseNumber(string value)
        //A function that edit the license number to the format XX-XXX-XX / XXX-XX-XX
        {
            string temp = "";
            if (value.Length == 7)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if ((i == 2) || (i == 5)) // for the format XX-XXX-XX
                        temp += "-";
                    temp += value[i];
                }
            }
            else if (value.Length == 8)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if ((i == 3) || (i == 5)) // for the format XXX-XX-XXX
                        temp += "-";
                    temp += value[i];
                }
            }
            else
            {
                //input defalt value
                throw new ArgumentException("error input licenseNumber");
            }
            return temp;
        }
        #endregion

        #region IsDigitsOnly
        private bool IsDigitsOnly(string str)
        //A function that cheak if the string is only numbers
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
        #endregion

        #region YearPassed
        public bool YearPassed()
        //A function that cheak if passed year from the date begin
        {
            DateTime zeroTime = new DateTime(1, 1, 1);

            var diff = DateTime.Now - DateBegin;
            int years = (zeroTime + diff).Year - 1;
            if (years >= 1)
            {
                return true;

            }
            return false;
        }
        #endregion

        public override string ToString()
        {
            return string.Format("The bus license number is:" + licenseNumber+ "\t DateBegin:" + DateBegin+ "\t the sum mileage is:"+ SumMileage);
        }
        public void goToTravel(double km)
        {
            if (kmBeforCare - km <= 0)
                MessageBox.Show("The bus cant perform this travel because the bus traveled 20000 km without care - need to care");
            if (kmBeforeFuel - km <= 0)
                MessageBox.Show("The fuel isnt enough for travel, you should fuel");
          
            //if (YearPassed())
            //    MessageBox.Show("The bus cant perform this travel because a year passed from");
            if (Status == BusStatus.Ready)
            {
                currentMileage = km;
                sumMileage += currentMileage;
                kmBeforeFuel -= km;
                kmBeforCare -= km;
            }
            else
                MessageBox.Show("The status of the bus is:" + Status + "\nThe bus can't go to drive");

        }


    }
}



