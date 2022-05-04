using System.Threading.Tasks;

namespace PmsAgentManagement.Hubs
{
    public interface IProxyClient
    {
        Task AddMessage(string message);
    }
}