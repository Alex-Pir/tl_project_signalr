using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PmsAgentManagement.Hubs;
using PmsAgentManager.Attributes;
using PmsAgentManager.HttpApi;
using PmsAgentManager.Hubs;
using PmsAgentManager.Services;

namespace PmsAgentManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ManagerController : ControllerBase
{
    private readonly IHubContext<AgentHub, IProxyClient> _hubContext;
    private readonly Registry _registry;
    
    public ManagerController(IHubContext<AgentHub, IProxyClient> hubContext)
    {
        _hubContext = hubContext;
        _registry = Registry.GetInstance();
    }
    
    [HttpGet]
    //[XmlHeader]
    public IActionResult Index()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<string> GetHotelInfo(string guid, string parameter)
    {
        await _hubContext.Clients.All.SendRequest(parameter);
        string result = "";

        while (string.IsNullOrEmpty(result))
        {
            result = _registry.GetParameter(guid);

            if (string.IsNullOrEmpty(result))
            {
                Thread.Sleep(300);
            }
        }
            
        return result;
    }
}