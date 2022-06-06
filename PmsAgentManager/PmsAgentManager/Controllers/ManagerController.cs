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
    private readonly IRegistry _registry;
    private readonly IConnectionMapping<string> _connections;

    public ManagerController(IHubContext<AgentHub, IProxyClient> hubContext, IRegistry registry, IConnectionMapping<string> connections)
    {
        _hubContext = hubContext;
        _registry = registry;
        _connections = connections;
    }
    
    [HttpGet]
    //[XmlHeader]
    public IActionResult Index()
    {
        var connections = _connections.GetAllConnections();
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> GetHotelInfo(string guid, string parameter)
    {
        int time = 0;
        
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        CancellationToken token = cancellationTokenSource.Token;
        
        await _hubContext.Clients.Group(guid).SendRequest(parameter);
        
        string? result = "";

        await Task.Delay(WaitingTime);
        result = _registry.GetParameter(guid);

        while (string.IsNullOrEmpty(result))
        {
            await Task.Delay(WaitingTime);
            result = _registry.GetParameter(guid);

            if (string.IsNullOrEmpty(result) && time >= DisconnectTime)
            {
                //cancellationTokenSource.Cancel();
                return BadRequest("Data could not be retrieved");
            }
            
            time += WaitingTime;
        }

        _registry.RemoveParameter(guid);
        
        return Ok(result);
    }
}