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
using NetworkMeter.Database.UriBuilder;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using NetworkMeter.Utils;

namespace NetworkMeter.ViewModel
{
    public class ProfileViewModel : NavigationViewModel
    {
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
            Profile p = GetStoredProfile(StorageUtils.EDIT_PROFILE);
            if (p != null)
            {
                EditProfile = p;
            }
            else
            {
                InitEditProfile();
            }
        }

        public void LoadCurrentProfile()
        {
            CurrentProfile = GetStoredProfile(StorageUtils.CURRENT_PROFILE);
        }

        public void InitEditProfile()
        {
            EditProfile = GetStoredProfile(StorageUtils.CURRENT_PROFILE);
            StoreProfile(StorageUtils.EDIT_PROFILE, EditProfile);
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

        public void UpdateEditProfile(string name, string surname, DateTime dateOfBirth)
        {
            Profile p = GetStoredProfile(StorageUtils.EDIT_PROFILE);
            p.Name = name;
            p.Surname = surname;
            p.DateOfBirth = dateOfBirth;

            StoreProfile(StorageUtils.EDIT_PROFILE, p);
        }

        public void SaveNewProfile(string name, string surname, DateTime dateOfBirth)
        {
            if (!String.IsNullOrWhiteSpace(name) && !String.IsNullOrWhiteSpace(surname))
            {
                Profile p = GetStoredProfile(StorageUtils.CURRENT_PROFILE);
                p.Name = name;
                p.Surname = surname;
                p.DateOfBirth = dateOfBirth;

                StoreProfile(StorageUtils.CURRENT_PROFILE, p);

                SendToReadProfilePage();
            }
        }
    }
}
