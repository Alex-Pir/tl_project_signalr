using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.AspNet.SignalR.Transports;
using PmsAgentManagement.HttpApi;

namespace PmsAgentManagement.Hubs
{
    public class AgentHub : Hub<IProxyClient>
    {
        private readonly IHttpApi _api;

        private static Dictionary<string, ITrackingConnection> _userConnections = new Dictionary<string, ITrackingConnection>();

        public AgentHub()
        {
            _api = new HttpNpbApi(); //Не работает DI через сервис контейнер
           // _userConnections = new Dictionary<string, Connection>();
        }
        
        public Task Request(string guid, string request)
        {
            var connection = _userConnections[guid];

            if (!connection.IsAlive)
            {
                throw new Exception("Connection is died");
            }
            
            var response = _api.GetData();
            return Clients.User(connection.ConnectionId).AddMessage(response);
        }

        public bool Register(string guid)
        {
            var heartBeat = GlobalHost.DependencyResolver.Resolve<ITransportHeartbeat>();
            var connectionId = Context.ConnectionId;
            var connection = heartBeat.GetConnections().FirstOrDefault(c => c.ConnectionId == connectionId);

            try
            {
                if (connection != null && connection.IsAlive)
                {
                    _userConnections.Remove(guid);
                    _userConnections.Add(guid, connection);
                    return true;
                }

                throw new Exception("Connection is died");
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
    }
}