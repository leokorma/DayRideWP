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

namespace NetworkMeter.Utils
{
    public class StorageUtils
    {
        public static readonly string CURRENT_PROFILE = "profile.current";
        public static readonly string EDIT_PROFILE = "profile.edit";

        private static IsolatedStorageSettings getSettings()
        {
            return IsolatedStorageSettings.ApplicationSettings;
        }


        public static void Set(string name, string value)
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

        public static string Get(string name)
        {
            IsolatedStorageSettings settings = getSettings();

            if (settings.Contains(name))
            {
                return (string)settings[name];
            }
            return null;
        }

        public static bool Contains(string name)
        {
            IsolatedStorageSettings settings = getSettings();
            return settings.Contains(name);
        }

        public static void Remove(string name)
        {
            IsolatedStorageSettings settings = getSettings();

            if (settings.Contains(name))
            {
                settings.Remove(name);
            }
        }
    }
}
