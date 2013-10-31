﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using NetworkMeter.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using NetworkMeter.Database.UriBuilder;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using NetworkMeter.Storage;

namespace NetworkMeter.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string _QuickLoginButtonContent;

        public string QuickLoginButtonContent
        {
            get { return _QuickLoginButtonContent; }
            set
            {
                if (_QuickLoginButtonContent != value)
                {
                    _QuickLoginButtonContent = value;
                    RaisePropertyChanged("QuickLoginButtonContent");
                }
            }
        }

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

        private bool _IsQuickLoginButtonVisible = false;

        public bool IsQuickLoginButtonVisible
        {
            get { return _IsQuickLoginButtonVisible; }
            set
            {
                if (_IsQuickLoginButtonVisible != value)
                {
                    _IsQuickLoginButtonVisible = value;
                    RaisePropertyChanged("IsQuickLoginButtonVisible");
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

        public void showQuickLoginButton(string username)
        {
            QuickLoginButtonContent = Localization.LocalizationResources.QuickLogin + " " + username;
            IsQuickLoginButtonVisible = true;
        }

        public void hideQuickLoginButton()
        {
            IsQuickLoginButtonVisible = false;
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

            string uri = new PeopleUriBuilder().listByNameAndPassword(username, password);

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

            Person person = people[0];

            StorageUtils storageUtils = new StorageUtils();
            storageUtils.Set(storageUtils.USERNAME, person.Name);
            storageUtils.Set(storageUtils.PASSWORD, person.Password);

            Uri uri = new Uri("/View/WelcomePage.xaml", UriKind.Relative);
            Messenger.Default.Send<Uri>(uri, "Navigate");
        }


        private bool isUsernameValid(string username)
        {
            return !String.IsNullOrWhiteSpace(username);
        }

        private bool isPasswordValid(string password)
        {
            return !String.IsNullOrWhiteSpace(password);
        }

        public void verifyIfUserIsLoggedIn()
        {
            StorageUtils storageUtils = new StorageUtils();

            if (storageUtils.Contains(storageUtils.USERNAME))
            {
                showQuickLoginButton(storageUtils.Get(storageUtils.USERNAME));
                return;
            }
            hideQuickLoginButton();
        }

        public void clear()
        {
            StorageUtils storageUtils = new StorageUtils();
            storageUtils.Remove(storageUtils.USERNAME);
        }
    }
}
