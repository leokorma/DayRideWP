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
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using System.Threading;
using NetworkMeter.ViewModel;

namespace NetworkMeter
{
    public partial class LoginPage : PhoneApplicationPage
    {
        private LoginViewModel _ViewModel;

        // Constructor
        public LoginPage()
        {
            InitializeComponent();
            _ViewModel = (LoginViewModel)this.Resources["ViewModel"];
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBlock.Text;
            string password = passwordTextBlock.Text;

            _ViewModel.validateCredentials(username, password);
        }

        private void loginTextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ViewModel.hideAllMessages();
        }

        private void passwordTextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ViewModel.hideAllMessages();
        }
    }
}