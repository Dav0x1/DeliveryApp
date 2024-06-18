using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DeliveryApp.Helpers;
using DeliveryApp.Commands;
using DeliveryApp.Services;
using System.Windows;
using DeliveryApp.ViewModels;

namespace DeliveryApp.Services
{
    public class AuthorizationService : ViewModelBase
    {
        private readonly DataService _dataService;

        public event EventHandler LoginStatusChanged;
        private bool isLogged = false;
        private User? _loggedUser = null;

        public User? LoggedUser
        {
            get => _loggedUser;
            set
            {
                if (_loggedUser != value)
                {
                    _loggedUser = value;
                    OnPropertyChange(nameof(LoggedUser));
                }
            }
        }

        public AuthorizationService(DataService dataService)
        {
            _dataService = dataService;
        }

        public string login(User user)
        {
            var users = _dataService.GetUsers();

            foreach (var u in users)
            {
                if ((u.Username == user.Username) && (u.Password == user.Password))
                {
                    LoggedUser = u;
                    isLogged = true;
                    break;
                }
            }
            if (isLogged)
            {
                OnLoginStatusChanged();
                return String.Empty;
            }
			return Application.Current.Resources["IncorrectLoginDetails"].ToString();
		}

        public string register(User user)
        {
			if (user.Username != null && !ValidateUsername(user.Username))
				return Application.Current.Resources["UsernameDoesntMeetRequirements"].ToString();

			if (user.Password != null && !ValidatePassword(user.Password))
				return Application.Current.Resources["PasswordDoesntMeetRequirements"].ToString();

			if (!_dataService.addUser(user))
			{
				return Application.Current.Resources["UserAlreadyExistsOrError"].ToString();
			}

			return Application.Current.Resources["UserAddedSuccessfully"].ToString();
		}

        public void logout()
        {
            isLogged = false;
            LoggedUser = null;
            _dataService.saveChange();
            OnLoginStatusChanged();
        }

        private bool ValidateUsername(string username)
        {
            // NEED TO BE REMOVE
            return true;

            var usernamePattern = @"^[a-zA-Z0-9_]{5,20}$";
            return Regex.IsMatch(username, usernamePattern);
        }

        private bool ValidatePassword(string password)
        {
            // NEED TO BE REMOVE
            return true;

            if (password.Length < 8)
            {
                return false;
            }

            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasLowerCase = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }

        public bool isLoggedStatus()
        {
            return isLogged;
        }

        protected virtual void OnLoginStatusChanged()
        {
            LoginStatusChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}