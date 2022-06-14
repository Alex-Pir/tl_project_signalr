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
    private readonly IConnectionMapping _connectionMapping;

    public ManagerController(
        IHubContext<AgentHub> hubContext, 
        IRegistry registry,
        IConnectionMapping connectionMapping
     )
    {
        _hubContext = hubContext;
        _registry = registry;
        _connectionMapping = connectionMapping;
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

        string connection = _connectionMapping.GetConnectionKeyByValue(guid);
        
        try
        {

            if (string.IsNullOrEmpty(connection))
            {
                throw new Exception("The client is not connected");
            }
        
            await _hubContext.Clients.Group(guid).SendCoreAsync("SendRequest", new object?[] {parameter}, token);
            
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                string result = _registry.GetParameter(guid);

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
