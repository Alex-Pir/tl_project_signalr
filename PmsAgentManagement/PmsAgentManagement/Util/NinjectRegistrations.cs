using Ninject.Modules;
using PmsAgentManagement.HttpApi;

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