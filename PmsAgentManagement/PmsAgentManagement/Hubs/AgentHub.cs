using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using PmsAgentManagement.HttpApi;

namespace PmsAgentManagement.Hubs
{
    public class AgentHub : Hub<IProxyClient>
    {
        private readonly IHttpApi _api = new HttpNpbApi();
        
        public void Request(string request)
        {
            Console.WriteLine(request);
            Clients.Caller.AddMessage(_api.GetData());
        }

        public void Register(long userId)
        {
            Groups.Add(Context.ConnectionId, userId.ToString(CultureInfo.InvariantCulture));
        }

        public void ToGroup(long id, string message)
        {
            Clients.Group(id.ToString()).AddMessage(message);
        }
    }
}