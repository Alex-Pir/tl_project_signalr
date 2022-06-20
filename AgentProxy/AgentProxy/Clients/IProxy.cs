using System.Threading.Tasks;

namespace AgentProxy.Clients
{
    public interface IProxy
    {
        Task<string> SendRequest(string guid, string data);
        void StartHub();
    }
}