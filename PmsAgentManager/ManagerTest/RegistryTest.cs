using PmsAgentManager.Services;
using Xunit;

namespace ManagerTest
{
    public class RegistryTest
    {
        [Fact]
        public void Registry_SetParameter_SetDataByClientGuid_Success()
        {
            //Arrange
            IRegistry registry = new Registry();
            registry.SetParameter("test_guid", "test_parameter");

            //Act
            var result = registry.GetParameter("test_guid");

            //Assert
            Assert.Equal("test_parameter", result);
        }

        [Fact]
        public void Registry_GetParameter_GetDataOfEmptyElement_EmptyString()
        {
            //Arrange
            IRegistry registry = new Registry();

            //Act
            var result = registry.GetParameter("test_guid");

            //Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void Registry_RemoveParameter_RemoveParameterSuccess_EmptyString()
        {
            //Arrange
            IRegistry registry = new Registry();
            registry.SetParameter("test_guid", "test_parameter");

            //Act
            registry.RemoveParameter("test_guid");
            var result = registry.GetParameter("test-guid");

            //Assert
            Assert.Equal(string.Empty, result);
        }
    }
}
