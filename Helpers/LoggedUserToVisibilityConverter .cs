using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace DeliveryApp.Helpers
{
    public class LoggedUserToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is User user && user.Role != null)
            {
                string privilegeName = parameter as string;

                switch (privilegeName)
                {
                    case "CanDisplayDeliveries":
                        return user.Role.CanDisplayDeliveries ? Visibility.Visible : Visibility.Collapsed;
                    case "CanRegisterDelivery":
                        return user.Role.CanRegisterDelivery ? Visibility.Visible : Visibility.Collapsed;
                    case "CanChangeDeliveryStatus":
                        return user.Role.CanChangeDeliveryStatus ? Visibility.Visible : Visibility.Collapsed;
                    case "CanDisplayRoles":
                        return user.Role.CanDisplayRoles ? Visibility.Visible : Visibility.Collapsed;
                    case "CanAddRole":
                        return user.Role.CanAddRole ? Visibility.Visible : Visibility.Collapsed;
                    case "CanDisplayUsers":
                        return user.Role.CanDisplayUsers ? Visibility.Visible : Visibility.Collapsed;
                    case "CanModifyUser":
                        return user.Role.CanModifyUser ? Visibility.Visible : Visibility.Collapsed;
                    default:
                        return Visibility.Collapsed; // Domyślnie ukryj, jeśli nieznane uprawnienie
                }
            }

            return Visibility.Collapsed; // Domyślnie ukryj, jeśli brak użytkownika lub roli
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
