using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    public class Privilege
    {
        public int PrivilegeId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
