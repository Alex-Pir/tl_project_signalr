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
        
        [HttpPost]
        public XmlActionResult SendData(string parameter)
        {
            var result = _client.GetMessage(parameter);
            return new XmlActionResult(result);
        }
    }
}