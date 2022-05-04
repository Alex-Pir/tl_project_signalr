using System.Configuration;

namespace PmsAgentManagement.Services.RemoteServices
{
    public class RemoteServicesConfigGroup : ConfigurationSectionGroup
    {
        public ServiceTLConfigSection ServiceTl => (ServiceTLConfigSection)Sections["serviceTL"];
    }
}