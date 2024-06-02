using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    public enum DeliveryStatus
    {
        Accepted,
        Processing,
        InTransit,
        OutForDelivery,
        DeliveryAttemptFailed,
        Delivered,
        Returned,
        OnHold,
        ReadyForPickup,
        Cancelled
    }

    public class DeliveryStatusUpdate
    {
        public int Id { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }
        [Required]
        public DeliveryStatus DeliveryStatus { get; set; }

        public int DeliveryId { get; set; }
        public virtual Delivery Delivery { get; set; }
    }
}