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
    public class PeopleUriBuilder : UriBuilder
    {
        private string collection = "people";

        public string listByName(string name)
        {
            return list(collection, "name", name);
        }
    }
}
