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
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using DayRide.Resources;

namespace DayRide.Utils
{
    /**
   * Helper Class to show MessageBox popups
   */
    public class MessageBoxUtils
    {
        public static void ShowNoProfileFoundMessageBox()
        {
            MessageBox.Show(LocalizationResources.NoProfileFoundErrorMessage);
        }

        public static void showPasswordErrorMessageBox()
        {
            MessageBox.Show(LocalizationResources.PasswordErrorMessage);
        }

        public static void showUsernameErrorMessageBox()
        {
            MessageBox.Show(LocalizationResources.UsernameErrorMessage);
        }

        public static void showNoUserFoundErrorMessageBox()
        {
            MessageBox.Show(LocalizationResources.NoUserFoundErrorMessage);
        }

        public static void showLoginErrorMessageBox()
        {
            MessageBox.Show(LocalizationResources.LoginErrorMessage);
        }

        public static void showProfileNameErrorMessageBox()
        {
            MessageBox.Show(LocalizationResources.ProfileNameErrorMessage);
        }

        public static void showProfileSurnameErrorMessageBox()
        {
            MessageBox.Show(LocalizationResources.ProfileSurnameErrorMessage);
        }
    }
}
