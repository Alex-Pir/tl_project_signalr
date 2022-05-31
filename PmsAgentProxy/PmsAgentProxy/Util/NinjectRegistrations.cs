using Ninject.Modules;
using PmsAgentProxy.Clients;

namespace PmsAgentProxy.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            /*Bind<IProxy>().To<HubProxy>()
                .InSingletonScope();*/
        }
    }
}