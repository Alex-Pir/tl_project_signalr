using System;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin;
using Ninject;
using Ninject.Modules;
using Owin;
using PmsAgentManagement.HttpApi;
using PmsAgentManagement.Hubs;
using PmsAgentManagement.Util;

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
            
            // внедрение зависимостей
            //NinjectModule registrations = new NinjectRegistrations();
            /*var kernel = new StandardKernel();
            var resolver = new NinjectSignalRDependencyResolver(kernel);
            
            kernel.Bind<IHttpApi>().To<HttpNpbApi>().InSingletonScope();

            kernel.Bind(typeof(IHubConnectionContext<dynamic>)).ToMethod(context =>
                resolver.Resolve<IConnectionManager>().GetHubContext<AgentHub>().Clients
            ).WhenInjectedInto<IHttpApi>();

            kernel.Bind(typeof(IHubContext<dynamic>)).To<AgentHub>();

            var config = new HubConfiguration();

            config.Resolver = resolver;*/
            app.MapSignalR();
        }
    }
}