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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace NetworkMeter.Model
{
    public class Profile : IComparable<Profile>
    {
        public static readonly string ROLE_USER = "ROLE_USER";
        public static readonly string ROLE_ADMIN = "ROLE_ADMIN";

        public Dictionary<string, string> _id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }

        [JsonIgnore]
        public string FullName
        {
            get { return Name + " " + Surname; }
        }

        [JsonIgnore]
        public string Oid
        {
            get
            {
                return (_id != null && _id.Count > 0) ? _id["$oid"] : null;
            }
        }

        public int CompareTo(Profile other)
        {
            return FullName.CompareTo(other.FullName);
        }
    }
}
