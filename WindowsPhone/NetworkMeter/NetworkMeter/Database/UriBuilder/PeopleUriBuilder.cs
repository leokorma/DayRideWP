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
using NetworkMeter.Crypto;

namespace NetworkMeter.Database.UriBuilder
{
    public class PeopleUriBuilder : UriBuilder
    {
        private string collection = "people";

        public string listByName(string name)
        {
            string query = "&q={\"name\":\"" + name + "\"}";
            return list(collection, query);
        }

        public string listByNameAndPassword(string name, string password)
        {
            string query = "&q={\"name\":\"" + name + "\",\"password\":\"" + password + "\" }";
            return list(collection, query);
        }
    }
}
