using Ninject.Modules;
using PmsAgentProxy.Clients;

namespace PmsAgentProxy.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IClient>().To<SignalRClient>()
                .InSingletonScope();
        }
    }
}