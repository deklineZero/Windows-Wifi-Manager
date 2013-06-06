using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSh
{
    public class WiFiSecurityStatus
    {
        public bool isSecure { get; set; }
        public List<WifiProfile> openWifiList { get; set; }

        public WiFiSecurityStatus() {
            this.isSecure = true;
        }

        public void CheckSecurityStatus()
        {
            bool secure = true;
            List<WifiProfile> openWifi = new List<WifiProfile>();

            var profiles = NetShWrapper.GetWifiProfiles();
            foreach (var a in profiles)
            {
                if (NetShWrapper.IsOpenAndAutoWifiProfile(a)) {
                    openWifi.Add(a);
                    secure = false; 
                }
            }

            this.openWifiList = openWifi;
            this.isSecure = secure;
        }

    }
}
