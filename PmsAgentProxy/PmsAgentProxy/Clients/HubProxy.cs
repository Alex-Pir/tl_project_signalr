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
        public HubProxy()
        {
            ServiceConfigSection service = RemoteServicesConfigGroup.GetServiceConfig();
            
            HubConnection hubConnection = new HubConnection(service.Url);
            
            _hubProxy = hubConnection.CreateHubProxy(service.Hub);
            hubConnection.Start().Wait();
        }
        
        public async Task<string> SendRequest(string data)
        {
           string result = await _hubProxy.Invoke<string>("Request", data).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null)
                    {
                        throw new Exception($"There was an error calling send: (0), {task.Exception.GetBaseException()}");
                    }

                    throw new Exception("Unable to retrieve data from remote service");
                }
               
                return task.Result;
            });

           return result;
        }
    }
}