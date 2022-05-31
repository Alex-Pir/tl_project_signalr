using Microsoft.AspNetCore.SignalR;
using PmsAgentManagement.Hubs;
using PmsAgentManager.HttpApi;

namespace PmsAgentManager.Hubs
{
    public class AgentHub : Hub<IProxyClient>
    {
        private readonly IHttpApi _api;

        private readonly Services.Registry _registry;

        public AgentHub(IHttpApi api)
        {
            _api = api;
            _registry = Services.Registry.GetInstance();
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
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void ToGroup(long id, string message)
        {
            Clients.Group(id.ToString()).AddMessage(message);
        }

        public void SetResponse(string guid, string message)
        {
            _registry.SetParameter(guid, message);
        }
    }
}