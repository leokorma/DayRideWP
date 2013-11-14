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

namespace NetworkMeter.ViewModel
{
    public abstract class NavigationViewModel : ViewModelBase
    {
        public static readonly string READ_PROFILE_PAGE = "page.profile.read";
        public static readonly string EDIT_PROFILE_PAGE = "page.profile.edit";

        public void SendToReadProfilePage()
        {
            Uri uri = new Uri("/View/Profile/ReadProfilePage.xaml", UriKind.Relative);
            Messenger.Default.Send<Uri>(uri, READ_PROFILE_PAGE);
        }

        public void SendToEditProfilePage()
        {
            Uri uri = new Uri("/View/Profile/EditProfilePage.xaml", UriKind.Relative);
            Messenger.Default.Send<Uri>(uri, EDIT_PROFILE_PAGE);
        }
    }
}
