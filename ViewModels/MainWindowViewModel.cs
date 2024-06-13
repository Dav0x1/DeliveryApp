using DeliveryApp.Commands;
using DeliveryApp.Models;
using DeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DeliveryApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private AuthorizationService _authorizationService;
        private bool _isLogged;
        // Command changing language
        public ICommand ChangeLanguageCommand { get; }
        // Commands for menu options
        public ICommand ShowLoginViewCommand { get; }
        public ICommand ShowRegisterViewCommand { get; }
        public ICommand ShowDeliveriesViewCommand { get; }
        public ICommand ShowRolesViewCommand { get; }
        public ICommand ShowUsersViewCommand { get; }
        public ICommand LogoutCommand { get; }

        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChange(nameof(CurrentViewModel));
            }
        }

        public MainWindowViewModel(AuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            _authorizationService.LoginStatusChanged += OnLoginStatusChanged;
            _isLogged = _authorizationService.isLoggedStatus();
            // Commands initialization for menu options
            ShowLoginViewCommand = new BaseCommand(o => CurrentViewModel = new LoginViewModel(authorizationService));
            ShowRegisterViewCommand = new BaseCommand(o => CurrentViewModel = new RegisterViewModel(authorizationService));
            ShowDeliveriesViewCommand = new BaseCommand(o => CurrentViewModel = new DeliveryListingViewModel());
            ShowRolesViewCommand = new BaseCommand(o => CurrentViewModel = new RoleListingViewModel());
            ShowUsersViewCommand = new BaseCommand(o => CurrentViewModel = new UserListingViewModel());
            LogoutCommand = new BaseCommand(o => _authorizationService.logout());
            // Change language command initialization
            ChangeLanguageCommand = new BaseCommand(param => SetLang((string)param));

            CurrentViewModel = new LoginViewModel(authorizationService);
        }

        public bool IsLogged
        {
            get => _isLogged;
            set
            {
                _isLogged = value;
                OnPropertyChange(nameof(IsLogged));
            }
        }

        private void OnLoginStatusChanged(object sender, EventArgs e)
        {
            IsLogged = _authorizationService.isLoggedStatus();
            if (IsLogged)
            {
                ShowDeliveriesViewCommand.Execute(null);
            }
            else
            {
                ShowLoginViewCommand.Execute(null);
            }
        }

        // Function changing language
        private void SetLang(string lang)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);

            var dictionaries = Application.Current.Resources.MergedDictionaries;
            var langDictionary = dictionaries.FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("/String/string."));

            if (langDictionary != null)
            {
                dictionaries.Remove(langDictionary);
            }

            var newLangDictionary = new ResourceDictionary()
            {
                Source = new Uri($"Resources/String/string.{lang}.xaml", UriKind.Relative)
            };
            dictionaries.Add(newLangDictionary);
        }
    }
}