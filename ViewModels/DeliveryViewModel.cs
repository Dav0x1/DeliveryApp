using DeliveryApp.Models;
using DeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace DeliveryApp.ViewModels
{
	public class DeliveryViewModel : ViewModelBase
	{
		private Delivery _delivery;
		private DeliveryStatus _currentStatus;
		private DeliveryService _deliveryService;

		public RelayCommand DeleteDeliveryCommand { get; }


		public DeliveryViewModel(Delivery delivery, DeliveryService deliveryService)
		{
			_delivery = delivery;
			_deliveryService = deliveryService;
			_currentStatus = _delivery.CurrentStatus;
			DeleteDeliveryCommand = new RelayCommand(DeleteDelivery);
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

		private void DeleteDelivery()
		{
			string message = Application.Current.Resources["DeleteConfirmationMessage"].ToString();
			string title = Application.Current.Resources["DeleteConfirmationTitle"].ToString();

			MessageBoxResult result = MessageBox.Show(
				message,
				title,
				MessageBoxButton.YesNo,
				MessageBoxImage.Warning);

			if (result == MessageBoxResult.Yes)
			{
				_deliveryService.remove(_delivery.Id);
				OnDeliveryDeleted?.Invoke(this, EventArgs.Empty);
			}
		}

		public event EventHandler OnDeliveryDeleted;
	}

}
