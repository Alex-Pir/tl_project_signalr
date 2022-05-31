using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using PmsAgentProxy.Services.RemoteServices;

namespace PmsAgentProxy.Clients
{
    public class HubProxy : IProxy
    {
        private const byte DisconnectTimeoutMinutes = 6;
        
        private const string MethodRegister = "Register";
        private const string MethodRequest = "Request";
		private const string ResponseMethod = "AddMessage";
		
		private const string ServerRequestMethod = "SendRequest";
		private const string ResponseForServerMethod = "SetResponse";

        private readonly HubConnection _hubConnection;

        private string _response;

        private static bool _connectionStart = false;

        private static HubProxy _instance;
        private static object syncRoot = new();
        
        private HubProxy()
        {
            ServiceConfigSection service = RemoteServicesConfigGroup.GetServiceConfig();
            
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(service.Url)
                .Build();
            
            _hubConnection.ServerTimeout = TimeSpan.FromMinutes(DisconnectTimeoutMinutes);
            
            _hubConnection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0,5) * 1000);
                await _hubConnection.StartAsync();
            };
        }

        public static HubProxy GetInstance()
        {
            if (_instance != null) 
            {
                return _instance;
            }
            
            lock (syncRoot)
            {
                _instance ??= new HubProxy();
            }

            return _instance;
        }
        
		public async Task RegisterToServer(string guid)
        {
            var registerResult = await _hubConnection.InvokeAsync<bool>(MethodRegister, guid);

            if (!registerResult)
            {
                throw new Exception("Error connect to server");
            }
        }

        public void RegisterResponseHandler()
        {
            _hubConnection.On<string>(ResponseMethod, async i =>
            {
                await PrepareResponse(i);
            });

            _hubConnection.On<string>(ServerRequestMethod, request =>
            {
                _hubConnection.InvokeAsync(ResponseForServerMethod, "test-guid", "response-test-parameter");
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
                await _hubConnection.InvokeAsync(MethodRequest, guid, data);
            }
            catch (InvalidOperationException ex)
            {
                return "Error";
            }
            
            return _response;
        }

        public async Task StartConnection()
        {
            await _hubConnection.StartAsync();
        }
    }
}