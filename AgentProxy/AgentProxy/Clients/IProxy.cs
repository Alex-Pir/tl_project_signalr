using System.Threading.Tasks;

namespace AgentProxy.Clients
{
    public interface IProxy
    {
        Task<string> SendRequest(int guid, string data);
        void StartHub();
    }
}