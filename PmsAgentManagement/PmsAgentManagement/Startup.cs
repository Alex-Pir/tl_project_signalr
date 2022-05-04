
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PmsAgentManagement.Startup))]
namespace PmsAgentManagement
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}