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

		public DeliveryStatus GetCurrentStatus(int deliveryId)
		{
			var delivery = _dataService.GetDeliveries().FirstOrDefault(d => d.Id == deliveryId);
			if (delivery != null && delivery.StatusHistory.Any())
			{
				return delivery.StatusHistory.OrderByDescending(s => s.UpdateDate).First().DeliveryStatus;
			}
			return DeliveryStatus.Accepted;
		}

		public bool UpdateDeliveryStatus(int deliveryId, DeliveryStatus newStatus)
		{
			var delivery = _dataService.GetDeliveries().FirstOrDefault(d => d.Id == deliveryId);
			if (delivery != null)
			{
				var newStatusUpdate = new DeliveryStatusUpdate
				{
					UpdateDate = DateTime.Now,
					DeliveryStatus = newStatus
				};

				delivery.StatusHistory.Add(newStatusUpdate);

				try
				{
					_dataService.saveChange(); 

					return true;
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Błąd podczas zapisywania zmian: {ex.Message}");
					return false; 
				}
			}
			return false;
		}


	}
}