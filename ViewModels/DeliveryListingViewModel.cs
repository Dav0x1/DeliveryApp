using DeliveryApp.Models;
using DeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace DeliveryApp.ViewModels
{
    public class DeliveryListingViewModel : ViewModelBase
    {
		private readonly DeliveryService _deliveryService;

		public ObservableCollection<Delivery> Deliveries { get; set; }
		public List<DeliveryStatus> AvailableStatuses { get; set; }

		public ICommand ChangeStatusCommand { get; }

		private Delivery _selectedDelivery;
		public Delivery SelectedDelivery
		{
			get { return _selectedDelivery; }
			set
			{
				_selectedDelivery = value;
				OnPropertyChange(nameof(SelectedDelivery));

				// Aktualizacja SelectedStatus po zmianie SelectedDelivery
				if (SelectedDelivery != null)
					SelectedStatus = SelectedDelivery.CurrentStatus;
			}
		}
		private DeliveryStatus _selectedStatus;
		public DeliveryStatus SelectedStatus
		{
			get { return _selectedStatus; }
			set
			{
				_selectedStatus = value;
				OnPropertyChange(nameof(SelectedStatus));
			}
		}

		public DeliveryListingViewModel(DeliveryService deliveryService)
		{
			_deliveryService = deliveryService;
			LoadDeliveries();
			LoadAvailableStatuses();

			ChangeStatusCommand = new RelayCommand(ChangeStatus, CanChangeStatus);
		}

		private void LoadDeliveries()
		{
			List<Delivery> deliveries = _deliveryService.get();
			Deliveries = new ObservableCollection<Delivery>(deliveries);
			foreach (var delivery in Deliveries)
			{
				delivery.CurrentStatus = _deliveryService.GetCurrentStatus(delivery.Id);
			}
			SelectedDelivery = Deliveries.FirstOrDefault();
		}

		private void LoadAvailableStatuses()
		{
			AvailableStatuses = Enum.GetValues(typeof(DeliveryStatus))
									.Cast<DeliveryStatus>()
									.ToList();
		}

		public void ChangeStatus()
		{
			if (SelectedDelivery == null || SelectedStatus == null)
				return;

			DeliveryStatusUpdate statusUpdate = new DeliveryStatusUpdate
			{
				UpdateDate = DateTime.Now,
				DeliveryStatus = SelectedStatus,
				DeliveryId = SelectedDelivery.Id
			};

			_deliveryService.UpdateDeliveryStatus(SelectedDelivery.Id, SelectedStatus);

			SelectedDelivery.CurrentStatus = SelectedStatus;
		}


		private bool CanChangeStatus()
		{
			return SelectedDelivery != null;
		}

	}
}
