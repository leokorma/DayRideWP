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

namespace NetworkMeter.View
{
    public partial class WelcomePage : PhoneApplicationPage
    {
        private LoginViewModel _viewModel;

        // Constructor
        public WelcomePage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            ViewModelLocator vml = (ViewModelLocator)Application.Current.Resources["ViewModelLocator"];
            _viewModel = vml.Login;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.clear();
        }
    }
}