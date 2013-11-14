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

namespace NetworkMeter.Database.UriBuilder
{
    public class UriBuilder
    {
        private static readonly string API_KEY = "clPjlSOPUfCXe0ZA5vaZtEMsBsgq_g3y";
        private static readonly string DATABASE_NAME = "networkmeter";

        private string getDatabaseUriFragment()
        {
            return "https://api.mongolab.com/api/1/databases/" + DATABASE_NAME;
        }

        private string getCollectionUriFragment(string collection)
        {
            return getDatabaseUriFragment() + "/collections/" + collection;
        }

        private string getApiKeyUriFragment()
        {
            return "?apiKey=" + API_KEY;
        }

        protected string list(string collection, string query)
        {
            return getCollectionUriFragment(collection) + getApiKeyUriFragment() + query;
        }
    }
}
