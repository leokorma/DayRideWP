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
        private string getDatabaseUriFragment()
        {
            return "https://api.mongolab.com/api/1/databases/" + DatabaseResources.database;
        }

        private string getCollectionUriFragment(string collection)
        {
            return getDatabaseUriFragment() + "/collections/" + collection;
        }

        private string getApiKeyUriFragment()
        {
            return "?apiKey=" + DatabaseResources.apiKey;
        }

        protected string list(string collection, string paramname, string paramvalue)
        {
            return getCollectionUriFragment(collection) + getApiKeyUriFragment() + "&q={\"" + paramname + "\":\"" + paramvalue + "\"}";
        }
    }
}
