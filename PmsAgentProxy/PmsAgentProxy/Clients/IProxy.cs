using System.Threading.Tasks;

namespace PmsAgentProxy.Clients
{
    public interface IProxy
    {
        Task RegisterToServer(string guid);
        Task<string> SendRequest(string guid, string data);
    }
}