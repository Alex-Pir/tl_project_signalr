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
        private readonly HubConnection _hubConnection;
        private readonly IHubProxy _hubProxy;

        private string _response;
        
        public HubProxy()
        {
            ServiceConfigSection service = RemoteServicesConfigGroup.GetServiceConfig();
            
            _hubConnection = new HubConnection(service.Url);
            
            _hubProxy = _hubConnection.CreateHubProxy(service.Hub);
            
            _hubConnection.Closed += () => StartConnection().Wait();
            //_hubConnection.Reconnecting += () => StartConnection().Wait();
        }

        public async Task RegisterToServer(string guid)
        {
            if (_hubConnection.State != ConnectionState.Connected)
            {
                await StartConnection();
            }
            
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
            try
            {
                await _hubProxy.Invoke(MethodRequest, guid, data);
            }
            catch (InvalidOperationException ex)
            {
                return "Error";
            }
            
            return _response;
        }

        private async Task StartConnection()
        {
            await _hubConnection.Start();
            Console.WriteLine("Start connection");
            //await SendRequest("test-guid", 'test-');
        }
    }
}