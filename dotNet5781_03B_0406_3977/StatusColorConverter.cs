using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;

namespace dotNet5781_03B_0406_3977
{
    public class StatusColorConverter : IValueConverter
    {
        #region  Convert
        /// <summary>
        /// In order to draw the rows according to the status
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((BusStatus)value == BusStatus.Ready)
            {              
                return "Ready";
            }
            else                
                return "Else";

        }
        #endregion

        #region  ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}