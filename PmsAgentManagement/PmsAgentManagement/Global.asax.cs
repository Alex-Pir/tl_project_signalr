using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;

namespace PmsAgentManagement
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

			GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(180);
            GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromSeconds(270);
            GlobalHost.Configuration.KeepAlive = TimeSpan.FromSeconds(90);

        }
    }
}