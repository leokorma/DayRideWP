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
using NetworkMeter.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using NetworkMeter.Utils;
using NetworkMeter.Resources;
using NetworkMeter.Utils.Database;

namespace NetworkMeter.ViewModel
{
    public class LoginViewModel : NavigationViewModel
    {
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
            IsUsernameErrorMessageVisible = false;
            IsPasswordErrorMessageVisible = false;
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

            Uri uri = new Uri(ProfileUriUtils.all());

            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(OnDownloadComplete);
            client.DownloadStringAsync(uri, username);
        }

        /**
         * Callback function for when the WebClient has finalize reaching data (json with weather data) from Yahoo
         */
        private void OnDownloadComplete(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                showLoginErrorMessageBox();
                return;
            }

            string json = e.Result;
            if (String.IsNullOrWhiteSpace(json))
            {
                showLoginErrorMessageBox();
                return;
            }

            List<Profile> profiles = JsonConvert.DeserializeObject<List<Profile>>(json);

            if (profiles != null && profiles.Count > 0)
            {
                Profile profile = null;

                string username = e.UserState.ToString();

                foreach (Profile p in profiles)
                {
                    if (p.Username == username)
                    {
                        profile = p;

                        StorageUtils.Set(StorageUtils.CURRENT_PROFILE, JsonUtils.toJson<Profile>(profile));

                        if (Profile.ROLE_ADMIN.Equals(profile.Role))
                        {
                            SendToAllProfilePage();
                        }
                        else
                        {
                            SendToReadProfilePage();
                        }

                        return;
                    }                    
                }
            }

            showNoUserFoundErrorMessageBox();
        }

        private void showNoUserFoundErrorMessageBox()
        {
            MessageBox.Show(LocalizationResources.NoUserFoundErrorMessage);
        }

        private void showLoginErrorMessageBox()
        {
            MessageBox.Show(LocalizationResources.LoginErrorMessage);
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
