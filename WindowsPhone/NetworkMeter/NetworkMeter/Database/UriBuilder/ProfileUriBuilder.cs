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
using System.Text;

namespace NetworkMeter.Database.UriBuilder
{
    public class ProfileUriBuilder : UriBuilder
    {
        private string collection = "profiles";

        public string listByUsername(string username)
        {
            string query = "&q={\"username\":\"" + username + "\"}";
            return list(collection, query);
        }

        public string listByUsernameAndPassword(string username, string password)
        {
            string query = "&q={\"username\":\"" + username + "\",\"password\":\"" + password + "\" }";
            return list(collection, query);
        }
    }
}
