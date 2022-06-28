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
            int hotelGuid = 10;
            IRegistry registry = new Registry();
            registry.SetParameter(hotelGuid, "test_parameter");

            //Act
            var result = registry.GetParameter(hotelGuid);

            //Assert
            Assert.Equal("test_parameter", result);
        }

        [Fact]
        public void Registry_GetParameter_GetDataOfEmptyElement_EmptyString()
        {
            //Arrange
            int hotelGuid = 10;
            IRegistry registry = new Registry();

            //Act
            var result = registry.GetParameter(hotelGuid);

            //Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void Registry_RemoveParameter_RemoveParameterSuccess_EmptyString()
        {
            //Arrange
            int hotelGuid = 10;
            IRegistry registry = new Registry();
            registry.SetParameter(hotelGuid, "test_parameter");

            //Act
            registry.RemoveParameter(hotelGuid);
            var result = registry.GetParameter(hotelGuid);

            //Assert
            Assert.Equal(string.Empty, result);
        }
    }
}
