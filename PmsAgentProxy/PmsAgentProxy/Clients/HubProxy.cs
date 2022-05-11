using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using PmsAgentProxy.Services.RemoteServices;

namespace PmsAgentProxy.Clients
{
    public class HubProxy : IProxy
    {
        private const string Method = "Request";
        private readonly IHubProxy _hubProxy;
        
        public HubProxy()
        {
            ServiceConfigSection service = RemoteServicesConfigGroup.GetServiceConfig();
            
            HubConnection hubConnection = new HubConnection(service.Url);
            
            _hubProxy = hubConnection.CreateHubProxy(service.Hub);
            hubConnection.Start().Wait();
        }
        
        public async Task<string> SendRequest(string data)
        {
            string result = await _hubProxy.Invoke<string>(Method, data);

            return result;
        }
    }
}