using System.Threading.Tasks;

namespace PmsAgentManagement.Hubs
{
    public interface IProxyClient
    {
        public Task AddMessage(string message);
        public Task SendRequest(string message);
    }
}