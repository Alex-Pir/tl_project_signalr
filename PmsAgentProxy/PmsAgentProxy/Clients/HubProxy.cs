using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using PmsAgentProxy.Services.RemoteServices;

namespace PmsAgentProxy.Clients
{
    public class HubProxy : IProxy
    {
        private const string Method = "Request";
        private readonly HubConnection _hubConnection;
        private readonly IHubProxy _hubProxy;
        
        public HubProxy()
        {
            ServiceConfigSection service = RemoteServicesConfigGroup.GetServiceConfig();
            
            _hubConnection = new HubConnection(service.Url);
            
            _hubProxy = _hubConnection.CreateHubProxy(service.Hub);
            _hubConnection.Closed += () => StartConnection().Wait();
        }
        
        public async Task<string> SendRequest(string data)
        {
            try
            {
                string result = await _hubProxy.Invoke<string>(Method, data);
                return result;
            }
            catch (InvalidOperationException ex)
            {
                return "Error";
            }
        }

        public async Task StartConnection()
        {
            await _hubConnection.Start();
        }
    }
}