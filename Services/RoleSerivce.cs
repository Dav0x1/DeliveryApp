using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Services
{
    public class RoleSerivce
    {
        DataService _dataService;
        public RoleSerivce(DataService dataService) { 
            _dataService = dataService;
        }

        public List<Role> GetRoles()
        {
            return _dataService.GetRoles();
        }

        public List<Privilege> GetPrivileges()
        {
            return _dataService.GetPrivileges();
        }

        public void addRole(Role role)
        {
            _dataService.addRole(role);
        }

        public Privilege GetPrivilegeByName(string name)
        {
            var privileges = _dataService.GetPrivileges();
            return privileges.FirstOrDefault(x => x.Name == name);
        }
    }
}
