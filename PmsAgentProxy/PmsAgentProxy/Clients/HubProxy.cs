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
        public string ResultMessage { get; set; }
        public bool Status { get; set; }
        public HubProxy()
        {
            ResultMessage = "";
            HubConnection hubConnection = new HubConnection(_connection);
            _hubProxy = hubConnection.CreateHubProxy(HubName);
            hubConnection.Start().Wait();
        }
        
        public void SendRequest(string data)
        {
            _hubProxy.Invoke<string>("Request", data).ContinueWith(task =>
            {
                Status = true;

                if (task.IsFaulted)
                {
                    if (task.Exception != null)
                    {
                        ResultMessage = $"There was an error calling send: (0), {task.Exception.GetBaseException()}";
                    }
                }
                else
                {
                    ResultMessage = task.Result;
                }
            });
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