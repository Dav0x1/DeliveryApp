using DeliveryApp.Commands;
using DeliveryApp.Models;
using DeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace DeliveryApp.ViewModels
{
    public class RegisterDeliveryViewModel : ViewModelBase
    {
		private readonly DeliveryService _deliveryService;

		public ICommand RegisterDeliveryCommand { get; }

		private string _senderAddress;
		public string SenderAddress
		{
			get { return _senderAddress; }
			set { _senderAddress = value; OnPropertyChange(nameof(SenderAddress)); }
		}

		private string _receiverAddress;
		public string ReceiverAddress
		{
			get { return _receiverAddress; }
			set { _receiverAddress = value; OnPropertyChange(nameof(ReceiverAddress)); }
		}

		private string _deliveryType;
		public string DeliveryType
		{
			get { return _deliveryType; }
			set { _deliveryType = value; OnPropertyChange(nameof(DeliveryType)); }
		}

		private float _weight;
		public float Weight
		{
			get { return _weight; }
			set { _weight = value; OnPropertyChange(nameof(Weight)); }
		}

		private float _width;
		public float Width
		{
			get { return _width; }
			set { _width = value; OnPropertyChange(nameof(Width)); }
		}

		private float _length;
		public float Length
		{
			get { return _length; }
			set { _length = value; OnPropertyChange(nameof(Length)); }
		}

		private float _height;
		public float Height
		{
			get { return _height; }
			set { _height = value; OnPropertyChange(nameof(Height)); }
		}

		public RegisterDeliveryViewModel(DeliveryService deliveryService)
		{
			_deliveryService = deliveryService;

			RegisterDeliveryCommand = new BaseCommand(param => RegisterDelivery());
		}

		private void RegisterDelivery()
		{
			Delivery newDelivery = new Delivery
			{
				SenderAddress = _senderAddress,
				ReceiverAddress = _receiverAddress,
				DeliveryType = _deliveryType,
				Weight = _weight,
				Width = _width,
				Length = _length,
				Height = _height,
				RegistrationDate = DateTime.Now
			};

			_deliveryService.add(newDelivery);

			MessageBox.Show("Delivery registered successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

			// Clear form fields
			SenderAddress = string.Empty;
			ReceiverAddress = string.Empty;
			DeliveryType = string.Empty;
			Weight = 0;
			Width = 0;
			Length = 0;
			Height = 0;

		}
	}
}