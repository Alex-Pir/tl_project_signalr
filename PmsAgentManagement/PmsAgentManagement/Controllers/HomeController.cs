using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Transports;
using PmsAgentManagement.HttpApi;
using PmsAgentManagement.Hubs;
using PmsAgentManagement.Services;

namespace PmsAgentManagement.Controllers
{
    public class HomeController : Controller
    {
        private IHubContext<IProxyClient> _hubContext;

        private AgentHub _hub;
/*
        public HomeController()
        {
        }*/
        
        public HomeController()
        {
            
        }
        
        [HttpPost]
        public string GetHotelData(string guid, string parameter)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<AgentHub>();
            
            ITrackingConnection connection = Registry.GetInstance().GetConnection(guid);
            hubContext.Clients.Client(connection.ConnectionId).SendRequest(parameter);
            
            
            
            /*var hubManager = new DefaultHubManager(GlobalHost.DependencyResolver);
            var hub = hubManager.ResolveHub("AgentHub").Clients;
            
            ITrackingConnection connection = Registry.GetInstance().GetConnection(guid);
            
            hub.Client(connection.ConnectionId).SendRequest(parameter);*/
            
            
            
            //_hub.GetHotelInfo(guid, parameter);
            //return _hub.GetHotelInfo(guid);
            
            //return _hub.GetHotelInfo(guid, parameter);
            
            return "";
        }
    }
}