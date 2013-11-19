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

namespace NetworkMeter.Utils.Database
{
    public abstract class RestUtils
    {
        private static readonly string API_VERSION = "1";
        private static readonly string API_KEY = "clPjlSOPUfCXe0ZA5vaZtEMsBsgq_g3y";
        private static readonly string DATABASE_NAME = "networkmeter";

        public static RestClient getClient()
        {
            return new RestClient("https://api.mongolab.com/");
        }

        private static string getListResource()
        {
            return "api/{api_version}/databases/{db_name}/collections/{db_collection}";
        }

        private static string getViewResource()
        {
            return getListResource() + "/{_id}";
        }

        private static string getDeleteResource()
        {
            return getViewResource();
        }

        private static RestRequest getRequest(string collection, string resource)
        {
            return getRequest(collection, resource, null);
        }

        private static RestRequest getRequest(string collection, string resource, string id)
        {
            RestRequest request = new RestRequest(resource);
            request.AddUrlSegment("api_version", API_VERSION);
            request.AddUrlSegment("db_name", DATABASE_NAME);
            request.AddUrlSegment("db_collection", collection);

            if (!String.IsNullOrWhiteSpace(id))
            {
                request.AddUrlSegment("_id", id);
            }

            request.AddParameter("apiKey", API_KEY);

            return request;
        }

        public static RestRequest getListRequest(string collection, Dictionary<string, string> parameters)
        {
            RestRequest request = getRequest(collection, getListResource());

            if (parameters != null && parameters.Count > 0)
            {
                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    request.AddParameter(entry.Key, entry.Value);
                }
            }

            request.Method = Method.GET;

            return request;
        }

        public static RestRequest getDeleteRequest(string collection, string id)
        {
            RestRequest request = getRequest(collection, getDeleteResource(), id);

            request.Method = Method.DELETE;

            return request;
        }
    }
}
