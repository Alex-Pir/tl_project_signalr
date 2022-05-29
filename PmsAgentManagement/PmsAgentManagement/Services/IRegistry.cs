namespace PmsAgentManagement.Services
{
    public interface IRegistry
    {
        void SetParameter(string guid, string parameter);
        string GetParameter(string guid);
    }
}