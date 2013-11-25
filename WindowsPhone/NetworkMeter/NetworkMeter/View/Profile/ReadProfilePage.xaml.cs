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
using DayRide.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using DayRide.Utils;

namespace DayRide.View.Profile
{
    /**
    * View Class to read data from an existing Profile
    */
    public partial class ReadProfilePage : PhoneApplicationPage
    {
        private ProfileViewModel _viewModel;

        public ReadProfilePage()
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

            Messenger.Default.Register<Uri>(this, NavigationViewModel.PROFILE_EDIT_PAGE, (uri) => NavigationService.Navigate(uri));

            _viewModel.LoadCurrentProfile();

            ApplicationBar = _viewModel.CreateReadProfilePageAppBar();
        }
    }
}