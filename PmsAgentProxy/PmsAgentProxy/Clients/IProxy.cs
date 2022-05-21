using System.Threading.Tasks;

namespace PmsAgentProxy.Clients
{
    public interface IProxy
    {
        Task StartConnection();
        Task<string> SendRequest(string data);
    }
}