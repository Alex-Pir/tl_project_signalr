using Ninject.Modules;
using PmsAgentProxy.Clients;

namespace PmsAgentProxy.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IProxy>().ToMethod(x => HubProxy.GetInstance(new NpbClient()));
        }
    }
}