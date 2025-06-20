using System.Configuration;
using System.Net;
using System.Reflection;

namespace System.Infrastructure.Tools.Services.Models
{
    public class LogRequest
    {
        public LogRequest()
        {
            hostName = Dns.GetHostName();
            ipOrigin = Dns.GetHostByName(hostName).AddressList[0].ToString();
        }
        public string hostName { get; set; }

        public int idSequence { get; set; }
        public string origin { get; set; } = ConfigurationManager.AppSettings["aplication_Name"].ToString();
        public string nameClass { get; set; }
        public string nameMethod { get; set; } 
        public string ipOrigin { get; set; }
        public string type { get; set; }
        public string user { get; set; }
        public string message { get; set; }
        public string content { get; set; }
        public string key { get; set; }

    }
}
