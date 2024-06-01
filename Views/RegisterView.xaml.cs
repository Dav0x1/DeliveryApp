//using DeliveryApp.Services;
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
		//private readonly UserService _userService;

		public RegisterView()
		{
			InitializeComponent();
			//_userService = new UserService(); // Inicjalizacja usługi użytkownika
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

		private async void RegisterButton_Click(object sender, RoutedEventArgs e)
		{
			string username = UsernameTextBox.Text;
			string password = PasswordBox.Password;
			string confirmPassword = ConfirmPasswordBox.Password;

			// Ewentualna dodatkowa walidacja - TUTAJ

			// Sprawdzenie czy hasła zgodne
			if (password != confirmPassword)
			{
				MessageBox.Show("Passwords do not match!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Rejestracja 
			/*bool registrationResult = await _userService.RegisterUserAsync(username, password);

			if (registrationResult)
			{
				MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			else
			{
				MessageBox.Show("Registration failed! Username already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}*/
		}
	}
}
