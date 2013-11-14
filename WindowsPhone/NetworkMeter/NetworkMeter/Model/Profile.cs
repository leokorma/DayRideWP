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

namespace NetworkMeter.Model
{
    public class Profile
    {
        public static readonly string ROLE_USER = "ROLE_USER";
        public static readonly string ROLE_ADMIN = "ROLE_ADMIN";

        public long Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }
    }
}
