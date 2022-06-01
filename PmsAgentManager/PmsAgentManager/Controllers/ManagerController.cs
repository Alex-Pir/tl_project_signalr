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
    private const int WaitingTime = 300;
    private const int DisconnectTime = 120000;
    
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
    public async Task<IActionResult> GetHotelInfo(string guid, string parameter)
    {
        int time = 0;
        
        await _hubContext.Clients.Group(guid).SendRequest(parameter);
        
        string? result = "";

        while (string.IsNullOrEmpty(result) && time < DisconnectTime)
        {
            result = _registry.GetParameter(guid);

            if (string.IsNullOrEmpty(result))
            {
                Thread.Sleep(WaitingTime);
                time += WaitingTime;
            }
        }

        if (string.IsNullOrEmpty(result))
        {
            BadRequest("Data could not be retrieved");
        }

        _registry.RemoveParameter(guid);
        
        return Ok(result);
    }
}