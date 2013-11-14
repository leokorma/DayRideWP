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
    public partial class ReadProfilePage : PhoneApplicationPage
    {
        private ProfileViewModel _viewModel;

        public ReadProfilePage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _viewModel = new ViewModelLocator().Profile;

            Messenger.Default.Register<Uri>(this, NavigationViewModel.EDIT_PROFILE_PAGE, (uri) => NavigationService.Navigate(uri));

            _viewModel.LoadCurrentProfile();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.InitEditProfile();
            _viewModel.SendToEditProfilePage();
        }
    }
}