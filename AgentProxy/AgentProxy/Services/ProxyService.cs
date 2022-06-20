using System.ServiceProcess;
using System.Threading;
using AgentProxy.Clients;

namespace AgentProxy.Services;

public class ProxyService : ServiceBase
{

    public ProxyService()
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
        ServiceName = "PMS Agent Proxy";
    }
}