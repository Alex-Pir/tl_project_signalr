using Microsoft.Owin;
using Owin;
using PmsAgentProxy.Clients;

[assembly: OwinStartup(typeof(PmsAgentProxy.Startup))]
namespace PmsAgentProxy
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var hub = HubProxy.GetInstance();
            hub.StartConnection().Wait();
        }
    }
}