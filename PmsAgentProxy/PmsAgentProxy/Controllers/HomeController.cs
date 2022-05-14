using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;
using PmsAgentProxy.Clients;
using PmsAgentProxy.Util;

namespace PmsAgentProxy.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProxy _proxy;
        
        public HomeController(IProxy proxy)
        {
            _proxy = proxy;
        }
        
        [HttpPost]
        public async Task<XmlActionResult> SendData(string guid, string parameter)
        {
            try
            {
                await _proxy.RegisterToServer(guid);
                var response = await _proxy.SendRequest(guid, parameter);
                return new XmlActionResult(response);
            }
            catch (Exception ex)
            {
                throw;
            }

            return new XmlActionResult("");
        }
    }
}