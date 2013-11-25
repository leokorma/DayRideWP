﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using System.Threading;
using DayRide.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using DayRide.View;
using DayRide.Utils;

namespace DayRide
{
    public partial class LoginPage : PhoneApplicationPage
    {
        private LoginViewModel _viewModel;

        // Constructor
        public LoginPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _viewModel = new ViewModelLocator().Login;

            Messenger.Default.Register<Uri>(this, NavigationViewModel.PROFILE_READ_PAGE, (uri) => NavigationService.Navigate(uri));
            Messenger.Default.Register<Uri>(this, NavigationViewModel.PROFILE_ALL_PAGE, (uri) => NavigationService.Navigate(uri));
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBlock.Text;
            string password = PasswordTextBlock.Password;

            string hash = CryptoUtils.toSHA256(password);

            _viewModel.validateCredentials(username, hash);
        }
    }
}