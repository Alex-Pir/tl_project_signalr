using PmsAgentManager.Services;
using Xunit;

namespace ManagerTest
{
    public class ConnectionMappingTest
    {
        [Fact]
        public void ConnectionMapping_GetConnection_GetConnectionSuccess_String()
        {
            // Arrange
            IConnectionMapping connectionMapping = new ConnectionMapping();

            // Act
            connectionMapping.Add("test-connection", "test-guid");
            var result = connectionMapping.GetConnection("test-connection");

            // Assert
            Assert.Equal("test-guid", result);
        }

        [Fact]
        public void ConnectionMapping_GetConnection_GetConnectionFalse_EmptyString()
        {
            // Arrange
            IConnectionMapping connectionMapping = new ConnectionMapping();

            // Act
            var result = connectionMapping.GetConnection("test-connection");

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void ConnectionMapping_GetAllConnections_ReturnsAllConnections_Dictionary()
        {
            // Arrange
            IConnectionMapping connectionMapping = new ConnectionMapping();
            connectionMapping.Add("test-connection1", "test-guid1");
            connectionMapping.Add("test-connection2", "test-guid2");

            // Act
            var result = connectionMapping.GetAllConnections();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void ConnectionMapping_Remove_RemoveConnectionSuccess_EmptyString()
        {
            // Arrange
            IConnectionMapping connectionMapping = new ConnectionMapping();
            connectionMapping.Add("test-connection1", "test-guid1");

            // Act
            connectionMapping.Remove("test-connection1");
            var result = connectionMapping.GetConnection("test-connection1");

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void ConnectionMapping_GetConnectionKeyByValue_ReturnConnectionId_String()
        {
            // Arrange
            IConnectionMapping connectionMapping = new ConnectionMapping();
            connectionMapping.Add("test-connection1", "test-guid1");

            // Act
            var result = connectionMapping.GetConnectionKeyByValue("test-guid1");

            // Assert
            Assert.Equal("test-connection1", result);
        }
    }
}
