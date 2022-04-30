using System;
using System.Web.Mvc;
using System.Xml.Linq;
using PmsAgentProxy.Clients;
using PmsAgentProxy.Util;

namespace PmsAgentProxy.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClient _client;
        
        public HomeController(IClient client)
        {
            _client = client;
        }
        
        public XmlActionResult SendData()
        {
            var result = _client.Connection();
            
            Console.WriteLine(result);
            
            return new XmlActionResult(result);
        }
    }
}