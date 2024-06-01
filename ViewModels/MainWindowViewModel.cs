﻿using DeliveryApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DeliveryApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
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

        public ICommand ShowLoginViewCommand { get; }
        public ICommand ShowRegisterViewCommand { get; }
        public ICommand ShowDeliveriesViewCommand { get; }
        public ICommand ShowRolesViewCommand { get; }
        public ICommand ShowUsersViewCommand { get; }

        public MainWindowViewModel()
        {
            ShowLoginViewCommand = new BaseCommand(o => CurrentViewModel = new LoginViewModel());
            ShowRegisterViewCommand = new BaseCommand(o => CurrentViewModel = new RegisterViewModel());
            ShowDeliveriesViewCommand = new BaseCommand(o => CurrentViewModel = new DeliveryListingViewModel());
            ShowRolesViewCommand = new BaseCommand(o => CurrentViewModel = new RoleListingViewModel());
            ShowUsersViewCommand = new BaseCommand(o => CurrentViewModel = new UserListingViewModel());

            // Ustaw początkowy widok
            CurrentViewModel = new LoginViewModel();
        }
    }
}