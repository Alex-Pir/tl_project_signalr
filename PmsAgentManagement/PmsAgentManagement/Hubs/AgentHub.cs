using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.AspNet.SignalR.Transports;
using PmsAgentManagement.HttpApi;
using PmsAgentManagement.Services;

namespace PmsAgentManagement.Hubs
{
    public class AgentHub : Hub<IProxyClient>
    {
        private readonly IHttpApi _api;

        //private static readonly Dictionary<string, ITrackingConnection> _userConnections = new Dictionary<string, ITrackingConnection>();

        private readonly Registry _registry;
        
        private string _responseMessage = null;
        
        public AgentHub()
        {
            _api = new HttpNpbApi();
            _registry = Registry.GetInstance();
        }
        
        public void Request(string guid, string request)
        {
            ITrackingConnection connection = _registry.GetConnection(guid);
            
            string response = _api.GetData();
            Clients.Client(connection.ConnectionId).AddMessage(response);
        }

        public bool Register(string guid)
        {
            var heartBeat = GlobalHost.DependencyResolver.Resolve<ITransportHeartbeat>();
            var connectionId = Context.ConnectionId;
            var connection = heartBeat.GetConnections().FirstOrDefault(c => c.ConnectionId == connectionId);
            try
            {
                _registry.SetConnection(guid, connection);
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

        public string GetHotelInfo(string guid, string parameter)
        {
            ITrackingConnection connection = _registry.GetConnection(guid);

            Clients.Client(connection.ConnectionId).SendRequest(parameter);
            
            while (_responseMessage == null)
            {
                Thread.Sleep(300);
            }
            
            return _responseMessage;
        }

        public void SetResponse(string message)
        {
            _responseMessage = message;
        }
    }
}