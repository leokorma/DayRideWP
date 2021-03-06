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
using DayRide.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace DayRide.View.Profile
{
    /**
    * View Class to edit an existing Profile
    */
    public partial class EditProfilePage : PhoneApplicationPage
    {
        private ProfileViewModel _viewModel;

        public EditProfilePage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        /**
         * When Page loaded, initialize ViewModel, URL listeners and elements' event handlers
         */
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _viewModel = new ViewModelLocator().Profile;

            Messenger.Default.Register<Uri>(this, NavigationViewModel.PROFILE_READ_PAGE, (uri) => NavigationService.Navigate(uri));
            Messenger.Default.Register<Uri>(this, NavigationViewModel.PROFILE_ALL_PAGE, (uri) => NavigationService.Navigate(uri));

            _viewModel.LoadEditProfile();

            ApplicationBar = _viewModel.CreateEditProfilePageAppBar();

            DateOfBirthDatePicker.ValueChanged += new EventHandler<DateTimeValueChangedEventArgs>(DateOfBirthDatePicker_ValueChanged);
        }

        private void DateOfBirthDatePicker_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            StoreOnChange();
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            StoreOnChange();
        }

        private void SurnameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            StoreOnChange();
        }
        
        /**
         * When any user data changes, store it in IsolatedStorage
         */
        private void StoreOnChange()
        {
            string name = NameTextBox.Text;
            string surname = SurnameTextBox.Text;
            DateTime dateOfBirth = (DateTime)DateOfBirthDatePicker.Value;

            _viewModel.UpdateEditProfile(name, surname, dateOfBirth);
        }
    }
}