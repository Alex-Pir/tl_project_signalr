using Ninject.Modules;
using PmsAgentManagement.HttpApi;
using PmsAgentManagement.Hubs.Factories;
using PmsAgentManagement.Services;

namespace PmsAgentManagement.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IHttpApi>().To<HttpNpbApi>();
            Bind<IHubContextFactory>().To<AgentHubContextFactory>().InSingletonScope();
            //Bind<IRegistry>().To<Registry>().InSingletonScope();
        }
    }
}