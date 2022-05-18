using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using NLog;
using NLog.Fluent;
using PmsAgentProxy.Clients;
using PmsAgentProxy.Util;

namespace PmsAgentProxy.Controllers
{
    public class HomeController : Controller
    {

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IProxy _proxy;
        
        public HomeController(IProxy proxy)
        {
            _proxy = proxy;
        }
        
        [HttpPost]
        public async Task<XmlActionResult> SendData(string parameter)
        {
            try
            {
                await _proxy.StartConnection();
                var response = await _proxy.SendRequest(parameter);
                return new XmlActionResult(response);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
            }

            return new ErrorActionResult("Service Error! Please try again later");
        }
    }
}