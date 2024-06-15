using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Services
{
    public class DeliveryService
    {
        private readonly DataService _dataService;

        public DeliveryService(DataService dataService)
        {
            _dataService = dataService;
        }

        public void add(Delivery delivery)
        {
            DeliveryStatusUpdate deliveryStatusUpdate = new DeliveryStatusUpdate();
            deliveryStatusUpdate.UpdateDate = DateTime.Now;
            deliveryStatusUpdate.DeliveryStatus = DeliveryStatus.Accepted;

            delivery.RegistrationDate = DateTime.Now;
            delivery.StatusHistory.Add(deliveryStatusUpdate);

            _dataService.addDelivery(delivery);
        }

        public void remove(int id)
        {
            var delivery = _dataService.GetDeliveries().FirstOrDefault(e=>e.Id == id);

            if (delivery != null)
            {
                _dataService.removeDelivery(delivery);
            }
        }

        public List<Delivery> get()
        {
            return _dataService.GetDeliveries();
        }
    }
}