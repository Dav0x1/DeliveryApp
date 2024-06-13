using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace DeliveryApp.Helpers
{
    public class InverseBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool booleanValue)
            {
                return booleanValue ? Visibility.Collapsed : Visibility.Visible;
            }
            return Visibility.Visible; // Default to Visible if value is not bool
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
