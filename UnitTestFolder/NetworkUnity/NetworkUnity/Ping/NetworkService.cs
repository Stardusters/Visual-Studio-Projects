using NetworkUnity.DNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUnity.Ping
{ 
    public class NetworkService
    {
        private readonly IDNS _dns;
        public NetworkService(IDNS dns)
        {
            _dns = dns;
        }
        public string SendPing()
        {
            var dnsSuccess = _dns.sendDNS();
            if (dnsSuccess)
                return "Success: Ping Sent!";
            else
                return "Failed: Ping Not Sent!";
        }

        public int PingTimeOut(int a, int b)
        {
            return a + b;
        }

        public DateTime LastPingDate()
        {
            return DateTime.Now;
        }

        public PingOptions GetPingOptions()
        {
            return new PingOptions() { DontFragment = true, Ttl = 1 };
        }

        public IEnumerable<PingOptions> MostRecentPings()
        {
            IEnumerable<PingOptions> pingOptions = new[]
            {
                new PingOptions() { DontFragment = true, Ttl = 1 },
                new PingOptions() { DontFragment = true, Ttl = 1 },
                new PingOptions() { DontFragment = true, Ttl = 1 }
            };
            return pingOptions;
        }
    }
}
