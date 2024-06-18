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
        public AuthorizationService AuthorizationService
        {
            get => _authorizationService;
            set
            {
                if (_authorizationService != value)
                {
                    _authorizationService = value;
                    OnPropertyChange(nameof(AuthorizationService));
                }
            }
        }

        private readonly ScreenManagerService _screenManagerService;
        public ScreenManagerService ScreenManagerService
        {
            get => _screenManagerService;
        }

        private bool _isLogged;
        // Command changing language
        public ICommand ChangeLanguageCommand { get; }

        public MainWindowViewModel(AuthorizationService authorizationService,DeliveryService deliveryService, RoleSerivce roleService, ScreenManagerService screenManagerService)
        {
            _authorizationService = authorizationService;
            _screenManagerService = screenManagerService;
            _authorizationService.LoginStatusChanged += OnLoginStatusChanged;
            _isLogged = _authorizationService.isLoggedStatus();
            // Change language command initialization
            ChangeLanguageCommand = new BaseCommand(param => SetLang((string)param));
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
                ScreenManagerService.ShowDeliveriesViewCommand.Execute(null);
            }
            else
            {
                ScreenManagerService.ShowLoginViewCommand.Execute(null);
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