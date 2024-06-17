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
    public class AddRoleViewModel : ViewModelBase
    {
        private readonly RoleSerivce _roleService;
        private readonly ScreenManagerService _screenManagerService;

        public ICommand AddRoleCommand { get; }
        public ICommand ReturnCommand { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChange(nameof(Name));
            }
        }

        private bool _canDisplayDeliveries;
        public bool CanDisplayDeliveries
        {
            get => _canDisplayDeliveries;
            set
            {
                _canDisplayDeliveries = value;
                OnPropertyChange(nameof(CanDisplayDeliveries));
            }
        }

        private bool _canRegisterDelivery;
        public bool CanRegisterDelivery
        {
            get => _canRegisterDelivery;
            set
            {
                _canRegisterDelivery = value;
                OnPropertyChange(nameof(CanRegisterDelivery));
            }
        }

        private bool _canChangeDeliveryStatus;
        public bool CanChangeDeliveryStatus
        {
            get => _canChangeDeliveryStatus;
            set
            {
                _canChangeDeliveryStatus = value;
                OnPropertyChange(nameof(CanChangeDeliveryStatus));
            }
        }

        private bool _canDisplayRoles;
        public bool CanDisplayRoles
        {
            get => _canDisplayRoles;
            set
            {
                _canDisplayRoles = value;
                OnPropertyChange(nameof(CanDisplayRoles));
            }
        }

        private bool _canAddRole;
        public bool CanAddRole
        {
            get => _canAddRole;
            set
            {
                _canAddRole = value;
                OnPropertyChange(nameof(CanAddRole));
            }
        }

        private bool _canDisplayUsers;
        public bool CanDisplayUsers
        {
            get => _canDisplayUsers;
            set
            {
                _canDisplayUsers = value;
                OnPropertyChange(nameof(CanDisplayUsers));
            }
        }

        private bool _canModifyUser;
        public bool CanModifyUser
        {
            get => _canModifyUser;
            set
            {
                _canModifyUser = value;
                OnPropertyChange(nameof(CanModifyUser));
            }
        }

        public AddRoleViewModel(RoleSerivce roleService,ScreenManagerService screenManagerService)
        {
            _roleService = roleService;
            _screenManagerService = screenManagerService;

            AddRoleCommand = new BaseCommand(AddRole);
            ReturnCommand = new BaseCommand(Return);
        }

        private void Return(object parameter)
        {
            _screenManagerService.ShowRolesViewCommand.Execute(null);
        }

        private void AddRole(object parameter)
        {
            Role newRole = new Role
            {
                Name = _name,
                Privileges = new List<Privilege>()
            };

            if (CanDisplayDeliveries) newRole.Privileges.Add(_roleService.GetPrivilegeByName("CanDisplayDeliveries"));
            if (CanRegisterDelivery) newRole.Privileges.Add(_roleService.GetPrivilegeByName("CanRegisterDelivery"));
            if (CanChangeDeliveryStatus) newRole.Privileges.Add(_roleService.GetPrivilegeByName("CanChangeDeliveryStatus"));
            if (CanDisplayRoles) newRole.Privileges.Add(_roleService.GetPrivilegeByName("CanDisplayRoles"));
            if (CanAddRole) newRole.Privileges.Add(_roleService.GetPrivilegeByName("CanAddRole"));
            if (CanDisplayUsers) newRole.Privileges.Add(_roleService.GetPrivilegeByName("CanDisplayUsers"));
            if (CanModifyUser) newRole.Privileges.Add(_roleService.GetPrivilegeByName("CanModifyUser"));

            _roleService.addRole(newRole);

            MessageBox.Show("Role added successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            // Clear form fields
            Name = string.Empty;
            CanDisplayDeliveries = false;
            CanRegisterDelivery = false;
            CanChangeDeliveryStatus = false;
            CanDisplayRoles = false;
            CanAddRole = false;
            CanDisplayUsers = false;
            CanModifyUser = false;
        }
    }
}