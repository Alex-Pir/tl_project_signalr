using Microsoft.AspNet.SignalR;

namespace PmsAgentManagement.Hubs.Factories
{
    public class AgentHubContextFactory : IHubContextFactory
    {
        public IHubContext GetContext()
        {
            return GlobalHost.ConnectionManager.GetHubContext<AgentHub>();
        }
    }
}
