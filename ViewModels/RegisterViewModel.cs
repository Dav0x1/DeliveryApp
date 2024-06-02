using DeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DeliveryApp.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly DataService _dataService;
        public RegisterViewModel(DataService dataSerice) 
        {
            _dataService = dataSerice;

            //Testowe dodanie uzytkownika
            _dataService.addUser(new Models.User() { Username = "Test", Password = "Test", Role = null});

            var users = _dataService.GetUsers();
            Console.WriteLine(users.ToString());
        }
    }
}
