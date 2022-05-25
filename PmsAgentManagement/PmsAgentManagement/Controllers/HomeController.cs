using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages;
using Microsoft.AspNet.SignalR;
using PmsAgentManagement.Hubs.Factories;
using PmsAgentManagement.Services;

namespace PmsAgentManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHubContext _hubContext;
        private readonly Registry _registry;

        public HomeController(IHubContextFactory factory)
        {
            _hubContext = factory.GetContext();
            _registry = Registry.GetInstance();
        }
        
        [HttpPost]
        public async Task<string> GetHotelData(string guid, string parameter)
        {
            await _hubContext.Clients.Group(guid).SendRequest(parameter);
            string result = "";

            while (result.IsEmpty())
            {
                result = _registry.GetParameter(guid);

                if (result.IsEmpty())
                {
                    Thread.Sleep(300);
                }
            }
            
            return result;
        }
    }
}