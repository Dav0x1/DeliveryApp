using DeliveryApp.Commands;
using DeliveryApp.Models;
using DeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DeliveryApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly AuthorizationService _authorizationService;

        public ICommand loginBtnClick { get; }

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

        public LoginViewModel(AuthorizationService authorizationSerivce) {
            _authorizationService = authorizationSerivce;

            loginBtnClick = new BaseCommand(param => login());
        }

        private void login()
        {
            User userModel = new User()
            {
                Username = _username,
                Password = _password
            };

            string info = _authorizationService.login(userModel);

            if (info != String.Empty)
            {
                MessageBox.Show(info, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }
    }
}
