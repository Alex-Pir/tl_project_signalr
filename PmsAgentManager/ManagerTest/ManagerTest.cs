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

    [Fact]
    public async Task ManagerController_GetHotelInfo_ResultString_Success()
    {
        // Arrange
        var clientsObject = new Mock<IHubClients>();
        var hubMock = new Mock<IHubContext<AgentHub>>();
        hubMock.Setup(x => x.Clients).Returns(clientsObject.Object);

        var registryMock = new Mock<IRegistry>();
        registryMock.Setup(x => x.GetParameter("test-guid")).Returns("test-response");

        var connectionsMock = new Mock<IConnectionMapping>();
        connectionsMock.Setup(x => x.GetConnectionKeyByValue("test-guid")).Returns(new string("test-connection"));

        var managerController = new ManagerController(hubMock.Object, registryMock.Object, connectionsMock.Object);

        // Act
        var result = await managerController.GetHotelInfo("test-guid", "test-param");

        // Assert
        Assert.Equal("test-response", result.ToString());
    }
}