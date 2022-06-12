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
    
    private readonly IHubContext<AgentHub> _hubContext;
    private readonly IRegistry _registry;

    public ManagerController(IHubContext<AgentHub> hubContext, IRegistry registry)
    {
        _hubContext = hubContext;
        _registry = registry;
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
        
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        CancellationToken token = cancellationTokenSource.Token;
        
        await _hubContext.Clients.Group(guid).SendCoreAsync("SendRequest", new object?[] {parameter}, token);
        
        string? result = "";

        await Task.Delay(WaitingTime);
        result = _registry.GetParameter(guid);

        SpinWait.SpinUntil(() =>
        {
            result = _registry.GetParameter(guid);
            time += WaitingTime;

            if (time >= DisconnectTime)
            {
                return true;
            }
            
            return !string.IsNullOrEmpty(result);
        }, WaitingTime);

        try
        {
            if (string.IsNullOrEmpty(result))
            {
                throw new Exception("Data could not be retrieved");
            }
            
            //TODO разобраться как сделать это через middleware
            Response.Headers.Add("Content-Type", "text/xml");
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        finally
        {
            _registry.RemoveParameter(guid);
        }
    }
}