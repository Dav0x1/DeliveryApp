using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    public class Report
    {
        public int Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
        
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}