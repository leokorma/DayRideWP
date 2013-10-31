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

namespace NetworkMeter.Database
{
    public class DatabaseUtils
    {
        // Constructor
        public DatabaseUtils()
        {
        }

        private static DatabaseResources databaseResources = new DatabaseResources();

        public DatabaseResources DatabaseResources
        {
            get { return databaseResources; }
        }
    }
}
