using System.Threading.Tasks;

namespace PmsAgentProxy.Clients
{
    public interface IProxy
    {
        Task SendRequest(string data);
        string ResultMessage { set; get; }
        bool Status { set; get; }
    }
}