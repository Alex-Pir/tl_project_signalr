using Ninject.Modules;
using PmsAgentManagement.HttpApi;
using PmsAgentManagement.Hubs.Factories;

namespace PmsAgentManagement.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IHttpApi>().To<HttpNpbApi>();
            Bind<IHubContextFactory>().To<AgentHubContextFactory>();
        }
    }
}