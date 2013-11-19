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
using RestSharp;
using System.Collections.Generic;
using NetworkMeter.Model;

namespace NetworkMeter.Utils.Database
{
    public class ProfileRestUtils : RestUtils
    {
        private static readonly string COLLECTION_PROFILES = "profiles";

        public static RestRequest getByUsername(string username)
        {
            Dictionary<string, string> q = new Dictionary<string, string>();
            q.Add("username", username);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("q", JsonUtils.toJson<Dictionary<string, string>>(q));

            return getListRequest(COLLECTION_PROFILES, parameters);
        }

        public static RestRequest getByUsernameAndPassword(string username, string password)
        {
            Dictionary<string, string> q = new Dictionary<string, string>();
            q.Add("username", username);
            q.Add("password", password);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("q", JsonUtils.toJson<Dictionary<string, string>>(q));

            return getListRequest(COLLECTION_PROFILES, parameters);
        }

        public static RestRequest getAll()
        {
            return getListRequest(COLLECTION_PROFILES, null);
        }

        public static RestRequest delete(string id)
        {
            return getDeleteRequest(COLLECTION_PROFILES, id);
        }
    }
}
