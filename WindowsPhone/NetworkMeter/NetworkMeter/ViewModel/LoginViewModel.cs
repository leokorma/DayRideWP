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
using DayRide.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using DayRide.Utils;
using DayRide.Resources;
using DayRide.Utils.Database;

namespace DayRide.ViewModel
{
    public class LoginViewModel : NavigationViewModel
    {
        public void validateCredentials(string username, string password)
        {
            if (!isUsernameValid(username))
            {
                showUsernameErrorMessageBox();
                return;
            }

            if (!isPasswordValid(password))
            {
                showPasswordErrorMessageBox();
                return;
            }

            Uri uri = new Uri(ProfileUriUtils.all());

            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(OnDownloadComplete);
            client.DownloadStringAsync(uri, username);
        }

        private void showPasswordErrorMessageBox()
        {
            MessageBox.Show(LocalizationResources.PasswordErrorMessage);
        }

        private void showUsernameErrorMessageBox()
        {
            MessageBox.Show(LocalizationResources.UsernameErrorMessage);
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
