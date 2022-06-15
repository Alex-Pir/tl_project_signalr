using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Moq;
using PmsAgentManager.Controllers;
using PmsAgentManager.Hubs;
using PmsAgentManager.Services;
using Xunit;

namespace ManagerTest;

public class ManagerTest
{
    [Fact]
    public async Task ManagerController_GetHotelInfo_RequestFromClient_BadRequest()
    {
        // Arrange
        var hubMock = new Mock<IHubContext<AgentHub>>();
        var registryMock = new Mock<IRegistry>();
        var connectionsMock = new Mock<IConnectionMapping>();
        var managerController = new ManagerController(hubMock.Object, registryMock.Object, connectionsMock.Object);
        
        // Act
        var result = await managerController.GetHotelInfo("test-guid", "test-param");
        
        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}