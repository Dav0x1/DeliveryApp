using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    public enum ComplaintStatus
    {
        ACTIVE,
        PROCESS,
        RESOLVED
    }

    public class Complaint
    {
        public int id { get; set; }
        public string Description { get; set; }
        public DateTime SubmissionDate { get; set; }
        public ComplaintStatus Status { get; set; }

        public int DeliveryId { get; set; }
        public virtual Delivery Delivery { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}