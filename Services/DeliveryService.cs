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
				// Zwracamy najnowszy status (ostatnią aktualizację statusu)
				return delivery.StatusHistory.OrderByDescending(s => s.UpdateDate).First().DeliveryStatus;
			}
			// Jeśli nie znaleziono dostawy lub nie ma historii statusów, możesz zwrócić domyślny status lub null, w zależności od logiki aplikacji.
			return DeliveryStatus.Accepted; // Domyślnie przyjmujemy, że dostawa jest "Accepted", jeśli nie ma innych statusów.
		}

		public bool UpdateDeliveryStatus(int deliveryId, DeliveryStatus newStatus)
		{
			var delivery = _dataService.GetDeliveries().FirstOrDefault(d => d.Id == deliveryId);
			if (delivery != null)
			{
				// Tworzymy nową aktualizację statusu
				var newStatusUpdate = new DeliveryStatusUpdate
				{
					UpdateDate = DateTime.Now,
					DeliveryStatus = newStatus
				};

				// Dodajemy nową aktualizację do historii statusów
				delivery.StatusHistory.Add(newStatusUpdate);

				try
				{
					// Zapisujemy zmiany do bazy danych
					_dataService.saveChange(); // Przyjmujemy, że ta metoda zapisuje zmiany w bazie danych (_dbContext.SaveChanges())

					return true; // Zwracamy true, jeśli zapis się powiódł
				}
				catch (Exception ex)
				{
					// Obsługa błędów zapisu do bazy danych
					Console.WriteLine($"Błąd podczas zapisywania zmian: {ex.Message}");
					return false; // Zwracamy false, jeśli wystąpił błąd
				}
			}
			return false; // Zwracamy false, jeśli nie znaleziono dostawy
		}


	}
}