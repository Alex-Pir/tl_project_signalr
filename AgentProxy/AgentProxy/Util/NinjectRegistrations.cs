using Ninject.Modules;
using AgentProxy.Clients;

namespace AgentProxy.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IProxy>().ToMethod(x => HubProxy.GetInstance(new NpbClient()));
        }
    }
}