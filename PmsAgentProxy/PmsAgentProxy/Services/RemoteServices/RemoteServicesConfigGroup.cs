using System;
using System.Configuration;
using System.Web.Configuration;

namespace PmsAgentProxy.Services.RemoteServices
{
    public class RemoteServicesConfigGroup : ConfigurationSectionGroup
    {
        public ServiceConfigSection Service => (ServiceConfigSection)Sections["service"];

        public static ServiceConfigSection GetServiceConfig()
        {
            RemoteServicesConfigGroup group = (RemoteServicesConfigGroup)WebConfigurationManager.OpenWebConfiguration("/")
                .GetSectionGroup("remoteServices");

            if (group == null)
            {
                throw new Exception("Service configuration not found");
            }
            
            ServiceConfigSection service = (ServiceConfigSection)group.Sections.Get("service");

            if (service == null)
            {
                throw new Exception("Service settings were not found. Please check settings in the file");
            }

            return service;
        }
    }
}