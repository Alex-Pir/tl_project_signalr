using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Ninject.Modules;
using PmsAgentManagement.HttpApi;
using PmsAgentManagement.Hubs;

namespace PmsAgentManagement.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IHttpApi>().To<HttpNpbApi>();
        }
    }
}