using Microsoft.AspNet.SignalR;

namespace PmsAgentManagement.Hubs.Factories
{
    public interface IHubContextFactory
    {
        public IHubContext GetContext();
    }
}