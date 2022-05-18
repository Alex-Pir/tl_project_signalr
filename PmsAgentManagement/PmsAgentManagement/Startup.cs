using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PmsAgentManagement.Startup))]
namespace PmsAgentManagement
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(180);
            GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromSeconds(270);
            GlobalHost.Configuration.KeepAlive = TimeSpan.FromSeconds(90);
            app.MapSignalR();
        }
    }
}