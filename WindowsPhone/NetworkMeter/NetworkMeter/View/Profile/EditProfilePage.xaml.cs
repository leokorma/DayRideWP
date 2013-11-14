using System;
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
using NetworkMeter.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace NetworkMeter.View.Profile
{
    public partial class EditProfilePage : PhoneApplicationPage
    {
        private ProfileViewModel _viewModel;

        public EditProfilePage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _viewModel = new ViewModelLocator().Profile;

            Messenger.Default.Register<Uri>(this, NavigationViewModel.READ_PROFILE_PAGE, (uri) => NavigationService.Navigate(uri));

            _viewModel.LoadEditProfile();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string surname = SurnameTextBox.Text;
            DateTime dateOfBirth = (DateTime)DateOfBirthDatePicker.Value;

            _viewModel.SaveNewProfile(name, surname, dateOfBirth);
        }

        private void NameTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            _viewModel.ValidateName(NameTextBox.Text);
        }

        private void SurnameTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            _viewModel.ValidateSurname(SurnameTextBox.Text);
        }

        private void DateOfBirthDatePicker_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string name = NameTextBox.Text;
            string surname = SurnameTextBox.Text;
            DateTime dateOfBirth = (DateTime)DateOfBirthDatePicker.Value;

            _viewModel.UpdateEditProfile(name, surname, dateOfBirth);
        }
    }
}