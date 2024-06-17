using DeliveryApp.Models;
using DeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.ViewModels
{
	public class DeliveryViewModel : ViewModelBase
	{
		private Delivery _delivery;
		private DeliveryStatus _currentStatus;
		private DeliveryService _deliveryService;

		public DeliveryViewModel(Delivery delivery, DeliveryService deliveryService)
		{
			_delivery = delivery;
			_deliveryService = deliveryService;
			_currentStatus = _delivery.CurrentStatus;
		}

		public Delivery Delivery => _delivery;

		public DeliveryStatus CurrentStatus
		{
			get => _currentStatus;
			set
			{
				_currentStatus = value;
				OnPropertyChange(nameof(CurrentStatus));
				_deliveryService.UpdateDeliveryStatus(_delivery.Id, _currentStatus);
				_delivery.CurrentStatus = _currentStatus;
			}
		}

		public List<DeliveryStatus> AvailableStatuses => Enum.GetValues(typeof(DeliveryStatus)).Cast<DeliveryStatus>().ToList();
	}

}
