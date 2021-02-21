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
    public class LineTrip
    {
        #region Id - uniqe number
        /// <summary>
        /// The ID of this line trip
        /// </summary>
        public int Id { get; set; }//uniqe number
        #endregion

        #region LineId - uniqe number
        /// <summary>
        /// The ID of the line for this travel
        /// </summary>
        public int LineId { get; set; }//uniqe number
        #endregion

        #region StartAt
        /// <summary>
        /// The time of the begining of this travel line
        /// </summary>
        //public TimeSpan StartAt { get; set; }
        private TimeSpan startAt;
        [XmlIgnore]
        public TimeSpan StartAt
        {
            get { return startAt; }
            set { startAt = value; }
        }
        [XmlElement("StartAt", DataType = "duration")]
        [DefaultValue("PT10M")]
        public string XmlTime
        {
            get { return XmlConvert.ToString(startAt); }
            set { startAt = XmlConvert.ToTimeSpan(value); }
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
