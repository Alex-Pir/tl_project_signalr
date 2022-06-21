using System.ServiceProcess;
using AgentProxy.Clients;
using AgentProxy.Services;

namespace AgentProxy
{
    
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceBase[] ServiceToRun;

            ServiceToRun = new ServiceBase[]
            {
                new ProxyService()
            };
            
            ServiceBase.Run(ServiceToRun);
        }
    }
}