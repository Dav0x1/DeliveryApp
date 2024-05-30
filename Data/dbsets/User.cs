using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

        public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();

        public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
    }
}