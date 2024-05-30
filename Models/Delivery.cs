using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public string SenderAddress { get; set; }
        public string ReceiverAddress { get; set; }
        public string DeliveryType { get; set; }
        public float Weight { get; set; }
        public float Width { get; set; }
        public float Lenght { get; set; }
        public float Height { get; set; }
        public DateTime RegistrationDate { get; set; }

        public DeliveryStatusUpdate CurrentStatus { get; set; }
        public ICollection<DeliveryStatusUpdate> StatusHistory { get; set; } = new List<DeliveryStatusUpdate>();
    }
}
