using DeliveryApp.Commands;
using DeliveryApp.Models;
using DeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace DeliveryApp.ViewModels
{
	public class RegisterDeliveryViewModel : ViewModelBase, IDataErrorInfo
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
			if (HasErrors || !AllFieldsFilled)
			{
				string errorMessage = Application.Current.Resources["FormErrorMessage"].ToString();
				string errorTitle = Application.Current.Resources["ErrorTitle"].ToString();

				MessageBox.Show(errorMessage, errorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

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

			string successMessage = Application.Current.Resources["DeliveryRegisteredMessage"].ToString();
			string infoTitle = Application.Current.Resources["InformationTitle"].ToString();

			MessageBox.Show(successMessage, infoTitle, MessageBoxButton.OK, MessageBoxImage.Information);

			// Clear form fields
			SenderAddress = string.Empty;
			ReceiverAddress = string.Empty;
			DeliveryType = string.Empty;
			Weight = 0;
			Width = 0;
			Length = 0;
			Height = 0;
		}

		public string Error => null;

		public bool HasErrors
		{
			get
			{
				return typeof(RegisterDeliveryViewModel)
					.GetProperties()
					.Any(prop => this[prop.Name] != null);
			}
		}
		public bool AllFieldsFilled
		{
			get
			{
				return !string.IsNullOrWhiteSpace(SenderAddress) &&
					   !string.IsNullOrWhiteSpace(ReceiverAddress) &&
					   !string.IsNullOrWhiteSpace(DeliveryType) &&
					   Weight > 0 &&
					   Width > 0 &&
					   Length > 0 &&
					   Height > 0;
			}
		}
		public string this[string columnName]
		{
			get
			{
				string result = null;
				switch (columnName)
				{
					case nameof(SenderAddress):
						if (string.IsNullOrWhiteSpace(SenderAddress))
						{
							result = $"{nameof(SenderAddress)} cannot be empty.";
						}
						break;
					case nameof(ReceiverAddress):
						if (string.IsNullOrWhiteSpace(ReceiverAddress))
						{
							result = $"{nameof(ReceiverAddress)} cannot be empty.";
						}
						break;
					case nameof(DeliveryType):
						if (string.IsNullOrWhiteSpace(DeliveryType))
						{
							result = $"{nameof(DeliveryType)} cannot be empty.";
						}
						break;
					case nameof(Weight):
						if (Weight <= 0)
						{
							result = $"{nameof(Weight)} must be greater than zero.";
						}
						break;
					case nameof(Width):
						if (Width <= 0)
						{
							result = $"{nameof(Width)} must be greater than zero.";
						}
						break;
					case nameof(Length):
						if (Length <= 0)
						{
							result = $"{nameof(Length)} must be greater than zero.";
						}
						break;
					case nameof(Height):
						if (Height <= 0)
						{
							result = $"{nameof(Height)} must be greater than zero.";
						}
						break;
				}
				return result;
			}

		}
	}
}