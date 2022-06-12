using System.Configuration;

namespace PmsAgentProxy.Services.RemoteServices
{
    public class ServiceConfigSection : ConfigSection
    {
        [ConfigurationProperty("hub")]
        public string Hub => (string)this["hub"];

        [ConfigurationProperty("url")] 
        public string Url => (string)this["url"];

        protected override string GetGroupName()
        {
            return "service";
        }
    }
}