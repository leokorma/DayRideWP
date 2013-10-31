using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using CommonLibrary.ViewModelBase;
using NetworkMeter.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using NetworkMeter.Database.UriBuilder;

namespace NetworkMeter.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private bool _IsLoginErrorMessageVisible = false;

        public bool IsLoginErrorMessageVisible
        {
            get { return _IsLoginErrorMessageVisible; }
            set
            {
                if (_IsLoginErrorMessageVisible != value)
                {
                    _IsLoginErrorMessageVisible = value;
                    RaisePropertyChanged("IsLoginErrorMessageVisible");
                }
            }
        }

        private bool _IsUsernameErrorMessageVisible = false;

        public bool IsUsernameErrorMessageVisible
        {
            get { return _IsUsernameErrorMessageVisible; }
            set
            {
                if (_IsUsernameErrorMessageVisible != value)
                {
                    _IsUsernameErrorMessageVisible = value;
                    RaisePropertyChanged("IsUsernameErrorMessageVisible");
                }
            }
        }

        private bool _IsPasswordErrorMessageVisible = false;

        public bool IsPasswordErrorMessageVisible
        {
            get { return _IsPasswordErrorMessageVisible; }
            set
            {
                if (_IsPasswordErrorMessageVisible != value)
                {
                    _IsPasswordErrorMessageVisible = value;
                    RaisePropertyChanged("IsPasswordErrorMessageVisible");
                }
            }
        }

        private bool _IsNoUserFoundErrorMessageVisible = false;

        public bool IsNoUserFoundErrorMessageVisible
        {
            get { return _IsNoUserFoundErrorMessageVisible; }
            set
            {
                if (_IsNoUserFoundErrorMessageVisible != value)
                {
                    _IsNoUserFoundErrorMessageVisible = value;
                    RaisePropertyChanged("IsNoUserFoundErrorMessageVisible");
                }
            }
        }

        public void hideAllMessages()
        {
            IsLoginErrorMessageVisible = false;
            IsUsernameErrorMessageVisible = false;
            IsPasswordErrorMessageVisible = false;
            IsNoUserFoundErrorMessageVisible = false;
        }

        public void showNoUserFoundErrorMessage()
        {
            hideAllMessages();
            IsNoUserFoundErrorMessageVisible = true;
        }

        public void showLoginErrorMessage()
        {
            hideAllMessages();
            IsLoginErrorMessageVisible = true;
        }

        public void showUsernameErrorMessage()
        {
            hideAllMessages();
            IsUsernameErrorMessageVisible = true;
        }

        public void showPasswordErrorMessage()
        {
            hideAllMessages();
            IsPasswordErrorMessageVisible = true;
        }

        public void validateCredentials(string username, string password)
        {
            if (!isUsernameValid(username))
            {
                showUsernameErrorMessage();
                return;
            }

            if (!isPasswordValid(password))
            {
                showPasswordErrorMessage();
                return;
            }

            string uri = new PeopleUriBuilder().listByName(username);

            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            client.DownloadStringAsync(new Uri(uri));
        }

        /**
         * Callback function for when the WebClient has finalize reaching data (json with weather data) from Yahoo
         */
        private void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                showLoginErrorMessage();
                return;
            }

            string json = e.Result;
            if (String.IsNullOrWhiteSpace(json))
            {
                showLoginErrorMessage();
                return;
            }

            List<Person> people = JsonConvert.DeserializeObject<List<Person>>(json);

            if (people == null || people.Count == 0)
            {
                showNoUserFoundErrorMessage();
                return;
            }

            // GO SOMEWHERE
        }


        private bool isUsernameValid(string username)
        {
            return !String.IsNullOrWhiteSpace(username);
        }

        private bool isPasswordValid(string password)
        {
            return !String.IsNullOrWhiteSpace(password);
        }
    }
}
