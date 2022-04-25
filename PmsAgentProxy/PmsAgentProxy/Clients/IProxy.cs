using System.Threading.Tasks;

namespace PmsAgentProxy.Clients
{
    public interface IProxy
    {
        void SendRequest(string data);
        void RegisterResponseHandler();
    }
}