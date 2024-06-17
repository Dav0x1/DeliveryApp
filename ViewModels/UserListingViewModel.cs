using DeliveryApp.Models;
using DeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.ViewModels
{
    public class UserListingViewModel : ViewModelBase
    {
        private readonly DataService _dataService;

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChange(nameof(Users));
            }
        }

        public UserListingViewModel(DataService dataService) {
            _dataService = dataService;

            _users = new ObservableCollection<User>(_dataService.GetUsers());
        }

        
    }
}
