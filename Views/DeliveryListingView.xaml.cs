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
				if (listBox.DataContext is DeliveryViewModel deliveryViewModel)
				{
					deliveryViewModel.CurrentStatus = selectedStatus;
				}

				if (listBox.Parent is Border border && border.Parent is Popup popup)
				{
					popup.IsOpen = false;
				}
			}
		}

		private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sender is ListView listView && listView.SelectedItem is DeliveryViewModel selectedDelivery)
			{
				// No need to do anything here as each item already has its own DataContext
			}
		}

	}
}
