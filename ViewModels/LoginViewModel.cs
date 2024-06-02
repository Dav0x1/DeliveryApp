using DeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly DataService _dataService;
        public LoginViewModel(DataService dataSerice) {
            _dataService = dataSerice;


        }
    }
}
