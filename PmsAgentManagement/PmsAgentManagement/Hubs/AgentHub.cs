using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using PmsAgentManagement.HttpApi;

namespace PmsAgentManagement.Hubs
{
    public class AgentHub : Hub<IProxyClient>
    {
        private readonly IHttpApi _api;

        public AgentHub()
        {
            _api = new HttpNpbApi(); //Не работает DI через сервис контейнер
        }
        
        public string Request(string request)
        {
            Console.WriteLine(request);
            return _api.GetData();
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