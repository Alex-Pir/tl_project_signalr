using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
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

        private static readonly Dictionary<string, ITrackingConnection> _userConnections = new Dictionary<string, ITrackingConnection>();

        public AgentHub()
        {
            _api = new HttpNpbApi(); //Не работает DI через сервис контейнер
        }
        
        public void Request(string guid, string request)
        {
            _userConnections.TryGetValue(guid, out var connection);     

            if (connection == null || !connection.IsAlive)
            {
                throw new Exception("Connection is died");
            }
            
            var response = _api.GetData();
            
            //Thread.Sleep(10000);
            
            Clients.Client(connection.ConnectionId).AddMessage(response);
        }

        public bool Register(string guid)
        {
            var heartBeat = GlobalHost.DependencyResolver.Resolve<ITransportHeartbeat>();
            var connectionId = Context.ConnectionId;
            var connection = heartBeat.GetConnections().FirstOrDefault(c => c.ConnectionId == connectionId);

            try
            {
                if (connection == null || !connection.IsAlive)
                {
                    throw new Exception("Connection is died");
                }
                
                _userConnections.Remove(guid);
                _userConnections.Add(guid, connection);
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
    }
}