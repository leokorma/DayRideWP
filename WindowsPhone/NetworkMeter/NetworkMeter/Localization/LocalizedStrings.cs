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

namespace NetworkMeter.Localization
{
    // Class to enable localization
    public class LocalizationUtils
    {
        // Constructor
        public LocalizationUtils()
        {
        }

        private static LocalizationResources _localizationResources = new LocalizationResources();

        public LocalizationResources LocalizationResources
        {
            get { return _localizationResources; }
        }
    }
}
