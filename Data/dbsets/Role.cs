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
    }
}
