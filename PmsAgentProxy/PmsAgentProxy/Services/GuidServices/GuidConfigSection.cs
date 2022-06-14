using System.Configuration;

namespace PmsAgentProxy.Services.GuidServices
{
    public class GuidConfigSection : ConfigSection
    {
        [ConfigurationProperty("value")] 
        public string Value => (string)this["value"];

        protected override string GetGroupName()
        {
            return "guid";
        }
    }
}