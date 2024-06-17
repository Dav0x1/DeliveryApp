using DeliveryApp.Models;
using DeliveryApp.Services;
using DeliveryApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeliveryApp.Views
{
    /// <summary>
    /// Interaction logic for DeliveryListingView.xaml
    /// </summary>
    public partial class DeliveryListingView : UserControl
    {
		private Popup StatusPopup;


		public DeliveryListingView()
        {
            InitializeComponent();
		}
	
		private void StatusListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sender is ListBox listBox && listBox.SelectedItem is DeliveryStatus selectedStatus)
			{
				if (DataContext is ViewModels.DeliveryListingViewModel viewModel && viewModel.SelectedStatus != null)
				{
					viewModel.SelectedStatus = selectedStatus;
					viewModel.ChangeStatusCommand.Execute(null); // zmiany statusu
				}
				if (StatusPopup != null)
				{
					StatusPopup.IsOpen = false; // W teorii powinien się zamknąć po wybraniu statusu
				}
			}
		}

		private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sender is ListView listView && listView.SelectedItem is Delivery selectedDelivery)
			{
				if (DataContext is DeliveryListingViewModel viewModel)
				{
					viewModel.SelectedDelivery = selectedDelivery;
				}
			}
		}

	}
}
