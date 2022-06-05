using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using NLog;
using PmsAgentProxy.Clients;
using PmsAgentProxy.Services.GuidServices;
using PmsAgentProxy.Util;

namespace PmsAgentProxy.Controllers
{
    public class HomeController : Controller
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IProxy _proxy;
        private readonly string _guid;
        
        public HomeController(IProxy proxy)
        {
            _proxy = proxy;
            _guid = GuidConfigSection.GetGuid();
        }
        
        [HttpPost]
        public async Task<XmlActionResult> SendData(string parameter)
        {
            try
            {
                var response = await _proxy.SendRequest(_guid, parameter);
                return new XmlActionResult(response);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Hub proxy exception");
            }

            return new ErrorActionResult("Service Error! Please try again later");
        }
    }
}