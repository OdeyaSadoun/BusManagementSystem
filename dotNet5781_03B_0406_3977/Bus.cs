
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
    public class Bus : DependencyObject
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
                //if ((temp.Length == 8) && (DateBegin.Year < 2018))
                //    throw new ArgumentException("error input licenseNumber");
                //if  ((temp.Length == 7) && (DateBegin.Year >= 2018))
                //    throw new ArgumentException("error input licenseNumber");

                //MessageBox.Show("error input licenseNumber od date");
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
        private float sumMileage;

        public float SumMileage
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
        public float currentMileage { get; set; }
        #endregion

        #region kmBeforCare //The amount of mileages left for the bus to travel before needing treatment
        private float kmBeforCare;

        public float KmBeforCare
        {
            get { return kmBeforCare; }
            set { kmBeforCare = value; }
        }
        #endregion 

        #region fuel
        private float fuel;

        public float Fuel
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

        private float kmBeforeFuel;

        public float KmBeforeFuel
        {
            get { return kmBeforeFuel; }
            set {kmBeforeFuel = value;}
        }
        #endregion

        #region lastCare
        public DateTime LastCare { get; set; }

        #endregion

        #region BusStatus
        public BusStatus Status
        {
            get { return (BusStatus)GetValue(StatusProperty); }

            set { SetValue(StatusProperty, value); }
        }
        public static readonly DependencyProperty StatusProperty =
        DependencyProperty.Register("Status", typeof(BusStatus), typeof(Bus), new UIPropertyMetadata(BusStatus.Ready));

        #endregion

        //FUNCTIONS:
        #region Constructor
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="licenseNumber"></param>
        /// <param name="dateBegin"></param>
        /// <param name="mileage"></param>
        /// <param name="fuel"></param>
        public Bus(string licenseNumber, DateTime dateBegin, float mileage = 0, float fuel = 0)
        
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
        /// <summary>
        ///defalt constructor
        /// </summary>
        public Bus()
        
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
        /// <summary>
        /// A function that edit the license number to the format XX-XXX-XX / XXX-XX-XX
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string VisualShapeOfLicenseNumber(string value)
        
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
        /// <summary>
        /// A function that cheak if the string is only numbers
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool IsDigitsOnly(string str)
        
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
        /// <summary>
        ///A function that cheak if passed year from the date begin
        /// </summary>
        /// <returns></returns>
        public bool YearPassed()
        
        {
            DateTime zeroTime = new DateTime(1, 1, 1);

            var diff = DateTime.Now - LastCare;
            int years = (zeroTime + diff).Year - 1;
            if (years >= 1)
            {
                return true;

            }
            return false;
        }
        #endregion

        #region tostring
        public override string ToString()
        {
            return string.Format("The bus license number is:" + licenseNumber+ "\t DateBegin:" + DateBegin+ "\t the sum mileage is:"+ SumMileage);
        }
        #endregion

        #region go to travel
        /// <summary>
        /// A func that check if the bus can go to travel according to his status and if the status is ready the func update the fildes.
        /// </summary>
        /// <param name="km"></param>
        public void goToTravel(float km)
        {
            if (Status == BusStatus.Ready)
            {
                currentMileage = km;
                sumMileage += currentMileage;
                kmBeforeFuel -= km;
                kmBeforCare -= km;
            }
        }
        #endregion

        #region refueling
        /// <summary>
        /// A func for refuel the bus
        /// </summary>
        public void refueling()
        {
            kmBeforeFuel = 1200;
            Status = BusStatus.Refueling;

        }
        #endregion

        #region care
        /// <summary>
        /// A func for care the bus
        /// </summary>
        public void care()
        {
            DateTime currentDate = DateTime.Now;
            LastCare = currentDate;
            KmBeforCare = 20000;
            kmBeforeFuel = 1200;
            Status = BusStatus.Careing;
        }
        #endregion

    }
}



