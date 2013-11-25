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
using DayRide.Resources;

namespace DayRide.Utils
{
    /**
    * Helper Class to allow localization
    */
    public class LocalizationUtils
    {
        public LocalizationUtils()
        {
        }

        private static LocalizationResources localizationResources = new LocalizationResources();

        public LocalizationResources LocalizationResources
        {
            get { return localizationResources; }
        }
    }
}
