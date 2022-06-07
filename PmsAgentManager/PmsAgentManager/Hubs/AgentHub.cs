using Microsoft.AspNetCore.SignalR;
using PmsAgentManagement.Hubs;
using PmsAgentManager.HttpApi;
using PmsAgentManager.Services;

namespace PmsAgentManager.Hubs
{
    public class AgentHub : Hub<IProxyClient>
    {
        private readonly IHttpApi _api;

        private readonly IRegistry _registry;

        private readonly IConnectionMapping _connections;
        
        public AgentHub(IHttpApi api, IRegistry registry, IConnectionMapping connectionMapping)
        {
            _api = api;
            _registry = registry;
            _connections = connectionMapping;
        }
        
        public void Request(string guid, string request)
        {
            string response = _api.GetData();
            Clients.Group(guid).AddMessage(response);
        }

        public bool Register(string guid)
        {
            try
            {
                Groups.AddToGroupAsync(Context.ConnectionId, guid);
                _connections.Add(Context.ConnectionId, guid);
                return true;
            }
            catch (Exception)
            { }

            return false;
        }

        public void ToGroup(long id, string message)
        {
            Clients.Group(id.ToString()).AddMessage(message);
        }

        public void SetResponse(string guid, string message)
        {
            _registry.SetParameter(guid, message);
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _connections.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}