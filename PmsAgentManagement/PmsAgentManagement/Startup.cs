using System;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.AspNet.SignalR.Transports;
using Microsoft.Owin;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using Owin;
using PmsAgentManagement.HttpApi;
using PmsAgentManagement.Hubs;
using PmsAgentManagement.Hubs.Factories;
using PmsAgentManagement.Services;
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
            NinjectModule registrations = new NinjectRegistrations();
            var kernel = new StandardKernel(registrations);
            /*var resolver = new NinjectSignalRDependencyResolver(kernel);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            
            kernel.Bind(typeof(IHubConnectionContext<dynamic>)).ToMethod(context =>
                resolver.Resolve<IConnectionManager>().GetHubContext<AgentHub>().Clients
            ).WhenInjectedInto<IHttpApi>();

            kernel.Bind(typeof(IHubConnectionContext<dynamic>)).ToMethod(context =>
                resolver.Resolve<IConnectionManager>().GetHubContext<AgentHub>().Clients
            ).WhenInjectedInto<IRegistry>();
            
            kernel.Bind(typeof(IHubConnectionContext<dynamic>)).ToMethod(context =>
                resolver.Resolve<IConnectionManager>().GetHubContext<AgentHub>().Clients
            ).WhenInjectedInto<IHubContextFactory>();
            
            var config = new HubConfiguration
            {
                Resolver = resolver
            };*/

            app.MapSignalR();
        }
    }
}