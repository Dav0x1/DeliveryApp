using DeliveryApp.Commands;
using DeliveryApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DeliveryApp.Services
{
    public class ScreenManagerService : INotifyPropertyChanged
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        // Commands for menu options
        public ICommand ShowLoginViewCommand { get; }
        public ICommand ShowRegisterViewCommand { get; }
        public ICommand ShowRegisterDeliveryViewCommand { get; }
        public ICommand ShowDeliveriesViewCommand { get; }
        public ICommand ShowRolesViewCommand { get; }
        public ICommand AddRoleViewCommand { get; }
        public ICommand ShowUsersViewCommand { get; }
        public ICommand LogoutCommand { get; }

        public ScreenManagerService(AuthorizationService authorizationService, DeliveryService deliveryService, RoleSerivce roleService)
        {
            // Commands initialization for menu options
            ShowLoginViewCommand = new BaseCommand(o => CurrentViewModel = new LoginViewModel(authorizationService));
            ShowRegisterViewCommand = new BaseCommand(o => CurrentViewModel = new RegisterViewModel(authorizationService));
            ShowRegisterDeliveryViewCommand = new BaseCommand(o => CurrentViewModel = new RegisterDeliveryViewModel(deliveryService));
            ShowDeliveriesViewCommand = new BaseCommand(o => CurrentViewModel = new DeliveryListingViewModel());
            ShowRolesViewCommand = new BaseCommand(o => CurrentViewModel = new RoleListingViewModel(roleService,this));
            AddRoleViewCommand = new BaseCommand(o => CurrentViewModel = new AddRoleViewModel(roleService));
            ShowUsersViewCommand = new BaseCommand(o => CurrentViewModel = new UserListingViewModel());
            LogoutCommand = new BaseCommand(o => { ShowLoginViewCommand.Execute(null); authorizationService.logout(); });

            // Set initial view model
            CurrentViewModel = new LoginViewModel(authorizationService);
        }

        // Implement INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}