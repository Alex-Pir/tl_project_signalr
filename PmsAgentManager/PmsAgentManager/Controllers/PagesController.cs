using Microsoft.AspNetCore.Mvc;
using PmsAgentManager.Services;

namespace PmsAgentManager.Controllers;

[Route("/[controller]")]
public class PagesController : Controller
{
    private readonly IConnectionMapping _connections;
    
    public PagesController(IConnectionMapping connections)
    {
        _connections = connections;
    }
    
    // GET
    public IActionResult Index()
    {
        return View(_connections.GetAllConnections());
    }
}