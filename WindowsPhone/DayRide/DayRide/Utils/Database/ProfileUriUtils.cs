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
using System.Collections.Generic;
using DayRide.Model;

namespace DayRide.Utils.Database
{
    /**
     * Helper Class to perform actions against Database Rest API
     */
    public class ProfileUriUtils : UriUtils
    {
        private static readonly string COLLECTION_PROFILES = "profiles";

        public static string all()
        {
            return all(COLLECTION_PROFILES);
        }

        public static string insert()
        {
            return insert(COLLECTION_PROFILES);
        }

        public static string delete(string oid)
        {
            return delete(COLLECTION_PROFILES, oid);
        }
        public static string select(string oid)
        {
            return select(COLLECTION_PROFILES, oid);
        }
    }
}
