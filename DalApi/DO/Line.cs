﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DO
{
    public class Line
    {
        #region Id - uniqe number
        /// <summary>
        /// Line ID
        /// </summary>
        public int Id { get; set; }//uniqe number
        #endregion

        #region LineNumber
        /// <summary>
        /// Line number
        /// </summary>
        public int LineNumber { get; set; }
        #endregion

        #region Area
        /// <summary>
        /// The area where the line travels
        /// </summary>
        public Area Area { get; set; }
        #endregion

        #region FirstStation
        /// <summary>
        /// The starting point of this line
        /// </summary>
        public int FirstStation { get; set; }
        #endregion

        #region LastStation
        /// <summary>
        /// The destination station of this line
        /// </summary>
        public int LastStation { get; set; }
        #endregion

        #region Fare
        /// <summary>
        /// How much do you have to pay for the trip
        /// </summary>
        public double Fare { get; set; }
        #endregion

        #region TravelTimeInThisLine
        /// <summary>
        /// Estimated travel time of the route
        /// </summary>
        //public TimeSpan TravelTimeInThisLine { get; set; }
        private TimeSpan travelTimeInThisLine;
        [XmlIgnore]
        public TimeSpan TravelTimeInThisLine
        {
            get { return travelTimeInThisLine; }
            set { travelTimeInThisLine = value; }
        }
        [XmlElement("TravelTimeInThisLine", DataType = "duration")]
        [DefaultValue("PT10M")]
        public string XmlTime
        {
            get { return XmlConvert.ToString(travelTimeInThisLine); }
            set { travelTimeInThisLine = XmlConvert.ToTimeSpan(value); }
        }
        #endregion

        #region IsDeleted
        /// <summary>
        /// Does this bus exist in the system or is it deleted from it
        /// </summary>
        public bool IsDeleted { get; set; }
        #endregion

        /*************************/

        #region ToString
        /// <summary>
        /// A to string function
        /// </summary>
        /// <returns></returns>
        public override string ToString() => this.ToStringProperty();
        #endregion
    }

}
