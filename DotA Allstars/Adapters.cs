using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace DotA_Allstars
{
    class Adapters
    {
        public List<String> net_adapters()
        {
            List<String> values = new List<String>();
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                values.Add(nic.Description);
            }
            return values;
        }
    }
}
