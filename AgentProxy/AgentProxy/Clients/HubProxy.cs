using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using AgentProxy.Exceptions;
using AgentProxy.Services.GuidServices;
using AgentProxy.Settings;

namespace AgentProxy.Clients
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

        private readonly int _guid;

        private static HubProxy _instance;
        private static object syncRoot = new();

        private HubProxy(IPmsAgentClient client)
        {
            _client = client;
            _guid =  Convert.ToInt32(new GuidConfigSection().Value);

            if (_guid <= 0)
            {
                throw new ArgumentException("Guid can not be negative");
            }

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(new ManagerSettings().Url)
                .Build();
            
            _hubConnection.ServerTimeout = TimeSpan.FromMinutes(DisconnectTimeoutMinutes);
            
            _hubConnection.Reconnected += async (error) =>
            {
                await Task.Delay(ReconnectTimeout);
                StartHub();
            };

            _hubConnection.Closed += async (error) =>
            {
                await Task.Delay(ReconnectTimeout);
                StartHub();
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

        public void StartHub()
        {
            RegisterResponseHandler();
            _hubConnection.StartAsync().Wait();
            RegisterToServer(_guid).Wait();
        }
        
		private async Task RegisterToServer(int guid)
        {
            var registerResult = await _hubConnection.InvokeAsync<bool>(MethodRegister, guid);

            if (!registerResult)
            {
                throw new Exception("Error connect to server");
            }
        }

        public async Task<string> SendRequest(int guid, string data)
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
                string result = _client.Call("", request, new AgentSettings().Url);
                _hubConnection.InvokeAsync(ResponseForServerMethod, _guid, result);
            });
        }
    }
}