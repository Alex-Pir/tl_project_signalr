using System.Configuration;

namespace PmsAgentProxy.Services.RemoteServices
{
    public class ServiceConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("hub")]
        public string Hub => (string)this["hub"];

        [ConfigurationProperty("url")] 
        public string Url => (string)this["url"];
    }
}