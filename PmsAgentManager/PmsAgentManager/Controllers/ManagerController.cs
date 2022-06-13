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
    private const int DisconnectTime = 120000;
    
    private readonly IHubContext<AgentHub> _hubContext;
    private readonly IRegistry _registry;

    public ManagerController(IHubContext<AgentHub> hubContext, IRegistry registry)
    {
        _hubContext = hubContext;
        _registry = registry;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> GetHotelInfo(string guid, string parameter)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(DisconnectTime);
        CancellationToken token = cancellationTokenSource.Token;
        
        await _hubContext.Clients.Group(guid).SendCoreAsync("SendRequest", new object?[] {parameter}, token);
        
        string result = string.Empty;

        try
        {

            while (!cancellationTokenSource.IsCancellationRequested)
            {
                result = _registry.GetParameter(guid);
                
                if (!string.IsNullOrEmpty(result))
                {
                    Response.Headers.Add("Content-Type", "text/xml");
            
                    return Ok(result);
                }
            }
            
            throw new Exception("Data could not be retrieved");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
