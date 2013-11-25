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
using Microsoft.Phone.Shell;
using DayRide.Resources;
using DayRide.Utils.Database;

namespace DayRide.ViewModel
{
    public class ProfileViewModel : NavigationViewModel
    {
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

        private Profile _AddProfile;

        public Profile AddProfile
        {
            get { return _AddProfile; }
            set
            {
                _AddProfile = value;
                RaisePropertyChanged("AddProfile");
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

        public void LoadAddProfile()
        {
            AddProfile = GetStoredProfile(StorageUtils.ADD_PROFILE);
        }

        public void LoadEditProfile()
        {
            EditProfile = GetStoredProfile(StorageUtils.EDIT_PROFILE);
        }

        public void LoadCurrentProfile()
        {
            CurrentProfile = GetStoredProfile(StorageUtils.CURRENT_PROFILE);
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
            LoadProfile(p.Oid);
        }

        public IApplicationBar CreateEditProfilePageAppBar()
        {
            ApplicationBar bar = new ApplicationBar();

            ApplicationBarIconButton saveButton = new ApplicationBarIconButton(new Uri("/Toolkit.Content/ApplicationBar.Save.png", UriKind.Relative));
            saveButton.Text = LocalizationResources.Save;
            saveButton.Click += new EventHandler(SaveButton_Click);
            bar.Buttons.Add(saveButton);

            ApplicationBarIconButton cancelButton = new ApplicationBarIconButton(new Uri("/Toolkit.Content/ApplicationBar.Cancel.png", UriKind.Relative));
            cancelButton.Text = LocalizationResources.Cancel;
            cancelButton.Click += new EventHandler(CancelButton_Click);
            bar.Buttons.Add(cancelButton);

            Profile cp = GetStoredProfile(StorageUtils.CURRENT_PROFILE);
           
            if (Profile.ROLE_ADMIN.Equals(cp.Role))
            {
                ApplicationBarMenuItem deleteMenuItem = new ApplicationBarMenuItem();
                deleteMenuItem.Text = LocalizationResources.Delete;
                deleteMenuItem.Click += new EventHandler(DeleteMenuItem_Click);
                bar.MenuItems.Add(deleteMenuItem);
            }          

            return bar;
        }

        public void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            Profile cp = GetStoredProfile(StorageUtils.CURRENT_PROFILE);
            Profile ep = GetStoredProfile(StorageUtils.EDIT_PROFILE);

            DeleteProfile(ep);

            if (Profile.ROLE_ADMIN.Equals(cp.Role))
            {
                SendToAllProfilePage();
            }
            else
            {
                SendToReadProfilePage();
            }
        }

        public void CancelButton_Click(object sender, EventArgs e)
        {
            Profile cp = GetStoredProfile(StorageUtils.CURRENT_PROFILE);

            if (Profile.ROLE_ADMIN.Equals(cp.Role))
            {
                SendToAllProfilePage();
            }
            else
            {
                SendToReadProfilePage();
            }
        }

        public void SaveButton_Click(object sender, EventArgs e)
        {
            Profile cp = GetStoredProfile(StorageUtils.CURRENT_PROFILE);
            Profile ep = GetStoredProfile(StorageUtils.EDIT_PROFILE);

            if (String.IsNullOrWhiteSpace(ep.Name))
            {
                showProfileNameErrorMessageBox();
                return;
            }

            if (String.IsNullOrWhiteSpace(ep.Surname))
            {
                showProfileSurnameErrorMessageBox();
                return;
            }

            SaveProfile(ep);

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

        private void showProfileNameErrorMessageBox()
        {
            MessageBox.Show(LocalizationResources.ProfileNameErrorMessage);
        }

        private void showProfileSurnameErrorMessageBox()
        {
            MessageBox.Show(LocalizationResources.ProfileSurnameErrorMessage);
        }

        private void DeleteProfile(Profile p)
        {
            Uri uri = new Uri(ProfileUriUtils.delete(p.Oid));

            WebClient client = new WebClient();
            client.Headers["Content-Type"] = "application/json";
            client.UploadStringCompleted += new UploadStringCompletedEventHandler(OnProfileDeleted);
            client.UploadStringAsync(uri, UriUtils.HTTP_METHOD_DELETE, String.Empty);
        }

        private void SaveProfile(Profile p)
        {
            Uri uri = new Uri(ProfileUriUtils.insert());

            WebClient client = new WebClient();
            client.Headers["Content-Type"] = "application/json";
            client.UploadStringCompleted += new UploadStringCompletedEventHandler(OnProfileSaved);
            client.UploadStringAsync(uri, UriUtils.HTTP_METHOD_POST, JsonUtils.toJson<Profile>(p));
        }

        private void OnProfileSaved(object sender, UploadStringCompletedEventArgs e)
        {
            LoadAllProfiles();
        }

        private void OnProfileDeleted(object sender, UploadStringCompletedEventArgs e)
        {
            LoadAllProfiles();
        }

        public void UpdateEditProfile(string name, string surname, DateTime dateOfBirth)
        {
            Profile p = GetStoredProfile(StorageUtils.EDIT_PROFILE);
            p.Name = name;
            p.Surname = surname;
            p.DateOfBirth = dateOfBirth;
            StoreProfile(StorageUtils.EDIT_PROFILE, p);
        }

        public void UpdateAddProfile(string name, string surname, DateTime dateOfBirth, bool isAdmin)
        {
            Profile p = GetStoredProfile(StorageUtils.ADD_PROFILE);
            p.Name = name;
            p.Surname = surname;
            p.DateOfBirth = dateOfBirth;

            if (isAdmin)
            {
                p.Role = Profile.ROLE_ADMIN;
            }
            else
            {
                p.Role = Profile.ROLE_USER;
            }

            StoreProfile(StorageUtils.ADD_PROFILE, p);
        }

        public void LoadProfile(string oid)
        {
            Uri uri = new Uri(ProfileUriUtils.select(oid));

            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(LoadProfile);
            client.DownloadStringAsync(uri);
        }

        private void LoadProfile(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ShowNoProfileFoundMessageBox();
                return;
            }

            string json = e.Result;
            if (String.IsNullOrWhiteSpace(json))
            {
                ShowNoProfileFoundMessageBox();
                return;
            }

            Profile profile = JsonConvert.DeserializeObject<Profile>(json);

            if (profile == null)
            {
                ShowNoProfileFoundMessageBox();
                return;
            }

            StorageUtils.Set(StorageUtils.EDIT_PROFILE, JsonUtils.toJson<Profile>(profile));

            SendToEditProfilePage();
        }

        private void ShowNoProfileFoundMessageBox()
        {
            MessageBox.Show(LocalizationResources.NoProfileFoundErrorMessage);
        }

        public void LoadAllProfiles()
        {
            Uri uri = new Uri(ProfileUriUtils.all());

            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(LoadAllProfiles);
            client.DownloadStringAsync(uri);
        }

        private void LoadAllProfiles(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            string json = e.Result;
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

        public IApplicationBar CreateAllProfilePageAppBar()
        {
            ApplicationBar bar = new ApplicationBar();

            ApplicationBarIconButton addButton = new ApplicationBarIconButton(new Uri("/Toolkit.Content/ApplicationBar.Add.png", UriKind.Relative));
            addButton.Text = LocalizationResources.Add;
            addButton.Click += new EventHandler(AddButton_Click);
            bar.Buttons.Add(addButton);

            return bar;
        }

        public void AddButton_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile();
            profile.DateOfBirth = DateTime.Now;

            StoreProfile(StorageUtils.ADD_PROFILE, profile);
            SendToAddProfilePage();
        }

        public IApplicationBar CreateAddProfilePageAppBar()
        {
            ApplicationBar bar = new ApplicationBar();

            ApplicationBarIconButton saveButton = new ApplicationBarIconButton(new Uri("/Toolkit.Content/ApplicationBar.Save.png", UriKind.Relative));
            saveButton.Text = LocalizationResources.Save;
            saveButton.Click += new EventHandler(NewButton_Click);
            bar.Buttons.Add(saveButton);

            ApplicationBarIconButton cancelButton = new ApplicationBarIconButton(new Uri("/Toolkit.Content/ApplicationBar.Cancel.png", UriKind.Relative));
            cancelButton.Text = LocalizationResources.Cancel;
            cancelButton.Click += new EventHandler(CancelButton_Click);
            bar.Buttons.Add(cancelButton);

            return bar;
        }

        public void NewButton_Click(object sender, EventArgs e)
        {
            Profile ap = GetStoredProfile(StorageUtils.ADD_PROFILE);
            Profile cp = GetStoredProfile(StorageUtils.CURRENT_PROFILE);

            if (String.IsNullOrWhiteSpace(ap.Name))
            {
                showProfileNameErrorMessageBox();
                return;
            }

            if (String.IsNullOrWhiteSpace(ap.Surname))
            {
                showProfileSurnameErrorMessageBox();
                return;
            }

            ap.Username = ap.Name;
            ap.Password = CryptoUtils.toSHA256(ap.Surname);

            SaveProfile(ap);

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
}
