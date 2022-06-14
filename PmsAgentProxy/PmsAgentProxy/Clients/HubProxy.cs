using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using PmsAgentProxy.Exceptions;
using PmsAgentProxy.Services.Client;
using PmsAgentProxy.Services.GuidServices;
using PmsAgentProxy.Services.RemoteServices;

namespace PmsAgentProxy.Clients
{
    public class HubProxy : IProxy
    {
        private const byte DisconnectTimeoutMinutes = 6;
        private const int ReconnectTimeout = 300;
        private const int ResponseWaitingEndTime = 1000;
        
        private const string MethodRegister = "Register";
        private const string MethodRequest = "Request";
		private const string ResponseMethod = "AddMessage";
		
		private const string ServerRequestMethod = "SendRequest";
		private const string ResponseForServerMethod = "SetResponse";

        private readonly IPmsAgentClient _client;
        private readonly HubConnection _hubConnection;

        private string _response;

        private readonly GuidConfigSection _guid;

        private static HubProxy _instance;
        private static object syncRoot = new();

        private HubProxy(IPmsAgentClient client)
        {
            _client = client;
            _guid = (GuidConfigSection)new GuidConfigSection().GetData();
            
            ServiceConfigSection service = (ServiceConfigSection)new ServiceConfigSection().GetData();
            
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(service.Url)
                .Build();
            
            _hubConnection.ServerTimeout = TimeSpan.FromMinutes(DisconnectTimeoutMinutes);
            
            _hubConnection.Closed += async (error) =>
            {
                await Task.Delay(ReconnectTimeout);
                await StartHub();
            };
        }

        public static HubProxy GetInstance(IPmsAgentClient client)
        {
            if (_instance != null) 
            {
                return _instance;
            }
            
            lock (syncRoot)
            {
                _instance ??= new HubProxy(client);
            }

            return _instance;
        }

        public async Task StartHub()
        {
            RegisterResponseHandler();
            await _hubConnection.StartAsync();
            await RegisterToServer(_guid.Value);
        }
        
		private async Task RegisterToServer(string guid)
        {
            var registerResult = await _hubConnection.InvokeAsync<bool>(MethodRegister, guid);

            if (!registerResult)
            {
                throw new Exception("Error connect to server");
            }
        }

        public async Task<string> SendRequest(string guid, string data)
        {
            try
            {
                _response = string.Empty;
                await _hubConnection.InvokeAsync(MethodRequest, guid, data);

                int waitingTime = 0;
                
                while (string.IsNullOrEmpty(_response) && waitingTime < ResponseWaitingEndTime)
                {
                    await Task.Delay(ReconnectTimeout);
                    waitingTime += ReconnectTimeout;
                }
                
            }
            catch (InvalidOperationException)
            {
                throw new HubRequestException("Data could not be retrieved");
            }
            
            return _response;
        }

        private void RegisterResponseHandler()
        {
            _hubConnection.On<string>(ResponseMethod,  message => _response = message);

            _hubConnection.On<string>(ServerRequestMethod, request =>
            {
                ClientConfigSection config = (ClientConfigSection)new ClientConfigSection().GetData();
                
                string result = _client.Call("", request, config.Page);
                _hubConnection.InvokeAsync(ResponseForServerMethod, _guid.Value, result);
            });
        }
    }
}