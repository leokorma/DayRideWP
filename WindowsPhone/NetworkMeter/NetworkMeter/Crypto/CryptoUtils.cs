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
using System.Security.Cryptography;

namespace NetworkMeter.Crypto
{
    public class CryptoUtils
    {
        public string toSHA256(string str) {
            byte[] data = Encoding.UTF8.GetBytes(str);
            SHA256 shaM = new SHA256Managed();

            string hash = String.Empty;
            foreach (byte bit in shaM.ComputeHash(data))
            {
                hash += bit.ToString("x2");
            }
            return hash;
        }
    }
}
