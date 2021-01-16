using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BO;
using BLApi;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace PlGui
{
    public class IsEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
          object parameter, CultureInfo culture)
        {
            BO.User u = value as BO.User;
            if (u.Admin)
            {
                return Visibility.Visible;
            }
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType,
        object parameter, CultureInfo culture)
        {
            // Do the Logic
            throw new NotImplementedException();
        }
    }
}
