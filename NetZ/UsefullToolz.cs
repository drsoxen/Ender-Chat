using System;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;


namespace NetZ
{
    public enum ClientType
    {
        MultiCast,
        TCP
    }

    public static class UsefullToolz
    {
        public static string GetIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ipa in host.AddressList)
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    return ipa.ToString();
            return "UNKNOWN_IP";
        }
    }
}
