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
using System.IO.IsolatedStorage;

namespace NetworkMeter.Storage
{
    public class StorageUtils
    {
        public readonly string USERNAME = "session.username";
        public readonly string PASSWORD = "session.password";

        private IsolatedStorageSettings getSettings()
        {
            return IsolatedStorageSettings.ApplicationSettings;
        }


        public void Set(string name, string value)
        {
            IsolatedStorageSettings settings = getSettings();

            if (settings.Contains(name))
            {
                settings[name] = value;
            }
            else
            {
                settings.Add(name, value);
            }
        }

        public string Get(string name)
        {
            IsolatedStorageSettings settings = getSettings();

            if (settings.Contains(name))
            {
                return (string)settings[name];
            }
            return null;
        }

        public bool Contains(string name)
        {
            IsolatedStorageSettings settings = getSettings();
            return settings.Contains(name);
        }

        public void Remove(string name)
        {
            IsolatedStorageSettings settings = getSettings();

            if (settings.Contains(name))
            {
                settings.Remove(name);
            }
        }
    }
}
