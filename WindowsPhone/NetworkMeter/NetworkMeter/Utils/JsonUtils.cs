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
using System.IO;
using Newtonsoft.Json;

namespace DayRide.Utils
{
    /**
     * Helper Class to work with Json objects (serialize and deserialize)
     */
    public class JsonUtils
    {
        public static string toJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T toObj<T>(String json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
