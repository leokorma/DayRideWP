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
using Microsoft.Phone.Shell;
using NetworkMeter.Resources;
using NetworkMeter.Utils.Database;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace NetworkMeter.ViewModel
{
    public class ProfileViewModel : NavigationViewModel
    {
        private bool _IsNoUsersFoundErrorMessageVisible = false;

        public bool IsNoUsersFoundErrorMessageVisible
        {
            get { return _IsNoUsersFoundErrorMessageVisible; }
            set
            {
                if (_IsNoUsersFoundErrorMessageVisible != value)
                {
                    _IsNoUsersFoundErrorMessageVisible = value;
                    RaisePropertyChanged("IsNoUsersFoundErrorMessageVisible");
                }
            }
        }

        private bool _IsProfileNameErrorMessageVisible = false;

        public bool IsProfileNameErrorMessageVisible
        {
            get { return _IsProfileNameErrorMessageVisible; }
            set
            {
                if (_IsProfileNameErrorMessageVisible != value)
                {
                    _IsProfileNameErrorMessageVisible = value;
                    RaisePropertyChanged("IsProfileNameErrorMessageVisible");
                }
            }
        }

        private bool _IsProfileSurnameErrorMessageVisible = false;

        public bool IsProfileSurnameErrorMessageVisible
        {
            get { return _IsProfileSurnameErrorMessageVisible; }
            set
            {
                if (_IsProfileSurnameErrorMessageVisible != value)
                {
                    _IsProfileSurnameErrorMessageVisible = value;
                    RaisePropertyChanged("IsProfileSurnameErrorMessageVisible");
                }
            }
        }

        private List<Profile> _Profiles;

        public List<Profile> Profiles
        {
            get { return _Profiles; }
            set
            {
                _Profiles = value;
                RaisePropertyChanged("Profiles");
            }
        }

        private Profile _CurrentProfile;

        public Profile CurrentProfile
        {
            get { return _CurrentProfile; }
            set
            {
                _CurrentProfile = value;
                RaisePropertyChanged("CurrentProfile");
            }
        }

        private Profile _EditProfile;

        public Profile EditProfile
        {
            get { return _EditProfile; }
            set
            {
                _EditProfile = value;
                RaisePropertyChanged("EditProfile");
            }
        }

        public Profile GetStoredProfile(string key)
        {
            if (!System.ComponentModel.DesignerProperties.IsInDesignTool)
            {
                if (StorageUtils.Contains(key))
                {
                    string json = StorageUtils.Get(key);

                    return JsonUtils.toObj<Profile>(json);
                }
            }
            return null;
        }

        public void StoreProfile(string key, Profile p)
        {
            StorageUtils.Set(key, JsonUtils.toJson<Profile>(p));
        }

        public void LoadEditProfile()
        {
            EditProfile = GetStoredProfile(StorageUtils.EDIT_PROFILE);
        }

        public void LoadCurrentProfile()
        {
            CurrentProfile = GetStoredProfile(StorageUtils.CURRENT_PROFILE);
        }

        public void ShowProfileNameErrorMessage()
        {
            IsProfileNameErrorMessageVisible = true;
        }

        public void HideProfileNameErrorMessage()
        {
            IsProfileNameErrorMessageVisible = false;
        }

        public void ShowProfileSurnameErrorMessage()
        {
            IsProfileSurnameErrorMessageVisible = true;
        }

        public void HideProfileSurnameErrorMessage()
        {
            IsProfileSurnameErrorMessageVisible = false;
        }

        public void ValidateName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                ShowProfileNameErrorMessage();
            }
            else
            {
                HideProfileNameErrorMessage();
            }
        }

        public void ValidateSurname(string surname)
        {
            if (String.IsNullOrWhiteSpace(surname))
            {
                ShowProfileSurnameErrorMessage();
            }
            else
            {
                HideProfileSurnameErrorMessage();
            }
        }

        public IApplicationBar CreateReadProfilePageAppBar()
        {
            ApplicationBar bar = new ApplicationBar();

            ApplicationBarIconButton editButton = new ApplicationBarIconButton(new Uri("/Toolkit.Content/ApplicationBar.Edit.png", UriKind.Relative));
            editButton.Text = LocalizationResources.Edit;
            editButton.Click += new EventHandler(EditButton_Click);
            bar.Buttons.Add(editButton);

            return bar;
        }

        public void EditButton_Click(object sender, EventArgs e)
        {
            Profile p = GetStoredProfile(StorageUtils.CURRENT_PROFILE);
            LoadProfile(p.Username);
        }

        public IApplicationBar CreateEditProfilePageAppBar()
        {
            ApplicationBar bar = new ApplicationBar();

            ApplicationBarIconButton saveButton = new ApplicationBarIconButton(new Uri("/Toolkit.Content/ApplicationBar.Save.png", UriKind.Relative));
            saveButton.Text = LocalizationResources.Save;
            saveButton.Click += new EventHandler(SaveButton_Click);
            bar.Buttons.Add(saveButton);

            return bar;
        }

        public void SaveButton_Click(object sender, EventArgs e)
        {
            Profile cp = GetStoredProfile(StorageUtils.CURRENT_PROFILE);
            Profile ep = GetStoredProfile(StorageUtils.EDIT_PROFILE);

            if (!String.IsNullOrWhiteSpace(ep.Name) && !String.IsNullOrWhiteSpace(ep.Surname))
            {
                RestClient client = ProfileRestUtils.getClient();
                RestRequest request = ProfileRestUtils.delete(ep.Oid);
                client.ExecuteAsync(request, response =>
                {
                    LoadAllProfiles();
                });

                if (ep.Username.Equals(cp.Username))
                {
                    StoreProfile(StorageUtils.CURRENT_PROFILE, ep);
                }

                if (Profile.ROLE_ADMIN.Equals(cp.Role))
                {
                    SendToAllProfilePage();
                }
                else
                {
                    SendToReadProfilePage();
                }
            }
        }

        public void UpdateEditProfile(string name, string surname, DateTime dateOfBirth)
        {
            Profile p = GetStoredProfile(StorageUtils.EDIT_PROFILE);
            p.Name = name;
            p.Surname = surname;
            p.DateOfBirth = dateOfBirth;
            StoreProfile(StorageUtils.EDIT_PROFILE, p);
        }

        public void LoadProfile(string username)
        {
            RestClient client = ProfileRestUtils.getClient();
            RestRequest request = ProfileRestUtils.getByUsername(username);
            client.ExecuteAsync(request, response =>
            {
                LoadProfile_DownloadStringCompleted(response);
            });
        }

        private void LoadProfile_DownloadStringCompleted(IRestResponse response)
        {
            if (response.ErrorException != null)
            {
                ShowNoProfileFoundMessageBox();
                return;
            }

            string json = response.Content;
            if (String.IsNullOrWhiteSpace(json))
            {
                ShowNoProfileFoundMessageBox();
                return;
            }

            List<Profile> profiles = JsonConvert.DeserializeObject<List<Profile>>(json);

            if (profiles == null || profiles.Count == 0)
            {
                ShowNoProfileFoundMessageBox();
                return;
            }

            Profile profile = profiles[0];

            StorageUtils.Set(StorageUtils.EDIT_PROFILE, JsonUtils.toJson<Profile>(profile));

            SendToEditProfilePage();
        }

        private void ShowNoProfileFoundMessageBox()
        {
            MessageBox.Show(LocalizationResources.NoProfileFoundErrorMessage);
        }

        public void LoadAllProfiles()
        {
            RestClient client = ProfileRestUtils.getClient();
            RestRequest request = ProfileRestUtils.getAll();
            client.ExecuteAsync(request, response =>
            {
                LoadAllProfiles_DownloadStringCompleted(response);
            });
        }

        private void LoadAllProfiles_DownloadStringCompleted(IRestResponse response)
        {
            if (response.ErrorException != null)
            {
                return;
            }

            string json = response.Content;
            if (String.IsNullOrWhiteSpace(json))
            {
                return;
            }

            List<Profile> profiles = JsonConvert.DeserializeObject<List<Profile>>(json);

            if (profiles == null || profiles.Count == 0)
            {
                return;
            }

            profiles.Sort();
            Profiles = profiles;
        }
    }
}
