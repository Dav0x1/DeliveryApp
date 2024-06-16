using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    public class Role
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Privilege> Privileges { get; set; } = new List<Privilege>();
        public virtual ICollection<User> Users { get; set; } = new List<User>();

        public bool CanDisplayDeliveries => Privileges.Any(p => p.Name == "CanDisplayDeliveries");
        public bool CanRegisterDelivery => Privileges.Any(p => p.Name == "CanRegisterDelivery");
        public bool CanChangeDeliveryStatus => Privileges.Any(p => p.Name == "CanChangeDeliveryStatus");
        public bool CanDisplayRoles => Privileges.Any(p => p.Name == "CanDisplayRoles");
        public bool CanAddRole => Privileges.Any(p => p.Name == "CanAddRole");
        public bool CanDisplayUsers => Privileges.Any(p => p.Name == "CanDisplayUsers");
        public bool CanModifyUser => Privileges.Any(p => p.Name == "CanModifyUser");
    }
}
