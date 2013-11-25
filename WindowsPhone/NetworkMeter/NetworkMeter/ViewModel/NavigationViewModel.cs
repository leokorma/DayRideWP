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

namespace DayRide.ViewModel
{
    /**
     * ViewModel for routing to the requested page
     */
    public abstract class NavigationViewModel : ViewModelBase
    {
        public static readonly string PROFILE_READ_PAGE = "page.profile.read";
        public static readonly string PROFILE_EDIT_PAGE = "page.profile.edit";
        public static readonly string PROFILE_ADD_PAGE = "page.profile.add";
        public static readonly string PROFILE_ALL_PAGE = "page.profile.all";

        public void SendToReadProfilePage()
        {
            Uri uri = new Uri("/View/Profile/ReadProfilePage.xaml", UriKind.Relative);
            Messenger.Default.Send<Uri>(uri, PROFILE_READ_PAGE);
        }

        public void SendToEditProfilePage()
        {
            Uri uri = new Uri("/View/Profile/EditProfilePage.xaml", UriKind.Relative);
            Messenger.Default.Send<Uri>(uri, PROFILE_EDIT_PAGE);
        }

        public void SendToAllProfilePage()
        {
            Uri uri = new Uri("/View/Profile/AllProfilePage.xaml", UriKind.Relative);
            Messenger.Default.Send<Uri>(uri, PROFILE_ALL_PAGE);
        }

        public void SendToAddProfilePage()
        {
            Uri uri = new Uri("/View/Profile/AddProfilePage.xaml", UriKind.Relative);
            Messenger.Default.Send<Uri>(uri, PROFILE_ADD_PAGE);
        }
    }
}
