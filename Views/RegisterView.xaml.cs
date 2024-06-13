//using DeliveryApp.Services;
using DeliveryApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
		public RegisterView()
		{
			InitializeComponent();
		}
		private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			PasswordTextBox.Text = PasswordBox.Password;
			ConfirmPasswordTextBox.Text = ConfirmPasswordBox.Password;
		}
		private void ShowPasswordCheckBox_Checked(object sender, RoutedEventArgs e)
		{
			PasswordBox.Visibility = Visibility.Collapsed;
			PasswordTextBox.Visibility = Visibility.Visible;
			PasswordTextBox.Text = PasswordBox.Password;

			ConfirmPasswordBox.Visibility = Visibility.Collapsed;
			ConfirmPasswordTextBox.Visibility = Visibility.Visible;
			ConfirmPasswordTextBox.Text = ConfirmPasswordBox.Password;
		}
		private void ShowPasswordCheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			PasswordBox.Visibility = Visibility.Visible;
			PasswordTextBox.Visibility = Visibility.Collapsed;
			PasswordBox.Password = PasswordTextBox.Text;

			ConfirmPasswordBox.Visibility = Visibility.Visible;
			ConfirmPasswordTextBox.Visibility = Visibility.Collapsed;
			ConfirmPasswordBox.Password = ConfirmPasswordTextBox.Text;
		}

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
			if (sender == PasswordBox)
			{
				if (DataContext is RegisterViewModel viewModel)
				{
					viewModel.Password = PasswordBox.Password;
				}
			}
			else if (sender == ConfirmPasswordBox)
			{
                if (DataContext is RegisterViewModel viewModel)
                {
                    viewModel.ConfirmPassword = ConfirmPasswordBox.Password;
                }
            }
        }
    }
}
