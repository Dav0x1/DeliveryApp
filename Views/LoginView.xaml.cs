﻿using DeliveryApp.ViewModels;
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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
		public LoginView()
		{
			InitializeComponent();
			PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
		}

		private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			PasswordTextBox.Text = PasswordBox.Password;
		}

		private void ShowPasswordCheckBox_Checked(object sender, RoutedEventArgs e)
		{
			PasswordBox.Visibility = Visibility.Collapsed;
			PasswordTextBox.Visibility = Visibility.Visible;
			PasswordTextBox.Text = PasswordBox.Password;
		}

		private void ShowPasswordCheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			PasswordBox.Visibility = Visibility.Visible;
			PasswordTextBox.Visibility = Visibility.Collapsed;
			PasswordBox.Password = PasswordTextBox.Text;
		}

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
			if (DataContext is LoginViewModel viewModel)
			{
				viewModel.Password = PasswordBox.Password;
			}
        }
    }
}
