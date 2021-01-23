using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace $safeprojectname$
{
    public static class SystemManager
    {
        public static string GetLocalIpAddress()
        {
            UnicastIPAddressInformation mostSuitableIp = null;

            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var network in networkInterfaces)
            {
                if (network.OperationalStatus != OperationalStatus.Up)
                    continue;

                var properties = network.GetIPProperties();

                if (properties.GatewayAddresses.Count == 0)
                    continue;

                foreach (var address in properties.UnicastAddresses)
                {
                    if (address.Address.AddressFamily != AddressFamily.InterNetwork)
                        continue;

                    if (IPAddress.IsLoopback(address.Address))
                        continue;

                    if (!address.IsDnsEligible)
                    {
                        if (mostSuitableIp == null)
                            mostSuitableIp = address;
                        continue;
                    }

                    // The best IP is the IP got from DHCP server
                    if (address.PrefixOrigin != PrefixOrigin.Dhcp)
                    {
                        if (mostSuitableIp == null || !mostSuitableIp.IsDnsEligible)
                            mostSuitableIp = address;
                        continue;
                    }

                    return address.Address.ToString();
                }
            }

            return mostSuitableIp != null ? mostSuitableIp.Address.ToString() : "";
        }

        public static string GetMacAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            string sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (string.IsNullOrEmpty(sMacAddress))
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
            }
            return sMacAddress;
        }

        public static string GetMacByIp(string ipAddress)
        {
            // grab all online interfaces
            var query = NetworkInterface.GetAllNetworkInterfaces()
                .Where(n =>
                    n.OperationalStatus == OperationalStatus.Up && // only grabbing what's online
                    n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(_ => new
                {
                    PhysicalAddress = _.GetPhysicalAddress(),
                    IPProperties = _.GetIPProperties(),
                });

            // grab the first interface that has a uni cast address that matches your search string
            var mac = query
                .FirstOrDefault(q => q.IPProperties.UnicastAddresses.Any<UnicastIPAddressInformation>
                    (ua => ua.Address.ToString() == ipAddress))?
                .PhysicalAddress;

            // return the mac address with formatting (eg "00-00-00-00-00-00")
            return string.Join("-", mac.GetAddressBytes().Select(b => b.ToString("X2")));
        }
    }
}