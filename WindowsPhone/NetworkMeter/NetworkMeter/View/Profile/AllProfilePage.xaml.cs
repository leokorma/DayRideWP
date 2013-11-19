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

namespace NetworkMeter.View.User
{
    public partial class AllProfilePage : PhoneApplicationPage
    {
        private ProfileViewModel _viewModel;

        public AllProfilePage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _viewModel = new ViewModelLocator().Profile;

            Messenger.Default.Register<Uri>(this, NavigationViewModel.PROFILE_EDIT_PAGE, (uri) => NavigationService.Navigate(uri));

            _viewModel.LoadAllProfiles();
        }

        private void ListBox_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            Model.Profile p = listBox.SelectedItem as Model.Profile;
            _viewModel.LoadProfile(p.Username); 
        }
    }
}