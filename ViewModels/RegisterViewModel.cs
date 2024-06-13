using DeliveryApp.Commands;
using DeliveryApp.Models;
using DeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DeliveryApp.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly AuthorizationService _authorizationService;

        public ICommand registerBtnClick { get; }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChange(nameof(Username)); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChange(nameof(Password)); }
        }
        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; OnPropertyChange(nameof(ConfirmPassword)); }
        }

        public RegisterViewModel(AuthorizationService authorizationService) 
        {
            _authorizationService = authorizationService;

            registerBtnClick = new BaseCommand(param => register());
        }

        private void register()
        {
            User userModel = new User()
            {
                Username = _username,
                Password = _password
            };

            // Sprawdzenie czy hasła zgodne
            if (_password != _confirmPassword)
            {
                MessageBox.Show("Passwords do not match!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string info = _authorizationService.register(userModel);

            MessageBox.Show(info, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
