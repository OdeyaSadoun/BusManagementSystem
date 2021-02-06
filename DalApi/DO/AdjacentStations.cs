using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DO
{
    public class AdjacentStations
    {
        #region CodeStation1 - uniqe number1
        /// <summary>
        /// Station ID of station number 1
        /// </summary>
        public int CodeStation1 { get; set; } //uniqe number1
        #endregion

        #region CodeStation2 - uniqe number2
        /// <summary>
        /// Station ID of station number 2
        /// </summary>
        public int CodeStation2 { get; set; }//uniqe number2
        #endregion

        #region Distance
        /// <summary>
        /// The distance between 2 stations
        /// </summary>
        public float  Distance { get; set; }
        #endregion

        #region TravelTime
        /// <summary>
        /// Average travel time between the 2 stations
        /// </summary>
        /// 
        private TimeSpan travelTime;
        [XmlIgnore]
        public TimeSpan TravelTime
        {
            get { return travelTime; }
            set { travelTime = value; }
        }
        [XmlElement("TravelTime", DataType="duration")]
        [DefaultValue("PT10M")]
        public string XmlTime
        {
            get { return XmlConvert.ToString(travelTime); }
            set { travelTime = XmlConvert.ToTimeSpan(value); }
        }


        #endregion

        #region IsDeleted
        /// <summary>
        /// Does this bus exist in the system or is it deleted from it
        /// </summary>
        public bool IsDeleted { get; set; }
        #endregion

        /*********************/

        #region ToString
        /// <summary>
        /// A to string function
        /// </summary>
        /// <returns></returns>
        public override string ToString() => this.ToStringProperty();
        #endregion
    }
}
