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

namespace DayRide.Utils.Database
{
    /**
    * Helper Class to perform actions against Database Rest API
    */
    public abstract class UriUtils
    {
        public static readonly string HTTP_METHOD_GET = "GET";
        public static readonly string HTTP_METHOD_POST = "POST";
        public static readonly string HTTP_METHOD_DELETE = "DELETE";
        public static readonly string HTTP_METHOD_PUT = "PUT";

        private static readonly string API_KEY = "clPjlSOPUfCXe0ZA5vaZtEMsBsgq_g3y";
        private static readonly string DATABASE_NAME = "networkmeter";
        private static readonly string URI = "https://api.mongolab.com/api/1/databases/" + DATABASE_NAME + "/collections/";

        private static string buildUri(string collection, string oid)
        {
            string uri = URI + collection;
            if (!String.IsNullOrWhiteSpace(oid))
            {
                uri += "/" + oid;
            }
            uri += "?apiKey=" + API_KEY;
            return uri;
        }

        protected static string all(string collection)
        {
            return buildUri(collection, null);
        }

        protected static string insert(string collection)
        {
            return buildUri(collection, null);
        }

        protected static string delete(string collection, string oid)
        {
            return buildUri(collection, oid);
        }

        protected static string select(string collection, string oid)
        {
            return buildUri(collection, oid);
        }
    }
}
