using System.ServiceProcess;
using System.Threading;
using AgentProxy.Clients;

namespace AgentProxy.Services;

public class ProxyServices : ServiceBase
{

    public ProxyServices()
    {
        InitializeComponent();
        AutoLog = true;
    }

    protected override void OnStart(string[] args)
    {
        HubProxy hubProxy = HubProxy.GetInstance(new NpbClient());

        Thread hubThread = new Thread(hubProxy.StartHub);
        hubThread.Start();
    }

    private void InitializeComponent()
    {
            // 
            // ProxyServices
            // 
            this.ServiceName = "PMS Agent Proxy";

    }
}