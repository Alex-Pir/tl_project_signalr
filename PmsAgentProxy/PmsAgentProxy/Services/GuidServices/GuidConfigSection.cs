using System;
using System.Configuration;
using System.Web.Configuration;

namespace PmsAgentProxy.Services.GuidServices
{
    public class GuidConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("value")] 
        public string Value => (string)this["value"];
        
        public static string GetGuid()
        {
            GuidConfigSection service = (GuidConfigSection)WebConfigurationManager.GetSection("guid");
            
            if (service == null)
            {
                throw new Exception("Service settings were not found. Please check settings in the file");
            }
            
            return service.Value;
        }
    }
}