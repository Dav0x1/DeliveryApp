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

		public ObservableCollection<DeliveryViewModel> Deliveries { get; set; }

		public DeliveryListingViewModel(DeliveryService deliveryService)
		{
			_deliveryService = deliveryService;
			LoadDeliveries();
		}

		private void LoadDeliveries()
		{
			List<Delivery> deliveries = _deliveryService.get();
			Deliveries = new ObservableCollection<DeliveryViewModel>(deliveries.Select(d => new DeliveryViewModel(d, _deliveryService)));
		}
	}
}
