using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using PmsAgentProxy.Services.RemoteServices;

namespace PmsAgentProxy.Clients
{
    public class HubProxy : IProxy
    {
        private const string MethodRegister = "Register";
        private const string MethodRequest = "Request";
        private readonly IHubProxy _hubProxy;

        private string _response;
        
        public HubProxy()
        {
            ServiceConfigSection service = RemoteServicesConfigGroup.GetServiceConfig();
            
            HubConnection hubConnection = new HubConnection(service.Url);
            
            _hubProxy = hubConnection.CreateHubProxy(service.Hub);
            hubConnection.Start().Wait();
        }

        public async Task RegisterToServer(string guid)
        {
            var registerResult = await _hubProxy.Invoke<bool>(MethodRegister, guid);

            if (!registerResult)
            {
                throw new Exception("Error connect to server");
            }
        }

        public void RegisterResponseHandler()
        {
            _hubProxy.On<string>("AddMessage", async i =>
            {
                await PrepareResponse(i);
            });
        }

        private async Task PrepareResponse(string message)
        {
            _response = message;
        }
        public async Task<string> SendRequest(string guid, string data)
        {
            await _hubProxy.Invoke(MethodRequest, guid, data);

            return _response;
        }
    }
}