using DeliveryApp.Commands;
using DeliveryApp.Models;
using DeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DeliveryApp.ViewModels
{
    public class RoleListingViewModel : ViewModelBase
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

        private readonly RoleSerivce _roleSerivce;
        private readonly ScreenManagerService _screenManagerService;
        public ICommand AddRoleViewCommand { get; }

        private ObservableCollection<Role> _roles;
        public ObservableCollection<Role> Roles
        {
            get { return _roles; }
            set
            {
                _roles = value;
                OnPropertyChange(nameof(Roles));
            }
        }

        public RoleListingViewModel(RoleSerivce roleService, ScreenManagerService screenManagerService,AuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            _roleSerivce = roleService;
            _screenManagerService = screenManagerService;

            _roles = new ObservableCollection<Role>(_roleSerivce.GetRoles());

            AddRoleViewCommand = new BaseCommand(AddRoleView);
        }

        private void AddRoleView(object parameter)
        {
            _screenManagerService.AddRoleViewCommand.Execute(null);
        }
    }
}
