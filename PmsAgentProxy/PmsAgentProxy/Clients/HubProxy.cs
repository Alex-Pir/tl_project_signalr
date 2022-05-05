using System;
using System.Threading.Tasks;
using System.Web.Configuration;
using Microsoft.AspNet.SignalR.Client;
using PmsAgentProxy.Services.RemoteServices;

namespace PmsAgentProxy.Clients
{
    public class HubProxy : IProxy
    {
        private readonly IHubProxy _hubProxy;
        public string ResultMessage { get; set; }
        public bool Status { get; set; }
        public HubProxy()
        {
            ServiceConfigSection service = RemoteServicesConfigGroup.GetServiceConfig();
            
            ResultMessage = "";
            
            HubConnection hubConnection = new HubConnection(service.Url);
            
            _hubProxy = hubConnection.CreateHubProxy(service.Hub);
            hubConnection.Start().Wait();
        }
        
        public async Task SendRequest(string data)
        {
            await _hubProxy.Invoke<string>("Request", data).ContinueWith(task =>
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
    }
}