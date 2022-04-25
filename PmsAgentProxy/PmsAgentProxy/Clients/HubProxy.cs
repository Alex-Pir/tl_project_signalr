using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace PmsAgentProxy.Clients
{
    public class HubProxy : IProxy
    {
        private const string HubName = "AgentHub";
        private readonly string _connection = "http://localhost:5000/";
        private IHubProxy _hubProxy;

        public HubProxy()
        {
            HubConnection hubConnection = new HubConnection(_connection);
            _hubProxy = hubConnection.CreateHubProxy(HubName);
            hubConnection.Start().Wait();
        }
        
        public void SendRequest(string data)
        {
            _hubProxy.Invoke("Request", data);
        }

        public void RegisterResponseHandler()
        {
            _hubProxy.On<string>("AddMessage", param =>
            {
                Console.WriteLine(param);
            });
        }
    }
}