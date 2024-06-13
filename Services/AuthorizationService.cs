using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DeliveryApp.Helpers;

namespace DeliveryApp.Services
{
    public class AuthorizationService
    {
        private readonly DataService _dataService;

        public event EventHandler LoginStatusChanged;
        private bool isLogged = false;
        private User? loggedUser = null;


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
                    loggedUser = u;
                    isLogged = true;
                    break;
                }
            }
            if (isLogged)
            {
                OnLoginStatusChanged();
                return String.Empty;
            }
            return "Incorrect login details";
        }

        public string register(User user)
        {
            if (user.Username!= null && !ValidateUsername(user.Username))
                return "Username doesn't meet requirements";

            if (user.Password != null && !ValidatePassword(user.Password))
                return "Password doesn't meet requirements";

            if (!_dataService.addUser(user))
            {
                return "User already exists or other error occure";
            }
            return "User has been added";
        }

        public void logout()
        {
            isLogged = false;
            loggedUser = null;
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