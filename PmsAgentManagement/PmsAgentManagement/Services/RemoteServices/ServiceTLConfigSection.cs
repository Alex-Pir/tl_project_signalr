using System.Configuration;

namespace PmsAgentManagement.Services.RemoteServices
{
    public class ServiceTLConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("url")]
        public string Url => (string)this["url"];

        [ConfigurationProperty("page")] 
        public string Page => (string)this["page"];
    }
}