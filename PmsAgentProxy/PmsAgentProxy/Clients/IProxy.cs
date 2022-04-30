using System.Threading.Tasks;

namespace PmsAgentProxy.Clients
{
    public interface IProxy
    {
        void SendRequest(string data);
        void RegisterResponseHandler();
        string ResultMessage { set; get; }
        bool Status { set; get; }
    }
}